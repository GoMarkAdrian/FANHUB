using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FanHub.Admin
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["Show_DataTable"] = "";
                if (Session["admin"] == null)
                {
                    Response.Redirect("../User/Login.aspx");
                }
                else
                {
                    DashboardCount dashboard = new DashboardCount();
                    Session["category"] = dashboard.Count("CATEGORY");
                    Session["product"] = dashboard.Count("PRODUCT");
                    Session["totalOrder"] = dashboard.Count("ORDER");
                    Session["deliveredItems"] = dashboard.Count("DELIVERED");
                    Session["pendingItems"] = dashboard.Count("PENDING");
                    Session["users"] = dashboard.Count("USER");
                    Session["soldAmount"] = dashboard.Count("SOLDAMOUNT");
                    Session["feedback"] = dashboard.Count("CONTACT");
                }
            }
        }
    }
}