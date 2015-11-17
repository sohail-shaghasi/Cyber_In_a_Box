using System.Web;
using System.Web.Optimization;

namespace CIAB
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                                         "~/Scripts/kendo/kendo.all.min.js",
                                         "~/Scripts/kendo.web.min.js",
                                         "~/Scripts/kendo/kendo.aspnetmvc.min.js"));
            
            bundles.Add(new StyleBundle("~/Content/kendo/css").Include(
                                        "~/Content/kendo/kendo.common-bootstrap.min.css",
                                        "~/Content/kendo/kendo.bootstrap.min.css"));
            
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                                         "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/Extended_jquery").Include(
                                        "~/Scripts/jquery.bxslider.js",
                                        "~/Scripts/jquery.royalslider.min.js",
                                        "~/Scripts/jquery.validate.min.js",
                                        "~/Scripts/jquery.validate.unobtrusive.min.js",
                                        "~/Scripts/jquery.colorbox-min.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                                         "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                                         "~/Scripts/bootstrap.js",
                                         "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/kendo/css").Include(
                                        "~/Content/kendo/kendo.common.min.css",
                                        "~/Content/kendo/kendo.default.min.css"));
            bundles.IgnoreList.Clear();
            BundleTable.EnableOptimizations = true;
        }
    }
}
