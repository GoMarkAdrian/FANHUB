
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
    }
}