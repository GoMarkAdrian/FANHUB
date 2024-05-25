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

        }

        public void clear()
        {
            txtName.Text = string.Empty;
            cbIsActive.Checked = false;
            hiddenID.Value = "0";
            btnAddorUpdate.Text = "Add";
        }

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
                    imagePath = "Images/Category" + obj.ToString() + fileExtenstion;
                    fuCategoryImage.PostedFile.SaveAs(Server.MapPath("~/Images/Category") + obj.ToString() + fileExtenstion);
                    cmd.Parameters.AddWithValue("@ImageURL", imagePath);
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
                    // getCategories();
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
    }
}