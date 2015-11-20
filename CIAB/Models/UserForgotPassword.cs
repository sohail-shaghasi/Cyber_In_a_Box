using System;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
namespace CIAB.Models
{
    public class UserForgotPassword
    {
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
        public void SendPasswordResetEmail(string stremail, string strserName, string strUniqueID)
        {
            string strEmailFrom = ConfigurationManager.AppSettings["smtpEmailFrom"];
            string strSubject = ConfigurationManager.AppSettings["smtpSubject"];
            string strSMTPUser = ConfigurationManager.AppSettings["smtpUser"];
            string strSMPTpass = ConfigurationManager.AppSettings["smtpPass"];
            string strSMPTHost = ConfigurationManager.AppSettings["smtpServer"];
            int SMPTPort = Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"]);
            bool smtpEnableSSL = Convert.ToBoolean(ConfigurationManager.AppSettings["smtpEnableSSL"]);
            var url = new System.Web.Mvc.UrlHelper(System.Web.HttpContext.Current.Request.RequestContext);
            var absolutePath = url.Action("PasswordReset", "ForgotPassword", new { UniqueID = strUniqueID });
            var baseUrl1 = HttpContext.Current.Request.Url.Authority;
            using (MailMessage message = new MailMessage())
            {
                if (string.IsNullOrEmpty(stremail) == false)
                {
                    message.To.Add(stremail);
                }
                message.From = new MailAddress(strEmailFrom);
                message.Subject = strSubject; //Subject;
                StringBuilder SBEmailBody = new StringBuilder();
                SBEmailBody.Append("Dear " + strserName + ", <br/><br/>");
                SBEmailBody.Append("You (or someone else) have requested to reset your password at " + DateTime.Now);
                SBEmailBody.Append("<br/><br/>");
                SBEmailBody.Append("Please Click on the following link to reset Your password");
                SBEmailBody.Append("<br/><br/>");
                SBEmailBody.Append("http://" + baseUrl1 + absolutePath);
                SBEmailBody.Append("<br/><br/><br/><br/>");
                SBEmailBody.Append("KPMG Singapore");
                message.Body = SBEmailBody.ToString();
                message.IsBodyHtml = true;
                using (SmtpClient smtp = new SmtpClient())
                {
                    if (string.IsNullOrEmpty(strSMTPUser) == false && string.IsNullOrEmpty(strSMPTpass) == false)
                    {
                        var credential = new NetworkCredential
                        {
                            UserName = strSMTPUser,
                            Password = strSMPTpass
                        };
                        smtp.Credentials = credential;
                    }
                    smtp.Host = strSMPTHost;
                    if (string.IsNullOrEmpty(SMPTPort.ToString()) == false)
                    {
                        smtp.Port = SMPTPort;
                    }
                    if (smtpEnableSSL ==true)
                    {
                        smtp.EnableSsl = smtpEnableSSL;
                    }
                    smtp.Send(message);
                }
            }
        }
    }
}

