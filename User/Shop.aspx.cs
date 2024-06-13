
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FanHub.Admin;
using System.CodeDom;

namespace FanHub.User
{
    public partial class Shop : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getCategories();
                getProducts();
            }

        }

        // Select * from DB categories that is active then show
        private void getCategories()
        {

            con = new SqlConnection(DBConnect.GetConnectionString());
            cmd = new SqlCommand("Category_CRUD", con);
            cmd.Parameters.AddWithValue("@Action", "ACTIVECAT");
            cmd.CommandType = CommandType.StoredProcedure;
            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            adapter.Fill(dt);
            Categories.DataSource = dt;
            Categories.DataBind();
        }
        // Select * from DB products that is active then show
        private void getProducts()
        {

            con = new SqlConnection(DBConnect.GetConnectionString());
            cmd = new SqlCommand("Products_CRUD", con);
            cmd.Parameters.AddWithValue("@Action", "ACTIVEPROD");
            cmd.CommandType = CommandType.StoredProcedure;
            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            adapter.Fill(dt);
            Products.DataSource = dt;
            Products.DataBind();
        }

        protected void Products_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (Session["userId"] != null)
            {
                bool isCartItemUpdated = false;
                int i = isItemExistInCart( Convert.ToInt32(e.CommandArgument) );
                if (i == 0)
                {
                    //add dito ng new item sa cart
                    con = new SqlConnection(DBConnect.GetConnectionString());
                    cmd = new SqlCommand("Cart_CRUD", con);
                    cmd.Parameters.AddWithValue("@Action", "INSERT");
                    cmd.Parameters.AddWithValue("@ProductID", e.CommandArgument);
                    cmd.Parameters.AddWithValue("@Quantity", 1);
                    cmd.Parameters.AddWithValue("@UserID", Session["userID"]);
                    cmd.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }catch(Exception ex)
                    {
                        Response.Write("<script>alert('Error - " + ex.Message + " '<script>");
                    }
                    finally
                    {
                        con.Close();
                    }
                }
                else
                {
                    //para mag add ng merong existing item sa cart
                    util util = new util();
                        isCartItemUpdated = util.updateCartQuantity(i + 1, Convert.ToInt32(e.CommandArgument),Convert.ToInt32(Session["userID"]));
                    
                }
                labelMsg.Visible = true;
                labelMsg.Text = "Item Added to Cart!";
                labelMsg.CssClass = "alert alert-success";
                Response.AddHeader("REFRESH", "1;URL = Cart.aspx");
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        int isItemExistInCart(int productId)
        {
            con = new SqlConnection(DBConnect.GetConnectionString());
            cmd = new SqlCommand("Cart_CRUD", con);
            cmd.Parameters.AddWithValue("@Action", "GETBYID");
            cmd.Parameters.AddWithValue("@ProductID",productId);
            cmd.Parameters.AddWithValue("@UserID", Session["userId"]);
            cmd.CommandType = CommandType.StoredProcedure;
            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            adapter.Fill(dt);
            int quantity = 0;
            if(dt.Rows.Count > 0)
            {
                quantity = Convert.ToInt32(dt.Rows[0]["Quantity"]);
            }        
            return quantity;
        }



    }
}