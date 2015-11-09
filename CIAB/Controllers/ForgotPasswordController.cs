using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using CIAB.Models;
using System.Text;
using System.Security.Cryptography;
using CIAB.DataLayer;

namespace CIAB.Controllers
{
    public class ForgotPasswordController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult RequestPasswordReset()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RequestPasswordReset(CIAB.Models.UserForgotPassword forgotPassword)
        {
            try
            {
                //if model validation fails return to page.
                if (!ModelState.IsValid)
                {
                    return View(forgotPassword);
                }
                using(SqlConnection CIABconnection = new SqlConnection(CIABconnectionString))
                {
                    using (SqlCommand CIABcommand = new SqlCommand())
                    {
                        CIABconnection.Open();
                        CIABcommand.CommandType = System.Data.CommandType.StoredProcedure;
                        CIABcommand.Connection = CIABconnection;
                        CIABcommand.CommandText = "sp_ForgotPassword";
                        CIABcommand.Parameters.AddWithValue("@email", forgotPassword.email);
                        SqlDataReader reader = CIABcommand.ExecuteReader();
                        while (reader.Read())
                        {
                            if (Convert.ToBoolean(reader["ReturnCode"]))
                            {
                                forgotPassword.UniqueID = reader["UniqueID"].ToString();
                                forgotPassword.EmailFromDB = reader["Email"].ToString();
                                forgotPassword.UserName = reader["UserName"].ToString();
                                //call to function SendPasswordResetEmail
                                CIAB.Models.UserForgotPassword ModelForgotPassword = new Models.UserForgotPassword();
                                ModelForgotPassword.SendPasswordResetEmail(forgotPassword.EmailFromDB, forgotPassword.UserName, forgotPassword.UniqueID);
                                TempData["ResetMessage"] = "You will receive an email with the password reset link. Follow the instruction in the email to reset your password.";
                                return RedirectToAction("GetMessages", "ForgotPassword");
                            }
                        }
                    }
                }
                TempData["ResetMessage"] = "You will receive an email with the password reset link. Follow the instruction in the email to reset your password.";
                return RedirectToAction("GetMessages", "ForgotPassword");
            }
            catch (Exception ex)
            {
                base.Logger.Error(ex, "RequestPasswordReset_{0} | StackTrace: {1}", ex.Message, ex.StackTrace);
                return RedirectToAction("GetMessages", "ForgotPassword");
            }
        }
        [HttpGet]
        public ActionResult PasswordReset(string UniqueID)
        {
            try
            {
                ForgotPasswordDataLayer forgotPasswordDataLayer = new ForgotPasswordDataLayer();
                var result = forgotPasswordDataLayer.IsPasswordResetLinkValid(UniqueID);
                if (!result)
                {
                    TempData["PasswordLinkExpired"] = "Forgot Password link is invalid!";
                    return RedirectToAction("GetMessages", "ForgotPassword");
                }
            }
            catch (Exception ex)
            {
                base.Logger.Error(ex, "PasswordReset_{0} | StackTrace: {1}", ex.Message, ex.StackTrace);
            }
            return View();
        }
        [HttpPost]
        public ActionResult PasswordReset(UserForgotPassword forgotPassword, string UniqueID)
        {
            try
            {
                ForgotPasswordDataLayer forgotPasswordDataLayer = new ForgotPasswordDataLayer();
                var result =forgotPasswordDataLayer.GetPasswordReset(forgotPassword, UniqueID);
                if (result)
                {
                    TempData["PasswordChangedConfirmation"] = "Your Password has been changed, please login using";
                    return RedirectToAction("GetMessages", "ForgotPassword");
                }
            }
            catch (Exception ex)
            {
                base.Logger.Error(ex, "PasswordReset_{0} | StackTrace: {1}", ex.Message, ex.StackTrace);
            }
            return View();
        }
        public ActionResult GetMessages()
        {
            return View();
        }
    }
}