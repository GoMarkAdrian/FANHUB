using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FanHub.User
{
    public partial class Default : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.Url.AbsoluteUri.ToString().Contains("Index.aspx"))
            {
                form1.Attributes.Add("class", "sub_page");
            }
            else
            {
                form1.Attributes.Remove("class");
                Control sliderControl = (Control)Page.LoadControl("sliderController.ascx");
                Panel1.Controls.Add(sliderControl);
            }
            if (Session["userID"] != null)
            {
                loginOrlogout.Text = "Logout";
                util util = new util();
                Session["cartCount"] = util.cartCount(Convert.ToInt32(Session["UserID"])).ToString();
            }
            else
            {
                loginOrlogout.Text = "Login";
                Session["cartCount"] = "0";
            }
        }

        protected void loginOrlogout_Click(object sender, EventArgs e)
        {
            if (Session["userID"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                Session.Abandon();
                Response.Redirect("Login.aspx");
            }
        }

        protected void RegisterOrProfile_Click(object sender, EventArgs e)
        {
            if (Session["userID"] != null)
            {
                RegisterOrProfile.ToolTip = "User Profile";
                Response.Redirect("Profile.aspx");
            }
            else
            {
                RegisterOrProfile.ToolTip = "User Registration";
                Response.Redirect("Registration.aspx");
            }
        }
    }
}