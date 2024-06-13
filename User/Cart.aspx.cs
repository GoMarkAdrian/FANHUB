using FanHub.Admin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FanHub.User
{
    public partial class Cart : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        decimal grandTotal = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["useriD"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    getCartItems();

                }
            }
        }
        void getCartItems()
        {
            con = new SqlConnection(DBConnect.GetConnectionString());
            cmd = new SqlCommand("Cart_CRUD", con);
            cmd.Parameters.AddWithValue("@Action", "SELECT");
            cmd.Parameters.AddWithValue("@UserID", Session["userID"]);
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            CartItem.DataSource = dt;
            if (dt.Rows.Count == 0)
            {
                CartItem.FooterTemplate = null;
                CartItem.FooterTemplate = new CustomTemplate(ListItemType.Footer);
            }
            CartItem.DataBind();
        }
        protected void CartItem_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }
        protected void CartItem_ItemDataBound(object source, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label totalPrice = e.Item.FindControl("LabelTotalPrice") as Label;
                Label productPrice = e.Item.FindControl("LabelPrice") as Label;
                TextBox quantity = e.Item.FindControl("txtQuantity") as TextBox;
                decimal calTotalPrice = Convert.ToDecimal(productPrice.Text) * Convert.ToDecimal(quantity.Text);
                totalPrice.Text = calTotalPrice.ToString();
                grandTotal += calTotalPrice;
            }
            Session["grandTotalPrice"] = grandTotal;
        }

        private sealed class CustomTemplate : ITemplate
        {
            private ListItemType ListItemType{ get; set;}

            public CustomTemplate(ListItemType type)
            {
            ListItemType = type;
            }
            public void InstantiareIn(Control container)
            {
            if (ListItemType == ListItemType.Footer)
            {
                var footer = new LiteralControl("<tr><td colspan='5'><b>Your Cart is empty.</b><a href='Shop.aspx' class='badge badge-info ml-2'>Continue Shopping</a></td></tr></tbody></table>");
                container.Controls.Add(footer);
            }
            }

            public void InstantiateIn(Control container)
            {
                throw new NotImplementedException();
            }
        }
    }

}