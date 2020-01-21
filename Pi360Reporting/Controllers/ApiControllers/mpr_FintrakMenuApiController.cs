using log4net;
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
    [RoutePrefix("api/menu")]
    //[UsesDisposableService]
    public class mpr_FintrakMenuApiController : ApiController   //ApiControllerBase
    {
        //[ImportingConstructor]
        public mpr_FintrakMenuApiController()
        {
        }

        public static readonly ILog appLog = LogManager.GetLogger("File1Appender");
        private MPRContext2 mx2 = new MPRContext2();



        //[HttpGet]
        //[Route("menusubmenu")]
        //// [AUT.MyAuthorizedAttribute(Roles = "systemadmin")]  
        //public HttpResponseMessage sqlList3()
        //{
        //    //var list = new List<mpr_FintrakMenu>();

        //    var newList = new List<mprFinTrackMenuList>();

        //    string query = "SELECT distinct MenuList FROM mpr_FintrakMenu where MenuList is not null or MenuList  <> '' order by MenuList asc";

        //    string CS = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakReportEntities"].ConnectionString;
        //    System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(CS);

        //    try
        //    {
        //        connection.Open();

        //        var menus = mx.Database.SqlQuery<string>(query);

        //        foreach (var item in menus)
        //        {
        //            var rp = mx.mpr_FintrakMenuSet.Where(x => x.MenuList == item).Select(y => y.ReportPAth).ToList();

        //            var dr = new mprFinTrackMenuList()
        //            {
        //                reportpath = rp,
        //                menulist = item
        //            };

        //            newList.Add(dr);
        //        }
        //        connection.Close();
        //        connection.Dispose();
        //    }

        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    finally
        //    {
        //        connection.Close();
        //        connection.Dispose();
        //    }
        //    // return Json(newList, JsonRequestBehavior.AllowGet);
        //    return Request.CreateResponse(HttpStatusCode.OK, newList);
        //}


        [HttpGet]
        [Route("menusubmenu")]
        public HttpResponseMessage MenuSubMenu(HttpRequestMessage request)
        {
            HttpResponseMessage res = null;

            var menus = new Mpr_FintrakMenuRepository();

            appLog.InfoFormat("About to call Getmpr_FintrakMenuList() method.");
            IEnumerable<mprFintrakMenu_ObjectListInfo> menusDataList = menus.Getmpr_FintrakMenuList();
            appLog.InfoFormat("Getmpr_FintrakMenuList() method successfully called.");

            res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, menusDataList);

            return res;
        }

        [HttpGet]
        [Route("menusubmenubylevelcode/{finallevelcode}")]
        public HttpResponseMessage MenuSubMenu(HttpRequestMessage request, string finallevelcode)
        {
            HttpResponseMessage res = null;

            var menus = new Mpr_FintrakMenuRepository();

            appLog.InfoFormat("About to call Getmpr_FintrakMenuList(finallevelcode) method.");
            IEnumerable<mprFintrakMenu_ObjectListInfo> menusDataList = menus.Getmpr_FintrakMenuList(finallevelcode);
            appLog.InfoFormat("Getmpr_FintrakMenuList(finallevelcode) method successfully called.");

            res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, menusDataList);

            return res;
        }

        [HttpGet]
        [Route("getmenuobject/{searchvalue}")]
        public HttpResponseMessage GetMenuObj(HttpRequestMessage request, string searchvalue)
        {
            HttpResponseMessage res = null;

            var menu = new Mpr_FintrakMenuRepository();

            appLog.InfoFormat("About to call GetFIntrakMenuOBJ(searchvalue) method.");
            mpr_FintrakMenu menuDataObj = menu.GetFIntrakMenuOBJ(searchvalue);
            appLog.InfoFormat("GetFIntrakMenuOBJ(searchvalue) method successfully called.");

            res = request.CreateResponse(HttpStatusCode.OK, menuDataObj);

            return res;
        }

        [HttpGet]
        [Route("mobility")]
        public HttpResponseMessage GetMobilityMenu(HttpRequestMessage request)
        {
            HttpResponseMessage res = null;

            var menu = new Mpr_FintrakMenuRepository();

            appLog.InfoFormat("About to call MobilityMth() method.");
            List<mpr_FintrakMenu> menuDataObj = menu.MobilityMth().ToList();
            appLog.InfoFormat("MobilityMth() method successfully called.");

            res = request.CreateResponse(HttpStatusCode.OK, menuDataObj);

            return res;
        }

        [HttpGet]
        [Route("echannel")]
        public HttpResponseMessage GeteChannelMenu(HttpRequestMessage request)
        {
            HttpResponseMessage res = null;

            var menu = new Mpr_FintrakMenuRepository();

            appLog.InfoFormat("About to call GeteChannelMenu() method.");
            List<mpr_FintrakMenu> menuDataObj = menu.EchannelMth().ToList();
            appLog.InfoFormat("GeteChannelMenu() method successfully called.");

            res = request.CreateResponse(HttpStatusCode.OK, menuDataObj);

            return res;
        }

        protected override void Dispose(bool disposing)
        {
            mx2.Dispose();
            base.Dispose(disposing);
        }
    }
}