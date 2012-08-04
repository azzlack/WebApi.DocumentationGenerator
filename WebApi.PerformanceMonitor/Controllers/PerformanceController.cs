namespace WebApi.PerformanceMonitor.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Routing;

    using AttributeRouting.Framework;

    using WebApi.PerformanceMonitor.ViewModels;

    /// <summary>
    /// Controller for viewing automatically generated documentation.
    /// </summary>
    public class PerformanceController : Controller
    {
        /// <summary>
        /// Default view.
        /// </summary>
        /// <returns>The default view.</returns>
        public ActionResult Index()
        {
            var viewmodel = new OverallPerformanceViewModel()
                {
                    ApiDescriptions = GlobalConfiguration.Configuration.Services.GetApiExplorer().ApiDescriptions
                };

            return this.View(viewmodel);
        }

        /// <summary>
        /// Details view
        /// </summary>
        /// <param name="id">The controller name.</param>
        /// <returns>The detailed view of a Controller.</returns>
        public ActionResult Details(string id)
        {
            var api = GlobalConfiguration.Configuration.Services.GetApiExplorer().ApiDescriptions
                .GroupBy(x => x.ActionDescriptor.ControllerDescriptor.ControllerName).FirstOrDefault(x => x.Key.Equals(id, StringComparison.InvariantCultureIgnoreCase));
            var controller = api.First().ActionDescriptor.ControllerDescriptor;

            var viewmodel = new ControllerPerformanceViewModel()
                           {
                               ApiDescriptions = GlobalConfiguration.Configuration.Services.GetApiExplorer().ApiDescriptions,
                               ControllerDescriptor = controller
                           };

            return this.View(viewmodel);
        }
    }
}
