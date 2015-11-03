using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CIAB.Models;
using System.Net.Mail;
using System.Net;
using System.Data.SqlClient;
using System.Data;
using System.Web.Security;
using System.Text;
using System.Security.Cryptography;
using System.Web.Configuration;
using CIAB.DataLayer;


namespace CIAB.Controllers
{


    public class HomeController : BaseController
    {
        public HomeController()
        {

        }
        public ActionResult Index()
        {


            return View();
        }
        public ActionResult Contact()
        {


            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(CIAB.Models.EmailFrom emailFrom)
        {

            string inputName, Subject, inputEmail, Message, optProduct;

            inputName = emailFrom.inputName;
            Subject = emailFrom.Subject;
            inputEmail = emailFrom.inputEmail;
            Message = emailFrom.Message;
            optProduct = emailFrom.optProduct;
            var body = "<p>Email From : {0}  </p> <p>Subject : {1} </p> <p>Name : {2} </p> <p>Product : {3} </p> <p>Message: {4} </p>";

            try
            {

                //SMTP parameters starts here
                string strReciever = System.Configuration.ConfigurationManager.AppSettings["smtpReciever"];
                string strEmailFrom = System.Configuration.ConfigurationManager.AppSettings["smtpEmailFrom"];
                string strSubject = System.Configuration.ConfigurationManager.AppSettings["smtpSubject"];
                string strSMTPUser = System.Configuration.ConfigurationManager.AppSettings["smtpUser"];
                string strSMPTpass = System.Configuration.ConfigurationManager.AppSettings["smtpPass"];
                string strSMPTHost = System.Configuration.ConfigurationManager.AppSettings["smtpServer"];
                int SMPTPort = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["smtpPort"]);

                using (MailMessage message = new MailMessage())
                {
                    message.From = new MailAddress(strEmailFrom);
                    message.To.Add(inputEmail);
                    message.Subject = strSubject; //Subject;
                    message.Body = string.Format(body, inputEmail, Subject, inputName, optProduct, Message);
                    message.IsBodyHtml = true;

                // credebtials for smtp client account
                    using (SmtpClient smtp = new SmtpClient())
                    {
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

                    }
                }
                    return RedirectToAction("sent");
            }
            catch (Exception ex)
            {
                ViewData["smtpError"] = "Unable to send an email";
                base.Logger.Error(ex, "Contact_{0} | StackTrace: {1}", ex.Message, ex.StackTrace);
                return View();
            }
        }
        public ActionResult CyberHealthSignUp()
        {


            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CyberHealthSignUp(CIAB.Models.CyberHealthCheckSignUp cyberHealthcheck)
        {

            string fullName, LastName, JobTitle, companyName, inputEmail;
            bool? RecievemarketingEmails, TermsAndConditions;
            int contactNumber;

            fullName = cyberHealthcheck.FullName.ToString();
            LastName = cyberHealthcheck.LastName.ToString();
            JobTitle = cyberHealthcheck.JobTitle.ToString();
            companyName = cyberHealthcheck.CompanyName.ToString();
            inputEmail = cyberHealthcheck.InputEmail.ToString();
            contactNumber = cyberHealthcheck.ContactNumber;
            RecievemarketingEmails = cyberHealthcheck.RecieveMarketingEmails;
            TermsAndConditions = cyberHealthcheck.TermsAndConditions;



            try
            {


                TempUser(cyberHealthcheck); //Function call to store the Temporary User information to TempUser Table

                var body = "<p>Email From : {0}  </p> <p>Email Address: {1} </p> <p>Job Title: {2} </p> <p>Company Name: {3} </p> <p>Contact Number: {4}</p>";
                //SMTP parameters starts here
                string strReciever = System.Configuration.ConfigurationManager.AppSettings["smtpReciever"];
                string strSubject = System.Configuration.ConfigurationManager.AppSettings["SubjectForHealthCeck"];
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
                message.Body = string.Format(body, fullName + " " + LastName, inputEmail, JobTitle, companyName, contactNumber);
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
                base.Logger.Error(ex, "CyberHealthSignUp_{0} | StackTrace: {1}", ex.Message, ex.StackTrace);
                return View();
            }
        }
        private void TempUser(CIAB.Models.CyberHealthCheckSignUp CyberHealthCheck)
        {
            SqlConnection CIABconnection = new SqlConnection(CIABconnectionString);
            SqlCommand CIABcommand = new SqlCommand();
            CIABconnection.Open();
            CIABcommand.CommandType = CommandType.StoredProcedure;
            CIABcommand.Connection = CIABconnection;
            CIABcommand.CommandText = "sp_TempUser";


            CIABcommand.Parameters.AddWithValue("@FullName", CyberHealthCheck.FullName);
            CIABcommand.Parameters.AddWithValue("@LastName", CyberHealthCheck.LastName);
            CIABcommand.Parameters.AddWithValue("@JobTitle", CyberHealthCheck.JobTitle);
            CIABcommand.Parameters.AddWithValue("@CompanyName", CyberHealthCheck.CompanyName);
            CIABcommand.Parameters.AddWithValue("@Email", CyberHealthCheck.InputEmail);
            CIABcommand.Parameters.AddWithValue("@ContactNumber", CyberHealthCheck.ContactNumber);
            CIABcommand.ExecuteNonQuery();
            ViewData["TempUser"] = "Successfull";//message for pop up Alert().
        }
        public ActionResult sent()
        {
            return View();
        }

        public ActionResult AboutUs()
        {
            return View();
        }
        public ActionResult Legal()
        {

            return View();
        }
        public ActionResult Privacy()
        {
            return View();
        }
        public ActionResult SignUpLogin()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View("Register");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User CIABuser)
        {

            try
            {
                if (ModelState.IsValid)//Check if Model properties are Valid
                {
                    var user = new User();
                    var userDL = new UserDataLayer();

                    user.UserName = CIABuser.LoginUserName;
                    user.Password = CIABuser.LoginPassword;

                    Encoding encoder = new UTF8Encoding();
                    SHA1 sha = new SHA1Managed();
                    byte[] passwordHash = sha.ComputeHash(encoder.GetBytes(user.Password + "d3katk00"));

                    user.HashPassword = base.ByteArrayToString(passwordHash);

                    if (userDL.IsValid(user))//Check the credentials against the database
                    {
                        //FormsAuthentication.SetAuthCookie(CIABuser.UserName, false);
                        //FormsAuthentication.RedirectFromLoginPage(CIABuser.UserName, false);//To make User's login credentials persistent.
                        Session["UserName"] = user.UserName;//Username store in the session
                        Session["Password"] = user.HashPassword;
                        Session["UserId"] = user.UserID;
                        Session["email"] = user.LoginEmail;
                        Session["UserRole"] = user.Role;

                        var productHandle = Convert.ToString(Session["productHandle"]);

                        if (string.IsNullOrEmpty(productHandle))
                            return RedirectToAction("Index", "Home");
                        else
                            return RedirectToAction("DispalyOrder", "Order", new { ProductHandle = productHandle });

                    }

                }

            }
            catch (Exception ex)
            {
                base.Logger.Error(ex, "Login_{0} | StackTrace: {1}", ex.Message, ex.StackTrace);
            }
            ModelState.AddModelError("LoginError", "Incorrect UserName / Password");
            return View("SignUpLogin", CIABuser);
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();

        }
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        //[ValidateAntiForgeryToken]
        public ActionResult Register(CIAB.Models.User NewUser)
        {
            string strFullName = NewUser.FullName;
            string strUserName = NewUser.UserName;
            string strEmail = NewUser.RegisterationEmail;
            string strPass = NewUser.Password;

            SqlConnection CIABconnection = new SqlConnection(CIABconnectionString);
            SqlCommand CIABcommand = new SqlCommand();

            try
            {
                if (ModelState.IsValid)
                {

                    CIABconnection.Open();
                    CIABcommand.CommandType = CommandType.StoredProcedure;
                    CIABcommand.Connection = CIABconnection;
                    CIABcommand.CommandText = "spRegisterUser";

                    CIABcommand.Parameters.AddWithValue("@Fullname", SqlDbType.NVarChar).Value = strFullName.ToString();//parameter for Full Name
                    CIABcommand.Parameters.AddWithValue("@UserName", SqlDbType.NVarChar).Value = strUserName.ToString();// parameter for UserName
                    CIABcommand.Parameters.AddWithValue("@Email", SqlDbType.NVarChar).Value = strEmail.ToString();//parameter for Email


                    Encoding encoder = new UTF8Encoding();
                    SHA1 sha = new SHA1Managed();
                    byte[] passwordHash = sha.ComputeHash(encoder.GetBytes(strPass + "d3katk00"));
                    string hashPass = ByteArrayToString(passwordHash);


                    CIABcommand.Parameters.AddWithValue("@Pass", SqlDbType.NVarChar).Value = hashPass.ToString();// parameter for pass
                    int UserIDReturnedFromSP = Convert.ToInt32(CIABcommand.ExecuteScalar());

                    //logic for checking whether UserName and Email already exists.

                    switch (UserIDReturnedFromSP)
                    {

                        case -1:
                            ModelState.AddModelError("UserNameExists", "Username Is in Use.");
                            return View("SignUpLogin", NewUser);


                        case -2:
                            ModelState.AddModelError("EmailExists", "Email Address Is in Use.");
                            return View("SignUpLogin", NewUser);



                        default:
                            return RedirectToAction("RegistrationConfirmation");

                    }
                }


                else
                {
                    ModelState.AddModelError("test", "testing");
                    return View("SignUpLogin", NewUser);
                }
            }

            catch (Exception ex)
            {
                base.Logger.Error(ex, "Register_{0} | StackTrace: {1}", ex.Message, ex.StackTrace);

                return View("SignUpLogin", NewUser);
            }
        }
        public ActionResult RegistrationConfirmation()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UserProfileEdit()
        {
            try
            {
                GetUserProfile();
            }
            catch (Exception ex)
            {
                base.Logger.Error(ex, "UserProfileEdit_{0} | StackTrace: {1}", ex.Message, ex.StackTrace);
            }

            return View();
        }

        [HttpPost]
        public ActionResult UserProfileEdit(CIAB.Models.UserProfileEdit userProfileEdit)
        {
            try
            {
                var profileDL = new ProfileDataLayer();

                userProfileEdit.UserName = Session["UserName"].ToString();
                userProfileEdit.Password = Session["Password"].ToString();

                profileDL.UpdateProfile(userProfileEdit);
                ViewData["ProfileUpdated"] = "Successfully Updated Your Profile. ";//message for pop up Alert().
                GetUserProfile();

            }
            catch (Exception ex)
            {
                base.Logger.Error(ex, "UserProfileEdit_{0} | StackTrace: {1}", ex.Message, ex.StackTrace);
            }

            return View(userProfileEdit);
        }

        private void GetUserProfile()
        {
            string strUserName = "";
            string strPassword = "";
            if (Session["UserName"] != null)
            {
                strUserName = Session["UserName"].ToString();
                strPassword = Session["Password"].ToString();
            }

            SqlConnection CIABconnection = new SqlConnection(CIABconnectionString);
            SqlCommand CIABcommand = new SqlCommand();
            CIABconnection.Open();
            CIABcommand.CommandType = CommandType.StoredProcedure;
            CIABcommand.Connection = CIABconnection;
            CIABcommand.CommandText = "sp_UpdateProfile";


            //Input Parameters for Stored Procedure
            CIABcommand.Parameters.AddWithValue("@Parameter", "select");
            CIABcommand.Parameters.AddWithValue("@UserName", strUserName);
            CIABcommand.Parameters.AddWithValue("@Password", strPassword);


            SqlDataReader reader = CIABcommand.ExecuteReader();
            while (reader.Read())
            {
                // Output Parameters from stored Procedure
                ViewData["Email"] = reader["Email"].ToString();
                ViewData["FullName"] = reader["FullName"].ToString();
                ViewData["Website"] = reader["Website"].ToString();
                ViewData["CompanyName"] = reader["CompanyName"].ToString();
                ViewData["CompanyAddress"] = reader["CompanyAddress"].ToString();
            }
        }

        /// <summary>
        /// Log out the Logged in User
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOut()
        {


            Session.Remove("UserName");
            //FormsAuthentication.SignOut();//remove the authentication ticket (cookie).
            //HttpContext.Session.Abandon();


            return RedirectToAction("Index");

        }

        [NonAction]//This is to specify that, Result() function is not invokable.
        public string Result()
        {
            return "";
        }
    }
}
