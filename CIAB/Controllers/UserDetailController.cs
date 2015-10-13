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
            try
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



                return View(data);
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }



        public ActionResult Create()
        {
            return View();
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, CIAB.Models.UserDetail userDetails)
        {
            try
            {

                SqlConnection CIABconnection = new SqlConnection(CIABconnectionString);
                SqlCommand CIABcommand = new SqlCommand();
                CIABconnection.Open();
                CIABcommand.CommandType = System.Data.CommandType.StoredProcedure;
                CIABcommand.Connection = CIABconnection;
                CIABcommand.CommandText = "sp_UpdateUserDetails";


                CIABcommand.Parameters.AddWithValue("@userID", userDetails.UserId);
                CIABcommand.Parameters.AddWithValue("@fullName", userDetails.FullName);
                CIABcommand.Parameters.AddWithValue("@userName", userDetails.UserName);
                CIABcommand.Parameters.AddWithValue("@email", userDetails.Email);

                CIABcommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(new[] { userDetails }.ToDataSourceResult(request, ModelState));
        }


        public ActionResult Delete()
        {
            return View();
        }


    }
}