﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CIAB.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Data.SqlClient;
using System.IO;
﻿


namespace CIAB.Controllers
{
    public class AdminController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        public ViewResult AdminView([DataSourceRequest]DataSourceRequest request)
        {
            var lstAdminModel = new List<AdminViewModel>();//List of Object
            try
            {
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
                    //objAdminModel.ProductID = Convert.ToInt32(reader["ProductID"]);
                    objAdminModel.ProductName.Product_Name = reader["ProductName"].ToString();
                    objAdminModel.UserId = Convert.ToInt32(reader["UserID"]);


                    lstAdminModel.Add(objAdminModel);//add an object to the list of type object
                }

            }
            catch (Exception ex)
            {
                base.Logger.Error(ex, "AdminView_{0} | StackTrace: {1}", ex.Message, ex.StackTrace);
            }

            return View(lstAdminModel);

        }

        public JsonResult AdminViewOrderStatus()
        {
            var result = new List<AdminOrderStatus>();
            try
            {
                result.Add(new CIAB.Models.AdminOrderStatus() { StatusValue = "Open", Code = 1 });
                result.Add(new CIAB.Models.AdminOrderStatus() { StatusValue = "Close", Code = 2 });
                result.Add(new CIAB.Models.AdminOrderStatus() { StatusValue = "In Progress", Code = 3 });
            }
            catch (Exception ex)
            {
                base.Logger.Error(ex, "AdminViewOrderStatus_{0} | StackTrace: {1}", ex.Message, ex.StackTrace);
            }

            return Json(result, JsonRequestBehavior.AllowGet);

        }
       
        public JsonResult AdminViewProductName()
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


            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult Create()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, CIAB.Models.AdminViewModel adminModel)
        {
            try
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
            catch (Exception ex)
            {
                base.Logger.Error(ex, "Update_{0} | StackTrace: {1}", ex.Message, ex.StackTrace);
            }
            return Json(new[] { adminModel }.ToDataSourceResult(request, ModelState));
        }
        public ActionResult Delete()
        {
            return View();
        }


    }
}