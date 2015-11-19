using CIAB.Models;
using System;
using System.Data;
using System.Data.SqlClient;
namespace CIAB.DataLayer
{
    public class ChangePasswordDataLayer:BaseDataLayer
    {
        #region Methods
        public int GetNewPassword(ResetPassword ResetPass, string strCurrentPass, string strNewPass)
        {
           using(SqlConnection CIABconnection = new SqlConnection(CIABconnectionString))
           {
               using (SqlCommand CIABcommand = new SqlCommand())
               {
               CIABconnection.Open();
               CIABcommand.CommandType = System.Data.CommandType.StoredProcedure;
               CIABcommand.Connection = CIABconnection;
               CIABcommand.CommandText = "sp_ResetPassword";
               CIABcommand.Parameters.AddWithValue("@username", ResetPass.UserName);
               CIABcommand.Parameters.AddWithValue("@old_pwd", strCurrentPass);
               CIABcommand.Parameters.AddWithValue("@new_pwd", strNewPass);
               CIABcommand.Parameters.Add("@Status", SqlDbType.Int);
               CIABcommand.Parameters["@Status"].Direction = ParameterDirection.Output;
               CIABcommand.ExecuteNonQuery();
               CIABcommand.Dispose();
               return Convert.ToInt32(CIABcommand.Parameters["@Status"].Value);
               }
           }
        }
    }
        #endregion
}