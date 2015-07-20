using System.Web;
using System.Web.Optimization;

namespace MVC5BoostrapDRAdminV4
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/jquery.dataTables.min.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/respond.js"));


            bundles.Add(new ScriptBundle("~/bundles/bootstrapDatePicker").Include(
                        "~/Scripts/bootstrap.min.js",
                        "~/Scripts/moment.js",
                        "~/Scripts/bootstrap-datetimepicker.min.js",
                        "~/Scripts/MyScript.js"
                        ));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/ubootstrap.css",
                      "~/Content/bootstrap-datetimepicker.min.css",
                      "~/Content/site.css"
                      ));


            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                //"~/Scripts/jquery-ui-{version}.js",
                       "~/Scripts/jquery-ui.unobtrusive-{version}.js"));



            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                       "~/Content/themes/base/jquery.ui.core.css",
                //"~/Content/themes/base/jquery.ui.datepicker.css",
                       "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}
