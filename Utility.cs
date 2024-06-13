 using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;

namespace FanHub
{
    public class DBConnect
    {
        public static string GetConnectionString()
        {

            return ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        }
    }

    public class util
    {
        SqlConnection con;
        SqlCommand cmd;
        public static bool IsValidExtension(string FileName)
        {
            bool IsValid = false;
            string[] fileExtension = { ".jpg", ".png", ".jpeg" };
            for (int i = 0; i <= fileExtension.Length - 1; i++)
            {
                if (FileName.Contains(fileExtension[i]))
                {
                    IsValid = true;
                    break;
                }
            }
            return IsValid;
        }
        public static string GetImageUrl(Object url)
        {
            string url_new = "";
            if (string.IsNullOrEmpty(url.ToString()) || url == DBNull.Value)
            {
                url_new = "../Images/Default.png";
            }
            else
            {
                url_new = string.Format("../{0}", url);
            }
            return url_new;
        }
        
        public bool updateCartQuantity(int quantity, int productID, int UserID)
        {
            bool isUpdated = false;
            con = new SqlConnection(DBConnect.GetConnectionString());
            cmd = new SqlCommand("Cart_CRUD", con);
            cmd.Parameters.AddWithValue("@Action", "UPDATE");
            cmd.Parameters.AddWithValue("@ProductID", productID);
            cmd.Parameters.AddWithValue("@Quantity", quantity);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                isUpdated = true;
            }
            catch (Exception ex)
            {
                System.Web.HttpContext.Current.Response.Write("<script>alert('Error - " + ex.Message + " '<script>");
            }
            finally
            {
                con.Close();
            }
            return isUpdated;
        }

    }

}
    