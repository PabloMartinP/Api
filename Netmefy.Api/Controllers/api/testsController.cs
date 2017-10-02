using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Netmefy.Data;

namespace Netmefy.Api.Controllers.api
{
    public class testsController : ApiController
    {
        private NETMEFYEntities db = new NETMEFYEntities();

        // GET: api/tests/5
        [ResponseType(typeof(List<Models.testsModel>))]
        public IHttpActionResult GetTests(int id)
        {
            List<bt_tests> tests = db.bt_tests.Where(x => x.cliente_sk == id).ToList();
            if (tests == null)
            {
                return NotFound();
            }

            List<Models.testsModel> modelTests = Models.testsModel.ListConvertTo(tests);

            return Ok(modelTests);
        }
        
        // POST: api/tests
        [ResponseType(typeof(Models.testsModel))]
        public IHttpActionResult PostTest(Models.testsModel test)
        {
            bt_tests testBD = Models.testsModel.ConvertToBD(test);

            db.bt_tests.Add(testBD);
            db.SaveChanges();

            test.test_id = testBD.test_id;
            
            return CreatedAtRoute("DefaultApi", new { id = test.test_id }, test);
        }
        
    }
}