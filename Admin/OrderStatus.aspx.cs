using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace FanHub.Admin
{
    public partial class OrderStatus : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["Show_DataTable"] = "Order Status";
                if (Session["admin"] == null)
                {
                    Response.Redirect("../User/Login.aspx");
                }
                else
                {
                    getOrderStatus();
                }
            }
            labelMessage.Visible = false;
            panelUpdateOrderStatus.Visible = false;
        }

        private void getOrderStatus()
        {

            con = new SqlConnection(DBConnect.GetConnectionString());
            cmd = new SqlCommand("Invoice", con);
            cmd.Parameters.AddWithValue("@Action", "GETSTATUS");
            cmd.CommandType = CommandType.StoredProcedure;
            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            adapter.Fill(dt);
            rOrderStatus.DataSource = dt;
            rOrderStatus.DataBind();
        }

        protected void OrderStatus_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            labelMessage.Visible = false;
            if (e.CommandName == "Edit")
            {
                con = new SqlConnection(DBConnect.GetConnectionString());
                cmd = new SqlCommand("Invoice", con);
                // Get GETBYID Procedure in DB
                cmd.Parameters.AddWithValue("@Action", "STATUSBYID");
                cmd.Parameters.AddWithValue("@OrderDetailsId", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;
                adapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                // FILL UP TEXT BOXES
                ddlOrderStatus.SelectedValue = dt.Rows[0]["Status"].ToString();
                hiddenID.Value = dt.Rows[0]["OderID"].ToString();
                // LINK EDIT BUTTON
                panelUpdateOrderStatus.Visible = true;
                LinkButton btn = e.Item.FindControl("lnkEdit") as LinkButton;
                //btn.CssClass = "badge badge-warning";
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int orderDetailsId = Convert.ToInt32(hiddenID.Value);
            con = new SqlConnection(DBConnect.GetConnectionString());
            cmd = new SqlCommand("Invoice", con);
            cmd.Parameters.AddWithValue("@Action", "UPDSTATUS");
            cmd.Parameters.AddWithValue ("@OrderDetailsId", orderDetailsId);
            cmd.Parameters.AddWithValue ("@Status", ddlOrderStatus.SelectedValue);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                labelMessage.Visible = true;
                labelMessage.Text = "Order Status Updated Sucessfully";
                labelMessage.CssClass = "alert alert-success";
                getOrderStatus();
            }
            catch (Exception ex)
            {
                labelMessage.Visible = true;
                labelMessage.Text = "ERROR : " + "  " + ex.Message;
                labelMessage.CssClass = "alert alert-danger";
            }
            finally
            {

                con.Close();
            }
        } 
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            panelUpdateOrderStatus.Visible=false;
        }
    }
}