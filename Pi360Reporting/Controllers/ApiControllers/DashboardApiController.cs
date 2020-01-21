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
    //[Adapters.CustomActionFilter]
    [RoutePrefix("api/dashboard")]
    //[UsesDisposableService]
    public class DashboardApiController : ApiController   //ApiControllerBase
    {
        //[ImportingConstructor]
        public DashboardApiController()
        {
        }

        private MPRContext2 mx2 = new MPRContext2();       

        [HttpGet]
        [Route("dashboardmix/{param}")]
        public HttpResponseMessage DashboardMix(HttpRequestMessage request, string param)
        {
            HttpResponseMessage res = null;

            var dashboardRepository = new DashboardRepository();

            IEnumerable<ChartMixInfo> dashboardList = dashboardRepository.DashboardMixMtd(param);


            res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, dashboardList);

            return res;
        }

        [HttpGet]
        [Route("depositbycurrency/{param}")]
        public HttpResponseMessage DepositByCurrency(HttpRequestMessage request, string param)
        {
            HttpResponseMessage res = null;

            var dashboardRepository = new DashboardRepository();

            IEnumerable<ChartMixInfo> dashboardList = dashboardRepository.DashboardMixMtd(param);

            SubCaptionByCurrencyInfo obj = new SubCaptionByCurrencyInfo();
            List<SubCaptionByCurrencyInfo> ls = new List<SubCaptionByCurrencyInfo>();

            string st = "fcy";

            foreach (var v in dashboardList)
            {
                //if(v.SubCaption.ToLower().Contains("fcy"))
                if (v.SubCaption.ToLower().Contains(st.ToLower()))
                {
                    obj.fcy = v.SubCaption;
                    obj.Amount = v.Amount;
                }
                else
                {
                    obj.lcy = v.SubCaption;
                    obj.Amount = v.Amount;
                }
                ls.Add(obj);
            }


            res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, ls);

            return res;
        }

        [HttpGet]
        [Route("dashboardtrend/{param}")]
        public HttpResponseMessage DashboardTrend(HttpRequestMessage request, string param)
        {
            HttpResponseMessage res = null;

            var dashboardRepository = new DashboardRepository();

            IEnumerable<SubCaptionDashboardTrendListInfo> dashboardList = dashboardRepository.DashboardTrendMtd(param);


            res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, dashboardList);

            return res;
        }

        [HttpGet]
        [Route("dashboardtrend2/{param}")]
        public HttpResponseMessage DashboardTrend2(HttpRequestMessage request, string param)
        {
            HttpResponseMessage res = null;

            var dashboardRepository = new DashboardRepository();

            IEnumerable<DashboardMainCaptionListInfo> dashboardList = dashboardRepository.DashboardTrendMtd2(param);


            res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, dashboardList);

            return res;
        }

        [HttpGet]
        [Route("dashboardcards/{param}")]
        public HttpResponseMessage DashboardCards(HttpRequestMessage request, string param)
        {
            HttpResponseMessage res = null;

            var dashboardRepository = new DashboardRepository();

            IEnumerable<DashboardMainCaptionCardsListInfo> dashboardList = dashboardRepository.DashboardMainCaptionCardsMtd(param);

            res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, dashboardList);

            return res;
        }

        [HttpGet]
        [Route("dashboardcardmix")]
        public HttpResponseMessage DashboardCardMix(HttpRequestMessage request)
        {
            HttpResponseMessage res = null;

            var dashboardcardRepository = new DashboardRepository();

            IEnumerable<DashboardMainCaptionListInfo> dashboardcardList = dashboardcardRepository.DashboardCardMainCaptionMtd();


            res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, dashboardcardList);

            return res;
        }
        protected override void Dispose(bool disposing)
        {
            mx2.Dispose();
            base.Dispose(disposing);
        }
    }
}