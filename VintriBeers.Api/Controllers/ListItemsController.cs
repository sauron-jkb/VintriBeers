using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace VintriBeers.Api.Controllers
{
    public class ListItemsController : ApiController
    {
        // GET: api/ListItems
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ListItems/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ListItems
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ListItems/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ListItems/5
        public void Delete(int id)
        {
        }
    }
}
