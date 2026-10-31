using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServiceReference1;
using BusinessModelCarShop;

public partial class Login : System.Web.UI.Page
{
    ServiceReference1.Service1Client ws = new ServiceReference1.Service1Client("BasicHttpBinding_IService1");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.lblInfo.Visible = false;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (ws.IsExistUserAndPass(this.txtusername.Text, this.txtpass.Text))
        {
            string username = this.txtusername.Text;
            Session["username"] = username;

            User user = ws.showUser(username);

            if (user != null)
            {
                Session["email"] = user.MyEmail;
            }

            Response.Redirect("HomeUser.aspx");
        }
        else
        {
            this.lblInfo.Visible = true;
            this.lblInfo.Text = "Invalid credentials.";
        }
    }
}
