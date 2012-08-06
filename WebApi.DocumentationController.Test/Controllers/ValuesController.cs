using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.DocumentationController.Test.Controllers
{
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
        public void Post(string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}