using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using CIAB.Models;
using System.Net.Mail;
using System.Net;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Security.Cryptography;
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
        public ActionResult Contact(CIAB.Models.EmailFrom emailFrom)
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
                HomeDataLayer homeDataLayer = new HomeDataLayer();
                homeDataLayer.ContactUs(inputName, Subject, inputEmail, Message, optProduct, body);
                return RedirectToAction("sent");
            }
            catch (Exception ex)
            {
                ViewData["smtpError"] = "Unable to send an email";
                base.Logger.Error(ex, "Contact_{0} | StackTrace: {1}", ex.Message, ex.StackTrace);
                return View();
            }
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
            return View(new User());
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View("SignUpLogin");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User CIABuser)
        {
            try
            {
                

                if (ModelState.IsValid)//Check if Model properties are Valid
                {
                    var homeDataLayer = new HomeDataLayer();
                    Encoding encoder = new UTF8Encoding();
                    SHA1 sha = new SHA1Managed();
                    byte[] passwordHash = sha.ComputeHash(encoder.GetBytes(CIABuser.loginViewModel.LoginPassword + "d3katk00"));
                    CIABuser.loginViewModel.LoginHashPassword = base.ByteArrayToString(passwordHash);
                    if (homeDataLayer.IsValid(CIABuser))
                    {
                        Session["UserName"] = CIABuser.loginViewModel.LoginUserName;//Username store in the session
                        Session["Password"] = CIABuser.loginViewModel.LoginHashPassword;
                        Session["UserId"] = CIABuser.loginViewModel.UserID;
                        Session["email"] = CIABuser.loginViewModel.LoginEmail;
                        Session["UserRole"] = CIABuser.loginViewModel.Role;
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
            return View("SignUpLogin");
        }
        [HttpPost]
        public ActionResult Register(User NewUser, string RegisterUserName, string RegisterEmail)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var homeDataLayer = new HomeDataLayer();

                    int UserIDReturnedFromSP = homeDataLayer.RegisterUser(NewUser);

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
            if (Session["UserName"] != null)
            {
                var homeDataLyer = new HomeDataLayer();
                var userProfile = homeDataLyer.GetUserProfile(Session["UserName"].ToString(), Session["Password"].ToString());
                ViewData["Email"] = userProfile.email;
                ViewData["FullName"] = userProfile.FullName;
                ViewData["Website"] = userProfile.Website;
                ViewData["CompanyName"] = userProfile.CompanyName;
                ViewData["CompanyAddress"] = userProfile.CompanyAddress;
            }
        }
        public ActionResult LogOut()
        {
            Session.Remove("UserName");
            return RedirectToAction("Index");
        }
     }
}
