using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace FanHub.Admin
{
    public partial class Users : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["Show_DataTable"] = "Users";
                if (Session["admin"] == null)
                {
                    Response.Redirect("../Users/Login.aspx");
                }
                else
                {
                    getUsers();
                }
            }
            labelMessage.Visible = false;
        }
        private void getUsers()
        {
            con = new SqlConnection(DBConnect.GetConnectionString());
            cmd = new SqlCommand("Users_CRUD", con);
            cmd.Parameters.AddWithValue("@Action", "SELECT4ADMIN");
            cmd.CommandType = CommandType.StoredProcedure;
            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            adapter.Fill(dt);
            dataTable_Users.DataSource = dt;
            dataTable_Users.DataBind();
        }

        protected void dataTable_Users_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                cmd = new SqlCommand("Users_CRUD", con);
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.Parameters.AddWithValue("@UserID", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    labelMessage.Visible = true;
                    labelMessage.Text = "User deleted successfully";
                    labelMessage.CssClass = "alert alert-danger";
                    // refresh table without the deleted file
                    getUsers();
                }
                catch (Exception ex)
                {
                    labelMessage.Visible = true;
                    labelMessage.Text = ex.Message;
                    labelMessage.CssClass = "alert alert-danger";
                }
                finally
                {
                    con.Close();
                }

            }
        }
    }
}