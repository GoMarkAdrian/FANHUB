using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Security.Cryptography;

namespace FanHub.User
{
    public partial class Profile : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userID"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    getUserDetails();
                    getPurchaseHistory();
                }
            }
        }
        void getUserDetails()
        {
            con = new SqlConnection(DBConnect.GetConnectionString());
            cmd = new SqlCommand("Users_CRUD", con);
            cmd.Parameters.AddWithValue("@Action", "SELECT4PROFILE");
            cmd.Parameters.AddWithValue("@UserID", Session["userID"]);
            cmd.CommandType = CommandType.StoredProcedure;
            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            adapter.Fill(dt);
            userProfile.DataSource = dt;
            userProfile.DataBind();

            if (dt.Rows.Count == 1)
            {
                Session["name"] = dt.Rows[0]["Name"].ToString();
                Session["email"] = dt.Rows[0]["Email"].ToString();
                Session["imageURL"] = dt.Rows[0]["ImageURL"].ToString();
                Session["createdDate"] = dt.Rows[0]["CreatedDate"].ToString();
            }
        }
        void getPurchaseHistory()
        {
            int sr = 1;
            con = new SqlConnection(DBConnect.GetConnectionString());
            cmd = new SqlCommand("Invoice", con);
            cmd.Parameters.AddWithValue("@Action", "ODRHISTORY");
            cmd.Parameters.AddWithValue("@UserID", Session["userID"]);
            cmd.CommandType = CommandType.StoredProcedure;
            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            adapter.Fill(dt);
            dt.Columns.Add("SrNo",typeof(Int32));
            if(dt.Rows.Count > 0)
            {
                foreach(DataRow dataRow in dt.Rows)
                {
                    dataRow["SrNo"] = sr;
                    sr++;
                }
            }
            if (dt.Rows.Count == 0)
            {
                PurchaseHistory.FooterTemplate = null;
                PurchaseHistory.FooterTemplate = new CustomTemplate(ListItemType.Footer);
            }
            PurchaseHistory.DataSource = dt;
            PurchaseHistory.DataBind();
        }

        protected void PurchaseHistory_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                double grandTotal = 0;
                HiddenField paymentId = e.Item.FindControl("hiddenPaymentId") as HiddenField;
                Repeater repOrder = e.Item.FindControl("Orders") as Repeater;
                con = new SqlConnection(DBConnect.GetConnectionString());
                cmd = new SqlCommand("Invoice", con);
                cmd.Parameters.AddWithValue("@Action", "INVOICEID");
                cmd.Parameters.AddWithValue("@PaymentId", Convert.ToInt32(paymentId.Value));
                cmd.Parameters.AddWithValue("@UserID", Session["userID"]);
                cmd.CommandType = CommandType.StoredProcedure;
                adapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        grandTotal += Convert.ToDouble(dataRow["TotalPrice"]);
                    }
                }
                DataRow dr = dt.NewRow();
                dr["TotalPrice"] = grandTotal;
                dt.Rows.Add(dr);
                repOrder.DataSource = dt;
                repOrder.DataBind();
            }
        }
        private sealed class CustomTemplate : ITemplate
        {
            private ListItemType ListItemType { get; set; }

            public CustomTemplate(ListItemType type)
            {
                ListItemType = type;
            }
            public void InstantiateIn(Control container)
            {
                //throw new NotImplementedException();
                if (ListItemType == ListItemType.Footer)
                {
                    var footer = new LiteralControl("<tr><td><b>No data to show. Order Items now!</b><a href='Shop.aspx' class='badge badge-info ml-2'>Shop Here</a></td></tr></tbody></table>");
                    container.Controls.Add(footer);
                }
            }
        }
    }
}