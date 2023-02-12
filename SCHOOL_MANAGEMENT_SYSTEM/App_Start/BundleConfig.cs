using System.Web;
using System.Web.Optimization;

namespace SCHOOL_MANAGEMENT_SYSTEM
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/bootstrap.js",
                        "~/scripts/bootbox.js",
                        "~/scripts/bootbox.notify.js",
                        "~/scripts/bootbox.notify.min.js",
                        "~/scripts/bootbox.min.js",
                        "~/scripts/bootbox.all.js",
                        "~/scripts/bootbox.all.min.js",
                        "~/scripts/bootbox.locales.js",
                        "~/scripts/bootbox.locales.min.js",
                        "~/scripts/datatables/jquery.datatables.js",
                        "~/scripts/datatables/datatables.bootstrap.js",
                        "~/scripts/toastr.js",
                        "~/scripts/select2/js/bootstrap-select.js",
                        "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap-cerolean.css",
                      "~/Content/toastr.css",
                      "~/Content/DataTables/css/dataTables.bootstrap.css",
                      "~/Content/select2/css/bootstrap-select.css",
                      "~/Content/site.css")
            );

            bundles.Add(new StyleBundle("~/Report/css").Include(
                      "~/Content/report-css/css/bootstrap-report.min.css",
                      "~/Content/report-css/css/main-report.css"
                 )
            );
            bundles.Add(new ScriptBundle("~/Report/lib").Include(
                        "~/Scripts/report-js/js/bootstrap.bundle.min.js",
                        "~/Scripts/report-js/js/coming-soon.js",
                        "~/Scripts/report-js/js/jquery-ui.min.js",
                        "~/Scripts/report-js/js/jquery.easing.1.3.js",
                        "~/Scripts/report-js/js/jquery.min.js",
                        "~/Scripts/report-js/js/main.js",
                        "~/Scripts/report-js/js/moment.js"
                   )
            );
        }
    }
}

