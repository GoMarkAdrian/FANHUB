using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FanHub.Admin
{

    public partial class Contact : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["Show_DataTable"] = "Contact Users";
                if (Session["admin"] == null)
                {
                    Response.Redirect("../User/Login.aspx");
                }
                else
                {
                    getContacts();
                }
            }
        }

        private void getContacts()
        {
            con = new SqlConnection(DBConnect.GetConnectionString());
            cmd = new SqlCommand("ContactSp", con);
            cmd.Parameters.AddWithValue("@Action", "SELECT");
            cmd.CommandType = CommandType.StoredProcedure;
            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            adapter.Fill(dt);
            rContacts.DataSource = dt;
            rContacts.DataBind();
        }

        protected void rContacts_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                con = new SqlConnection(DBConnect.GetConnectionString());
                cmd = new SqlCommand("ContactSp", con);
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.Parameters.AddWithValue("@ContactId", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    labelMessage.Visible = true;
                    labelMessage.Text = "Record deleted successfully";
                    labelMessage.CssClass = "alert alert-danger";
                    // refresh table without the deleted file
                    getContacts();
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