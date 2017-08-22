using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Netmefy.Api.Controllers.api
{

    public class ispController : ApiController
    {

        [HttpPut]
        public IHttpActionResult rate(int id, int rate)
        {
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
