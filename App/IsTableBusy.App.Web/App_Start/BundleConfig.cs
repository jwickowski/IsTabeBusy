using System.Web.Optimization;

namespace IsTableBusy.App.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/libs").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js",
                "~/Scripts/knockout-3.4.0.debug.js",
                "~/Scripts/jquery.signalR-{version}.js",
                "~/Scripts/lodash.js",
                "~/Scripts/vendor/jsgrid/jsgrid.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/Scripts/_allScripts.build.js"));

        // Use the development version of Modernizr to develop with and learn from. Then, when you're
        // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
        bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/vendor/jsgrid.css",
                "~/Content/vendor/jsgrid-theme.css"));

            BundleTable.EnableOptimizations = true;

        }
    }
}
