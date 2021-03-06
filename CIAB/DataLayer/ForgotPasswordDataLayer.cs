﻿using CIAB.Models;
using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
namespace CIAB.DataLayer
{
    public class ForgotPasswordDataLayer:BaseDataLayer
    {
        #region Methods
        public bool IsPasswordResetLinkValid(string UniqueID)
        {
            bool result;
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
            SqlConnection CIABconnection = new SqlConnection(CIABconnectionString);
            SqlCommand CIABcommand = new SqlCommand();
            CIABconnection.Open();
            CIABcommand.CommandType = System.Data.CommandType.StoredProcedure;
            CIABcommand.Connection = CIABconnection;
            CIABcommand.CommandText = "sp_ChangePassword";
            Encoding encoder = new UTF8Encoding();
            SHA1 sha = new SHA1Managed();
            byte[] passwordHash = sha.ComputeHash(encoder.GetBytes(strNewPassword + "d3katk00"));
            string hashPass = ByteArrayToString(passwordHash);
            HashPassword = hashPass;
            CIABcommand.Parameters.AddWithValue("@GUID", UniqueID);
            CIABcommand.Parameters.AddWithValue("@Password", HashPassword);
            result = Convert.ToBoolean(CIABcommand.ExecuteScalar());
            return result;
        }
        #endregion
    }
}