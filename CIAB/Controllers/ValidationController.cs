using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CIAB.Models;


namespace CIAB.Controllers
{
    public class ValidationController : Controller
    {
        //
        string CIABconnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CIABConnectionString"].ConnectionString;
        public ActionResult Index()
        {
            return View();
        }

        //[HttpPost]
       // [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        
        [HttpGet]
        
        public ActionResult IsUserNameAvailable(string UserName)
        {

            if (UserName != "sohail")
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(string.Format("{0} is invalid", UserName),
                    JsonRequestBehavior.AllowGet);



            //int _count = 0;
            //SqlConnection con = new SqlConnection(CIABconnectionString);
            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = con;
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.CommandText = "sp_CheckUsers";
            //cmd.Parameters.Clear();

            //cmd.Parameters.AddWithValue("@UserName", UserName);
            //SqlParameter param = new SqlParameter("@IsExists", System.Data.SqlDbType.Int);
            //param.Direction = System.Data.ParameterDirection.Output;
            //cmd.Parameters.Add(param);
            //cmd.ExecuteNonQuery();
            //_count = Convert.ToInt32(param.Value);
            //return Json(_count);
            //// return _count;
            //// return Json( == null);
        }


         //[HttpPost]
        //[AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [HttpGet]
        public JsonResult IsEmailAvailable(string RegisterationEmail)
        {
        

             return Json(!RegisterationEmail.Equals("this@email.com"), JsonRequestBehavior.AllowGet);
        }




        //[AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [HttpGet]
        public JsonResult IsCurrentPasswordAvailable(string CurrentPass)
        {
            var result = false;

            if (CurrentPass == "sohail")
            {
                result = true;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }

}