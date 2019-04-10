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



            bundles.Add(new StyleBundle("~/style/base").Include(
                  "~/Content/bootstrap.min.css",
                  "~/Content/jquery-ui.min.css",
                  "~/Content/normalize.css",
                   "~/Content/main.css"));

            bundles.Add(new StyleBundle("~/style/menu").Include(
                     "~/Content/ef.css",
                     "~/Content/sidebar.css"));
            bundles.Add(new StyleBundle("~/style/layui").Include(
                     "~/layui/css/layui.css"));

            bundles.Add(new ScriptBundle("~/js/base").Include(
                     "~/Scripts/base64.js",
                     "~/Scripts/jquery.min.js",
                     "~/Scripts/layer.js",
                     "~/Scripts/iconfont.js",
                      "~/Scripts/localforage.min.js",
                     "~/Scripts/snap.svg-min.js"
                     ));

            bundles.Add(new ScriptBundle("~/js/ui").Include(    
                     "~/Scripts/jquery-ui.min.js",//依赖 JQ
                     "~/Scripts/bootstrap.min.js",//依赖 JQ
                     "~/Scripts/popper.min.js",//依赖 JQ
                    "~/Scripts/cookie.js"//依赖layer
                     ));

            bundles.Add(new ScriptBundle("~/js/pos").Include(
               "~/Scripts/pos.js",
                   "~/Scripts/main.js"
                 ));
            bundles.Add(new ScriptBundle("~/js/layui").Include(
              "~/layui/layui.js"
                ));

            bundles.Add(new ScriptBundle("~/end").Include(
      "~/Scripts/endpage.js"
        ));

            bundles.Add(new ScriptBundle("~/js/dater").Include(
                "~/Scripts/laydate.js"
                   ));

            bundles.Add(new ScriptBundle("~/js/register").Include(
               "~/Scripts/register.js"
                  ));

        }
    }
}
