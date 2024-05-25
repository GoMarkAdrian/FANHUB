using System;
using System.Collections.Generic;
using System.Configuration;
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
    }

}
