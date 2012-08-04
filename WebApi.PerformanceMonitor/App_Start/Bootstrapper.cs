using WebApi.PerformanceMonitor.App_Start;

[assembly: WebActivator.PostApplicationStartMethod(typeof(Bootstrapper), "Start")]

namespace WebApi.PerformanceMonitor.App_Start
{
    /// <summary>
    /// Configures the performance monitor.
    /// </summary>
    public static class Bootstrapper
    {
        /// <summary>
        /// Executes before the application starts.
        /// </summary>
        public static void Start()
        {
        }
    }
}