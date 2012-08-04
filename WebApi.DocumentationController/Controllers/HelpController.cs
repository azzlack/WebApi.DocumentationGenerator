namespace WebApi.DocumentationController.Controllers
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Routing;

    using AttributeRouting.Framework;

    using WebApi.DocumentationController.ViewModels;

    /// <summary>
    /// Controller for viewing automatically generated documentation.
    /// </summary>
    public class HelpController : Controller
    {
        /// <summary>
        /// Default view.
        /// </summary>
        /// <returns>The default view.</returns>
        public ActionResult Index()
        {
            var viewmodel = new ApiExplorerViewModel()
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
            var descriptions = api;
            var controller = api.First().ActionDescriptor.ControllerDescriptor;
            var routes = RouteTable.Routes.OfType<IAttributeRoute>();
            var croutes = routes.Where(x => x.Defaults != null && x.Defaults.Any(c => c.Key.Equals("controller") && c.Value != null && c.Value.ToString().Equals(controller.ControllerName)));

            var viewmodel = new ApiExplorerDetailsViewModel()
                           {
                               ApiDescriptions = GlobalConfiguration.Configuration.Services.GetApiExplorer().ApiDescriptions,
                               ControllerDescriptor = controller,
                               ControllerApiDescriptions = descriptions,
                               AttributeRoutes = croutes
                           };

            return View(viewmodel);
        }

        /// <summary>
        /// Models diagram view.
        /// </summary>
        /// <returns>The model diagram view.</returns>
        public ActionResult ModelDiagram()
        {
            var viewmodel = new ApiExplorerViewModel()
            {
                ApiDescriptions = GlobalConfiguration.Configuration.Services.GetApiExplorer().ApiDescriptions
            };

            return this.View(viewmodel);
        }
    }
}
