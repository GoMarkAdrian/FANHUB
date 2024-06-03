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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userID"] != null)
            {
                Response.Redirect("Index.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            SqlConnection con;
            SqlCommand cmd;
            SqlDataAdapter adapter;
            DataTable dt;

            if (txtUsername.Text.Trim() == "Admin")
            {
                con = new SqlConnection(DBConnect.GetConnectionString());
                cmd = new SqlCommand("Users_CRUD", con);
                cmd.Parameters.AddWithValue("@Action", "SELECT4LOGIN");
                cmd.Parameters.AddWithValue("@Username", "Admin");
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());
                cmd.CommandType = CommandType.StoredProcedure;
                adapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    Session["admin"] = txtUsername.Text.Trim();
                    Session["userID"] = dt.Rows[0]["UserID"];
                    Response.Redirect("../Admin/Dashboard.aspx");
                }
                else
                {
                    labelMessage.Visible = true;
                    labelMessage.Text = "Invalid Credentials";
                    labelMessage.CssClass = "alert alert-danger";
                }
            }
            else
            {
                con = new SqlConnection(DBConnect.GetConnectionString());
                cmd = new SqlCommand("Users_CRUD", con);
                cmd.Parameters.AddWithValue("@Action", "SELECT4LOGIN");
                cmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());
                cmd.CommandType = CommandType.StoredProcedure;
                adapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    Session["username"] = txtUsername.Text.Trim();
                    Session["userID"] = dt.Rows[0]["UserID"];
                    Response.Redirect("Index.aspx");
                }
                else
                {
                    labelMessage.Visible = true;
                    labelMessage.Text = "Invalid Credentials";
                    labelMessage.CssClass = "alert alert-danger";
                }
            }
        }
    }
}