using System.Web;
using System.Web.Optimization;

namespace CIAB
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            //--------------------Add a script bundle for Kendo UI.------------------------------------------

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
            "~/Scripts/kendo/kendo.all.min.js",

            // "~/Scripts/kendo/kendo.timezones.min.js", // uncomment if using the Scheduler

            "~/Scripts/kendo/kendo.aspnetmvc.min.js"));




            //Add a style bundle for Kendo UI.  
            bundles.Add(new StyleBundle("~/Content/kendo/css").Include(
                       "~/Content/kendo/kendo.common-bootstrap.min.css",
                        "~/Content/kendo/kendo.bootstrap.min.css"));



            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                                         "~/Scripts/jquery-{version}.js"));




            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                                         "~/Scripts/modernizr-*"));



            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                                         "~/Scripts/bootstrap.js",
                                         "~/Scripts/respond.js"));




       

            //---------------CSS Bundles-----------------------------------





            bundles.Add(new StyleBundle("~/Content/css").Include(
                                        "~/Content/bootstrap.css",
                                        "~/Content/site.css"));




            bundles.Add(new StyleBundle("~/Content/kendo/css").Include(
                                        "~/Content/kendo/kendo.common.min.css",
                                        "~/Content/kendo/kendo.default.min.css"));





            bundles.IgnoreList.Clear();
            BundleTable.EnableOptimizations = true;
        }
    }
}
