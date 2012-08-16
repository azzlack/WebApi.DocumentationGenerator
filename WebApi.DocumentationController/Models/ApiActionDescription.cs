namespace WebApi.DocumentationController.Models
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Description;

    /// <summary>
    /// Documentation for an <see cref="ApiController"/> action
    /// </summary>
    public class ApiActionDescription
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the paths.
        /// </summary>
        /// <value>
        /// The paths.
        /// </value>
        public IList<ApiRouteDescription> Routes { get; set; }

        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        /// <value>
        /// The summary.
        /// </value>
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the example.
        /// </summary>
        /// <value>
        /// The example.
        /// </value>
        public string Example { get; set; }

        /// <summary>
        /// Gets or sets the remarks.
        /// </summary>
        /// <value>
        /// The remarks.
        /// </value>
        public string Remarks { get; set; }

        /// <summary>
        /// Gets or sets the returns.
        /// </summary>
        /// <value>
        /// The returns.
        /// </value>
        public string Returns { get; set; }

        /// <summary>
        /// Gets or sets the parameter descriptions.
        /// </summary>
        /// <value>
        /// The parameter descriptions.
        /// </value>
        public IEnumerable<ApiParameterDescription> ParameterDescriptions { get; set; }
    }
}