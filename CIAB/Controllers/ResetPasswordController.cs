using System;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Mvc;
using CIAB.Models;
using CIAB.DataLayer;
namespace CIAB.Controllers
{
    public class ResetPasswordController : BaseController
    {
        #region Methods
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult PasswordResetPage(string UniqueID)
        {
            if (Convert.ToString(Session["UserName"]).ToLower() == null || Convert.ToString(Session["UserName"]).ToLower() == string.Empty)
            {
                return RedirectToAction("SignUpLogin", "Home");
            }
            var resetPasswordDL = new ResetPasswordDataLayer();
            try
            {
                bool result = resetPasswordDL.GetPasswordResetPage(UniqueID);
                if (!result)
                {
                    TempData["PasswordLinkExpired"] = "Forgot Password link is invalid!";
                    return RedirectToAction("GetMessages", "ResetPassword");
                }
            }
            catch (Exception ex)
            {
                base.Logger.Error(ex, "PasswordResetPage_{0} | StackTrace: {1}", ex.Message, ex.StackTrace);
                return View();
            }
            return View();
        }
        [HttpPost]
        public ActionResult PasswordResetPage(ChangePasswordFromUserDetailsPage changePassword, string UniqueID)
        {
            var resetPasswordDL = new ResetPasswordDataLayer();
            try
            {
                bool result = resetPasswordDL.ChangeUserPassword(changePassword, UniqueID);
                if (result)
                {
                    TempData["PasswordChangedConfirmation"] = "Your Password has been changed, please login using";
                    return RedirectToAction("GetMessages", "ResetPassword");
                }
            }
            catch (Exception ex)
            {
                base.Logger.Error(ex, "PasswordResetRequestPage_{0} | StackTrace: {1}", ex.Message, ex.StackTrace);
                return View();
            }
            return View();
        }
        public ActionResult GetMessages()
        {
            return View();
        }
        private void SendPasswordFromUserDetailsPage(string strEmailFromDB, string strUserName, string strUniqueID)
        {
            //get the controller and action method
            var url = new UrlHelper(Request.RequestContext);
            var absolutePath = url.Action("PasswordResetPage", "ResetPassword", new { UniqueID = strUniqueID });
            var baseUrl1 = Request.Url.Authority;
            string strEmailFrom = System.Configuration.ConfigurationManager.AppSettings["smtpEmailFrom"];
            string strSubject = System.Configuration.ConfigurationManager.AppSettings["smtpSubject"];
            string strSMTPUser = System.Configuration.ConfigurationManager.AppSettings["smtpUser"];
            string strSMPTpass = System.Configuration.ConfigurationManager.AppSettings["smtpPass"];
            string strSMPTHost = System.Configuration.ConfigurationManager.AppSettings["smtpServer"];
            int SMPTPort = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["smtpPort"]);
            if (strEmailFrom != null || strEmailFrom != string.Empty || strSubject != string.Empty || strSubject != null
                || strSMPTpass != null || strSMPTpass != string.Empty || strSMPTHost != string.Empty || strSMPTHost != null || strSMPTHost != string.Empty)
            {
                MailMessage message = new MailMessage();
                if (strEmailFromDB.ToString() != string.Empty || strEmailFromDB.ToString() != null)
                {
                    foreach (string tos in strEmailFromDB.Split(';'))
                    {
                        MailAddress to = new MailAddress(tos);
                        message.To.Add(to);//sent to email address
                    }
                }
                message.From = new MailAddress(strEmailFrom);
                message.Subject = strSubject; //Subject;
                StringBuilder SBEmailBody = new StringBuilder();
                SBEmailBody.Append("Dear " + strUserName + ", <br/><br/>");
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
                    if (string.IsNullOrEmpty(strSMTPUser) == false || string.IsNullOrEmpty(strSMPTpass) == false)
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
                    smtp.EnableSsl = true;
                    smtp.Send(message);
                }
            }
        }
        [HttpGet]
        public void PasswordResetFromUserListingPage(string Email, string UserName)
        {
            string strUniqueID = string.Empty;
            string strEmailFromDB = string.Empty;
            string strUserName = string.Empty;
            try
            {
                SqlConnection CIABconnection = new SqlConnection(CIABconnectionString);
                SqlCommand CIABcommand = new SqlCommand();
                CIABconnection.Open();
                CIABcommand.CommandType = System.Data.CommandType.StoredProcedure;
                CIABcommand.Connection = CIABconnection;
                CIABcommand.CommandText = "sp_ForgotPassword";
                CIABcommand.Parameters.AddWithValue("@email", Email);
                SqlDataReader reader = CIABcommand.ExecuteReader();
                while (reader.Read())
                {
                    if (Convert.ToBoolean(reader["ReturnCode"]))
                    {
                        strUniqueID = reader["UniqueID"].ToString();
                        strEmailFromDB = reader["Email"].ToString();
                        strUserName = reader["UserName"].ToString();
                        SendPasswordFromUserDetailsPage(strEmailFromDB, strUserName, strUniqueID);
                    }
                }
            }
            catch (Exception ex)
            {
                base.Logger.Error(ex, "PasswordResetFromUserListingPage_{0} | StackTrace: {1}", ex.Message, ex.StackTrace);
            }
        }
        #endregion
    }
}
