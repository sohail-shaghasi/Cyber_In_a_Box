using CIAB.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CIAB.DataLayer
{
    public class AdminDataLayer : BaseDataLayer
    {
        public List<AdminViewModel> GetListOfOrders()
        {
            var lstAdminModel = new List<AdminViewModel>();//List of Object

            SqlConnection CIABconnection = new SqlConnection(base.CIABconnectionString);
            SqlCommand CIABcommand = new SqlCommand();
            CIABconnection.Open();
            CIABcommand.CommandType = System.Data.CommandType.StoredProcedure;
            CIABcommand.Connection = CIABconnection;
            CIABcommand.CommandText = "sp_ReadOrders";

            SqlDataReader reader = CIABcommand.ExecuteReader();
            while (reader.Read())
            {
                var objAdminModel = new AdminViewModel();
                objAdminModel.OrderID = Convert.ToInt32(reader["OrderID"]);
                objAdminModel.OrderDate = (DateTime)reader["OrderDate"];
                objAdminModel.AdminOrderStatus.StatusValue = reader["OrderStatus"].ToString();
                objAdminModel.Quantity = Convert.ToInt32(reader["Quantity"]);

                decimal Result;
                if (decimal.TryParse(reader["UnitPrice"].ToString(), out Result))
                {
                    objAdminModel.UnitPrice = Result;
                }
                objAdminModel.UserCompany = reader["CompanyName"].ToString();
                objAdminModel.UserContactNumber = reader["ContactNumber"].ToString();
                objAdminModel.UserEmail = reader["Email"].ToString();
                objAdminModel.ProductName.Product_Name = reader["ProductName"].ToString();
                objAdminModel.UserId = Convert.ToInt32(reader["UserID"]);


                lstAdminModel.Add(objAdminModel);//add an object to the list of type object
            }

            return lstAdminModel;
        }

        public List<ProductName> GetListofProductName()
        {
            SqlConnection CIABconnection = new SqlConnection(CIABconnectionString);
            SqlCommand CIABcommand = new SqlCommand();
            CIABconnection.Open();
            CIABcommand.CommandType = System.Data.CommandType.StoredProcedure;
            CIABcommand.Connection = CIABconnection;
            CIABcommand.CommandText = "sp_ReadProducts";

            var result = new List<ProductName>();
            SqlDataReader reader = CIABcommand.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new CIAB.Models.ProductName()
                {
                    Product_Name = reader["ProductName"].ToString(),
                    Product_ID = Convert.ToInt32(reader["ProductID"])
                });
            }

            return result;
        }

        public void UpdateOrders(AdminViewModel adminModel)
        {
            SqlConnection CIABconnection = new SqlConnection(CIABconnectionString);
            SqlCommand CIABcommand = new SqlCommand();
            CIABconnection.Open();
            CIABcommand.CommandType = System.Data.CommandType.StoredProcedure;
            CIABcommand.Connection = CIABconnection;
            CIABcommand.CommandText = "sp_UpdateOrderDetails";


            CIABcommand.Parameters.AddWithValue("@orderid", adminModel.OrderID);
            //CIABcommand.Parameters.AddWithValue("@productID", adminModel.ProductID);
            CIABcommand.Parameters.AddWithValue("@userID", adminModel.UserId);
            CIABcommand.Parameters.AddWithValue("@orderDate", adminModel.OrderDate);
            CIABcommand.Parameters.AddWithValue("@orderStatus", adminModel.AdminOrderStatus.StatusValue);
            CIABcommand.Parameters.AddWithValue("@email", adminModel.UserEmail);
            CIABcommand.Parameters.AddWithValue("@userCompany", adminModel.UserCompany);
            CIABcommand.Parameters.AddWithValue("@userContactNumber", adminModel.UserContactNumber);
            CIABcommand.Parameters.AddWithValue("@productName", adminModel.ProductName);
            CIABcommand.ExecuteNonQuery();
        }

    }
}