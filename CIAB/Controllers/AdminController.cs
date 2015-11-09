using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CIAB.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Data.SqlClient;
using System.IO;
using CIAB.DataLayer;
﻿


namespace CIAB.Controllers
{
    public class AdminController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        public ViewResult AdminView([DataSourceRequest]DataSourceRequest request)
        {
            try
            {
                var adminDataLayer = new AdminDataLayer();

                var result = adminDataLayer.GetListOfOrders();

                return View(result);
            }
            catch (Exception ex)
            {
                base.Logger.Error(ex, "AdminView_{0} | StackTrace: {1}", ex.Message, ex.StackTrace);

                return View(new List<AdminViewModel>());
            }
        }
        public JsonResult AdminViewOrderStatus()
        {
            var result = new List<AdminOrderStatus>();
            try
            {
                result.Add(new CIAB.Models.AdminOrderStatus() { StatusValue = "Open", Code = 1 });
                result.Add(new CIAB.Models.AdminOrderStatus() { StatusValue = "Close", Code = 2 });
                result.Add(new CIAB.Models.AdminOrderStatus() { StatusValue = "In Progress", Code = 3 });
            }
            catch (Exception ex)
            {
                base.Logger.Error(ex, "AdminViewOrderStatus_{0} | StackTrace: {1}", ex.Message, ex.StackTrace);
            }

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public JsonResult AdminViewProductName()
        {
            var adminDataLayer = new AdminDataLayer();

            var result = adminDataLayer.GetListofProductName();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, AdminViewModel adminModel)
        {
            try
            {
                var adminDataLayer = new AdminDataLayer();
                adminDataLayer.UpdateOrders(adminModel);

            }
            catch (Exception ex)
            {
                base.Logger.Error(ex, "Update_{0} | StackTrace: {1}", ex.Message, ex.StackTrace);
            }
            return Json(new[] { adminModel }.ToDataSourceResult(request, ModelState));
        }
        public ActionResult Delete()
        {
            return View();
        }


    }
}