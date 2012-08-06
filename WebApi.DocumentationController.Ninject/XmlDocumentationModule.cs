namespace Ninject.Web.WebApi.DocumentationController
{
    using System.Web.Http.Description;

    using global::WebApi.DocumentationController.DocumentationProviders;

    /// <summary>
    /// Module for wiring up documentation providers
    /// </summary>
    public class XmlDocumentationModule : Ninject.Modules.NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            this.Kernel.Bind<IDocumentationProvider>().To<XmlCommentDocumentationProvider>();
        }
    }
}
