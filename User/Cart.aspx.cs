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
            util util = new util();
            if (e.CommandName == "remove")
            {
                con = new SqlConnection(DBConnect.GetConnectionString());
                cmd = new SqlCommand("Cart_CRUD", con);
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.Parameters.AddWithValue("@ProductID",e.CommandArgument);
                cmd.Parameters.AddWithValue("@UserID", Session["userID"]);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    getCartItems();
                    //titingnan yung cart count
                    Session["cartCount"] = util.cartCount(Convert.ToInt32(Session["userID"]));
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Error - " + ex.Message + " '<script>");
                }
                finally
                {
                    con.Close();
                }
            }
            if(e.CommandName == "updateCart")
            {
                bool isCartUpdated = false;
                for(int item = 0; item < CartItem.Items.Count; item ++)
                {
                    if (CartItem.Items[item].ItemType == ListItemType.Item || CartItem.Items[item].ItemType == ListItemType.AlternatingItem)
                    {
                        TextBox quantity = CartItem.Items[item].FindControl("txtQuantity") as TextBox;
                        HiddenField _productID = CartItem.Items[item].FindControl("HiddenProductID") as HiddenField;
                        HiddenField _quantity = CartItem.Items[item].FindControl("HiddenQuantity") as HiddenField;
                        int quantityFromCart = Convert.ToInt32(quantity.Text);
                        int ProductID = Convert.ToInt32(_productID.Value);
                        int quantityFromDB = Convert.ToInt32(_quantity.Value);
                        bool isTrue = false;
                        int updatedQuantity = 1;
                        if(quantityFromCart > quantityFromDB)
                        {
                            updatedQuantity = quantityFromCart;
                            isTrue = true;
                        }else if(quantityFromCart < quantityFromDB)
                        {
                            updatedQuantity = quantityFromCart;
                            isTrue = true;
                        }
                        if (isTrue)
                        {
                            //na update ba yung cart item quantity sa DB
                            isCartUpdated = util.updateCartQuantity(updatedQuantity, ProductID, Convert.ToInt32(Session["userID"]));
                        }
                    }
                }
                getCartItems();
            }
            if(e.CommandName == "checkout")
            {
                bool isTrue = false;
                string pName = string.Empty;
                // check kung ilang pcs yung item sa cart
                for (int item = 0; item < CartItem.Items.Count; item++)
                {
                    if (CartItem.Items[item].ItemType == ListItemType.Item || CartItem.Items[item].ItemType == ListItemType.AlternatingItem)
                    {
                        HiddenField _productID = CartItem.Items[item].FindControl("HiddenProductID") as HiddenField;
                        HiddenField _cartQuantity = CartItem.Items[item].FindControl("HiddenQuantity") as HiddenField;
                        HiddenField _productQuantity = CartItem.Items[item].FindControl("HiddenPrdQuantity") as HiddenField;
                        Label productName = CartItem.Items[item].FindControl("LabelName") as Label;
                        int ProductID = Convert.ToInt32(_productID.Value);
                        int cartQuantity = Convert.ToInt32(_cartQuantity.Value);
                        int productQuantity = Convert.ToInt32(_productQuantity.Value);
                        //ito yung mag ccheck sa db kung may stock at kung mag pproceed sa checkout
                        if (productQuantity > cartQuantity && productQuantity > 2)
                        {
                            //so mag pupunta sa checkout page
                            isTrue = true;
                        }
                        else
                        {
                            //walang stock or low stock
                            isTrue = false;
                            pName = productName.Text.ToString();
                            break;
                        }
                    }
                }
                if (isTrue)
                {
                    //ito ang mag reredirect papunta sa payment
                    Response.Redirect("Paymemt.aspx");
                }
                else
                {
                    labelMsg.Visible = true;
                    labelMsg.Text = "Oh No! Item <b>'" + pName + "'</b> is out of stock!";
                    labelMsg.CssClass = "alert alert-warning";
                }
            }
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