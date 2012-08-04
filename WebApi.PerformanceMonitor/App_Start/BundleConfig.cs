using WebApi.PerformanceMonitor.App_Start;

[assembly: WebActivator.PostApplicationStartMethod(typeof(BundleConfig), "Start")]

namespace WebApi.PerformanceMonitor.App_Start
{
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
            BundleTable.Bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/performance.css"));
        }
    }
}