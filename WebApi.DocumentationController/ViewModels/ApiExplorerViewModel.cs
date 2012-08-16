namespace WebApi.DocumentationController.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Reflection;
    using System.Web.Http.Description;

    using WebApi.DocumentationController.Models;

    /// <summary>
    /// Viewmodel for the general API view.
    /// </summary>
    public class ApiExplorerViewModel
    {
        /// <summary>
        /// Gets or sets the API descriptions.
        /// </summary>
        /// <value>
        /// The API descriptions.
        /// </value>
        public IEnumerable<ApiControllerDescription> ApiControllerDescriptions { get; set; }

        /// <summary>
        /// Gets or sets the current assembly.
        /// </summary>
        /// <value>
        /// The current assembly.
        /// </value>
        public Assembly CurrentAssembly { get; set; }
    }
}