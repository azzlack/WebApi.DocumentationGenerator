namespace WebApi.DocumentationController.ViewModels
{
    using System.Collections.Generic;
    using System.Web.Http.Controllers;
    using System.Web.Http.Description;

    using AttributeRouting.Framework;

    /// <summary>
    /// Viewmodel for the detailed API view.
    /// </summary>
    public class ApiExplorerDetailsViewModel : ApiExplorerViewModel
    {
        /// <summary>
        /// Gets or sets the controller descriptor.
        /// </summary>
        /// <value>
        /// The controller descriptor.
        /// </value>
        public HttpControllerDescriptor ControllerDescriptor { get; set; }

        /// <summary>
        /// Gets or sets the controller API descriptions.
        /// </summary>
        /// <value>
        /// The controller API descriptions.
        /// </value>
        public IEnumerable<ApiDescription> ControllerApiDescriptions { get; set; }

        /// <summary>
        /// Gets or sets the attribute routes.
        /// </summary>
        /// <value>
        /// The attribute routes.
        /// </value>
        public IEnumerable<IAttributeRoute> AttributeRoutes { get; set; }
    }
}