using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CIAB.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using CIAB.DataLayer;

namespace CIAB.Controllers
{
    public class UserDetailController : BaseController
    {
        public List<UserDetail> LstUserModel { get; set; }

        #region Methods
        public ActionResult Index()
        {
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post | HttpVerbs.Get)]
        public ActionResult UserDetails([DataSourceRequest]DataSourceRequest request)
        {
            if (Convert.ToString(Session["UserName"]).ToLower() == null || Convert.ToString(Session["UserName"]).ToLower() == string.Empty)
            {
                return RedirectToAction("SignUpLogin", "Home");
            }
            var userDetailsDL = new UserDetailDataLayer();
            try
            {
                LstUserModel = userDetailsDL.GetUserDetails();
                return View(LstUserModel);
            }
            catch (Exception ex)
            {
                base.Logger.Error(ex, "UserDetails_{0} | StackTrace: {1}", ex.Message, ex.StackTrace);
                return View(LstUserModel);
            }
        }
        [AcceptVerbs(HttpVerbs.Post | HttpVerbs.Get)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, UserDetail userDetails)
        {
            var userDetailsDL = new UserDetailDataLayer();
            try
            {
               userDetailsDL.UpdateUserDetails(userDetails);
            }
            catch (Exception ex)
            {
                base.Logger.Error(ex, "Update_{0} | StackTrace: {1}", ex.Message, ex.StackTrace);
                return Json(new[] { userDetails }.ToDataSourceResult(request, ModelState));
            }
            return Json(new[] { userDetails }.ToDataSourceResult(request, ModelState));
        }
        #endregion
    }
}