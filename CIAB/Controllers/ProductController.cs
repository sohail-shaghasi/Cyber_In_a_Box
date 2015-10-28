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
    public class ProductController : BaseController
    {

        public ActionResult Index()
        {
                return View();
        }

        public ActionResult Marketplace()
        {
            return View();
        }
        public ActionResult CyberSecurityHealthCheck()
        {
            return View();
        }

        public ActionResult vulnerabilityScan()
        {
            return View();
        }


        public ActionResult DefacementMonitoring()
        {
            return View();
        }

        public ActionResult EmailThreatPrevention()
        {


            return View();
        }



        [HttpGet]
        public ActionResult pricingContactUs()
        {
            return View();
        }




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
                base.Logger.Error(ex, "pricingContactUs_{0} | StackTrace: {1}", ex.Message, ex.StackTrace);
                return View();
            }

        }

    }
}