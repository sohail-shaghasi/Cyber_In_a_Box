using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;
using CIAB.Models;
using System.Security.Cryptography;
using System.Text;
namespace CIAB.Controllers
{
    public class ValidationController : BaseController
    {
        #region Methods
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult IsUserNameAvailable(User RegisterUserName)
        {
            string name = Request["registerViewModel.RegisterEmail"];
            try
            {
                SqlConnection con = new SqlConnection(CIABconnectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_CheckUserName";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@UserName", RegisterUserName.registerViewModel.RegisterUserName);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if ((int)reader["ReturnCode"] == 1)
                    {
                        return Json(string.Format("{0} is in Use", RegisterUserName.registerViewModel.RegisterUserName), JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                base.Logger.Error(ex, "IsUserNameAvailable_{0} | StackTrace: {1}", ex.Message, ex.StackTrace);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult IsEmailAvailable(User RegisterEmail)
        {
            try
            {
                SqlConnection con = new SqlConnection(CIABconnectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_CheckRegisterEmailAddress";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Email", RegisterEmail.registerViewModel.RegisterEmail);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if ((int)reader["ReturnCode"] == 1)
                    {
                        return Json(string.Format("{0} is in Use", RegisterEmail.registerViewModel.RegisterEmail), JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                base.Logger.Error(ex, "IsEmailAvailable_{0} | StackTrace: {1}", ex.Message, ex.StackTrace);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult IsCurrentPasswordAvailable(string CurrentPass)
        {
            string CurrentPassWithSalt = CurrentPass + "d3katk00";
            StringBuilder SB_Hexadecimal = new StringBuilder();
            var result = false;
            string strOldPassword = "";
            try
            {
                SHA1CryptoServiceProvider sha1Provider = new SHA1CryptoServiceProvider();
                sha1Provider.ComputeHash(ASCIIEncoding.ASCII.GetBytes(CurrentPassWithSalt));
                byte[] hashArray = sha1Provider.Hash;
                foreach (byte passByte in hashArray)
                {
                    SB_Hexadecimal.AppendFormat("{0:x2}", passByte);
                }
                if (HttpContext.Session["Password"] != null)
                {
                    strOldPassword = HttpContext.Session["Password"].ToString();
                }
                if (SB_Hexadecimal.ToString() == strOldPassword)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                base.Logger.Error(ex, "IsCurrentPasswordAvailable_{0} | StackTrace: {1}", ex.Message, ex.StackTrace);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}