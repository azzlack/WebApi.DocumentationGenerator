namespace WebApi.DocumentationController.Ninject
{
    using System.Web.Http.Description;

    using WebApi.DocumentationController.DocumentationProviders;

    using global::Ninject.Modules;

    /// <summary>
    /// Module for wiring up documentation providers
    /// </summary>
    public class XmlDocumentationModule : NinjectModule
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
