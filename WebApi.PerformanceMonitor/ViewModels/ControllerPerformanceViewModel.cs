namespace WebApi.PerformanceMonitor.ViewModels
{
    using System.Collections.Generic;
    using System.Web.Http.Controllers;
    using System.Web.Http.Description;

    using AttributeRouting.Framework;

    /// <summary>
    /// Viewmodel for the detailed API view.
    /// </summary>
    public class ControllerPerformanceViewModel : OverallPerformanceViewModel
    {
        /// <summary>
        /// Gets or sets the controller descriptor.
        /// </summary>
        /// <value>
        /// The controller descriptor.
        /// </value>
        public HttpControllerDescriptor ControllerDescriptor { get; set; }
    }
}