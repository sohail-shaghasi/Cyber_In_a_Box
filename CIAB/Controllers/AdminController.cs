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

        [HttpPost]
        public ActionResult test(string ddlValue)
        {
            return View();
        }

        public ViewResult AdminView([DataSourceRequest]DataSourceRequest request)
        {


            try
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
                    objAdminModel.ProductID = Convert.ToInt32(reader["ProductID"]);
                    objAdminModel.ProductName = reader["ProductName"].ToString();
                    objAdminModel.UserId = Convert.ToInt32(reader["UserID"]);


                    lstAdminModel.Add(objAdminModel);//add an object to the list of type object
                }


                var data = lstAdminModel.ToList();

                return View(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ActionResult AdminViewOrderStatus()
        {
            var result = new List<CIAB.Models.AdminOrderStatus>();
            result.Add(new CIAB.Models.AdminOrderStatus() { StatusValue = "Open", Code = 1 });
            result.Add(new CIAB.Models.AdminOrderStatus() { StatusValue = "Close", Code = 2 });
            result.Add(new CIAB.Models.AdminOrderStatus() { StatusValue = "In Progress", Code = 3 });

            return Json(result,JsonRequestBehavior.AllowGet);

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
                CIABcommand.Parameters.AddWithValue("@productID", adminModel.ProductID);
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
                throw ex;
            }

            return Json(new[] { adminModel }.ToDataSourceResult(request, ModelState));
        }


        public ActionResult Delete()
        {
            return View();
        }
    }
}