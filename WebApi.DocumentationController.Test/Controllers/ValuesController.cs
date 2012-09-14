using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.DocumentationController.Test.Controllers
{
    using AttributeRouting.Web.Http;

    public class ValuesController : ApiController
    {
        /// <summary>
        /// Gets all stored strings.
        /// </summary>
        /// <example>api/values</example>
        /// <returns>A list of strings.</returns>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// Gets the string with the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <example>api/values/5</example>
        /// <returns>A single string.</returns>
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
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
        public string AddValue([FromBody] string value)
        {
            return value;
        }

        [POST("api/values/{id}/{value}")]
        public string PostAddValue(string value)
        {
            return value;
        }

        [GET("api/values/search/{searchString}")]
        public string Search(string searchString)
        {
            return searchString;
        }
    }
}