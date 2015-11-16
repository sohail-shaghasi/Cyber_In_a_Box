using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CIAB.Models;
using System.Data.SqlClient;
using System.Data;

namespace CIAB.DataLayer
{
    public class UserPurchasesDataLayer : BaseDataLayer
    {
        public int UserID { get; set; }
        public List<UserPurchasesViewModel> GetListOfUserOrders()
        {

            if (HttpContext.Current.Session["UserId"] != null && HttpContext.Current.Session["UserId"] != string.Empty)
            {
                UserID = Convert.ToInt32(HttpContext.Current.Session["UserId"]);
            }
            var lstUserPurchaseOrders = new List<UserPurchasesViewModel>();
            SqlConnection CIABconnection = new SqlConnection(base.CIABconnectionString);
            SqlCommand CIABcommand = new SqlCommand();
            CIABconnection.Open();
            CIABcommand.CommandType = System.Data.CommandType.StoredProcedure;
            CIABcommand.Connection = CIABconnection;
            CIABcommand.CommandText = "sp_ReadUserPurchaseOrders";
            CIABcommand.Parameters.AddWithValue("@UserID", UserID);//Input Parameter
            var reader = CIABcommand.ExecuteReader();
            while (reader.Read())
            {
                var objUserPurchasesViewModel = new UserPurchasesViewModel();
                objUserPurchasesViewModel.OrderID = Convert.ToInt32(reader["OrderID"]);
                objUserPurchasesViewModel.ProductName = reader["ProductName"].ToString();
                objUserPurchasesViewModel.OrderDate = (DateTime)reader["OrderDate"];
                objUserPurchasesViewModel.UserEmail = reader["Email"].ToString();
                objUserPurchasesViewModel.UserCompany = reader["CompanyName"].ToString();
                objUserPurchasesViewModel.UserContactNumber = reader["ContactNumber"].ToString();
                objUserPurchasesViewModel.OrderStatus = reader["OrderStatus"].ToString();
                var fileName1 = Convert.ToString(reader["FileName1"]);
                objUserPurchasesViewModel.FileName1 = (string.IsNullOrEmpty(fileName1)) ? "" : fileName1;
                var fileName2 = Convert.ToString(reader["FileName2"]);
                objUserPurchasesViewModel.FileName2 = (string.IsNullOrEmpty(fileName2)) ? "" : fileName2;
                var fileName3 = Convert.ToString(reader["FileName3"]);
                objUserPurchasesViewModel.FileName3 = (string.IsNullOrEmpty(fileName3)) ? "" : fileName3;
                lstUserPurchaseOrders.Add(objUserPurchasesViewModel);
            }
            return lstUserPurchaseOrders;
        }


        //public byte[] DownloadReport()
        //{
        //    return 
        //}
    }
}