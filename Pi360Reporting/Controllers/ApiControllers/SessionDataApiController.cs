using MPR.Report.Core;
using MPR.Report.Core.Entities;
using MPR.Report.Data.Business;
using MPR.Report.Data.Business.DataRepositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Pi360Reporting.Controllers.ApiControllers
{
    //[Export]
    //[PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/sessiondata")]
    //[UsesDisposableService]
    public class SessionDataApiController : ApiController   //ApiControllerBase
    {
        //[ImportingConstructor]
        public SessionDataApiController()
        {
        }

        private MPRContext2 mx2 = new MPRContext2();

        
        [HttpGet]
        [Route("sessionvariables")]
        public HttpResponseMessage SessionVar(HttpRequestMessage request)
        {
            HttpResponseMessage res = null;

            var sessiondatarepository = new SessionDataRepository();

            SessionVariableInfo sessiondataObj = sessiondatarepository.SessionDataMtd();

            res = request.CreateResponse(HttpStatusCode.OK, sessiondataObj);

            return res;            
        }

       
        protected override void Dispose(bool disposing)
        {
            mx2.Dispose();
            base.Dispose(disposing);
        }
    }
}