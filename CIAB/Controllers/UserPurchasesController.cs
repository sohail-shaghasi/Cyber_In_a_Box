using CIAB.DataLayer;
using System;
using System.Web.Mvc;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace CIAB.Controllers
{
    public class UserPurchasesController : BaseController
    {
        #region Methods
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ViewUserOrders()
        {
            if (Convert.ToString(Session["UserName"]).ToLower() == null || Convert.ToString(Session["UserName"]).ToLower() == string.Empty)
            {
                return RedirectToAction("SignUpLogin", "Home");
            }
            try
            {
                var userPurchaseDL = new UserPurchasesDataLayer();
                var Result = userPurchaseDL.GetListOfUserOrders();
                return View(Result);
            }
            catch (Exception ex)
            {
                base.Logger.Error(ex, "ViewUserOrders_{0} | StackTrace: {1}", ex.Message, ex.StackTrace);
                return View();
            }
        }
        public FileResult DownloadReport(int? orderID, string File, string filename)
        {
            var userOrdersDL = new UserPurchasesDataLayer();
            byte[] pdfbytes =userOrdersDL.GetPDFFromDB(orderID, File);
            MemoryStream workStream = new MemoryStream();
            Document document = new Document();
            PdfWriter.GetInstance(document, workStream).CloseStream = false;
            byte[] byteInfo = pdfbytes;
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
            return new FileStreamResult(workStream, "application/pdf");
        }
        #endregion
    }
}