using System.Web;
using System.Web.Optimization;

namespace OnlineJudge {
    public class BundleConfig {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles) {
            bundles.Add(new ScriptBundle("~/Script/Bundles")
                .Include("~/bundles/inline.*",
                    "~/bundles/polyfills.*",
                    "~/bundles/scripts.*",
                    "~/bundles/vendor.*",
                    "~/bundles/main.*"));

            bundles.Add(new StyleBundle("~/Content/Styles")
                .Include("~/bundles/styles.*"));
        }
    }
}
