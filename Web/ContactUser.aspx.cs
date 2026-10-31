using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ContactUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Username"] == null)
        {
            Response.Redirect("SignInVisitor.aspx");
            return;
        }

        if (!IsPostBack)
        {
            if (Session["email"] != null)
            {
                txtEmail.Text = Session["email"].ToString();
            }
        }
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtName.Text) ||
            string.IsNullOrWhiteSpace(txtEmail.Text) ||
            string.IsNullOrWhiteSpace(txtSubject.Text) ||
            string.IsNullOrWhiteSpace(txtMessage.Text))
        {
            ShowStatus("Please fill in all fields.", false);
            return;
        }

        try
        {
            ServiceReference1.Service1Client ws = new ServiceReference1.Service1Client("BasicHttpBinding_IService1");

            string currentUser = (Session["username"] != null) ? Session["username"].ToString() : "Guest";

            DateTime cleanDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                                             DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            ws.AddContact(
                currentUser,
                txtName.Text,
                txtEmail.Text,
                txtSubject.Text,
                txtMessage.Text,
                cleanDate
            );

            ShowStatus("Message forged successfully! We will contact you soon.", true);
            ClearForm();
        }
        catch (Exception ex)
        {
            string errUser = (Session["username"] != null) ? Session["username"].ToString() : "Guest";
            string fullMessage = string.Format("Error: {0} (Attempted User: '{1}')", ex.Message, errUser);

            ShowStatus(fullMessage, false);
        }
    }

    private void ShowStatus(string message, bool isSuccess)
    {
        lblStatus.Visible = true;
        lblStatus.Text = message;
        lblStatus.CssClass = isSuccess ? "info-label status-success" : "info-label status-error";
    }

    private void ClearForm()
    {
        txtName.Text = "";
        txtSubject.Text = "";
        txtMessage.Text = "";

        if (Session["email"] != null)
            txtEmail.Text = Session["email"].ToString();
        else
            txtEmail.Text = "";
    }
}