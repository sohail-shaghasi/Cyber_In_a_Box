using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CIAB.Models;
using System.Security.Cryptography;
using System.Text;


namespace CIAB.Controllers
{
    public class ValidationController : BaseController
    {
        //
        public ActionResult Index()
        {
            return View();
        }


        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult IsUserNameAvailable(string UserName)
        {

            try
            {

                SqlConnection con = new SqlConnection(CIABconnectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_CheckUserName";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@UserName", UserName);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if ((int)reader["ReturnCode"] == 1)
                    {
                        return Json(string.Format("{0} is in Use", UserName), JsonRequestBehavior.AllowGet);

                    }
                }
            }
            catch (Exception ex)
            {
                base.Logger.Error(ex, "IsUserNameAvailable_{0} | StackTrace: {1}", ex.Message, ex.StackTrace);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult IsEmailAvailable(string RegisterationEmail)
        {

            try
            {

                SqlConnection con = new SqlConnection(CIABconnectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_CheckRegisterEmailAddress";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Email", RegisterationEmail);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if ((int)reader["ReturnCode"] == 1)
                    {
                        return Json(string.Format("{0} is in Use", RegisterationEmail), JsonRequestBehavior.AllowGet);

                    }
                }
            }
            catch (Exception ex)
            {
                base.Logger.Error(ex, "IsEmailAvailable_{0} | StackTrace: {1}", ex.Message, ex.StackTrace);
            }


            return Json(true, JsonRequestBehavior.AllowGet);
        }




        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]

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

                //check if Password session is empty.
                if (HttpContext.Session["Password"] != null)
                {
                    strOldPassword = HttpContext.Session["Password"].ToString();
                }

                //check if Old Password is same as the current password.
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

    }

}