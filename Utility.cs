 using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using FanHub.Admin;
using Org.BouncyCastle.Asn1.Cms;

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
        SqlDataAdapter sda;
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
                isUpdated = false;
                System.Web.HttpContext.Current.Response.Write("<script>alert('Error - " + ex.Message + " '<script>");
            }
            finally
            {
                con.Close();
            }
            return isUpdated;
        }
        public int cartCount(int UserID)
        {
            con = new SqlConnection(DBConnect.GetConnectionString());
            cmd = new SqlCommand("Cart_CRUD", con);
            cmd.Parameters.AddWithValue("@Action", "SELECT");
            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt.Rows.Count;
        }

        public static string GetUniqueId()
        {
            Guid guid = Guid.NewGuid();
            string uniqueId = guid.ToString();
            return uniqueId;

        }
    }

    public class DashboardCount
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader sdr;

        public int Count(string tablename)
        {
            int count = 0;
            con = new SqlConnection (DBConnect.GetConnectionString());
            cmd = new SqlCommand("Dashboard", con);
            cmd.Parameters.AddWithValue ("@Action", tablename);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            //sda = new SqlDataAdapter(cmd);
            sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                if (sdr[0] == DBNull.Value)
                {
                    count = 0;
                }
                else
                {
                    count = Convert.ToInt32(sdr[0]);
                }
            }
            sdr.Close();
            con.Close();
            return count;
        }
    }

}
    