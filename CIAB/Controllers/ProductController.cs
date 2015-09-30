using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CIAB.Controllers
{
    public class ProductController : Controller
    {


     




        public ActionResult View1()
        {
            return View();
        }



        //
        // GET: /Product/
        public ActionResult Index()
        {
            //CIAB.Models.Products ciabProducts = new Models.Products();
            //var products = ciabProducts.DisplayProduct();
            //return View(products);

            return View();
        }


        //------------------------------------------------------------------


        public ActionResult CyberMart()
        {
            //CIAB.Models.Products ciabProducts = new Models.Products();
            //var products = ciabProducts.DisplayProduct();
            //return View(products.AsEnumerable());

            return View();
        }



        //------------------------------------------------------------------



        public ActionResult CyberSecurityHealthCheck()
        {
            return View();
        }


        //------------------------------------------------------------------


        public ActionResult vulnerabilityScan()
        {
            return View();
        }



        //------------------------------------------------------------------



        public ActionResult DefacementMonitoring()
        {
            return View();
        }


        //------------------------------------------------------------------




        public ActionResult EmailThreatPrevention()
        {

         

            return View();
        }



        //------------------------------------------------------------------



        //[HttpGet]
        //    public ActionResult GetProductList()
        //    {
        //        return View();
        //    }

        //[HttpPost]
        //    public ActionResult GetProductList(FormCollection formCollection)
        //    {

        //        CIAB.Models.Products products = new Models.Products();

        //        products.ProductName = formCollection["ProductName"];
        //        products.ProductDescription = formCollection["ProductDescription"];
        //        products.ProductQuantity = Convert.ToInt32(formCollection["ProductQuantity"]);
        //        products.ReorderLevel = Convert.ToInt32(formCollection["ReorderLevel"]);
        //        products.UnitPrice = Convert.ToDecimal(formCollection["UnitPrice"]);


        //        products.addProduct(products);
        //        //CIAB.DataAccessLayer.ProductBusinessLayer bLayer = new DataAccessLayer.ProductBusinessLayer();
        //        //bLayer.addProduct(products);

        //        Response.Write("uploaded successfully");




        //        return View();
        //    }




        //------------------------------------------------------------------



        [HttpGet]
        public ActionResult pricingContactUs()
        {
            return View();
        }




        //------------------------------------------------------------------



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> pricingContactUs(CIAB.Models.EmailFrom emailFrom)
        {
            string inputName, Subject, inputEmail, Message, optProduct;

            inputName = emailFrom.inputName;
            Subject = emailFrom.Subject;
            inputEmail = emailFrom.inputEmail;
            Message = emailFrom.Message;
            optProduct = emailFrom.optProduct;

            try
            {
                var body = "<p>Email From : {0}  </p> <p>Subject : {1} </p> <p>Name : {2} </p> <p>Product : {3} </p> <p>Message: {4} </p>";

                //SMTP parameters starts here
                string strReciever = System.Configuration.ConfigurationManager.AppSettings["smtpReciever"];
                string strSubject = System.Configuration.ConfigurationManager.AppSettings["smtpSubject"];
                string strSMTPUser = System.Configuration.ConfigurationManager.AppSettings["smtpUser"];
                string strSMPTpass = System.Configuration.ConfigurationManager.AppSettings["smtpPass"];
                string strSMPTHost = System.Configuration.ConfigurationManager.AppSettings["smtpServer"];
                int SMPTPort = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["smtpPort"]);
                string strEmailCC = System.Configuration.ConfigurationManager.AppSettings["smtpcc"];

                MailMessage message = new MailMessage();
                //Reciever of the email.
                if (strReciever.ToString() != string.Empty)
                {
                    foreach (string tos in strReciever.Split(';'))
                    {
                        MailAddress to = new MailAddress(tos);
                        message.To.Add(to);//sent to email address
                    }
                }


                //cc email 
                //if (strEmailCC.ToString() != string.Empty)
                //{
                //    foreach (string ccs in strEmailCC.Split(';'))
                //    {
                //        MailAddress cc = new MailAddress(ccs);
                //        message.CC.Add(cc);
                //    }
                //}




                message.From = new MailAddress(inputEmail);
                message.Subject = strSubject; //Subject;
                message.Body = string.Format(body, inputEmail, Subject, inputName, optProduct, Message);
                message.IsBodyHtml = true;


                // credebtials for smtp client account
                SmtpClient smtp = new SmtpClient();
                var credential = new NetworkCredential
                {
                    UserName = strSMTPUser,
                    Password = strSMPTpass
                };


                smtp.Credentials = credential;
                smtp.Host = strSMPTHost;
                smtp.Port = SMPTPort;
                smtp.EnableSsl = true;

                await smtp.SendMailAsync(message);
                return Redirect("https://cmasurvey2015.questionpro.com");

            }
            catch (Exception ex)
            {
                ViewData["smtpError"] = "Unable to send an email";
                return View();
            }

        }



        //[HttpGet]
        //public ActionResult ProductContactUs()
        //{
        //    return View();
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ProductContactUs(string fullName, string LastName, string JobTitle, string companyName, string inputEmail, string contactNumber)
        //{
        //    try
        //    {
        //        var body = "<p>Email From : {0}  </p> <p>Subject: {1}</p> <p>Name: {2} </p> <p>Contact No.: {3} </p> <p>Company Name: {4} </p>";

        //        //SMTP parameters starts here
        //        string strReciever = System.Configuration.ConfigurationManager.AppSettings["smtpReciever"];
        //        string strSubject = System.Configuration.ConfigurationManager.AppSettings["SubjectForHealthCeck"];
        //        string strSMTPUser = System.Configuration.ConfigurationManager.AppSettings["smtpUser"];
        //        string strSMPTpass = System.Configuration.ConfigurationManager.AppSettings["smtpPass"];
        //        string strSMPTHost = System.Configuration.ConfigurationManager.AppSettings["smtpServer"];
        //        int SMPTPort = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["smtpPort"]);
        //        string strEmailCC = System.Configuration.ConfigurationManager.AppSettings["smtpcc"];

        //        MailMessage message = new MailMessage();
        //        //Reciever of the email.
        //        if (strReciever.ToString() != string.Empty)
        //        {
        //            foreach (string tos in strReciever.Split(';'))
        //            {
        //                MailAddress to = new MailAddress(tos);
        //                message.To.Add(to);//sent to email address
        //            }
        //        }

        //        //cc email 
        //        //if (strEmailCC.ToString() != string.Empty)
        //        //{
        //        //    foreach (string ccs in strEmailCC.Split(';'))
        //        //    {
        //        //        MailAddress cc = new MailAddress(ccs);
        //        //        message.CC.Add(cc);
        //        //    }
        //        //}

        //        message.From = new MailAddress(inputEmail);
        //        message.Subject = strSubject; //Subject;
        //        message.Body = string.Format(body, inputEmail, strSubject, fullName + " " + LastName, contactNumber, companyName);
        //        message.IsBodyHtml = true;


        //        // credebtials for smtp client account
        //        SmtpClient smtp = new SmtpClient();
        //        var credential = new NetworkCredential
        //        {
        //            UserName = strSMTPUser,
        //            Password = strSMPTpass
        //        };


        //        smtp.Credentials = credential;
        //        smtp.Host = strSMPTHost;
        //        smtp.Port = SMPTPort;
        //        smtp.EnableSsl = true;

        //        await smtp.SendMailAsync(message);
        //        return Redirect("https://cmasurvey2015.questionpro.com");

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}





    }
}