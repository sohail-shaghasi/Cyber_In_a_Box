using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace CIAB.DataAccessLayer
{
    public class ProductBusinessLayer
    {

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