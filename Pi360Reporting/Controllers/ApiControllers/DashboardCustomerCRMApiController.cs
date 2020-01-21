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
   // [Adapters.CustomActionFilter]
    [RoutePrefix("api/customercrm")]
    //[UsesDisposableService]
    public class DashboardCustomerCRMApiController : ApiController   //ApiControllerBase
    {
        //[ImportingConstructor]
        public DashboardCustomerCRMApiController()
        {
        }

        private MPRContext2 mx2 = new MPRContext2();

        [HttpGet]
        [Route("ccrm/{param}")]
        public HttpResponseMessage CCRM(HttpRequestMessage request, string param)
        {
            HttpResponseMessage res = null;
           
            var dashboardccrmRepository = new DashboardCustomerCRMRepository();

            IEnumerable<customerCRM> ccrm = dashboardccrmRepository.DashboardCustomerCRMMtd(param);


            res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, ccrm);

            return res;
        }

        
        protected override void Dispose(bool disposing)
        {
            mx2.Dispose();
            base.Dispose(disposing);
        }

    }
    }