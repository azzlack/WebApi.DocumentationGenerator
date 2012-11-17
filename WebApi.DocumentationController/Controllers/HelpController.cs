﻿namespace WebApi.DocumentationController.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Routing;

    using AttributeRouting.Framework;

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
                    .GroupBy(x => x.ActionDescriptor.ControllerDescriptor.ControllerName)
                    .First(x => x.Key.Equals(id, StringComparison.InvariantCultureIgnoreCase));

            var concatParams = actions.GroupBy(x => new { x.ActionDescriptor.ActionName, ConcatParameterNames = string.Join(",", x.ParameterDescriptions.Select(p => p.Name)) });

            var controller = new ApiControllerDescription()
                {
                    Name = actions.First().ActionDescriptor.ControllerDescriptor.ControllerName,
                    Actions = actions.GroupBy(x => new { x.ActionDescriptor.ActionName, ConcatParameterNames = string.Join(",", x.ParameterDescriptions.Select(p => p.Name)) }).Select(x => new ApiActionDescription(x)).OrderBy(a => a.Name).ToList()
                };

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
