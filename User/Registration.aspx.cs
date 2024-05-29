using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using FanHub.Admin;
using System.Xml.Linq;

namespace FanHub.User
{
    public partial class Registration : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    getUserDetails();
                }
                else if (Session["userID"] != null)
                {
                    Response.Redirect("Index.aspx");
                }
            }
        }

        private void clear()
        {
            txtName.Text = String.Empty;
            txtUsername.Text = String.Empty;
            txtMobile.Text = String.Empty;
            txtEmail.Text = String.Empty;
            txtAddress.Text = String.Empty;
            txtPostCode.Text = String.Empty;
            txtPassword.Text = String.Empty;
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string action = String.Empty, imagePath = string.Empty, fileExtenstion = string.Empty;
            bool isValidToExecute = false;
            int userID = Convert.ToInt32(Request.QueryString["id"]);
            con = new SqlConnection(DBConnect.GetConnectionString());
            cmd = new SqlCommand("Users_CRUD", con);
            cmd.Parameters.AddWithValue("@Action", userID == 0 ? "INSERT" : "UPDATE");
            cmd.Parameters.AddWithValue("@UserID", userID);
            cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
            cmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
            cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text.Trim());
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
            cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
            cmd.Parameters.AddWithValue("@PostCode", txtPostCode.Text.Trim());
            cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());
            if (fuUserImage.HasFile)
            {
                if (util.IsValidExtension(fuUserImage.FileName))
                {
                    Guid obj = Guid.NewGuid();
                    fileExtenstion = Path.GetExtension(fuUserImage.FileName);
                    imagePath = "Images/UserImages/" + obj.ToString() + fileExtenstion;
                    fuUserImage.PostedFile.SaveAs(Server.MapPath("~/Images/UserImages/") + obj.ToString() + fileExtenstion);
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
                    action = userID == 0 ?
                        "Registration is Successful! <b><a href='Login.aspx'>Click Here</a></b> to Login" :
                        "Details Updated Successfully! <b><a href='Profile.aspx'>Click Here</a></b> to Check";
                    labelMessage.Visible = true;
                    labelMessage.Text = "<b> " + txtUsername.Text.Trim() + " </b>" + action;
                    labelMessage.CssClass = "alert alert-success";
                    if (userID != 0)
                    {
                        Response.AddHeader("REFRESH", "1;URL=Profile.aspx");
                    }
                    clear();
                }
                catch (SqlException ex)
                {
                    if (ex.Message.Contains("Violation of UNIQUE KEY constraint"))
                    {
                        labelMessage.Visible = true;
                        labelMessage.Text = "<b>" + txtUsername.Text.Trim() + "</b> Username already Exists";
                        labelMessage.CssClass = "alert alert-danger";
                    }

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

        void getUserDetails()
        {
            con = new SqlConnection(DBConnect.GetConnectionString());
            cmd = new SqlCommand("Users_CRUD", con);
            cmd.Parameters.AddWithValue("@Action", "SELECT4PROFILE");
            cmd.Parameters.AddWithValue("@UserID", Request.QueryString["id"]);
            cmd.CommandType = CommandType.StoredProcedure;

            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            adapter.Fill(dt);

            if (dt.Rows.Count == 1)
            {
                txtName.Text = dt.Rows[0]["Name"].ToString();
                txtUsername.Text = dt.Rows[0]["Username"].ToString();
                txtMobile.Text = dt.Rows[0]["Mobile"].ToString();
                txtEmail.Text = dt.Rows[0]["Email"].ToString();
                txtAddress.Text = dt.Rows[0]["Address"].ToString();
                txtPostCode.Text = dt.Rows[0]["PostCode"].ToString();
                txtPassword.TextMode = TextBoxMode.SingleLine;
                txtPassword.ReadOnly = true;
                txtPassword.Text = dt.Rows[0]["Password"].ToString();
                imgUser.ImageUrl = string.IsNullOrEmpty(dt.Rows[0]["ImageURL"].ToString()) ? "../Images/Default.png" : "../" + dt.Rows[0]["ImageURL"].ToString();
                imgUser.Height = 200;
                imgUser.Width = 200;
            }
            lblHeader.Text = "<h2>Edit Profile</h2>";
            btnRegister.Text = "Update";
            AlreadyUser.Text = "";
        }
    }
}