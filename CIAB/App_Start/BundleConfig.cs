using System.Web;
using System.Web.Optimization;

namespace CIAB
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
   
            //---------------------JavaScript Bundles-------------------------------------------




            
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                                         "~/Scripts/jquery-{version}.js"));

          
            
            
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                                         "~/Scripts/modernizr-*"));

            
            
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                                         "~/Scripts/bootstrap.js",
                                         "~/Scripts/respond.js"));

           
            
            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                                        "~/Scripts/kendo/kendo.all.min.js" ));
                                        // "~/Scripts/kendo/kendo.timezones.min.js", // uncomment if using the Scheduler
                                       



      
            //---------------CSS Bundles-----------------------------------





            bundles.Add(new StyleBundle("~/Content/css").Include(
                                        "~/Content/bootstrap.css",
                                        "~/Content/site.css"));


            
            
            bundles.Add(new StyleBundle("~/Content/kendo/css").Include(
                                        "~/Content/kendo/kendo.common.min.css",
                                        "~/Content/kendo/kendo.default.min.css"));

        }
    }
}
