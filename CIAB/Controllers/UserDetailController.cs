using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CIAB.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace CIAB.Controllers
{
    public class UserDetailController : Controller
    {
        string CIABconnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CIABConnectionString"].ConnectionString;

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult UserDetails([DataSourceRequest]DataSourceRequest request)
        {
            
            
            
            var lstUserModel = new List<UserDetail>();//List of Object
            SqlConnection CIABconnection = new SqlConnection(CIABconnectionString);
            SqlCommand CIABcommand = new SqlCommand();
            CIABconnection.Open();
            CIABcommand.CommandType = System.Data.CommandType.StoredProcedure;
            CIABcommand.Connection = CIABconnection;
            CIABcommand.CommandText = "sp_UserDetails";
            SqlDataReader reader = CIABcommand.ExecuteReader();

           

            
            while (reader.Read())
            {
                var objUserDetails = new CIAB.Models.UserDetail();

                objUserDetails.UserId = reader["UserID"].ToString();
                objUserDetails.FullName = reader["FullName"].ToString();
                objUserDetails.UserName = reader["UserName"].ToString();
                objUserDetails.Email = reader["Email"].ToString();
                objUserDetails.pass = reader["Pass"].ToString();
                lstUserModel.Add(objUserDetails);//add an object to the list of type object
            }
            var data = lstUserModel.ToList();



           
            if (Request.IsAjaxRequest())
            {
                DataSourceResult result = data.ToDataSourceResult(request);//convert json to object
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return View("UserDetails", data);
            }
        
        
        
        }

    }
}