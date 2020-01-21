using MPR.Report.Core;
using MPR.Report.Core.Entities;
using MPR.Report.Data.Business;
using MPR.Report.Data.Business.DataRepositories;
using Pi360Reporting.Models.AccountModel;
using Pi360Reporting.OtherMethods;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Pi360Reporting.Controllers.ApiControllers
{
    //[Authorize]
    //[Export]
    //[PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/reportprocedure")]
    //[UsesDisposableService]
    public class ReportProcedureApiController : ApiController   //ApiControllerBase
    {
        //[ImportingConstructor]
        public ReportProcedureApiController()
        {
        }
        MPRContext2 entityContext = new MPRContext2();
        string connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;


        //==================== APRdistinctCaption_ReportInfo starts ================================

        [HttpGet]
        [Route("APRdistinctcaptionreport")]
        public HttpResponseMessage APRCaptionReport(HttpRequestMessage request)
        {
            HttpResponseMessage res = null;

            var newList = new List<APRdistinctCaption_ReportInfo>();

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("APRdistinctCaption_Report", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var cap = new APRdistinctCaption_ReportInfo();

                    //cap.ProductCode = reader["ProductCode"].ToString();
                    //cap.Caption = reader["Caption"].ToString();

                    cap.ProductCode = reader["ProductCode"] != DBNull.Value ? Convert.ToString(reader["ProductCode"]) : "null";
                    cap.Caption = reader["Caption"] != DBNull.Value ? Convert.ToString(reader["Caption"]) : "null";

                    newList.Add(cap);
                }
                con.Close();
            }  // using               


            res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, newList);

            return res;
        }

        //==================== APRdistinctCaption_ReportInfo ends ================================


        // //==================== caption report parameter starts ================================

        [HttpGet]
        [Route("captionreport/{captiontype}")]
        //[Route("teamstructurebyselection/{selectedcode}/{selectedyear}/{selectedlevel}")]
        public HttpResponseMessage CaptionParameter(HttpRequestMessage request, string captiontype)
        {
            HttpResponseMessage res = null;

            var newList = new List<string>();

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("DropDown_Caption", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "CaptionType",
                    Value = captiontype,
                });

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //var cap = reader["Caption"].ToString();
                    var cap = reader["Caption"] != DBNull.Value ? Convert.ToString(reader["Caption"]) : "null";

                    newList.Add(cap);
                }
                con.Close();
            }  // using               


            res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, newList);

            return res;
        }

        // //==================== caption report parameter ends ================================

        ////==================== main caption report parameter starts ================================


        [HttpGet]
        [Route("maincaptionreport/{reporttype}")]
        //[Route("teamstructurebyselection/{selectedcode}/{selectedyear}/{selectedlevel}")]
        public HttpResponseMessage MainCaptionParameter(HttpRequestMessage request, string reporttype)
        {
            HttpResponseMessage res = null;

            //var newList = new List<string>();
            List<ReportProcedureModel> newList = new List<ReportProcedureModel>();

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("DropDown_MainCaption", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "ReportType",
                    Value = reporttype,
                });

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ReportProcedureModel cap = new ReportProcedureModel();
                    //var cap = reader["MainCaption"].ToString();
                    //var cap = reader["MainCaption"] != DBNull.Value ? reader["MainCaption"].ToString() : "null";
                    cap.fiName = reader["MainCaption"] != DBNull.Value ? Convert.ToString(reader["MainCaption"]) : "null";

                    newList.Add(cap);
                }
                con.Close();
            }  // using               


            res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, newList);

            return res;
        }

        ////==================== main caption report parameter ends ================================

        [HttpGet]
        [Route("memo")]
        //[Route("teamstructurebyselection/{selectedcode}/{selectedyear}/{selectedlevel}")]
        public HttpResponseMessage MemoParameter(HttpRequestMessage request)
        {
            HttpResponseMessage res = null;

            var newList = new List<string>();

            string reportlevel = Convert.ToString(System.Web.HttpContext.Current.Session["session_levelcode"]);

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("DropDown_memo", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "ReportLevel",
                    Value = reportlevel,
                });

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //var cap = reader["Unit"].ToString();
                    var cap = reader["Unit"] != DBNull.Value ? Convert.ToString(reader["Unit"]) : "null";

                    newList.Add(cap);
                }
                con.Close();
            }  // using               


            res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, newList);

            return res;
        }

        ////==================== main caption report parameter ends ================================

        ////==================== ppr retail group report parameter ends ================================


        [HttpGet]
        [Route("pprretailgroupreport")]
        public HttpResponseMessage PPRRetailGroupParameter(HttpRequestMessage request)
        {
            HttpResponseMessage res = null;

            var newList = new List<string>();

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("DropDown_PPRRetail_Group", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //var cap = reader["Caption"].ToString();
                    var cap = reader["Caption"] != DBNull.Value ? Convert.ToString(reader["Caption"]) : "null";

                    newList.Add(cap);
                }
                con.Close();
            }  // using               


            res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, newList);

            return res;
        }

        ////==================== ppr retail group report parameter ends ================================


        [HttpGet]
        [Route("ranking/{report}")]
        //[Route("teamstructurebyselection/{selectedcode}/{selectedyear}/{selectedlevel}")]
        public HttpResponseMessage Ranking(HttpRequestMessage request, string report)
        {
            HttpResponseMessage res = null;

            List<Rank_ReportInfo> newList = new List<Rank_ReportInfo>();

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("mpr_get_apr_ranking", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Report",
                    Value = report,
                });

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Rank_ReportInfo cap = new Rank_ReportInfo();
                    //var cap = reader["MainCaption"].ToString();
                    cap.name = reader["name"] != DBNull.Value ? Convert.ToString(reader["name"]) : "null";
                    cap.value = reader["value"] != DBNull.Value ? Convert.ToString(reader["value"]) : "null";

                    newList.Add(cap);
                }
                con.Close();
            }  // using               


            res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, newList);

            return res;
        }

        ////==================== main caption report parameter ends ================================



        ////==================== rundate List starts ================================


        [HttpGet]
        [Route("rundate/{yearstring}")]
        //[Route("teamstructurebyselection/{selectedcode}/{selectedyear}/{selectedlevel}")]
        public HttpResponseMessage RunDate(HttpRequestMessage request, string yearstring)
        {
            HttpResponseMessage res = null;

            //var newList = new List<string>();
            List<rundateModel> newList = new List<rundateModel>();

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("dropdown_dailydate2", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Year",
                    Value = yearstring,
                });

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    rundateModel cap = new rundateModel();
                    //var cap = reader["MainCaption"] != DBNull.Value ? reader["MainCaption"].ToString() : "null";
                    //cap.RunDate = reader["date"] != DBNull.Value ? (Convert.ToDateTime(reader["date"].ToString())).Date : Convert.ToDateTime("19-01-01");
                    DateTime d = reader["date"] != DBNull.Value ? (Convert.ToDateTime(reader["date"].ToString())).Date : Convert.ToDateTime("19-01-01");
                    cap.RunDate = d.ToString("yyyy-MM-dd");
                    //exProg.EstimatedTimeString = progressInfo.Select(x => x.EstimatedTime).FirstOrDefault().ToString("yyyy-MM-dd");

                    newList.Add(cap);
                }
                con.Close();
            }  // using               


            res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, newList);

            return res;
        }

        [HttpGet]
        [Route("rundate2_old/{yearstring}")]
        //[Route("teamstructurebyselection/{selectedcode}/{selectedyear}/{selectedlevel}")]
        public HttpResponseMessage RunDate2_old(HttpRequestMessage request, string yearstring)
        {
            HttpResponseMessage res = null;

            //var newList = new List<string>();
            List<rundateModel> newList = new List<rundateModel>();

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("mpr_volume_analysis_rundates", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Year",
                    Value = yearstring,
                });

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    rundateModel cap = new rundateModel();
                    //var cap = reader["MainCaption"] != DBNull.Value ? reader["MainCaption"].ToString() : "null";
                    ////cap.RunDate = reader["date"] != DBNull.Value ? (Convert.ToDateTime(reader["date"].ToString())).Date : Convert.ToDateTime("19-01-01");
                    //DateTime d = reader["rundate"] != DBNull.Value ? (Convert.ToDateTime(reader["rundate"].ToString())).Date : Convert.ToDateTime("19-01-01");
                    //cap.RunDate = d.ToString("yyyy-MM-dd");

                    DateTime d = reader["rundate"] != DBNull.Value ? (Convert.ToDateTime(reader["rundate"].ToString())).Date : Convert.ToDateTime("19-01-01");
                    cap.RunDate = d.ToString("yyyy-MM-dd");

                    newList.Add(cap);
                }
                con.Close();
            }  // using               


            res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, newList);

            return res;
        }

        [HttpGet]
        [Route("rundate2/{yearstring}")]
        //[Route("teamstructurebyselection/{selectedcode}/{selectedyear}/{selectedlevel}")]
        public HttpResponseMessage RunDate2(HttpRequestMessage request, string yearstring)
        {
            HttpResponseMessage res = null;

            //var newList = new List<string>();
            List<rundateModel> newList = new List<rundateModel>();
            string mislevel = Convert.ToString(System.Web.HttpContext.Current.Session["session_loggedinmislevel"]);

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("mpr_volume_analysis_rundates_v2", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Year",
                    Value = yearstring,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MISCode",
                    Value = mislevel,
                });

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    rundateModel cap = new rundateModel();

                    DateTime d = reader["rundate"] != DBNull.Value ? (Convert.ToDateTime(reader["rundate"].ToString())).Date : Convert.ToDateTime("19-01-01");
                    cap.RunDate = d.ToString("yyyy-MM-dd");


                    newList.Add(cap);
                }
                con.Close();
            }  // using               


            res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, newList);

            return res;
        }

        ////==================== rundate List ends ================================


        ////========================= Status before report can be viewed starts ===========================================

        //[HttpGet]
        //[Route("reportstatus")]
        ////[Route("teamstructurebyselection/{selectedcode}/{selectedyear}/{selectedlevel}")]
        //public HttpResponseMessage ReportStatus(HttpRequestMessage request)
        //{
        //    HttpResponseMessage res = null;

        //    reportStatusModel rStatus = new reportStatusModel();

        //    rStatus.ReportStatus = entityContext.ReportstatusSet.Select(x => x.ReportStatus).FirstOrDefault();
        //    rStatus.ReportStatus = rStatus.ReportStatus.ToUpper();
        //    rStatus.Year = Convert.ToInt32((System.Web.HttpContext.Current.Session["latestyear"]));
        //    rStatus.Period = Convert.ToInt32((System.Web.HttpContext.Current.Session["currentperiod"]));
        //    rStatus.loggedinmiscode = Convert.ToString((System.Web.HttpContext.Current.Session["ProfitCenterMisCode"]));
        //    rStatus.loggedindefcode = Convert.ToString((System.Web.HttpContext.Current.Session["ProfitCenterDefinitionCode"]));

        //    res = request.CreateResponse(HttpStatusCode.OK, rStatus);

        //    return res;
        //}

        //// for single period
        //[HttpGet]
        //[Route("mprreportstatus")]
        ////[Route("teamstructurebyselection/{selectedcode}/{selectedyear}/{selectedlevel}")]
        //public HttpResponseMessage MPRReportStatus(HttpRequestMessage request)
        //{
        //    HttpResponseMessage res = null;

        //    MPRReportStatusModel rStatus = new MPRReportStatusModel();
        //    MPRReportStatus rStatus2 = entityContext.MPRReportStatusSet.FirstOrDefault();

        //    rStatus.Year = rStatus2.Year;
        //    rStatus.Period = rStatus2.Period;
        //    rStatus.ReportStatus = rStatus2.Status.ToUpper().Trim();           
        //    rStatus.loggedinmiscode = Convert.ToString((System.Web.HttpContext.Current.Session["ProfitCenterMisCode"]));
        //    rStatus.loggedindefcode = Convert.ToString((System.Web.HttpContext.Current.Session["ProfitCenterDefinitionCode"]));

        //    res = request.CreateResponse(HttpStatusCode.OK, rStatus);

        //    return res;
        //}

        //for multiple periods
        [HttpGet]
        [Route("mprreportstatus")]
        //[Route("teamstructurebyselection/{selectedcode}/{selectedyear}/{selectedlevel}")]
        public HttpResponseMessage MPRReportStatus(HttpRequestMessage request)
        {
            HttpResponseMessage res = null;

            MPRReportStatusModel rStatus = new MPRReportStatusModel();
            MPRReportStatus rStatus2 = entityContext.MPRReportStatusSet.FirstOrDefault();

            rStatus.Year = rStatus2.Year;
            rStatus.Period = rStatus2.Period;
            rStatus.ReportStatus = rStatus2.Status.ToUpper().Trim();
            rStatus.loggedinmiscode = Convert.ToString((System.Web.HttpContext.Current.Session["ProfitCenterMisCode"]));
            rStatus.loggedindefcode = Convert.ToString((System.Web.HttpContext.Current.Session["ProfitCenterDefinitionCode"]));

            res = request.CreateResponse(HttpStatusCode.OK, rStatus);

            return res;
        }

        ////==================== rundate List ends ================================

        //========================= Status before report can be viewed ends ===================================


        ////==================== scorecard metric starts ================================


        [HttpGet]
        [Route("scorecardmetric/{yearstring}")]
        //[Route("teamstructurebyselection/{selectedcode}/{selectedyear}/{selectedlevel}")]
        public HttpResponseMessage SCMetric(HttpRequestMessage request, string yearstring)
        {
            HttpResponseMessage res = null;

            //var newList = new List<string>();
            List<ScoreCardMetricsModel> newList = new List<ScoreCardMetricsModel>();

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("dropdown_ScoreCardTrend_v2", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Year",
                    Value = yearstring,
                });

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ScoreCardMetricsModel cap = new ScoreCardMetricsModel();

                    //DateTime d = reader["date"] != DBNull.Value ? (Convert.ToDateTime(reader["date"].ToString())).Date : Convert.ToDateTime("19-01-01");

                    cap.Metric = reader["Metric"] != DBNull.Value ? Convert.ToString(reader["Metric"]) : "null";
                    cap.Position = reader["position"] != DBNull.Value ? int.Parse(Convert.ToString(reader["position"])) : 0;

                    newList.Add(cap);
                }
                con.Close();
            }  // using               

            res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, newList);

            return res;
        }

        ////==================== scorecard role List ends ================================



        ////==================== scorecard metric starts ================================


        [HttpGet]
        [Route("scorecardroles")]
        //[Route("teamstructurebyselection/{selectedcode}/{selectedyear}/{selectedlevel}")]
        public HttpResponseMessage SCRoles(HttpRequestMessage request)
        {
            HttpResponseMessage res = null;

            List<ScoreCardRolesModel> newList = new List<ScoreCardRolesModel>();

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("ScoreCard_Roles", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ScoreCardRolesModel cap = new ScoreCardRolesModel();

                    //DateTime d = reader["date"] != DBNull.Value ? (Convert.ToDateTime(reader["date"].ToString())).Date : Convert.ToDateTime("19-01-01");

                    cap.Caption = reader["Caption"] != DBNull.Value ? Convert.ToString(reader["Caption"]) : "null";
                    cap.Position = reader["Position"] != DBNull.Value ? int.Parse(Convert.ToString(reader["Position"])) : 0;

                    newList.Add(cap);
                }
                con.Close();
            }  // using               

            res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, newList);

            return res;
        }

        ////==================== rundate List ends ================================

        //[HttpGet]
        //[Route("loggedinusername")]
        ////[Route("teamstructurebyselection/{selectedcode}/{selectedyear}/{selectedlevel}")]
        //public HttpResponseMessage LoggedInUserName(HttpRequestMessage request)
        //{
        //    HttpResponseMessage res = null;

        //    string LoginID = Convert.ToString(System.Web.HttpContext.Current.Session["session_loggedinID"]);

        //    res = request.CreateResponse(HttpStatusCode.OK, LoginID);

        //    return res;
        //}

        [HttpGet]
        // [Route("unit/{loginid}")]
        [Route("units")]
        //[Route("teamstructurebyselection/{selectedcode}/{selectedyear}/{selectedlevel}")]
        public HttpResponseMessage Units(HttpRequestMessage request)
        {
            HttpResponseMessage res = null;

            var newList = new List<string>();
            string loginid = Convert.ToString(System.Web.HttpContext.Current.Session["session_loggedinID"]);

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("", con);
                //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.CommandTimeout = 0;

                con.Open();
                cmd.CommandText = "select distinct department from Opex_mapping where LOGINID = (@login_Id) order by department";
                cmd.Parameters.AddWithValue("@login_Id", loginid);
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //var cap = reader["Caption"] != DBNull.Value ? Convert.ToString(reader["Caption"]) : "null";
                    var un = reader["department"] != DBNull.Value ? Convert.ToString(reader["department"]) : "null";

                    newList.Add(un);
                }
                con.Close();
            }  // using               


            res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, newList);

            return res;
        }


        [HttpGet]
        [Route("otherinfo/{year}")]
        //public HttpResponseMessage OtherInformation(HttpRequestMessage request, int year)
        public HttpResponseMessage OtherInformation(HttpRequestMessage request, int year)
        {
            HttpResponseMessage res = null;

            DDMtd obj = new DDMtd();
            List<string> newList = obj.OtherInfo(year);

            res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, newList);

            return res;
        }


        [HttpGet]
        [Route("productssupercation2")]
        //[Route("teamstructurebyselection/{selectedcode}/{selectedyear}/{selectedlevel}")]
        public HttpResponseMessage ProductsSuperCaption2(HttpRequestMessage request)
        {
            HttpResponseMessage res = null;

            var newList = new List<string>();

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("", con);
                //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.CommandTimeout = 0;

                con.Open();
                //cmd.CommandText = "select distinct department from Opex_mapping where LOGINID = (@login_Id) order by department";
                // cmd.CommandText = "select distinct Product_SUPERCAPTION from income_productstable";
                cmd.CommandText = "select 'All' Product_SUPERCAPTION union select distinct Product_SUPERCAPTION from income_productstable";
                //cmd.Parameters.AddWithValue("@login_Id", loginid);
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //var cap = reader["Caption"] != DBNull.Value ? Convert.ToString(reader["Caption"]) : "null";
                    var un = reader["Product_SUPERCAPTION"] != DBNull.Value ? Convert.ToString(reader["Product_SUPERCAPTION"]) : "";

                    newList.Add(un);
                }
                con.Close();
            }  // using               


            res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, newList);

            return res;
        }


        ////==================== Product Super Caption ================================

        [HttpGet]
        [Route("productssupercation")]
        //[Route("teamstructurebyselection/{selectedcode}/{selectedyear}/{selectedlevel}")]
        public HttpResponseMessage Productssupercation(HttpRequestMessage request)
        {
            HttpResponseMessage res = null;

            var newList = new List<string>();

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("mpr_dropdown_product_analysis", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var un = reader["Product_SUPERCAPTION"] != DBNull.Value ? Convert.ToString(reader["Product_SUPERCAPTION"]) : "";

                    newList.Add(un);
                }
                con.Close();
            }  // using               

            res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, newList);

            return res;
        }

        ////==================== rundate List ends ================================





        //================ current run date starts ======================================

        //[HttpGet]
        //[Route("currrentrundate")]
        ////[Route("teamstructurebyselection/{selectedcode}/{selectedyear}/{selectedlevel}")]
        //public HttpResponseMessage CurrentRunDate(HttpRequestMessage request)
        //{
        //    HttpResponseMessage res = null;


        //    string query = "select max(rundate) from cor_solutionrundate";

        //    string CS = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;
        //    System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(CS);

        //    try
        //    {
        //        connection.Open();

        //        var menus = db.Database.SqlQuery<string>(query);

        //        foreach (var item in menus)
        //        {
        //            var rp = db.mpr_FintrakMenu.Where(x => x.MenuList == item).Select(y => y.ReportPAth).ToList();

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

        //    res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, newList);

        //    return res;
        //}

        //================ current run date ends ========================================




        // //==================== unit report parameter starts ================================

        //[HttpGet]
        //[Route("unitreport")]
        //public HttpResponseMessage UnitReport(HttpRequestMessage request)
        //{
        //    HttpResponseMessage res = null;

        //    var newList = new List<string>();

        //    string reportlevel = Convert.ToString(System.Web.HttpContext.Current.Session["session_levelcode"]);
        //    string currentlyselectedreportlevel = Convert.ToString(System.Web.HttpContext.Current.Session["session_currentlyselectedlevel"]);

        //    string MisCode = Convert.ToString(System.Web.HttpContext.Current.Session["session_miscode"]);
        //    string currentlyselectedmiscode = Convert.ToString(System.Web.HttpContext.Current.Session["session_currentlyselectedmiscode"]);

        //    if (!string.IsNullOrEmpty(Convert.ToString(currentlyselectedmiscode)))
        //    {
        //        MisCode = "";
        //        MisCode = currentlyselectedmiscode;
        //        Level = currentlyselectedlevel;
        //    }

        //    using (var con = new SqlConnection(connectionString))
        //    {
        //        var cmd = new SqlCommand("dropdown_memo", con);
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.CommandTimeout = 0;

        //        cmd.Parameters.Add(new SqlParameter
        //        {
        //            ParameterName = "ReportLevel",
        //            Value = miscode,
        //        });

        //        con.Open();

        //        SqlDataReader reader = cmd.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            var cap = new APRdistinctCaption_ReportInfo();

        //            cap.ProductCode = reader["Unit"].ToString();

        //            newList.Add(cap);
        //        }
        //        con.Close();
        //    }  // using               


        //    res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, newList);

        //    return res;
        //}

        ////==================== unit report parameter ends ================================


        protected override void Dispose(bool disposing)
        {
            entityContext.Dispose();
            base.Dispose(disposing);
        }
    }
}