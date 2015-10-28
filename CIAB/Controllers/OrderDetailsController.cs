using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using GhostscriptSharp.Settings;
using CIAB.Models;


namespace CIAB.Controllers
{
    public class OrderDetailsController : BaseController
    {
        //
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult OrderDetail(string OrderNumber)
        {
            var orderDetailsPage = new OrderDetailsPage();
            try
            {

                SqlConnection CIABconnection = new SqlConnection(CIABconnectionString);
                SqlCommand CIABcommand = new SqlCommand();
                CIABconnection.Open();
                CIABcommand.CommandType = System.Data.CommandType.StoredProcedure;
                CIABcommand.Connection = CIABconnection;
                CIABcommand.CommandText = "sp_OrdersDetailPage";
                CIABcommand.Parameters.AddWithValue("@OrderID", OrderNumber);//pass order number to sql server
                SqlDataReader reader = CIABcommand.ExecuteReader();


                while (reader.Read())
                {
                    orderDetailsPage.OrderId = Convert.ToInt32(reader["OrderID"]);
                    orderDetailsPage.ProductName = reader["ProductName"].ToString();
                    orderDetailsPage.Email = reader["Email"].ToString();
                    orderDetailsPage.CompanyName = reader["CompanyName"].ToString();
                }
            }
            catch (Exception ex)
            {
                base.Logger.Error(ex, "OrderDetail_{0} | StackTrace: {1}", ex.Message, ex.StackTrace);
            }

            return View(orderDetailsPage);
        }

        [HttpPost]
        public ActionResult OrderReport1(HttpPostedFileBase OrderReportFile1, int? orderNumber)
        {
            var viewModel = TempData["OrderDetail"];
            try
            {
                string strFileName = "File1";
                var orderDetails = new Models.OrderDetailsPage();
                if (OrderReportFile1 != null && OrderReportFile1.ContentLength > 0)// The Name of the Upload component is "OrderReportFile"
                {
                    orderDetails.FileName = Path.GetFileName(OrderReportFile1.FileName);//Get the File Name.
                    orderDetails.FileExtension = Path.GetExtension(orderDetails.FileName);//Get the File Extension for Validation.

                    if (orderDetails.FileExtension == ".PDF" || orderDetails.FileExtension == ".pdf")
                    {
                        byte[] orderReportFileToUpload = new byte[OrderReportFile1.ContentLength]; //declare byte array

                        string strBase64String = Convert.ToBase64String(orderReportFileToUpload, 0, orderReportFileToUpload.Length);

                        OrderReportFile1.InputStream.Read(orderReportFileToUpload, 0, OrderReportFile1.ContentLength);

                        SqlConnection CIABconnection = new SqlConnection(CIABconnectionString);
                        SqlCommand CIABcommand = new SqlCommand();
                        CIABconnection.Open();
                        CIABcommand.CommandType = System.Data.CommandType.StoredProcedure;
                        CIABcommand.Connection = CIABconnection;
                        CIABcommand.CommandText = "sp_StoreOrderReports";
                        CIABcommand.Parameters.AddWithValue("@fileName", SqlDbType.NVarChar).Value = strFileName;
                        CIABcommand.Parameters.AddWithValue("@OrderID", SqlDbType.Int).Value = orderNumber;//pass orderNumber to sql server
                        CIABcommand.Parameters.AddWithValue("@Data", SqlDbType.VarBinary).Value = orderReportFileToUpload;
                        SqlDataReader reader = CIABcommand.ExecuteReader();

                        ViewBag.UploadMessageFile1 = "File uploaded successfully";
                        return View("OrderDetail", viewModel);
                    }

                }

                ViewBag.UploadMessageFile1 = "Please Select a .PDF File";
            }
            catch (Exception ex)
            {
                base.Logger.Error(ex, "OrderReport1_{0} | StackTrace: {1}", ex.Message, ex.StackTrace);
            }

            return View("OrderDetail", viewModel);
        }

        [HttpPost]
        public ActionResult OrderReport2(HttpPostedFileBase OrderReportFile2, int? orderNumber)
        {
            string strFileName = "File2";
            var viewModel = TempData["OrderDetail"];
            var orderDetails = new Models.OrderDetailsPage();
            try
            {

                if (OrderReportFile2 != null && OrderReportFile2.ContentLength > 0)// The Name of the Upload component is "OrderReportFile"
                {
                    orderDetails.FileName = Path.GetFileName(OrderReportFile2.FileName);//Get the File Name.
                    orderDetails.FileExtension = Path.GetExtension(orderDetails.FileName);//Get the File Extension for Validation.

                    if (orderDetails.FileExtension == ".PDF" || orderDetails.FileExtension == ".pdf")
                    {
                        byte[] orderReportFileToUpload = new byte[OrderReportFile2.ContentLength]; //declare byte array
                        OrderReportFile2.InputStream.Read(orderReportFileToUpload, 0, OrderReportFile2.ContentLength);

                        SqlConnection CIABconnection = new SqlConnection(CIABconnectionString);
                        SqlCommand CIABcommand = new SqlCommand();
                        CIABconnection.Open();
                        CIABcommand.CommandType = System.Data.CommandType.StoredProcedure;
                        CIABcommand.Connection = CIABconnection;
                        CIABcommand.CommandText = "sp_StoreOrderReports";
                        CIABcommand.Parameters.AddWithValue("@fileName", SqlDbType.NVarChar).Value = strFileName;
                        CIABcommand.Parameters.AddWithValue("@OrderID", SqlDbType.Int).Value = orderNumber;//pass orderNumber to sql server
                        CIABcommand.Parameters.AddWithValue("@Data", SqlDbType.VarBinary).Value = orderReportFileToUpload;
                        SqlDataReader reader = CIABcommand.ExecuteReader();


                        ViewBag.UploadMessageFile2 = "File uploaded successfully";
                        return View("OrderDetail", viewModel);
                    }

                }
                ViewBag.UploadMessageFile2 = "Please Select a .PDF File";
            }

            catch (Exception ex)
            {
                base.Logger.Error(ex, "OrderReport2_{0} | StackTrace: {1}", ex.Message, ex.StackTrace);
            }
            return View("OrderDetail", viewModel);
        }

        [HttpPost]
        public ActionResult OrderReport3(HttpPostedFileBase OrderReportFile3, int? orderNumber)
        {
            string strFileName = "File3";
            var viewModel = TempData["OrderDetail"];
            var orderDetails = new OrderDetailsPage();

            try
            {
                if (OrderReportFile3 != null && OrderReportFile3.ContentLength > 0)// The Name of the Upload component is "OrderReportFile"
                {
                    orderDetails.FileName = Path.GetFileName(OrderReportFile3.FileName);//Get the File Name.
                    orderDetails.FileExtension = Path.GetExtension(orderDetails.FileName);//Get the File Extension for Validation.

                    if (orderDetails.FileExtension == ".PDF" || orderDetails.FileExtension == ".pdf")
                    {
                        byte[] orderReportFileToUpload = new byte[OrderReportFile3.ContentLength]; //declare byte array
                        OrderReportFile3.InputStream.Read(orderReportFileToUpload, 0, OrderReportFile3.ContentLength);

                        SqlConnection CIABconnection = new SqlConnection(CIABconnectionString);
                        SqlCommand CIABcommand = new SqlCommand();
                        CIABconnection.Open();
                        CIABcommand.CommandType = System.Data.CommandType.StoredProcedure;
                        CIABcommand.Connection = CIABconnection;
                        CIABcommand.CommandText = "sp_StoreOrderReports";
                        CIABcommand.Parameters.AddWithValue("@fileName", SqlDbType.NVarChar).Value = strFileName;
                        CIABcommand.Parameters.AddWithValue("@OrderID", SqlDbType.Int).Value = orderNumber;//pass orderNumber to sql server
                        CIABcommand.Parameters.AddWithValue("@Data", SqlDbType.VarBinary).Value = orderReportFileToUpload;
                        SqlDataReader reader = CIABcommand.ExecuteReader();

                        ViewBag.UploadMessageFile3 = "File uploaded successfully";
                        return View("OrderDetail", viewModel);
                    }

                }
                ViewBag.UploadMessageFile3 = "Please Select a .PDF File";
            }

            catch (Exception ex)
            {
                base.Logger.Error(ex, "OrderReport3_{0} | StackTrace: {1}", ex.Message, ex.StackTrace);
            }
            return View("OrderDetail", viewModel);
        }



        //This function is to read the binary Record from DB and return to The View.
        public FileResult ShowDocument1(int? orderNumber, string FileName)
        {
            try
            {
                SqlConnection CIABconnection = new SqlConnection(CIABconnectionString);
                SqlCommand CIABcommand = new SqlCommand();
                CIABconnection.Open();
                CIABcommand.CommandType = System.Data.CommandType.StoredProcedure;
                CIABcommand.Connection = CIABconnection;
                CIABcommand.CommandText = "sp_ReadOrderReports";
                CIABcommand.Parameters.AddWithValue("@OrderId", SqlDbType.Int).Value = orderNumber;//pass orderNumber to sql server
                CIABcommand.Parameters.AddWithValue("@File", SqlDbType.VarBinary).Value = FileName;
                SqlDataReader reader = CIABcommand.ExecuteReader();

                byte[] pdfBinaryArray = null;

                while (reader.Read())
                {
                    if (reader["File1"] != DBNull.Value)
                        pdfBinaryArray = (byte[])reader["File1"];//assign to variable byte array.
                }

                if (pdfBinaryArray != null)

                    return File(pdfBinaryArray, "application/pdf");
            }
            catch (Exception ex)
            {
                base.Logger.Error(ex, "ShowDocument1_{0} | StackTrace: {1}", ex.Message, ex.StackTrace);
            }
            return null;
        }

        public FileResult ShowDocument2(int? orderNumber, string FileName)
        {
            try
            {
                SqlConnection CIABconnection = new SqlConnection(CIABconnectionString);
                SqlCommand CIABcommand = new SqlCommand();
                CIABconnection.Open();
                CIABcommand.CommandType = System.Data.CommandType.StoredProcedure;
                CIABcommand.Connection = CIABconnection;
                CIABcommand.CommandText = "sp_ReadOrderReports";
                CIABcommand.Parameters.AddWithValue("@OrderId", SqlDbType.Int).Value = orderNumber;//pass orderNumber to sql server
                CIABcommand.Parameters.AddWithValue("@File", SqlDbType.VarBinary).Value = FileName;
                SqlDataReader reader = CIABcommand.ExecuteReader();

                byte[] pdfBinaryArray = null;

                while (reader.Read())
                {
                    if (reader["File2"] != DBNull.Value)
                        pdfBinaryArray = (byte[])reader["File2"];//assign to variable byte array.
                }

                if (pdfBinaryArray != null)

                    return File(pdfBinaryArray, "application/pdf");
            }
            catch (Exception ex)
            {
                base.Logger.Error(ex, "ShowDocument2_{0} | StackTrace: {1}", ex.Message, ex.StackTrace);
            }
            return null;
        }

        public FileResult ShowDocument3(int? orderNumber, string FileName)
        {
            try
            {
                SqlConnection CIABconnection = new SqlConnection(CIABconnectionString);
                SqlCommand CIABcommand = new SqlCommand();
                CIABconnection.Open();
                CIABcommand.CommandType = System.Data.CommandType.StoredProcedure;
                CIABcommand.Connection = CIABconnection;
                CIABcommand.CommandText = "sp_ReadOrderReports";
                CIABcommand.Parameters.AddWithValue("@OrderId", SqlDbType.Int).Value = orderNumber;//pass orderNumber to sql server
                CIABcommand.Parameters.AddWithValue("@File", SqlDbType.VarBinary).Value = FileName;
                SqlDataReader reader = CIABcommand.ExecuteReader();

                byte[] pdfBinaryArray = null;

                while (reader.Read())
                {
                    if (reader["File3"] != DBNull.Value)
                        pdfBinaryArray = (byte[])reader["File3"];//assign to variable byte array.
                }

                if (pdfBinaryArray != null)

                    return File(pdfBinaryArray, "application/pdf");
            }
            catch (Exception ex)
            {
                base.Logger.Error(ex, "RequestPasswordReset_{0} | StackTrace: {1}", ex.Message, ex.StackTrace);
            }
            return null;
        }

    }
}
