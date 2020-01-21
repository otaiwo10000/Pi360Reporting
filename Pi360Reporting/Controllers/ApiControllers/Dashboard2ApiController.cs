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
    [RoutePrefix("api/dashboard2")]
    //[UsesDisposableService]
    public class Dashboard2ApiController : ApiController   //ApiControllerBase
    {
        //[ImportingConstructor]
        public Dashboard2ApiController()
        {
        }

        private MPRContext2 mx2 = new MPRContext2();

        [HttpGet]
        [Route("dashboard2ratio")]
        public HttpResponseMessage DashboardMix(HttpRequestMessage request)
        {
            HttpResponseMessage res = null;
           
            var dashboard2Repository = new Dashboard2Repository();

            IEnumerable<DashboardMainCaptionListInfo> ratios = dashboard2Repository.DashboardMainCaptionMtd();


            res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, ratios);

            return res;
        }

        [HttpGet]
        [Route("landingpagecards/{valu}")]
        public HttpResponseMessage DashboardCardsMix(HttpRequestMessage request, string valu)
        {
            HttpResponseMessage res = null;

            var dashboard2Repository = new Dashboard2Repository();

            IEnumerable<DashboardMainCaptionListInfo> cards = dashboard2Repository.DashboardMainCaptionCardsMtd(valu);


            res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, cards);

            return res;
        }

        [HttpGet]
        [Route("subcaptions/{typevalu}/{trendORmonthly}")]
        public HttpResponseMessage DashboardSubCaptionsMix(HttpRequestMessage request, string typevalu, string trendORmonthly )
        {
            HttpResponseMessage res = null;

            var dashboard2Repository = new Dashboard2Repository();

            IEnumerable<SubCaptionTrendListInfo> subcaptions = dashboard2Repository.DashboardSubCaptionMtd(typevalu, trendORmonthly);


            res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, subcaptions);

            return res;
        }

        [HttpGet]
        [Route("subcaptions/{typevalu}")]
        public HttpResponseMessage DashboardSubCaptionsMix(HttpRequestMessage request, string typevalu)
        {
            HttpResponseMessage res = null;

            var dashboard2Repository = new Dashboard2Repository();

            IEnumerable<subcaptions> subcaptions = dashboard2Repository.DashboardSubCaptionMtd(typevalu);


            res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, subcaptions);

            return res;
        }

        [HttpGet]
        [Route("maincaptions/{typevalu}/{trendORmonthly}")]
        public HttpResponseMessage DashboardMainCaptionsMix(HttpRequestMessage request, string typevalu, string trendORmonthly)
        {
            HttpResponseMessage res = null;

            var dashboard2Repository = new Dashboard2Repository();

            IEnumerable<SubCaptionTrendListInfo> subcaptions = dashboard2Repository.DashboardMainCaptionMtd(typevalu, trendORmonthly);


            res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, subcaptions);

            return res;
        }


        protected override void Dispose(bool disposing)
        {
            mx2.Dispose();
            base.Dispose(disposing);
        }

    }
    }