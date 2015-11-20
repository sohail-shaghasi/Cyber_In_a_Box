using System;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;
using CIAB.Models;
using CIAB.DataLayer;

namespace CIAB.Controllers
{
    public class ChangePasswordController : BaseController
    {
        #region Methods
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ChangeUserPassword()
        {
            if (Convert.ToString(Session["UserName"]).ToLower() == null || Convert.ToString(Session["UserName"]).ToLower() == string.Empty)
            {
                return RedirectToAction("SignUpLogin", "Home");
            }

            return View();
        }
        [HttpPost]
        public ActionResult ChangeUserPassword(ResetPassword ResetPass)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string strCurrentPass = GetSHA1(ResetPass.CurrentPass + "d3katk00");
                    string strNewPass = GetSHA1(ResetPass.NewPass + "d3katk00");
                    string strOldPassword = "";
                    if (HttpContext.Session["Password"] != null)
                    {
                        strOldPassword = HttpContext.Session["Password"].ToString();
                        if (strCurrentPass != strOldPassword)
                        {
                            ViewBag.ErrorMessage = "Current Password is not Valid!";
                            return View("ChangeUserPassword");
                        }
                        else if (strOldPassword == strNewPass)
                        {
                            ViewBag.Error = "New password cannot be the same as old password";
                            return View("ChangeUserPassword");
                        }
                    }
                    //check if old password and new hashed password resembles.
                    int returnValue = AddNewPassword(ResetPass, strCurrentPass, strNewPass);
                    if (returnValue == 1)
                    {
                        return RedirectToAction("PasswordResetSuccess");
                    }
                    else
                    {
                        return View(ResetPass);
                    }
                }
                else
                {
                    ModelState.AddModelError("resetpass", "Process failed.");
                    return View("ChangeUserPassword", ResetPass);
                }
            }
            catch (Exception ex)
            {
                base.Logger.Error(ex, "ChangeUserPassword_{0} | StackTrace: {1}", ex.Message, ex.StackTrace);
                ModelState.AddModelError("resetpass", "Process failed.");
                return View("ChangeUserPassword", ResetPass);
            }
        }
        public ViewResult PasswordResetSuccess()
        {
            return View();
        }
        private int AddNewPassword(ResetPassword ResetPass, string strCurrentPass, string strNewPass)
        {
            if (HttpContext.Session["UserName"] != null)
            {
                ResetPass.UserName = HttpContext.Session["UserName"].ToString();
            }
            ChangePasswordDataLayer changepasswordDataLayer = new ChangePasswordDataLayer();
            var result = changepasswordDataLayer.GetNewPassword(ResetPass, strCurrentPass, strNewPass);

            return (result);
        }
        private string GetSHA1(string password)
        {
            SHA1CryptoServiceProvider sha1Provider = new SHA1CryptoServiceProvider();
            sha1Provider.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password));
            byte[] hashArray = sha1Provider.Hash;
            StringBuilder SB_Hexadecimal = new StringBuilder();
            foreach (byte passByte in hashArray)
            {
                SB_Hexadecimal.AppendFormat("{0:x2}", passByte);
            }
            return SB_Hexadecimal.ToString();
        }
        #endregion
    }
}