using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CIAB.Models;
using System.Data.SqlClient;
using System.Data;
namespace CIAB.DataLayer
{
    public class ProfileDataLayer : BaseDataLayer
    {
        public void UpdateProfile(UserProfileEdit profile)
        {
            var CIABconnection = new SqlConnection(base.CIABconnectionString);
            var CIABcommand = new SqlCommand();
            CIABconnection.Open();
            CIABcommand.CommandType = CommandType.StoredProcedure;
            CIABcommand.Connection = CIABconnection;
            CIABcommand.CommandText = "sp_UpdateProfile";

            CIABcommand.Parameters.AddWithValue("@Parameter", "update");//parameter for if condition in Stored Procedure
            CIABcommand.Parameters.AddWithValue("@UserName", profile.UserName);//parameter for User Name
            CIABcommand.Parameters.AddWithValue("@Password", profile.Password);// parameter for Password
            CIABcommand.Parameters.AddWithValue("@FullName", profile.FullName);//parameter for FullName
            CIABcommand.Parameters.AddWithValue("@Email", profile.email); // parameter for Email
            CIABcommand.Parameters.AddWithValue("@Website", profile.Website); //Parameter for Website
            CIABcommand.Parameters.AddWithValue("@CompanyName", profile.CompanyName);//Parameter for Company Name
            CIABcommand.Parameters.AddWithValue("@CompanyAddress", profile.CompanyAddress); // Parameter for Company Address
            CIABcommand.ExecuteNonQuery();

        }
    }
}