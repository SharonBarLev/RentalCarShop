using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

public partial class AccountUser : System.Web.UI.Page
{
    private readonly ServiceReference1.Service1Client _ws = new ServiceReference1.Service1Client("BasicHttpBinding_IService1");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Username"] == null)
        {
            Response.Redirect("SignInVisitor.aspx");
            return;
        }

        if (!IsPostBack)
        {
            string currentUsername = Session["Username"].ToString();
            lblWelcomeUser.Text = currentUsername;

            LoadUserData();
            BindUserOrders();
        }
    }

    private void LoadUserData()
    {
        try
        {
            string username = Session["Username"].ToString();
            txtUsername.Text = username;

            var user = _ws.showAllUsers().FirstOrDefault(u => u.MyUsername == username);
            if (user != null)
            {
                txtEmail.Text = user.MyEmail;
                txtPhone.Text = user.MyPhoneNumber;
            }
        }
        catch (Exception ex)
        {
            ShowMessage("Error loading profile data: " + ex.Message, false);
        }
    }

    private void BindUserOrders()
    {
        try
        {
            string username = Session["Username"].ToString();

            var userOrders = _ws.showAllOrders()
                                .Where(o => o.MyUsername == username)
                                .OrderByDescending(o => o.MyOrderID)
                                .ToList();

            if (userOrders != null && userOrders.Count > 0)
            {
                rptOrders.DataSource = userOrders;
                rptOrders.DataBind();
                pnlNoOrders.Visible = false;
                rptOrders.Visible = true;
            }
            else
            {
                pnlNoOrders.Visible = true;
                rptOrders.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ShowMessage("Error loading order history: " + ex.Message, false);
        }
    }

    protected void btnUpdateDetails_Click(object sender, EventArgs e)
    {
        ClearAlert();
        string username = Session["Username"].ToString();

        if (string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtPhone.Text))
        {
            ShowMessage("Email and Phone fields are required.", false);
            return;
        }

        if (string.IsNullOrWhiteSpace(txtConfirmPassForDetails.Text))
        {
            ShowMessage("Please enter your current password to confirm these adjustments.", false);
            return;
        }

        try
        {
            var allUsers = _ws.showAllUsers();
            var currentUser = allUsers.FirstOrDefault(u => u.MyUsername == username);

            if (currentUser == null || currentUser.MyPassword != txtConfirmPassForDetails.Text)
            {
                ShowMessage("Incorrect current password. Changes were not saved.", false);
                return;
            }

            string newEmail = txtEmail.Text.Trim();
            string newPhone = txtPhone.Text.Trim();

            bool emailExists = allUsers.Any(u => u.MyUsername != username && u.MyEmail.Equals(newEmail, StringComparison.OrdinalIgnoreCase));
            if (emailExists)
            {
                ShowMessage("The specified email address is already tied to another account.", false);
                return;
            }

            bool phoneExists = allUsers.Any(u => u.MyUsername != username && u.MyPhoneNumber == newPhone);
            if (phoneExists)
            {
                ShowMessage("The specified phone number is already registered under another account.", false);
                return;
            }

            _ws.UpdateUser(
                currentUser.MyPassword,
                newPhone,
                newEmail,
                currentUser.MyAdmin,
                currentUser.MyJoinDate,
                username
            );

            Session["Email"] = newEmail;
            Session["PhoneNumber"] = newPhone;

            ShowMessage("Profile configuration saved successfully!", true);
            txtConfirmPassForDetails.Text = "";
        }
        catch (Exception ex)
        {
            ShowMessage("Error saving data changes to server: " + ex.Message, false);
        }
    }

    protected void btnChangePassword_Click(object sender, EventArgs e)
    {
        ClearAlert();
        string username = Session["Username"].ToString();

        if (string.IsNullOrWhiteSpace(txtCurrentPassword.Text) || string.IsNullOrWhiteSpace(txtNewPassword.Text) || string.IsNullOrWhiteSpace(txtConfirmNewPassword.Text))
        {
            ShowMessage("All password input fields must be provided.", false);
            return;
        }

        if (txtNewPassword.Text != txtConfirmNewPassword.Text)
        {
            ShowMessage("Password confirmation mismatch. Fields do not match.", false);
            return;
        }

        if (!IsPasswordValid(txtNewPassword.Text))
        {
            ShowMessage("The new password does not meet the complexity standards.", false);
            return;
        }

        try
        {
            var user = _ws.showAllUsers().FirstOrDefault(u => u.MyUsername == username);
            if (user == null || user.MyPassword != txtCurrentPassword.Text)
            {
                ShowMessage("The current password provided is incorrect.", false);
                return;
            }

            _ws.UpdateUser(
                txtNewPassword.Text,
                user.MyPhoneNumber,
                user.MyEmail,
                user.MyAdmin,
                user.MyJoinDate,
                username
            );

            ShowMessage("Your account password has been safely modified!", true);

            txtCurrentPassword.Text = "";
            txtNewPassword.Text = "";
            txtConfirmNewPassword.Text = "";
        }
        catch (Exception ex)
        {
            ShowMessage("An issue occurred during password modification: " + ex.Message, false);
        }
    }

    protected void rptOrders_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "ViewDetails")
        {
            int orderId = Convert.ToInt32(e.CommandArgument);
            LoadOrderDetails(orderId);
        }
    }

    private void LoadOrderDetails(int orderId)
    {
        try
        {
            var order = _ws.showAllOrders().FirstOrDefault(o => o.MyOrderID == orderId);

            if (order != null)
            {
                lblDetailOrderID.Text = "#" + orderId.ToString();

                string fullAddress = order.MyAddress;
                if (!string.IsNullOrEmpty(fullAddress) && fullAddress.Contains(","))
                {
                    int lastCommaIndex = fullAddress.LastIndexOf(',');
                    lblDetailAddress.Text = fullAddress.Substring(0, lastCommaIndex).Trim();
                }
                else
                {
                    lblDetailAddress.Text = fullAddress;
                }

                lblDetailNotes.Text = string.IsNullOrWhiteSpace(order.MyAdditionalInstructions) ? "No additional notes provided." : order.MyAdditionalInstructions;
                lblDetailTotalSum.Text = order.MyTotalPrice.ToString();

                var orderDetailsItems = _ws.showAllOrderDetails()
                                           .Where(od => od.MyOrderID == orderId)
                                           .ToList();

                var allProducts = _ws.showAllProduct();

                var detailedItemsWithNames = orderDetailsItems.Join(
                    allProducts,
                    od => od.MyProductID,
                    p => p.MyProductID,
                    (od, p) => new {
                        MyProductName = p.MyProductName,
                        MyQuantity = od.MyQuantity,
                        MyUnitPrice = od.MyUnitPrice
                    }).ToList();

                rptOrderItems.DataSource = detailedItemsWithNames;
                rptOrderItems.DataBind();

                mvOrders.SetActiveView(viewOrderDetails);
            }
        }
        catch (Exception ex)
        {
            ShowMessage("Error fetching detailed information for this order: " + ex.Message, false);
        }
    }

    protected void btnBackToOrders_Click(object sender, EventArgs e)
    {
        mvOrders.SetActiveView(viewOrderList);
        BindUserOrders();
    }

    private bool IsPasswordValid(string password)
    {
        if (password.Length < 8)
            return false;

        int criteriaCount = 0;
        if (Regex.IsMatch(password, @"[a-z]"))
            criteriaCount++;
        if (Regex.IsMatch(password, @"[A-Z]"))
            criteriaCount++;
        if (Regex.IsMatch(password, @"[0-9]"))
            criteriaCount++;
        if (Regex.IsMatch(password, @"[!@#$%^&*]"))
            criteriaCount++;

        if (criteriaCount < 3)
            return false;

        if (Regex.IsMatch(password, @"(.)\1{2,}"))
            return false;

        return true;
    }

    private void ShowMessage(string message, bool isSuccess)
    {
        pnlAlert.Visible = true;
        lblAlertMessage.Text = message;
        if (isSuccess)
        {
            pnlAlert.CssClass = "alert alert-success alert-dismissible fade show text-start bg-success text-white border-0";
        }
        else
        {
            pnlAlert.CssClass = "alert alert-danger alert-dismissible fade show text-start bg-danger text-white border-0";
        }
    }

    private void ClearAlert()
    {
        pnlAlert.Visible = false;
        lblAlertMessage.Text = "";
    }
}