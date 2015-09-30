using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.ComponentModel.DataAnnotations;

namespace CIAB.Models
{


    //--------------------------------------------------------------------------------------------------------


    public class Products
    {
        public Products()
        {

        }


        //--------------------------------------------------------------------------------------------------------



        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProducImage { get; set; }
        public int ProductQuantity { get; set; }
        public int UnitsInStock { get; set; }
        public int UnitsOnOrder { get; set; }
        public int ReorderLevel { get; set; }
        public decimal? UnitPrice { get; set; }
        public string ProductDescription { get; set; }



        //--------------------------------------------------------------------------------------------------------




        //public List<DataRow> DisplayProduct()
        //{
        //    List<DataRow> list = null;

        //    string strSQLstatement = "SELECT [ProductID],[ProductHandle],[ProductName] ,[ProductQuantity],[UnitPrice] ,[ProductDescription] FROM [CIAB].[dbo].[Product] order by [ProductID] ASC";

        //    string strCS = ConfigurationManager.ConnectionStrings["CIABConnectionString"].ConnectionString;
        //    SqlConnection CIABConnection = new SqlConnection(strCS);
        //    CIABConnection.Open();
        //    SqlCommand command = new SqlCommand(strSQLstatement);
        //    command.Connection = CIABConnection;
        //    command.CommandType = CommandType.Text;

        //    DataTable dt = new DataTable();

        //    SqlDataAdapter sqlDA = new SqlDataAdapter(command);
        //    sqlDA.Fill(dt);


        //    if (dt != null)
        //    {
        //        list = dt.AsEnumerable().ToList();
        //    }
        //    return list;
        //}

















        public void addProduct(Models.Products products)
        {

            string strCS = ConfigurationManager.ConnectionStrings["CIABConnectionString"].ConnectionString;
            SqlConnection CIABConnection = new SqlConnection(strCS);
            CIABConnection.Open();
            SqlCommand command = new SqlCommand("spAddProduct", CIABConnection);
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter paramName = new SqlParameter();
            paramName.ParameterName = "@ProductName";
            paramName.Value = products.ProductName;
            command.Parameters.Add(paramName);

            SqlParameter paramQuantity = new SqlParameter();
            paramQuantity.ParameterName = "@ProductQuantity";
            paramQuantity.Value = products.ProductQuantity;
            command.Parameters.Add(paramQuantity);

            SqlParameter paramUnitPrice = new SqlParameter();
            paramUnitPrice.ParameterName = "@UnitPrice";
            paramUnitPrice.Value = products.UnitPrice;
            command.Parameters.Add(paramUnitPrice);

            SqlParameter paramUnitsInStock = new SqlParameter();
            paramUnitsInStock.ParameterName = "@UnitsInStock";
            paramUnitsInStock.Value = products.UnitsInStock;
            command.Parameters.Add(paramUnitsInStock);


            SqlParameter paramUnitsOnOrder = new SqlParameter();
            paramUnitsOnOrder.ParameterName = "@UnitsOnOrder";
            paramUnitsOnOrder.Value = products.UnitsOnOrder;
            command.Parameters.Add(paramUnitsOnOrder);

            SqlParameter paramReorderLevel = new SqlParameter();
            paramReorderLevel.ParameterName = "@ReorderLevel";
            paramReorderLevel.Value = products.ReorderLevel;
            command.Parameters.Add(paramReorderLevel);

            SqlParameter paramDescription = new SqlParameter();
            paramDescription.ParameterName = "@ProductDescription";
            paramDescription.Value = products.ProductDescription;
            command.Parameters.Add(paramDescription);

            command.ExecuteNonQuery();

        }
    }
}
