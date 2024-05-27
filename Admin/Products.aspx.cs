using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace FanHub.Admin
{
    public partial class Products : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["Show_DataTable"] = "Products";
                getProducts();
            }
            labelMessage.Visible = false;
        }
        private void getProducts()
        {

            con = new SqlConnection(DBConnect.GetConnectionString());
            cmd = new SqlCommand("Products_CRUD", con);
            cmd.Parameters.AddWithValue("@Action", "SELECT");
            cmd.CommandType = CommandType.StoredProcedure;
            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            adapter.Fill(dt);
            dataTable_Products.DataSource = dt;
            dataTable_Products.DataBind();
        }

        public void clear()
        {
            txtName.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtPrice.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            ddlCategories.ClearSelection();
            cbIsActive.Checked = false;
            hiddenID.Value = "0";
            btnAddorUpdate.Text = "Add";
            imgProduct.ImageUrl = string.Empty;
        }

        protected void btnAddorUpdate_Click(object sender, EventArgs e)
        {
            string action = String.Empty, imagePath = string.Empty, fileExtenstion = string.Empty;
            bool isValidToExecute = false;
            int productID = Convert.ToInt32(hiddenID.Value);
            con = new SqlConnection(DBConnect.GetConnectionString());
            cmd = new SqlCommand("Products_CRUD", con);
            cmd.Parameters.AddWithValue("@Action", productID == 0 ? "INSERT" : "UPDATE");
            cmd.Parameters.AddWithValue("@ProductID", productID);
            cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
            cmd.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());
            cmd.Parameters.AddWithValue("@Price", txtPrice.Text.Trim());
            cmd.Parameters.AddWithValue("@Quantity", txtQuantity.Text.Trim());
            cmd.Parameters.AddWithValue("@CategoryID", ddlCategories.SelectedValue);
            cmd.Parameters.AddWithValue("@IsActive", cbIsActive.Checked);
            if (fuProductImage.HasFile)
            {
                if (util.IsValidExtension(fuProductImage.FileName))
                {
                    Guid obj = Guid.NewGuid();
                    fileExtenstion = Path.GetExtension(fuProductImage.FileName);
                    imagePath = "Images/Products/" + obj.ToString() + fileExtenstion;
                    fuProductImage.PostedFile.SaveAs(Server.MapPath("~/Images/Products/") + obj.ToString() + fileExtenstion);
                    cmd.Parameters.AddWithValue("@ImageURL", imagePath);
                    isValidToExecute = true;
                }
                else
                {
                    labelMessage.Visible = true;
                    labelMessage.Text = "Please Select .JPG, .JPEG, .PNG files only";
                    labelMessage.CssClass = "alert alert-danger";
                    isValidToExecute = false;
                }
            }
            else
            {
                isValidToExecute = true;
            }
            if (isValidToExecute)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    action = productID == 0 ? "inserted" : "updated";
                    labelMessage.Visible = true;
                    labelMessage.Text = "Product " + action + " Sucessfully";
                    labelMessage.CssClass = "alert alert-success";
                    getProducts();
                    clear();
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
        }

        protected void dataTable_Products_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            con = new SqlConnection(DBConnect.GetConnectionString());
            labelMessage.Visible = false;
            if (e.CommandName == "Edit")
            {
                //CONNECT TO Database
                cmd = new SqlCommand("Products_CRUD", con);
                // Get GETBYID Procedure in DB
                cmd.Parameters.AddWithValue("@Action", "GETBYID");
                cmd.Parameters.AddWithValue("@ProductID", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;
                adapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                // FILL UP TEXT BOXES
                txtName.Text = dt.Rows[0]["Name"].ToString();
                txtDescription.Text = dt.Rows[0]["Description"].ToString();
                txtPrice.Text = dt.Rows[0]["Price"].ToString();
                txtQuantity.Text = dt.Rows[0]["Quantity"].ToString();
                ddlCategories.SelectedValue = dt.Rows[0]["CategoryID"].ToString();
                cbIsActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                imgProduct.ImageUrl = string.IsNullOrEmpty(dt.Rows[0]["ImageURL"].ToString()) ? "../Images/Default.png" : "../" + dt.Rows[0]["ImageURL"].ToString();
                imgProduct.Height = 200;
                imgProduct.Width = 200;
                hiddenID.Value = dt.Rows[0]["ProductID"].ToString();
                // LINK EDIT BUTTON
                btnAddorUpdate.Text = "Update";
                LinkButton btn = e.Item.FindControl("ProductsEdit") as LinkButton;
                btn.CssClass = "badge badge-warning";
            }
            else if (e.CommandName == "Delete")
            {
                cmd = new SqlCommand("Products_CRUD", con);
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.Parameters.AddWithValue("@ProductID", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    labelMessage.Visible = true;
                    labelMessage.Text = "Product deleted successfully";
                    labelMessage.CssClass = "alert alert-danger";
                    // refresh table without the deleted file
                    getProducts();
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

        protected void dataTable_Products_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // FIND STRING lblIsActive
                Label lblActive = e.Item.FindControl("lblIsActive") as Label;
                Label lblQuantity = e.Item.FindControl("lblQuantity") as Label;
                if (lblActive.Text == "True")
                {
                    lblActive.Text = "Active";
                    lblActive.CssClass = "badge badge-success";
                }
                else
                {
                    lblActive.Text = "In-Active";
                    lblActive.CssClass = "badge badge-danger";
                }

                if (Convert.ToInt32(lblQuantity.Text) <= 5)
                {
                    lblQuantity.CssClass = "badge badge-danger";
                    lblQuantity.ToolTip = "Low Stocks";
                }

            }
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }
    }
}