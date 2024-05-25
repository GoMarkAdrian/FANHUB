using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;


namespace FanHub.Admin
{
    public partial class Category : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["Show_DataTable"] = "Category";
                getCategories();
            }
            labelMessage.Visible = false;
        }

        public void clear()
        {
            txtName.Text = string.Empty;
            cbIsActive.Checked = false;
            hiddenID.Value = "0";
            btnAddorUpdate.Text = "Add";
            imgCategory.ImageUrl = string.Empty;
        }

        // Select * from DB then show
        private void getCategories()
        {
            con = new SqlConnection(DBConnect.GetConnectionString());
            cmd = new SqlCommand("Category_CRUD", con);
            cmd.Parameters.AddWithValue("@Action", "SELECT");
            cmd.CommandType = CommandType.StoredProcedure;
            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            adapter.Fill(dt);
            dataTable_Category.DataSource = dt;
            dataTable_Category.DataBind();
        }

        // Insert
        protected void btnAddorUpdate_Click(object sender, EventArgs e)
        {
            string action = String.Empty, imagePath = string.Empty, fileExtenstion = string.Empty;
            bool isValidToExecute = false;
            int categoryID = Convert.ToInt32(hiddenID.Value);
            con = new SqlConnection(DBConnect.GetConnectionString());
            cmd = new SqlCommand("Category_CRUD", con);
            cmd.Parameters.AddWithValue("@Action", categoryID == 0 ? "INSERT" : "UPDATE");
            cmd.Parameters.AddWithValue("@CategoryID", categoryID);
            cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
            cmd.Parameters.AddWithValue("@IsActive", cbIsActive.Checked);
            if (fuCategoryImage.HasFile)
            {
                if (util.IsValidExtension(fuCategoryImage.FileName))
                {
                    Guid obj = Guid.NewGuid();
                    fileExtenstion = Path.GetExtension(fuCategoryImage.FileName);
                    imagePath = "Images/Category/" + obj.ToString() + fileExtenstion;
                    fuCategoryImage.PostedFile.SaveAs(Server.MapPath("~/Images/Category/") + obj.ToString() + fileExtenstion);
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
                    action = categoryID == 0 ? "inserted" : "updated";
                    labelMessage.Visible = true;
                    labelMessage.Text = "Category " + action + " Sucessfully";
                    labelMessage.CssClass = "alert alert-success";
                    getCategories();
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

        protected void dataTable_Category_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            con = new SqlConnection(DBConnect.GetConnectionString());
            labelMessage.Visible = false;
            if (e.CommandName == "Edit")
            {
                //CONNECT TO Database
                //con = new SqlConnection (DBConnect.GetConnectionString());
                cmd = new SqlCommand("Category_CRUD", con);
                // Get GETBYID Procedure in DB
                cmd.Parameters.AddWithValue("@Action", "GETBYID");
                cmd.Parameters.AddWithValue("@CategoryID", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;
                adapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                // FILL UP TEXT BOXES
                txtName.Text = dt.Rows[0]["Name"].ToString();
                cbIsActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                imgCategory.ImageUrl = string.IsNullOrEmpty(dt.Rows[0]["ImageURL"].ToString()) ? "../Images/Default.png" : "../" + dt.Rows[0]["ImageURL"].ToString();
                imgCategory.Height = 200;
                imgCategory.Width = 200;
                hiddenID.Value = dt.Rows[0]["CategoryID"].ToString();
                // LINK EDIT BUTTON
                btnAddorUpdate.Text = "Update";
                LinkButton btn = e.Item.FindControl("CategoryEdit") as LinkButton;
                btn.CssClass = "badge badge-warning";
            }
            else if (e.CommandName == "Delete")
            {
                cmd = new SqlCommand("Category_CRUD", con);
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.Parameters.AddWithValue("@CategoryID", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    //con = new SqlConnection(DBConnect.GetConnectionString());
                    con.Open();
                    cmd.ExecuteNonQuery();
                    labelMessage.Visible = true;
                    labelMessage.Text = "Category deleted successfully";
                    labelMessage.CssClass = "alert alert-danger";
                    // refresh table without the deleted file
                    getCategories();
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

        protected void dataTable_Category_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // FIND STRING lblIsActive
                Label lbl = e.Item.FindControl("lblIsActive") as Label;
                if (lbl.Text == "True")
                {
                    lbl.Text = "Active";
                    lbl.CssClass = "badge badge-success";
                }
                else
                {
                    lbl.Text = "In-Active";
                    lbl.CssClass = "badge badge-Danger";
                }
            }
        }
    }
}