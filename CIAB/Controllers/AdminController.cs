using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CIAB.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Data.SqlClient;
﻿


namespace CIAB.Controllers
{
    public class AdminController : Controller
    {
        //
        string CIABconnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CIABConnectionString"].ConnectionString;
        public ActionResult Index()
        {
            return View();
        }


        public ViewResult AdminView([DataSourceRequest] DataSourceRequest request)
        {


            var lstAdminModel = new List<AdminViewModel>();//List of Object

            SqlConnection CIABconnection = new SqlConnection(CIABconnectionString);
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
                objAdminModel.OrderDate = Convert.ToDateTime(reader["OrderDate"]);
                objAdminModel.OrderStatus = reader["OrderStatus"].ToString();
                objAdminModel.Quantity = Convert.ToInt32(reader["Quantity"]);
                objAdminModel.UnitPrice = Convert.ToDecimal(reader["UnitPrice"]);
                objAdminModel.UserCompany = reader["CompanyName"].ToString();
                objAdminModel.UserContactNumber = reader["ContactNumber"].ToString();
                objAdminModel.UserEmail = reader["Email"].ToString();
                objAdminModel.ProductID = Convert.ToInt32(reader["ProductID"]);
                objAdminModel.ProductName = reader["ProductName"].ToString();


                lstAdminModel.Add(objAdminModel);//add an object to the list of type object
            }

            
            var data = lstAdminModel.ToList();

            return View(data);


        }
    }
}