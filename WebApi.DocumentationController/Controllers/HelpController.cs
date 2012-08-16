namespace WebApi.DocumentationController.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Reflection;
    using System.Web.Http;
    using System.Web.Http.Description;
    using System.Web.Mvc;
    using System.Web.Routing;

    using AttributeRouting.Framework;
    using AttributeRouting.Web.Constraints;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    using WebApi.DocumentationController.Models;
    using WebApi.DocumentationController.ViewModels;

    /// <summary>
    /// Controller for viewing automatically generated documentation.
    /// </summary>
    public class HelpController : Controller
    {
        /// <summary>
        /// The controllers.
        /// </summary>
        private readonly IList<ApiControllerDescription> controllers;

        /// <summary>
        /// The routes.
        /// </summary>
        private IList<IAttributeRoute> routes; 

        /// <summary>
        /// The current assembly.
        /// </summary>
        private Assembly currentAssembly;

        /// <summary>
        /// Initializes a new instance of the <see cref="HelpController" /> class.
        /// </summary>
        public HelpController()
        {
            this.controllers = GlobalConfiguration.Configuration.Services.GetApiExplorer().ApiDescriptions.Select(
                                    x => new ApiControllerDescription()
                                    {
                                        Name = x.ActionDescriptor.ControllerDescriptor.ControllerName
                                    }).ToList();
        }

        /// <summary>
        /// Default view.
        /// </summary>
        /// <returns>The default view.</returns>
        public ActionResult Index()
        {
            var viewmodel = new ApiExplorerViewModel()
                {
                    ApiControllerDescriptions = this.controllers,
                    CurrentAssembly = this.currentAssembly ?? (this.currentAssembly = Assembly.GetAssembly(HttpContext.ApplicationInstance.GetType().BaseType))
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
            // Get actions
            var actions =
                GlobalConfiguration.Configuration.Services.GetApiExplorer().ApiDescriptions
                    .GroupBy(c => c.ActionDescriptor.ControllerDescriptor.ControllerName)
                    .First(x => x.Key.Equals(id, StringComparison.InvariantCultureIgnoreCase));

            var controller = new ApiControllerDescription()
                {
                    Name = actions.First().ActionDescriptor.ControllerDescriptor.ControllerName,
                    Actions = actions.Select(x => new ApiActionDescription()
                        {
                            Name = x.ActionDescriptor.ActionName,
                            Routes = new List<ApiRouteDescription>() { new ApiRouteDescription() { Method = x.HttpMethod.Method, Path = x.RelativePath.ToLower() }},
                            Summary = JsonConvert.DeserializeObject<JObject>(x.Documentation).Value<string>("summary"),
                            Example = JsonConvert.DeserializeObject<JObject>(x.Documentation).Value<string>("example"),
                            Remarks = JsonConvert.DeserializeObject<JObject>(x.Documentation).Value<string>("remarks"),
                            Returns = JsonConvert.DeserializeObject<JObject>(x.Documentation).Value<string>("returns"),
                            ParameterDescriptions = x.ParameterDescriptions
                        }).ToList()
                };

            this.routes = RouteTable.Routes.OfType<IAttributeRoute>()
                                .Where(x => x.Defaults != null && x.Defaults.Any(c => c.Key.Equals("controller") && c.Value != null && c.Value.ToString().Equals(controller.Name)))
                                .ToList();

            // Add attribute routes
            for (var i = 0; i < controller.Actions.Count(); i++)
            {
                var actionRoutes = this.routes.Where(x => x.Defaults.Any(c => c.Key.Equals("action") && c.Value.ToString() == controller.Actions[i].Name)).ToList();

                if (actionRoutes.Any())
                {
                    var r = actionRoutes.Select(x => new ApiRouteDescription()
                        {
                            Method = x.Constraints.Values.OfType<RestfulHttpMethodConstraint>().First().AllowedMethods.FirstOrDefault(),
                            Path = x.Url
                        });

                    controller.Actions[i].Routes = r.ToList();
                }
            }

            var viewmodel = new ApiExplorerDetailsViewModel()
                           {
                               ApiControllerDescriptions = this.controllers,
                               CurrentAssembly = this.currentAssembly ?? (this.currentAssembly = Assembly.GetAssembly(HttpContext.ApplicationInstance.GetType().BaseType)),
                               ApiControllerDescription = controller
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
                ApiControllerDescriptions = this.controllers,
                CurrentAssembly = this.currentAssembly ?? (this.currentAssembly = Assembly.GetAssembly(HttpContext.ApplicationInstance.GetType().BaseType))
            };

            return this.View(viewmodel);
        }
    }
}
