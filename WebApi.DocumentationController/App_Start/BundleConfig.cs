[assembly: WebActivator.PostApplicationStartMethod(typeof(WebApi.DocumentationController.App_Start.BundleConfig), "Start")]

namespace WebApi.DocumentationController.App_Start
{
    using System.IO;
    using System.Linq;
    using System.Web.Optimization;

    /// <summary>
    /// Configures the documentation resource bundle.
    /// </summary>
    public static class BundleConfig
    {
        /// <summary>
        /// Executes after the application starts.
        /// </summary>
        public static void Start()
        {
            if (BundleTable.Bundles.Any(x => x.Path == "~/Content/css"))
            {
                BundleTable.Bundles.First(x => x.Path == "~/Content/css").Include("~/Content/documentation/documentation.css");
            }
        }
    }
}