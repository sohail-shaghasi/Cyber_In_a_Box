using CIAB.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace CIAB.DataLayer
{
    public class UserDataLayer : BaseDataLayer
    {

        /// <summary>
        /// Check if the User Exists in the Database.
        /// </summary>
        /// <param name="_UserName"></param>
        /// <param name="_Password"></param>
        /// <returns>True if User Exists and Password is Correct</returns>
        public bool IsValid(User user)
        {
            SqlConnection CIABconnection = new SqlConnection(base.CIABconnectionString);
            SqlCommand CIABcommand = new SqlCommand();
            CIABconnection.Open();
            CIABcommand.CommandType = CommandType.StoredProcedure;
            CIABcommand.Connection = CIABconnection;
            CIABcommand.CommandText = "sp_Login";

            //Login Parameters
            CIABcommand.Parameters.AddWithValue("@UserName", user.UserName);//parameter for User Name
            CIABcommand.Parameters.AddWithValue("@Password", user.HashPassword);// parameter for Password
            SqlDataReader reader = CIABcommand.ExecuteReader();


            while (reader.Read())
            {
                user.UserID = (int)reader["UserID"];
                user.LoginEmail = reader["Email"].ToString();
                user.Role = reader["Role"].ToString();

                reader.Dispose();
                CIABcommand.Dispose();
                return true;
            }

            return false;

        }
    }
}