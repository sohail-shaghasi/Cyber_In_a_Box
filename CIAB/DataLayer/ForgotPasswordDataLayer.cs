using CIAB.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace CIAB.DataLayer
{
    public class ForgotPasswordDataLayer:BaseDataLayer
    {
        public bool IsPasswordResetLinkValid(string UniqueID)
        {
            bool result;
            //call the store proc to check if the GUID is valid and still exists in the table.
            using (SqlConnection CIABconnection = new SqlConnection(CIABconnectionString))
            {
                SqlCommand CIABcommand = new SqlCommand();
                CIABconnection.Open();
                CIABcommand.CommandType = System.Data.CommandType.StoredProcedure;
                CIABcommand.Connection = CIABconnection;
                CIABcommand.CommandText = "[dbo].[sp_IsPasswordResetLinkValid]";
                CIABcommand.Parameters.AddWithValue("@GUID", UniqueID);//pass a parameter to the stored proc
                result = Convert.ToBoolean(CIABcommand.ExecuteScalar());
                return result;
            }
        }

        public bool GetPasswordReset(UserForgotPassword forgotPassword, string UniqueID)
        {
            string strNewPassword = forgotPassword.ConfirmPassword;
            string HashPassword;
            bool result;
            //call the store proc to check if the GUID is valid and still exists in the table.
            SqlConnection CIABconnection = new SqlConnection(CIABconnectionString);
            SqlCommand CIABcommand = new SqlCommand();
            CIABconnection.Open();
            CIABcommand.CommandType = System.Data.CommandType.StoredProcedure;
            CIABcommand.Connection = CIABconnection;
            CIABcommand.CommandText = "sp_ChangePassword";

            //apply hash and salt
            Encoding encoder = new UTF8Encoding();
            SHA1 sha = new SHA1Managed();
            byte[] passwordHash = sha.ComputeHash(encoder.GetBytes(strNewPassword + "d3katk00"));
            string hashPass = ByteArrayToString(passwordHash);
            HashPassword = hashPass;

            //send parameters to store procedure.
            CIABcommand.Parameters.AddWithValue("@GUID", UniqueID);
            CIABcommand.Parameters.AddWithValue("@Password", HashPassword);
            
            result = Convert.ToBoolean(CIABcommand.ExecuteScalar());
            return result;
        }



    }
}