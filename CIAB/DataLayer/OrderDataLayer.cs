using CIAB.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace CIAB.DataLayer
{
    public class OrderDataLayer :BaseDataLayer
    {
        public string UserIDFromSession { get; set; }

        #region Methods
        public void DisplayOrderConfirmation(string strProductHandle, int userIDFromSession)
        {
            SqlConnection CIABconnection = new SqlConnection(CIABconnectionString);
            SqlCommand CIABcommand = new SqlCommand();
            CIABconnection.Open();
            CIABcommand.CommandType = CommandType.StoredProcedure;
            CIABcommand.Connection = CIABconnection;
            CIABcommand.CommandText = "sp_CreateOrder";
            CIABcommand.Parameters.AddWithValue("@UserID", userIDFromSession);
            CIABcommand.Parameters.AddWithValue("@parameter", strProductHandle);
            CIABcommand.ExecuteNonQuery();
        }
        public ConfirmOrderCustomer GetUserDetails()
        {
            SqlConnection CIABconnection = new SqlConnection(CIABconnectionString);
            SqlCommand CIABcommand = new SqlCommand();
            CIABconnection.Open();
            var CustomerOrderConfirmation = new ConfirmOrderCustomer();
            CIABcommand.CommandType = CommandType.StoredProcedure;
            CIABcommand.Connection = CIABconnection;
            CIABcommand.CommandText = "sp_UserDetails";
            if (HttpContext.Current.Session["UserId"] != null)
            {
                UserIDFromSession = HttpContext.Current.Session["UserId"].ToString();
            }
            CIABcommand.Parameters.AddWithValue("@UserID", UserIDFromSession);//
            SqlDataReader reader = CIABcommand.ExecuteReader();
            while (reader.Read())
            {
                CustomerOrderConfirmation.FullName = reader["FullName"].ToString();
                CustomerOrderConfirmation.UserID = Convert.ToInt32(UserIDFromSession);
                CustomerOrderConfirmation.JobTitle = reader["JobTitle"].ToString();
                CustomerOrderConfirmation.LastName = reader["LastName"].ToString();
                CustomerOrderConfirmation.UserName = reader["UserName"].ToString();
                CustomerOrderConfirmation.Website = reader["Website"].ToString();
                CustomerOrderConfirmation.Email = reader["Email"].ToString();
                CustomerOrderConfirmation.ContactNumber = reader["ContactNumber"].ToString();
                CustomerOrderConfirmation.CompanyName = reader["CompanyName"].ToString();
                CustomerOrderConfirmation.CompanyAddress = reader["CompanyAddress"].ToString();
            }
            return CustomerOrderConfirmation;
        }
        #endregion
    }
}