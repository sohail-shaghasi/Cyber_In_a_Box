using CIAB.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace CIAB.Controllers
{
    public class OrderController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DispalyOrder(string ProductHandle)
        {
            Session["productHandle"] = ProductHandle;


            if (Convert.ToString(Session["UserName"]).ToLower() == null || Convert.ToString(Session["UserName"]).ToLower() == string.Empty)
            {
                return RedirectToAction("SignUpLogin", "Home");
            }

            ConfirmOrderCustomer confirmOrderCustomer = UserDetails();

            try
            {
                switch (ProductHandle)
                {
                    case "CIABWD":
                        return View("OrderDefacementMonitoring", confirmOrderCustomer);
                    case "CIABETP":
                        return View("OrderEmailThreatPrevention", confirmOrderCustomer);
                    case "CIABVS":
                        return View("OrderVulnerabilityScan", confirmOrderCustomer);
                    case "CIABCSC":
                        return View("OrderCyberSecurityHealthCheck", confirmOrderCustomer);
                    default: 
                        return RedirectToAction("index", "Home");
                }
            }
            catch (Exception ex)
            {
                base.Logger.Error(ex, "DispalyOrder_{0} | StackTrace: {1}", ex.Message, ex.StackTrace);
            }
            return RedirectToAction("index", "Home");
        }
        public ActionResult CreateOrder()
        {
            try
            {
                var productHandle = Convert.ToString(Session["productHandle"]);

                if (productHandle == "CIABWD")
                {
                    // ViewData["Product"] = CustomerOrderConfirmation;
                    Session["ProductName"] = "WebSite Defacement Monitoring Starting from: $333/Month*";
                    Session["ProductDuration"] = "*Min 6 Months";

                    return RedirectToAction("DisplayThankYou", "Order");
                }


                else if (productHandle == "CIABETP")
                {
                    Session["ProductName"] = "Email Threat Prevention Starting from: $583/Month*";
                    Session["ProductDuration"] = "*Min 12 Months";

                    return RedirectToAction("DisplayThankYou", "Order");
                }


                else if (productHandle == "CIABVS")
                {
                    Session["ProductName"] = "Vulnerability Scan Starting from: $599 Per Scan*";
                    Session["ProductDuration"] = "*Blocks of 10 IP Addresses";

                    return RedirectToAction("DisplayThankYou", "Order");
                }



                else if (productHandle == "CIABCSC")
                {
                    Session["ProductName"] = "Cybersecurity Health Check*";
                    Session["ProductDuration"] = "*Starting from: $599 for detailed report";

                    return RedirectToAction("DisplayThankYou", "Order");
                }

            }
            catch (Exception ex)
            {
                base.Logger.Error(ex, "CreateOrder_{0} | StackTrace: {1}", ex.Message, ex.StackTrace);
            }
            return View();
        }
        public ActionResult DisplayThankYou()
        {
            try
            {
                string strProductHandle = string.Empty;
                if (Session["productHandle"] != null)
                {
                    strProductHandle = Session["productHandle"].ToString();
                }

                int userIDFromSession = Convert.ToInt32(System.Web.HttpContext.Current.Session["UserId"]);

                ConfirmOrderCustomer CustomerOrderConfirmation = UserDetails();


                SqlConnection CIABconnection = new SqlConnection(CIABconnectionString);
                SqlCommand CIABcommand = new SqlCommand();
                CIABconnection.Open();
                CIABcommand.CommandType = CommandType.StoredProcedure;
                CIABcommand.Connection = CIABconnection;
                CIABcommand.CommandText = "sp_CreateOrder";
                CIABcommand.Parameters.AddWithValue("@UserID", userIDFromSession);
                CIABcommand.Parameters.AddWithValue("@parameter", strProductHandle);
                CIABcommand.ExecuteNonQuery();

                SendOrderConfirmationEmail();//send email confirmation to customer

                return View(CustomerOrderConfirmation);
            }
            catch (Exception ex)
            {
                base.Logger.Error(ex, "DisplayThankYou_{0} | StackTrace: {1}", ex.Message, ex.StackTrace);
                return View(new ConfirmOrderCustomer());
            }
        }
        private void SendOrderConfirmationEmail()//This function sends email for each placed orders
        {
            try
            {

                var body = "<p>Email From : {0} ";

                string strReciever = HttpContext.Session["email"].ToString();
                string strEmailFrom = System.Configuration.ConfigurationManager.AppSettings["smtpEmailFrom"];
                string strSubject = "Great, you just confirm your purchase ";
                string strSMTPUser = System.Configuration.ConfigurationManager.AppSettings["smtpUser"];
                string strSMPTpass = System.Configuration.ConfigurationManager.AppSettings["smtpPass"];
                string strSMPTHost = System.Configuration.ConfigurationManager.AppSettings["smtpServer"];
                int SMPTPort = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["smtpPort"]);
                //string strEmailCC = System.Configuration.ConfigurationManager.AppSettings["smtpcc"];



                MailMessage message = new MailMessage();
                MailAddress to = new MailAddress(strReciever);
                message.To.Add(to);//sent to email address


                message.From = new MailAddress(strEmailFrom);
                message.Subject = strSubject; //Subject;
                message.Body = string.Format(body, strEmailFrom);
                message.IsBodyHtml = true;




                SmtpClient smtp = new SmtpClient();
                var credential = new NetworkCredential //credebtials for smtp client account
                {
                    UserName = strSMTPUser,
                    Password = strSMPTpass
                };


                smtp.Credentials = credential;
                smtp.Host = strSMPTHost;
                smtp.Port = SMPTPort;
                smtp.EnableSsl = true;
                smtp.Send(message);

            }
            catch (Exception ex)
            {
                ViewData["smtpError"] = "Unable to send an email";
                base.Logger.Error(ex, "SendOrderConfirmationEmail_{0} | StackTrace: {1}", ex.Message, ex.StackTrace);
            }
        }
        private ConfirmOrderCustomer UserDetails()//This Function returns User information from User Table.
        {
            var UserIDFromSession = "";
            try
            {
                SqlConnection CIABconnection = new SqlConnection(CIABconnectionString);
                SqlCommand CIABcommand = new SqlCommand();
                CIABconnection.Open();

                CIAB.Models.ConfirmOrderCustomer CustomerOrderConfirmation = new Models.ConfirmOrderCustomer();

                CIABcommand.CommandType = CommandType.StoredProcedure;
                CIABcommand.Connection = CIABconnection;
                CIABcommand.CommandText = "sp_UserDetails";



                if (Session["UserId"] != null)
                {
                    UserIDFromSession = Session["UserId"].ToString();
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
            catch (Exception ex)
            {
                base.Logger.Error(ex, "UserDetails_{0} | StackTrace: {1}", ex.Message, ex.StackTrace);
                return new ConfirmOrderCustomer();
            }

        }

    }
}