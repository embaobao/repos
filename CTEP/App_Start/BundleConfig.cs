using System.Web;
using System.Web.Optimization;

namespace CTEP
{
    public class BundleConfig
    {
        // 有关捆绑的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备就绪，请使用 https://modernizr.com 上的生成工具仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/user/css").Include(
                     "~/Content/bootstrap.min.css",
                     "~/Content/ef.css",
                     "~/Content/jquery-ui.min.css",
                     "~/Content/normalize.css",
                      "~/Content/main.css",
                     "~/Content/sidebar.css"));

            bundles.Add(new ScriptBundle("~/bundles/user/main").Include(
                     "~/Scripts/jquery.min.js",
                     "~/Scripts/iconfont.js",
                     "~/Scripts/snap.svg-min.js",
                     "~/Scripts/popper.min.js",
                     "~/Scripts/bootstrap.min.js",
                     "~/Scripts/base64.js",
                     "~/Scripts/layer.js",
                    "~/Scripts/cookie.js"
                     ));

            bundles.Add(new ScriptBundle("~/bundles/user/pos").Include(
               "~/Scripts/pos.js",
                   "~/Scripts/main.js"
                 ));
            
                   

            bundles.Add(new ScriptBundle("~/bundles/user/dater").Include(
                "~/Scripts/laydate.js"
                   ));
            bundles.Add(new ScriptBundle("~/bundles/user/register").Include(
               "~/Scripts/register.js"
                  ));

        }
    }
}
