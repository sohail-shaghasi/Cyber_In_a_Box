using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;




namespace CIAB.Models

{
    public class AdminViewModel
    {
        string CIABconnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CIABConnectionString"].ConnectionString;



        public int? OrderID { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? OrderNumber { get; set; }
        public DateTimeOffset? OrderDate { get; set; }
        public int? ProductID { get; set; }
        public string OrderStatus { get; set; }
        //public decimal subTotal{ get{return Quantity * UnitPrice;}}

        public string UserEmail { get; set; }
        public string UserCompany { get; set; }
        public string UserContactNumber { get; set; }
        public string ProductName { get; set; }

       
        //for dropDownList 
        
        //public List<string> AvailableValues
        //{
        //    get
        //    {
        //        if (!string.IsNullOrEmpty(Value))
        //        {
        //            return Value.Split(',').ToList();
        //        }

        //        return null;
        //    }
        //}



        //public void ReadOrder()
        //{
        //    SqlConnection CIABconnection = new SqlConnection(CIABconnectionString);
        //    SqlCommand CIABcommand = new SqlCommand();
        //    CIABcommand.CommandText = "sp_ReadOrders";
        //    CIABcommand.CommandType = CommandType.StoredProcedure;
        //    CIABcommand.Connection = CIABconnection;

        //    SqlDataReader reader = CIABcommand.ExecuteReader();

        //    while(reader.Read())
        //    {
        //        OrderID = Convert.ToInt32(reader["OrderID"]);
        //        Quantity = Convert.ToInt32(reader["Quantity"]);
        //        UnitPrice = Convert.ToDouble(reader["UnitPrice"]);

        //    }




        //}



    }
}