namespace WebApi.PerformanceMonitor.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Web.Http.Description;

    /// <summary>
    /// Viewmodel for the general API view.
    /// </summary>
    public class OverallPerformanceViewModel
    {
        /// <summary>
        /// Gets or sets the API descriptions.
        /// </summary>
        /// <value>
        /// The API descriptions.
        /// </value>
        public Collection<ApiDescription> ApiDescriptions { get; set; }
    }
}