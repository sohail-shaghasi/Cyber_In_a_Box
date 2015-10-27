using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Text;


namespace CIAB.Models
{
    public class UserForgotPassword
    {

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string email { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string EmailFromDB { get; set; }

        public string UniqueID { get; set; }

       
        
        [StringLength(10, MinimumLength = 5, ErrorMessage = "5 characters minimum, no spaces, no special characters")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "The Password and Confirm Password do not match")]//to compare the password fields.
        public string ConfirmPassword { get; set; }


        string CIABconnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CIABConnectionString"].ConnectionString;
        public void SendPasswordResetEmail(string stremail, string strserName, string strUniqueID)
        {
            string strPath = HttpContext.Current.Request.Url.Authority;


            try
            {

                //SMTP parameters starts here
                string strEmailFrom = System.Configuration.ConfigurationManager.AppSettings["smtpEmailFrom"];
                string strSubject = System.Configuration.ConfigurationManager.AppSettings["smtpSubject"];
                string strSMTPUser = System.Configuration.ConfigurationManager.AppSettings["smtpUser"];
                string strSMPTpass = System.Configuration.ConfigurationManager.AppSettings["smtpPass"];
                string strSMPTHost = System.Configuration.ConfigurationManager.AppSettings["smtpServer"];
                int SMPTPort = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["smtpPort"]);
               

                MailMessage message = new MailMessage();
                //Reciever of the email.
                if (stremail.ToString() != string.Empty || stremail.ToString() != null)
                {
                    foreach (string tos in stremail.Split(';'))
                    {
                        MailAddress to = new MailAddress(tos);
                        message.To.Add(to);//sent to email address
                    }
                }
                message.From = new MailAddress(strEmailFrom);
                message.Subject = strSubject; //Subject;
                StringBuilder SBEmailBody = new StringBuilder();
                SBEmailBody.Append("Dear " + strserName + ", <br/><br/>");
                SBEmailBody.Append("You (or someone else) have requested to reset your password at " + DateTime.Now);
                SBEmailBody.Append("<br/><br/>");
                SBEmailBody.Append("Please Click on the following link to reset Your password");
                SBEmailBody.Append("<br/><br/>");
                SBEmailBody.Append("http://" + strPath + "/ForgotPassword/PasswordReset?UniqueID=" + strUniqueID);
                SBEmailBody.Append("<br/><br/><br/><br/>");
                SBEmailBody.Append("KPMG Singapore");

                message.Body = SBEmailBody.ToString();
                // message.Body = string.Format(body, inputEmail, Subject, inputName, optProduct, Message);
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
                smtp.Send(message);
            }
            catch
            {
            }
        }
    
    }
}