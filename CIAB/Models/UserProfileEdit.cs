using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CIAB.Models
{
    public class UserProfileEdit
    {
        public int UserID { get; set; }
        
        public string FullName { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

        [StringLength(150)]
        public string email { get; set; }

        public string Website { get; set; }

        public string CompanyName { get; set; }

        public string CompanyAddress { get; set; }



        string CIABconnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CIABConnectionString"].ConnectionString;





        //------------------------------------------------------------------------





        public void UpdateProfile()
        {
                 UserName = HttpContext.Current.Session["UserName"].ToString();
                 Password = HttpContext.Current.Session["Password"].ToString();
            try
            {
                SqlConnection CIABconnection = new SqlConnection(CIABconnectionString);
                SqlCommand CIABcommand = new SqlCommand();
                CIABconnection.Open();
                CIABcommand.CommandType = CommandType.StoredProcedure;
                CIABcommand.Connection = CIABconnection;
                CIABcommand.CommandText = "sp_UpdateProfile";

                CIABcommand.Parameters.AddWithValue("@Parameter", "update");//parameter for if condition in Stored Procedure
                CIABcommand.Parameters.AddWithValue("@UserName", UserName);//parameter for User Name
                CIABcommand.Parameters.AddWithValue("@Password", Password);// parameter for Password
                CIABcommand.Parameters.AddWithValue("@FullName", FullName);//parameter for FullName
                CIABcommand.Parameters.AddWithValue("@Email", email); // parameter for Email
                CIABcommand.Parameters.AddWithValue("@Website", Website); //Parameter for Website
                CIABcommand.Parameters.AddWithValue("@CompanyName", CompanyName);//Parameter for Company Name
                CIABcommand.Parameters.AddWithValue("@CompanyAddress", CompanyAddress); // Parameter for Company Address
                CIABcommand.ExecuteNonQuery();

            }

            catch
            {

            }



        }


    }
}