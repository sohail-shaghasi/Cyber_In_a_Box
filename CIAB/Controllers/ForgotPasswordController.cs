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

namespace CIAB.Controllers
{
    public class ForgotPasswordController : Controller
    {
        string CIABconnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CIABConnectionString"].ConnectionString;

        // GET: /ForgotPassword/
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
            //if model validation fails return to page.
            if (!ModelState.IsValid)
            {
                return View(forgotPassword);
            }

            SqlConnection CIABconnection = new SqlConnection(CIABconnectionString);
            SqlCommand CIABcommand = new SqlCommand();
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
                    //ViewBag.EmailSentMessage = "If a user account with that Email address exists, we will have sent a password reset link";

                    return View("ResetMessage");
                }

            }
            return View("ResetMessage");
        }



        [HttpGet]
        public ActionResult PasswordReset(string UniqueID)
        {
            bool result;
            //call the store proc to check if the GUID is valid and still exists in the table.
            SqlConnection CIABconnection = new SqlConnection(CIABconnectionString);
            SqlCommand CIABcommand = new SqlCommand();
            CIABconnection.Open();
            CIABcommand.CommandType = System.Data.CommandType.StoredProcedure;
            CIABcommand.Connection = CIABconnection;
            CIABcommand.CommandText = "[dbo].[sp_IsPasswordResetLinkValid]";
            CIABcommand.Parameters.AddWithValue("@GUID", UniqueID);//pass a parameter to the stored proc
            result =Convert.ToBoolean(CIABcommand.ExecuteScalar());
            
            if (!result)
            {
                return RedirectToAction("PasswordLinkExpired", "ForgotPassword");
            }
            
         


            return View();
        }


        [HttpPost]
        public ActionResult PasswordReset(UserForgotPassword forgotPassword, string UniqueID)
        {

            if (!ModelState.IsValid)
            {
                return View(forgotPassword);
            }

            string HashPassword;
            bool result;
            //call the store proc to check if the GUID is valid and still exists in the table.
            SqlConnection CIABconnection = new SqlConnection(CIABconnectionString);
            SqlCommand CIABcommand = new SqlCommand();
            CIABconnection.Open();
            CIABcommand.CommandType = System.Data.CommandType.StoredProcedure;
            CIABcommand.Connection = CIABconnection;
            CIABcommand.CommandText = "sp_ChangePassword";

            //apply hash and salt
            Encoding encoder = new UTF8Encoding();
            SHA1 sha = new SHA1Managed();
            byte[] passwordHash = sha.ComputeHash(encoder.GetBytes(forgotPassword + "d3katk00"));
            string hashPass = ByteArrayToString(passwordHash);
            HashPassword = hashPass;

            //send parameters to store procedure.
            CIABcommand.Parameters.AddWithValue("@GUID", UniqueID);
            CIABcommand.Parameters.AddWithValue("@Password", HashPassword);
            result = Convert.ToBoolean(CIABcommand.ExecuteScalar());

            if (result)
            {
                return RedirectToAction("PasswordChangedConfirmation", "ForgotPassword");
            }
            else
            {
                return View();
            }
            
        }
        public static string ByteArrayToString(byte[] byteArray)
        {
            StringBuilder hex = new StringBuilder(byteArray.Length * 2);
            foreach (byte b in byteArray)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

    }
}