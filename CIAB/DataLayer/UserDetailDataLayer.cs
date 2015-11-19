using CIAB.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CIAB.DataLayer
{
    public class UserDetailDataLayer:BaseDataLayer
    {
        #region Methods
        public List<UserDetail> GetUserDetails()
        {
            var lstUserModel = new List<UserDetail>();//List of Object
            SqlConnection CIABconnection = new SqlConnection(CIABconnectionString);
            SqlCommand CIABcommand = new SqlCommand();
            CIABconnection.Open();
            CIABcommand.CommandType = System.Data.CommandType.StoredProcedure;
            CIABcommand.Connection = CIABconnection;
            CIABcommand.CommandText = "sp_UserDetails";
            SqlDataReader reader = CIABcommand.ExecuteReader();
            while (reader.Read())
            {
                var objUserDetails = new CIAB.Models.UserDetail();
                objUserDetails.UserId = reader["UserID"].ToString();
                objUserDetails.FullName = reader["FullName"].ToString();
                objUserDetails.UserName = reader["UserName"].ToString();
                objUserDetails.Email = reader["Email"].ToString();
                objUserDetails.pass = reader["Pass"].ToString();
                lstUserModel.Add(objUserDetails);//add an object to the list of type object
            }
            return lstUserModel;
        }
        public void UpdateUserDetails(UserDetail userDetails)
        {
            SqlConnection CIABconnection = new SqlConnection(CIABconnectionString);
            SqlCommand CIABcommand = new SqlCommand();
            CIABconnection.Open();
            CIABcommand.CommandType = System.Data.CommandType.StoredProcedure;
            CIABcommand.Connection = CIABconnection;
            CIABcommand.CommandText = "sp_UpdateUserDetails";
            CIABcommand.Parameters.AddWithValue("@userID", userDetails.UserId);
            CIABcommand.Parameters.AddWithValue("@fullName", userDetails.FullName);
            CIABcommand.Parameters.AddWithValue("@userName", userDetails.UserName);
            CIABcommand.Parameters.AddWithValue("@email", userDetails.Email);
            CIABcommand.ExecuteNonQuery();
        }
        #endregion
    }
}