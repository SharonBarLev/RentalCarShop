using System;
using System.Collections.Generic;
using System.ServiceModel.Configuration;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtusername.Text) || string.IsNullOrWhiteSpace(txtPass.Text) ||
            string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtPhoneNumber.Text))
        {
            lblInfo.Text = "All fields are required.";
            return;
        }

        if (!IsPasswordValid(txtPass.Text))
        {
            lblInfo.Text = "Password does not meet the required criteria.";
            return;
        }

        ServiceReference1.Service1Client ws = new ServiceReference1.Service1Client("BasicHttpBinding_IService1");

        if (ws.isExistUser(txtusername.Text))
        {
            lblInfo.Text = "User already exists";
        }
        else
        {
            DateTime cleanDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                                              DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            ws.AddUser(
                txtusername.Text,
                txtPass.Text,
                txtPhoneNumber.Text,
                txtEmail.Text,
                false,
                cleanDate
            );


            Session["Username"] = txtusername.Text;
            Session["Email"] = txtEmail.Text;
            Session["PhoneNumber"] = txtPhoneNumber.Text;

            lblInfo.Text = "You have registered successfully";

            Session["user"] = txtusername.Text;

            Response.Redirect("HomeUser.aspx");
        }
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
}