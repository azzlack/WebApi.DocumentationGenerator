namespace WebApi.DocumentationController.DocumentationProviders
{
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Http.Controllers;
    using System.Web.Http.Description;
    using System.Xml.XPath;

    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Documentation provider for XML comments.
    /// </summary>
    public class XmlCommentDocumentationProvider : IDocumentationProvider
    {
        /// <summary>
        /// XPath expression for finding methods.
        /// </summary>
        private const string MethodExpression = "/doc/members/member[@name='M:{0}']";

        /// <summary>
        /// Regular expression for determining nullable types.
        /// </summary>
        private static readonly Regex NullableTypeNameRegex = new Regex(@"(.*\.Nullable)" + Regex.Escape("`1[[") + "([^,]*),.*");

        /// <summary>
        /// The xml document navigator.
        /// </summary>
        private readonly XPathNavigator documentNavigator;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlCommentDocumentationProvider" /> class.
        /// </summary> 
        public XmlCommentDocumentationProvider()
        {
            var assemblyname = Assembly.GetCallingAssembly().FullName;

            var path = HttpContext.Current.Server.MapPath("~/bin/" + assemblyname + ".xml");

            var xpath = new XPathDocument(path);

            this.documentNavigator = xpath.CreateNavigator();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlCommentDocumentationProvider" /> class.
        /// </summary>
        /// <param name="documentPath">The document path.</param>
        public XmlCommentDocumentationProvider(string documentPath)
        {
            var path = HttpContext.Current.Server.MapPath(documentPath);

            var xpath = new XPathDocument(path);

            this.documentNavigator = xpath.CreateNavigator();
        }

        /// <summary>
        /// Gets the action documentation.
        /// </summary>
        /// <param name="actionDescriptor">The action descriptor.</param>
        /// <returns>The documentation for the action as JSON.</returns>
        public string GetDocumentation(HttpActionDescriptor actionDescriptor)
        {
            var memberNode = this.GetMemberNode(actionDescriptor);

            var documentation = new JObject()
                                        {
                                            { "summary", string.Empty },
                                            { "example", string.Empty },
                                            { "remarks", string.Empty },
                                            { "returns", string.Empty }
                                        };

            if (memberNode != null)
            {
                var summaryNode = memberNode.SelectSingleNode("summary");
                var exampleNode = memberNode.SelectSingleNode("example");
                var remarksNode = memberNode.SelectSingleNode("remarks");
                var returnsNode = memberNode.SelectSingleNode("returns");
                
                if (summaryNode != null)
                {
                    documentation["summary"] = summaryNode.Value.Trim();
                }

                if (exampleNode != null)
                {
                    documentation["example"] = exampleNode.Value.Trim();
                }

                if (remarksNode != null)
                {
                    documentation["remarks"] = remarksNode.Value.Trim();
                }

                if (returnsNode != null)
                {
                    documentation["returns"] = returnsNode.Value.Trim();
                }

                return documentation.ToString();
            }

            documentation["summary"] = "No Documentation Found.";
            
            return documentation.ToString();
        }

        /// <summary>
        /// Gets the parameter documentation.
        /// </summary>
        /// <param name="parameterDescriptor">The parameter descriptor.</param>
        /// <returns>The summary documentation for the parameter as a string.</returns>
        public string GetDocumentation(HttpParameterDescriptor parameterDescriptor)
        {
            var reflectedParameterDescriptor = parameterDescriptor as ReflectedHttpParameterDescriptor;

            if (reflectedParameterDescriptor != null)
            {
                var memberNode = this.GetMemberNode(reflectedParameterDescriptor.ActionDescriptor);

                if (memberNode != null)
                {
                    var parameterName = reflectedParameterDescriptor.ParameterInfo.Name;
                    var parameterNode = memberNode.SelectSingleNode(string.Format("param[@name='{0}']", parameterName));

                    if (parameterNode != null)
                    {
                        return parameterNode.Value.Trim();
                    }
                }
            }

            return "No Documentation Found.";
        }

        /// <summary>
        /// Gets the name of the member.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>The member name as a string.</returns>
        private static string GetMemberName(MethodInfo method)
        {
            var name = string.Format("{0}.{1}", method.DeclaringType.FullName, method.Name);
            var parameters = method.GetParameters();

            if (parameters.Length != 0)
            {
                var parameterTypeNames = parameters.Select(param => ProcessTypeName(param.ParameterType.FullName)).ToArray();
                name += string.Format("({0})", string.Join(",", parameterTypeNames));
            }

            return name;
        }

        /// <summary>
        /// Processes the name of the type.
        /// </summary>
        /// <param name="typeName">Name of the type.</param>
        /// <returns>The process type name as a string.</returns>
        private static string ProcessTypeName(string typeName)
        {
            // Handle nullable
            var result = NullableTypeNameRegex.Match(typeName);

            if (result.Success)
            {
                return string.Format("{0}{{{1}}}", result.Groups[1].Value, result.Groups[2].Value);
            }

            return typeName;
        }

        /// <summary>
        /// Gets the member node.
        /// </summary>
        /// <param name="actionDescriptor">The action descriptor.</param>
        /// <returns>The xpath navigator for the member node.</returns>
        private XPathNavigator GetMemberNode(HttpActionDescriptor actionDescriptor)
        {
            var reflectedActionDescriptor = actionDescriptor as ReflectedHttpActionDescriptor;
            
            if (reflectedActionDescriptor != null)
            {
                var selectExpression = string.Format(MethodExpression, GetMemberName(reflectedActionDescriptor.MethodInfo));
                var node = this.documentNavigator.SelectSingleNode(selectExpression);
                
                if (node != null)
                {
                    return node;
                }
            }

            return null;
        }
    }
}