﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Security.Cryptography;
using System.Text;

namespace CIAB.Controllers
{
    public class ChangePasswordController : Controller
    {
        //
        string CIABconnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CIABConnectionString"].ConnectionString;
        public ActionResult Index()
        {
            return View();
        }





        [HttpGet]
        public ActionResult ChangeUserPassword()
        {
            return View();
        }




        [HttpPost]
        public ActionResult ChangeUserPassword(CIAB.Models.ResetPassword ResetPass)
        {




            if (ModelState.IsValid)
            {
                string strCurrentPass = GetSHA1(ResetPass.CurrentPass + "d3katk00");
                string strNewPass = GetSHA1(ResetPass.NewPass + "d3katk00");



                string strOldPasswordTobeCompared = "";

                if (HttpContext.Session["Password"] != null)
                {
                    strOldPasswordTobeCompared = HttpContext.Session["Password"].ToString();
                    if (strCurrentPass != strOldPasswordTobeCompared)
                    {
                        ViewBag.ErrorMessage = "Current Password is not Valid!";
                        return View("ChangeUserPassword");
                    }

                }
                //check if old password and new hashed password resembles.

                int returnValue = AddNewPassword(ResetPass, strCurrentPass, strNewPass);


                if (returnValue == 1)
                {
                    return RedirectToAction("PasswordResetSuccess");
                    // Or show in messagebox using: ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Password has been changed successfully');", true);
                }
                else
                {
                    return View(ResetPass);
                    // Or show in messagebox using: ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Wrong username/password. Please re-enter.');", true);
                }
            }

            else
            {
                ModelState.AddModelError("resetpass", "testing");
                return View("ChangeUserPassword", ResetPass);
            }

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
            // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           



        }


        public ViewResult PasswordResetSuccess()
        {
            return View();
        }


        private int AddNewPassword(CIAB.Models.ResetPassword ResetPass, string strCurrentPass, string strNewPass)
        {

            if (HttpContext.Session["UserName"] != null)
            {
                ResetPass.UserName = HttpContext.Session["UserName"].ToString();
            }

            SqlConnection CIABconnection = new SqlConnection(CIABconnectionString);
            SqlCommand CIABcommand = new SqlCommand();
            CIABconnection.Open();
            CIABcommand.CommandType = System.Data.CommandType.StoredProcedure;
            CIABcommand.Connection = CIABconnection;
            CIABcommand.CommandText = "sp_ResetPassword";



            CIABcommand.Parameters.AddWithValue("@username", ResetPass.UserName);
            CIABcommand.Parameters.AddWithValue("@old_pwd", strCurrentPass);
            CIABcommand.Parameters.AddWithValue("@new_pwd", strNewPass);
            CIABcommand.Parameters.Add("@Status", SqlDbType.Int);
            CIABcommand.Parameters["@Status"].Direction = ParameterDirection.Output;
            CIABcommand.ExecuteNonQuery();
            CIABcommand.Dispose();
            //read the return value (i.e status) from the stored procedure
            int returnValue = Convert.ToInt32(CIABcommand.Parameters["@Status"].Value);
            return returnValue;
        }





        public string GetSHA1(string password)
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





    }
}