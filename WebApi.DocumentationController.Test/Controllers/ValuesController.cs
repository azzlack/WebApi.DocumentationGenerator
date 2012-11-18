using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.DocumentationController.Test.Controllers
{
    using AttributeRouting.Web.Http;

    using WebApi.DocumentationController.Models;
    using WebApi.DocumentationController.Test.Models;

    public class ValuesController : ApiController
    {
        /// <summary>
        /// Gets all stored strings.
        /// </summary>
        /// <example>api/values</example>
        /// <returns>A list of strings.</returns>
        public IEnumerable<string> Get()
        {
            return new[] { "value1", "value2" };
        }

        /// <summary>
        /// Gets the <see cref="SampleResponseType"/> with the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <example>api/values/5</example>
        /// <returns>A single string.</returns>
        [ApiReturnType(typeof(SampleResponseType))]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public HttpResponseMessage Post([FromBody] SampleRequestType value)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        [POST("api/values/{id}/value")]
        public string PostAddValue(string id, [FromBody] string value)
        {
            return value;
        }

        [GET("api/values/search/{searchString}")]
        public string GetSearch(string searchString)
        {
            return searchString;
        }
    }
}