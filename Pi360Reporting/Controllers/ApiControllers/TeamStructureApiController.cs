using MPR.Report.Core;
using MPR.Report.Core.Entities;
using MPR.Report.Data.Business;
using MPR.Report.Data.Business.DataRepositories;
using Pi360Reporting.Models.AccountModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Pi360Reporting.Controllers.ApiControllers
{
    //[Export]
    //[PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/teamstructure")]
    //[UsesDisposableService]
    public class TeamStructureApiController : ApiController   //ApiControllerBase
    {
        //[ImportingConstructor]
        public TeamStructureApiController()
        {
        }

        private MPRContext2 mx2 = new MPRContext2();
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

        //======================== for Keystone Bank starts =================================================================================

        [HttpGet]
        [Route("teamstructurebymiscodelevelyear")]
        public HttpResponseMessage GetTeamStructureUsinggGeneratedmiscodelevelyear(HttpRequestMessage request)
        {
            HttpResponseMessage res = null;
            
            var teamstructure = new TeamStructureRepository();
            //var teamStructureDataList = new List<TeamStructureData>();

            IEnumerable<TeamStructureData> teamStructureDataList = teamstructure.GetTeamStructureByMISCodeLevelYear();

            res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, teamStructureDataList);

            return res;           
        }

        [HttpGet]
        [Route("teamstructurebyselection/{selectedcode}/{selectedyear}/{selectedlevel}/{selectedmisname}")]
        public HttpResponseMessage GetTeamStructureBySelection(HttpRequestMessage request, string selectedcode, int selectedyear, string selectedlevel, string selectedmisname)
        {
            HttpResponseMessage res = null;
            System.Web.HttpContext.Current.Session["session_currentlyselectedmiscode"] = selectedcode;
            System.Web.HttpContext.Current.Session["session_currentlyselectedlevel"] = selectedlevel;
            System.Web.HttpContext.Current.Session["session_currentlyselectedmisname"] = selectedmisname;

            var teamstructure = new TeamStructureRepository();

            IEnumerable<TeamStructureData> teamStructureDataList = teamstructure.GetTeamStructureBySelectedMisCodeAndYear(selectedcode, selectedyear);

            res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, teamStructureDataList);

            return res;
        }


        //////[HttpGet]
        //////[Route("teamstructurebymiscode/{searchvalue}")]
        //////public HttpResponseMessage GetTeamStructureByMISCode(HttpRequestMessage request, string searchvalue)
        //////{
        //////    HttpResponseMessage res = null;          

        //////    var teamstructure = new TeamStructureRepository();

        //////    IEnumerable<TeamStructureData> teamStructureDataList = teamstructure.GetSelectedTeamStructureMisCode(searchvalue);


        //////    res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, teamStructureDataList);

        //////    return res;            
        //////}

        //======================== for Keystone Bank ends =================================================================================

        //======================== for Wema Bank starts =================================================================================

        [HttpGet]
        [Route("teamstructurebymiscodelevelyearWMB")]
        public HttpResponseMessage GetTeamStructureUsinggGeneratedmiscodelevelyearWMB(HttpRequestMessage request)
        {
            HttpResponseMessage res = null;

            var teamstructure = new TeamStructureWMBRepository();

            IEnumerable<TeamStructureData> teamStructureDataList = teamstructure.GetTeamStructureByMISCodeLevelYear();

            res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, teamStructureDataList);

            return res;
        }

        [HttpGet]
        [Route("teamstructurebyselectionWMB/{selectedcode}/{selectedyear}/{selectedlevel}/{selectedmisname}")]
        public HttpResponseMessage GetTeamStructureBySelectionWMB(HttpRequestMessage request, string selectedcode, int selectedyear, string selectedlevel, string selectedmisname)
        {
            HttpResponseMessage res = null;
            System.Web.HttpContext.Current.Session["session_currentlyselectedmiscode"] = selectedcode;
            System.Web.HttpContext.Current.Session["session_currentlyselectedlevel"] = selectedlevel;
            System.Web.HttpContext.Current.Session["session_currentlyselectedmisname"] = selectedmisname;

            var teamstructure = new TeamStructureWMBRepository();

            IEnumerable<TeamStructureData> teamStructureDataList = teamstructure.GetTeamStructureBySelectedMisCodeAndYear(selectedcode, selectedyear);

            res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, teamStructureDataList);

            return res;
        }

        //======================== for Wema Bank ends ===================================================================================

        //======================== for Access Bank starts =================================================================================

        [HttpGet]
        [Route("teamstructurebymiscodelevelyearABP")]
        public HttpResponseMessage GetTeamStructureUsinggGeneratedmiscodelevelyearABP(HttpRequestMessage request)
        {
            HttpResponseMessage res = null;

            var teamstructure = new TeamStructureABPRepository();

            IEnumerable<TeamStructureData> teamStructureDataList = teamstructure.GetTeamStructureByMISCodeLevelYear();

            res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, teamStructureDataList);

            return res;
        }

        [HttpGet]
        [Route("teamstructurebyselectionABP/{selectedcode}/{selectedyear}/{selectedlevel}/{selectedmisname}")]
        public HttpResponseMessage GetTeamStructureBySelectionABP(HttpRequestMessage request, string selectedcode, int selectedyear, string selectedlevel, string selectedmisname)
        {
            HttpResponseMessage res = null;
            System.Web.HttpContext.Current.Session["session_currentlyselectedmiscode"] = selectedcode;
            System.Web.HttpContext.Current.Session["session_currentlyselectedlevel"] = selectedlevel;
            System.Web.HttpContext.Current.Session["session_currentlyselectedmisname"] = selectedmisname;

            var teamstructure = new TeamStructureABPRepository();

            IEnumerable<TeamStructureData> teamStructureDataList = teamstructure.GetTeamStructureBySelectedMisCodeAndYear(selectedcode, selectedyear);

            res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, teamStructureDataList);

            return res;
        }

        //======================== for Access Bank ends ===================================================================================

        //======================== for LBIC starts =================================================================================

        [HttpGet]
        [Route("teamstructurebymiscodelevelyearLBIC")]
        public HttpResponseMessage GetTeamStructureUsinggGeneratedmiscodelevelyearLBIC(HttpRequestMessage request)
        {
            HttpResponseMessage res = null;

            var teamstructure = new TeamStructureLBICRepository();

            IEnumerable<TeamStructureData> teamStructureDataList = teamstructure.GetTeamStructureByMISCodeLevelYear();

            res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, teamStructureDataList);

            return res;
        }

        [HttpGet]
        [Route("teamstructurebyselectionLBIC/{selectedcode}/{selectedyear}/{selectedlevel}/{selectedmisname}")]
        public HttpResponseMessage GetTeamStructureBySelectionLBIC(HttpRequestMessage request, string selectedcode, int selectedyear, string selectedlevel, string selectedmisname)
        {
            HttpResponseMessage res = null;
            System.Web.HttpContext.Current.Session["session_currentlyselectedmiscode"] = selectedcode;
            System.Web.HttpContext.Current.Session["session_currentlyselectedlevel"] = selectedlevel;
            System.Web.HttpContext.Current.Session["session_currentlyselectedmisname"] = selectedmisname;

            var teamstructure = new TeamStructureLBICRepository();

            IEnumerable<TeamStructureData> teamStructureDataList = teamstructure.GetTeamStructureBySelectedMisCodeAndYear(selectedcode, selectedyear);

            res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, teamStructureDataList);

            return res;
        }

        //======================== for LBIC ends ===================================================================================



        //[HttpGet]
        //[Route("latestyearandperiod")]
        //public HttpResponseMessage LatestYearPeriod(HttpRequestMessage request)
        //{
        //    HttpResponseMessage res = null;

        //    var yearperiod = new TeamStructureRepository();

        //    YearPeriodData yearperiodData = yearperiod.GetLatestYearAndPeriod();

        //    res = request.CreateResponse(HttpStatusCode.OK, yearperiodData);

        //    return res;
        //}


        [HttpGet]
        [Route("latestyearandperiod")]
        public HttpResponseMessage LatestYearPeriod(HttpRequestMessage request)
        {
            HttpResponseMessage res = null;

            //var yearperiod = new TeamStructureRepository();
            YearPeriodData yearperiodData = new YearPeriodData();

            yearperiodData.Year = Convert.ToInt32(System.Web.HttpContext.Current.Session["latestyear"]);
            yearperiodData.Period = Convert.ToInt32(System.Web.HttpContext.Current.Session["currentperiod"]);

            res = request.CreateResponse(HttpStatusCode.OK, yearperiodData);

            return res;
        }

        [HttpGet]
        [Route("years")]
        public HttpResponseMessage Years(HttpRequestMessage request)
        {
            HttpResponseMessage res = null;

            var newList = new List<YearModel>();
            //List<int> newList = new List<int>();

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_years", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var yearObj = new YearModel();

                    //int year = reader["year"] != DBNull.Value ? Convert.ToInt32(reader["year"]) : 0;

                    //yearObj.value = reader["year"] != DBNull.Value ? Convert.ToInt32(reader["year"]) : 0;
                    //yearObj.name = reader["year"] != DBNull.Value ? Convert.ToInt32(reader["year"]) : 0;

                    yearObj.value = reader["year"] != DBNull.Value ? Convert.ToString(reader["year"]) : null;
                    yearObj.name = reader["year"] != DBNull.Value ? Convert.ToString(reader["year"]) : null;

                    newList.Add(yearObj);
                }
                con.Close();
            }  // using               


            res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, newList);

            return res;
        }

        //====================== the below are not used ==========================================================================

        //[HttpGet]
        //[Route("teamstructurebyselection/{dir}/{reg}/{div}/{brh}/{acct}")]
        //public HttpResponseMessage GetTeamStructureBySelection(HttpRequestMessage request, string dir, string reg, string div, string brh, string acct)
        //{
        //    HttpResponseMessage res = null;

        //    var teamstructure = new TeamStructureRepository();
        //    //var teamStructureDataList = new List<TeamStructureData>();

        //    IEnumerable<Models.DataInfo.TeamStructureData> teamStructureDataList = teamstructure.GetSelectedTeamStructureByMISCodeLevelYear(dir, reg, div, brh, acct);

        //    res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, teamStructureDataList);

        //    return res;
        //}


        ////////=============== the part below is not used =====================================================================================
        //////[HttpGet]
        //////[Route("regionbydirectorate/{reg_dir}")]
        //////public HttpResponseMessage GetRegionbyDirectorate(HttpRequestMessage request, string reg_dir)
        //////{
        //////    HttpResponseMessage res = null;

        //////    var teamstructure = new TeamStructureRepository();

        //////    //IEnumerable<Models.DataInfo.TeamStructureData> regionDataList = teamstructure.GetRegByDir(reg_div);
        //////    IEnumerable<RegionData> regionDataList = teamstructure.GetRegByDir(reg_dir);


        //////    res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, regionDataList);

        //////    return res;
        //////}

        //////[HttpGet]
        //////[Route("divisionbyregion/{div_reg}")]
        //////public HttpResponseMessage GetRegionbyDivision(HttpRequestMessage request, string div_reg)
        //////{
        //////    HttpResponseMessage res = null;

        //////    var teamstructure = new TeamStructureRepository();

        //////    //IEnumerable<Models.DataInfo.TeamStructureData> regionDataList = teamstructure.GetRegByDir(reg_div);
        //////    IEnumerable<DivisionData> divisionDataList = teamstructure.GetDivByReg(div_reg);


        //////    res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, divisionDataList);

        //////    return res;
        //////}

        //////[HttpGet]
        //////[Route("branchbydivision/{brh_div}")]
        //////public HttpResponseMessage GetBranchbyDivision(HttpRequestMessage request, string brh_div)
        //////{
        //////    HttpResponseMessage res = null;

        //////    var teamstructure = new TeamStructureRepository();

        //////    //IEnumerable<Models.DataInfo.TeamStructureData> regionDataList = teamstructure.GetRegByDir(reg_div);
        //////    IEnumerable<BranchData> branchDataList = teamstructure.GetBrhByDiv(brh_div);


        //////    res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, branchDataList);

        //////    return res;
        //////}

        //////[HttpGet]
        //////[Route("acctbybrh/{acct_brh}")]
        //////public HttpResponseMessage GetAcctByBrh(HttpRequestMessage request, string acct_brh)
        //////{
        //////    HttpResponseMessage res = null;

        //////    var teamstructure = new TeamStructureRepository();

        //////    //IEnumerable<Models.DataInfo.TeamStructureData> regionDataList = teamstructure.GetRegByDir(reg_div);
        //////    IEnumerable<AccountOfficerData> acctofficerDataList = teamstructure.GetAcctByBrh(acct_brh);


        //////    res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, acctofficerDataList);

        //////    return res;
        //////}

        ////////=============== the part above is not used =====================================================================================

       

        protected override void Dispose(bool disposing)
        {
            mx2.Dispose();
            base.Dispose(disposing);
        }
    }
}