using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Contact : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Email"] != null)
            {
                txtEmail.Text = Session["Email"].ToString();
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

            string currentUser = (Session["user"] != null) ? Session["user"].ToString() : "Guest";
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
            ShowStatus("Error: " + ex.Message, false);
        }
    }

    private void ShowStatus(string message, bool isSuccess)
    {
        lblStatus.Visible = true;
        lblStatus.Text = message;
        lblStatus.CssClass = isSuccess ? "info-label success-style" : "info-label";

        if (isSuccess)
        {
            lblStatus.Style["background"] = "rgba(52, 211, 153, 0.1)";
            lblStatus.Style["color"] = "#34d399";
        }
    }

    private void ClearForm()
    {
        txtName.Text = "";
        txtSubject.Text = "";
        txtMessage.Text = "";
        if (Session["Email"] == null) txtEmail.Text = "";
    }
}