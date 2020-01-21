using log4net;
using MPR.Report.Core;
using MPR.Report.Core.Entities;
using MPR.Report.Data.Business;
using MPR.Report.Data.Business.DataRepositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web;
using System.Web.Http;

namespace Pi360Reporting.Controllers.ApiControllers
{
    //[Adapters.CustomActionFilter]
    //[Authorize]
    //[Export]
    //[PartCreationPolicy(CreationPolicy.NonShared)]  
    //[Adapters.AuthenticationFilter]
    [RoutePrefix("api/landingpage")]
    //[UsesDisposableService]
    public class LandingPageApiController : ApiController   //ApiControllerBase
    {
        //[ImportingConstructor]
        public LandingPageApiController()
        {
        }
        public static readonly ILog appLog = LogManager.GetLogger("File1Appender");

        MPRContext2 entityContext = new MPRContext2();
        //string connectionString = ConfigurationManager.ConnectionStrings["FintrakMiniConnection"].ConnectionString;
        string connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

        //======================= mix starts =======================================================================
        [HttpGet]
        [Route("mix")]
        //public List<MixListInfo> MixMtd(HttpRequestMessage request)
        public HttpResponseMessage MixMtd(HttpRequestMessage request)
        {
            //string uname = System.Web.HttpContext.Current.Session["session_loggedinUser"].ToString();
            //appLog.InfoFormat("Calling the MixMtd() method for the user: {0}", uname);
            appLog.InfoFormat("Calling the MixMtd() method");

            HttpResponseMessage res = null;

            //Get the culture property of the thread.
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            //Create TextInfo object.
            TextInfo textInfo = cultureInfo.TextInfo;

            var newList3 = new List<DListInfo>();
            var newList2 = new List<MixListInfo>();

            DListInfo dlist = new DListInfo();

            //var latestmonthyear = (from a in entityContext.IncomeSetUpDailySet

            //                       where a.Year == entityContext.IncomeSetUpDailySet.Max(x => x.Year)
            //                       //select a);
            //                       select new
            //                       {
            //                           Year = a.Year,
            //                           CurrentPeriod = a.CurrentPeriod
            //                       })
            //                  .AsEnumerable().Select(x => new IncomeSetUpDaily
            //                  {
            //                      Year = x.Year,
            //                      CurrentPeriod = x.CurrentPeriod
            //                  })
            //                 .ToList();

            //int latestyear = latestmonthyear.Select(x => x.Year).FirstOrDefault();
            //int period = latestmonthyear.Max(x => x.CurrentPeriod);

            //var latestmonthyear = entityContext.IncomeSetUpDailySet.OrderByDescending(x => x.Year).Take(1);
            //int latestyear = Convert.ToInt32(latestmonthyear.Max(x => x.Year));
            //int period = Convert.ToInt32(latestmonthyear.Max(x => x.CurrentPeriod));
            //appLog.InfoFormat("Latestyear: {0} and Latestperiod: {1} are gotten.", latestyear, period);

            int latestyear = Convert.ToInt32(System.Web.HttpContext.Current.Session["latestyear"]);
            int period = Convert.ToInt32(System.Web.HttpContext.Current.Session["currentperiod"]);
            appLog.InfoFormat("Latestyear: {0} and Latestperiod: {1} are gotten.", latestyear, period);

            List<string> mcp = new List<string>() { "D1", "D2", "D3" };

            //string miscode = System.Web.HttpContext.Current.Session["session_miscode"].ToString();
            //int level = Convert.ToInt32(System.Web.HttpContext.Current.Session["session_level"]);

            string miscode =Convert.ToString(System.Web.HttpContext.Current.Session["session_miscode"]);
            //appLog.InfoFormat("Loggedin user miscode: {} received", miscode);
            string currentlyselectedmiscode = Convert.ToString(System.Web.HttpContext.Current.Session["session_currentlyselectedmiscode"]);
            //appLog.InfoFormat("Currently selected miscode: {} received", currentlyselectedmiscode);

            int level = Convert.ToInt32(System.Web.HttpContext.Current.Session["session_level"]);
            int currentlyselectedlevel = Convert.ToInt32(System.Web.HttpContext.Current.Session["session_currentlyselectedlevel"]);

            //if (currentlyselectedmiscode != "")
            //{
            //    miscode = "";
            //    miscode = currentlyselectedmiscode;
            //    level = currentlyselectedlevel;
            //}

            appLog.InfoFormat("level: {0} received", level);
            appLog.InfoFormat("miscode: {0} received", miscode);

            var conf = entityContext.LandingPageConfigurationSet.Where(x=>x.Deleted == false).ToList();

            string d1 = conf.Where(x => x.DisplayCaptionObjectCode == "D1").Select(x => x.MainCaption).FirstOrDefault();
            string d2 = conf.Where(x => x.DisplayCaptionObjectCode == "D2").Select(x => x.MainCaption).FirstOrDefault();
            string d3 = conf.Where(x => x.DisplayCaptionObjectCode == "D3").Select(x => x.MainCaption).FirstOrDefault();

            var query = from a in conf
                        join b in mcp on a.DisplayCaptionObjectCode equals b
                        select a;

            List<string> maincaptions = query.Select(x => x.MainCaption).ToList();

            

            foreach (var v in maincaptions)
            {
                var newList = new List<SubMixListInfo>();

                using (var con = new SqlConnection(connectionString))
                {
                    var cmd = new SqlCommand("proc_mpr_report_dashboard_by_caption", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "MisCode",
                        Value = miscode,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "Period",
                        Value = period,
                        //Value = "6",
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "Year",
                        Value = latestyear,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "Level",
                        Value = level,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "MainCaption",
                        Value = v,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "TrendOrMonthly",
                        Value = "monthly",
                    });

                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var pts = new SubMixListInfo();

                        //pts.MPR_Processing_Id = int.Parse(reader["MPR_Processing_Id"].ToString());
                        //pts.DateTimeToStart = (DateTime)reader["DateTimeToStart"];
                        //pts.MainCaption = reader["MainCaption"].ToString();                        
                        //pts.SubCaption = textInfo.ToTitleCase(reader["SubCaption"].ToString());
                        
                        pts.SubCaption = reader["SubCaption"] != DBNull.Value ? reader["SubCaption"].ToString() : "null";
                        pts.Amount = reader["Amount"] != DBNull.Value ? Convert.ToDouble(reader["Amount"].ToString()) : 0;
                        pts.Budget = reader["Budget"] != DBNull.Value ? Convert.ToDouble(reader["Budget"].ToString()) : 0;
                        pts.Period = reader["Period"] != DBNull.Value ? Convert.ToInt32(reader["Period"].ToString()) : 0;


                        newList.Add(pts);
                    }
                    con.Close();
                }  // using               

                var ob = new MixListInfo()
                {
                    subobj = newList.ToList(),
                    MainCaption = v
                 };
                newList2.Add(ob);                

            }  // foreach

            dlist.D1List = newList2.Where(x => x.MainCaption == d1).ToList();
            dlist.D2List = newList2.Where(x => x.MainCaption == d2).ToList();
            dlist.D3List = newList2.Where(x => x.MainCaption == d3).ToList();
            
            newList3.Add(dlist);

            res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, newList3);

            appLog.InfoFormat("about to return the result");

            return res;
        }

        //============================== mix ends ===========================================================================


        //============================== T1 trend for MainCaption starts =======================================================================

        [HttpGet]
        [Route("maincaptiontrend_t1")]
        public HttpResponseMessage t1MainCaptionTrendMtd(HttpRequestMessage request)
        {
            appLog.InfoFormat("Calling the t1MainCaptionTrendMtd() method");

            HttpResponseMessage res = null;

            var newList3 = new List<TListInfo>();
            //var newList2 = new List<MixListInfo>(); 
            var newList2 = new List<SubMixListInfo>();
           //var newList = new List<SubMixListInfo>();

           TListInfo tlist = new TListInfo();

            //var latestmonthyear = entityContext.IncomeSetUpDailySet.OrderByDescending(x => x.Year).Take(1);
            //int latestyear = Convert.ToInt32(latestmonthyear.Max(x => x.Year));
            //int period = Convert.ToInt32(latestmonthyear.Max(x => x.CurrentPeriod));

            int latestyear = Convert.ToInt32(System.Web.HttpContext.Current.Session["latestyear"]);
            int period = Convert.ToInt32(System.Web.HttpContext.Current.Session["currentperiod"]);

            appLog.InfoFormat("Latestyear: {0} and Latestperiod: {1} are gotten.", latestyear, period);

            //string miscode = System.Web.HttpContext.Current.Session["session_miscode"].ToString();
            //int level = Convert.ToInt32(System.Web.HttpContext.Current.Session["session_level"]);

            string miscode = Convert.ToString(System.Web.HttpContext.Current.Session["session_miscode"]);
            string currentlyselectedmiscode = Convert.ToString(System.Web.HttpContext.Current.Session["session_currentlyselectedmiscode"]);


            int level = Convert.ToInt32(System.Web.HttpContext.Current.Session["session_level"]);
            int currentlyselectedlevel = Convert.ToInt32(System.Web.HttpContext.Current.Session["session_currentlyselectedlevel"]);

            //if (currentlyselectedmiscode != "")
            //{
            //    miscode = "";
            //    miscode = currentlyselectedmiscode;
            //    level = currentlyselectedlevel;
            //}

            appLog.InfoFormat("level: {0} received", level);
            appLog.InfoFormat("miscode: {0} received", miscode);

            var conf = entityContext.LandingPageConfigurationSet.Where(x => x.Deleted == false).ToList();

            string d1 = conf.Where(x => x.DisplayCaptionObjectCode == "T1").Select(x => x.MainCaption).FirstOrDefault();
            
                var newList = new List<MainMixListInfo>();

                using (var con = new SqlConnection(connectionString))
                {
                    var cmd = new SqlCommand("proc_mpr_report_dashboard_by_caption_2", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "MisCode",
                        Value = miscode,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "Period",
                        Value = period,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "Year",
                        Value = latestyear,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "Level",
                        Value = level,
                    });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MainCaption",
                    Value = d1,
                });

                cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "TrendOrMonthly",
                        Value = "Trend",
                    });

                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var pts = new MainMixListInfo();

                    //    //pts.MPR_Processing_Id = int.Parse(reader["MPR_Processing_Id"].ToString());
                    //    //pts.DateTimeToStart = (DateTime)reader["DateTimeToStart"];
                    
                    pts.MainCaption = reader["MainCaption"] != DBNull.Value ? reader["MainCaption"].ToString() : "null";
                    pts.Amount = reader["Amount"] != DBNull.Value ? Convert.ToDouble(reader["Amount"].ToString()) : 0;
                    pts.Budget = reader["Budget"] != DBNull.Value ? Convert.ToDouble(reader["Budget"].ToString()) : 0;
                    pts.Period = reader["Period"] != DBNull.Value ? Convert.ToInt32(reader["Period"].ToString()) : 0;

                    newList.Add(pts);
                    }
                    con.Close();
                }  // using               

            res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, newList);

            appLog.InfoFormat("about to return the result");

            return res;
        }

        //============================== T1 trend for MainCaption ends =========================================================================


        //======================= T2 trend for subcaption starts =======================================================================
        [HttpGet]
        [Route("subcaptiontrend_t2")]
        public HttpResponseMessage t2SubCaptionTrendMtd(HttpRequestMessage request)
        {
            appLog.InfoFormat("Calling the t2SubCaptionTrendMtd() method");

            HttpResponseMessage res = null;

            var newList2 = new List<SubCaptionTrendListInfo>();

            DListInfo dlist = new DListInfo();

            //var latestmonthyear = entityContext.IncomeSetUpDailySet.OrderByDescending(x => x.Year).Take(1);
            //int latestyear = Convert.ToInt32(latestmonthyear.Max(x => x.Year));
            //int period = Convert.ToInt32(latestmonthyear.Max(x => x.CurrentPeriod));

            int latestyear = Convert.ToInt32(System.Web.HttpContext.Current.Session["latestyear"]);
            int period = Convert.ToInt32(System.Web.HttpContext.Current.Session["currentperiod"]);

            appLog.InfoFormat("Latestyear: {0} and Latestperiod: {1} are gotten.", latestyear, period);

            //string miscode = System.Web.HttpContext.Current.Session["session_miscode"].ToString();
            //int level = Convert.ToInt32(System.Web.HttpContext.Current.Session["session_level"]);

            string miscode = Convert.ToString(System.Web.HttpContext.Current.Session["session_miscode"]);
            string currentlyselectedmiscode = Convert.ToString(System.Web.HttpContext.Current.Session["session_currentlyselectedmiscode"]);

            int level = Convert.ToInt32(System.Web.HttpContext.Current.Session["session_level"]);
            int currentlyselectedlevel = Convert.ToInt32(System.Web.HttpContext.Current.Session["session_currentlyselectedlevel"]);

            //if (currentlyselectedmiscode != "")
            //{
            //    miscode = "";
            //    miscode = currentlyselectedmiscode;
            //    level = currentlyselectedlevel;
            //}

            appLog.InfoFormat("level: {0} received", level);
            appLog.InfoFormat("miscode: {0} received", miscode);

            var conf = entityContext.LandingPageConfigurationSet.Where(x => x.Deleted == false).ToList();

            string d2 = conf.Where(x => x.DisplayCaptionObjectCode == "D2").Select(x => x.MainCaption).FirstOrDefault();

                var newList = new List<SubMixListInfo>();

            using (var con = new SqlConnection(connectionString))
                {
                    var cmd = new SqlCommand("proc_mpr_report_dashboard_by_caption", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "MisCode",
                        Value = miscode,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "Period",
                        Value = period,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "Year",
                        Value = latestyear,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "Level",
                        Value = level,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "MainCaption",
                        Value = d2,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "TrendOrMonthly",
                        Value = "trend",
                    });

                    con.Open();

                    
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var pts = new SubMixListInfo();
                    
                    pts.SubCaption = reader["SubCaption"] != DBNull.Value ? reader["SubCaption"].ToString() : "null";
                    pts.Amount = reader["Amount"] != DBNull.Value ? Convert.ToDouble(reader["Amount"].ToString()) : 0;
                    pts.Budget = reader["Budget"] != DBNull.Value ? Convert.ToDouble(reader["Budget"].ToString()) : 0;
                    pts.Period = reader["Period"] != DBNull.Value ? Convert.ToInt32(reader["Period"].ToString()) : 0;

                    newList.Add(pts);
                    }
                    con.Close();
                }  // using               

            List<string>  sub =  newList.Select(x => x.SubCaption).Distinct().ToList();

            foreach (var vv in sub)
            {
                //var query = menu2.Where(x => x.MenuList == p).Select(x => new mprFintrakMenu_SubObjectListInfo { ReportPAth = x.ReportPAth, ParameterKey = x.ParameterKey, ReportTitle = x.ReportTitle, UIsrefState = x.UIsrefState, ID = x.ID });
               var query = newList.Where(x => x.SubCaption == vv).Select(x => new ABPTrendListInfo { Amount = x.Amount, Budget = x.Budget, Period = x.Period }).GroupBy(x=>x.Period).Select(x=>x.FirstOrDefault()).ToList();

                var ob = new SubCaptionTrendListInfo()
                {
                    abpList = query.ToList(),
                    SubCaption = vv
                };
                newList2.Add(ob);
            }
           
            res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, newList2);

            appLog.InfoFormat("about to return the result");

            return res;
        }

        //============================== T2 trend for subcaption ends ===========================================================================


        //============================== T3 trend for MainCaption starts =======================================================================

        [HttpGet]
        [Route("maincaptiontrend_t3")]
        public HttpResponseMessage t3MainCaptionTrendMtd(HttpRequestMessage request)
        {
            appLog.InfoFormat("Calling the t3MainCaptionTrendMtd() method");


            HttpResponseMessage res = null;

            var newList3 = new List<TListInfo>();
            var newList2 = new List<SubMixListInfo>();

            TListInfo tlist = new TListInfo();

            //var latestmonthyear = entityContext.IncomeSetUpDailySet.OrderByDescending(x => x.Year).Take(1);
            //int latestyear = Convert.ToInt32(latestmonthyear.Max(x => x.Year));
            //int period = Convert.ToInt32(latestmonthyear.Max(x => x.CurrentPeriod));

            int latestyear = Convert.ToInt32(System.Web.HttpContext.Current.Session["latestyear"]);
            int period = Convert.ToInt32(System.Web.HttpContext.Current.Session["currentperiod"]);

            appLog.InfoFormat("Latestyear: {0} and Latestperiod: {1} are gotten.", latestyear, period);

            //string miscode = System.Web.HttpContext.Current.Session["session_miscode"].ToString();
            //int level = Convert.ToInt32(System.Web.HttpContext.Current.Session["session_level"]);

            string miscode = Convert.ToString(System.Web.HttpContext.Current.Session["session_miscode"]);
            string currentlyselectedmiscode = Convert.ToString(System.Web.HttpContext.Current.Session["session_currentlyselectedmiscode"]);

            int level = Convert.ToInt32(System.Web.HttpContext.Current.Session["session_level"]);
            int currentlyselectedlevel = Convert.ToInt32(System.Web.HttpContext.Current.Session["session_currentlyselectedlevel"]);

            //if (currentlyselectedmiscode != "")
            //{
            //    miscode = "";
            //    miscode = currentlyselectedmiscode;
            //    level = currentlyselectedlevel;
            //}

            appLog.InfoFormat("level: {0} received", level);
            appLog.InfoFormat("miscode: {0} received", miscode);

            var conf = entityContext.LandingPageConfigurationSet.Where(x => x.Deleted == false).ToList();

            string d3 = conf.Where(x => x.DisplayCaptionObjectCode == "T3").Select(x => x.MainCaption).FirstOrDefault();

            var newList = new List<MainMixListInfo>();

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("proc_mpr_report_dashboard_by_caption_2", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MisCode",
                    Value = miscode,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Period",
                    Value = period,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Year",
                    Value = latestyear,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Level",
                    Value = level,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MainCaption",
                    Value = d3,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "TrendOrMonthly",
                    Value = "Trend",
                });

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var pts = new MainMixListInfo();
                    
                    pts.MainCaption = reader["MainCaption"] != DBNull.Value ? reader["MainCaption"].ToString() : "null";
                    pts.Amount = reader["Amount"] != DBNull.Value ? Convert.ToDouble(reader["Amount"].ToString()) : 0;
                    pts.Budget = reader["Budget"] != DBNull.Value ? Convert.ToDouble(reader["Budget"].ToString()) : 0;
                    pts.Period = reader["Period"] != DBNull.Value ? Convert.ToInt32(reader["Period"].ToString()) : 0;

                    newList.Add(pts);
                }
                con.Close();
            }  // using               

            res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, newList);

            appLog.InfoFormat("about to return the result");

            return res;
        }

//========================= T3 MainCaption ends ==============================================================================

        //============================== trend for SubCaption starts =======================================================================

        [HttpGet]
        [Route("trendsubcaption")]
        public HttpResponseMessage TrendSubcaptionMtd(HttpRequestMessage request)
        {
            appLog.InfoFormat("Calling the TrendSubcaptionMtd() method");

            HttpResponseMessage res = null;

            var newList3 = new List<TListInfo>();
            var newList2 = new List<MixListInfo>();

            TListInfo tlist = new TListInfo();

            //var latestmonthyear = entityContext.IncomeSetUpDailySet.OrderByDescending(x => x.Year).Take(1);
            //int latestyear = Convert.ToInt32(latestmonthyear.Max(x => x.Year));
            //int period = Convert.ToInt32(latestmonthyear.Max(x => x.CurrentPeriod));

            int latestyear = Convert.ToInt32(System.Web.HttpContext.Current.Session["latestyear"]);
            int period = Convert.ToInt32(System.Web.HttpContext.Current.Session["currentperiod"]);

            appLog.InfoFormat("Latestyear: {0} and Latestperiod: {1} are gotten.", latestyear, period);

            List<string> mcp = new List<string>() { "T1", "T2", "T3" };

            //string miscode = System.Web.HttpContext.Current.Session["session_miscode"].ToString();
            //int level = Convert.ToInt32(System.Web.HttpContext.Current.Session["session_level"]);

            string miscode = Convert.ToString(System.Web.HttpContext.Current.Session["session_miscode"]);
            string currentlyselectedmiscode = Convert.ToString(System.Web.HttpContext.Current.Session["session_currentlyselectedmiscode"]);

            int level = Convert.ToInt32(System.Web.HttpContext.Current.Session["session_level"]);
            int currentlyselectedlevel = Convert.ToInt32(System.Web.HttpContext.Current.Session["session_currentlyselectedlevel"]);

            //if (currentlyselectedmiscode != "")
            //{
            //    miscode = "";
            //    miscode = currentlyselectedmiscode;
            //    level = currentlyselectedlevel;
            //}

            appLog.InfoFormat("level: {0} received", level);
            appLog.InfoFormat("miscode: {0} received", miscode);

            var conf = entityContext.LandingPageConfigurationSet.Where(x => x.Deleted == false).ToList();

            string d1 = conf.Where(x => x.DisplayCaptionObjectCode == "T1").Select(x => x.MainCaption).FirstOrDefault();
            string d2 = conf.Where(x => x.DisplayCaptionObjectCode == "T2").Select(x => x.MainCaption).FirstOrDefault();
            string d3 = conf.Where(x => x.DisplayCaptionObjectCode == "T3").Select(x => x.MainCaption).FirstOrDefault();

            var query = from a in conf
                        join b in mcp on a.DisplayCaptionObjectCode equals b
                        select a;

            List<string> maincaptions = query.Select(x => x.MainCaption).ToList();

            foreach (var v in maincaptions)
            {
                var newList = new List<SubMixListInfo>();

                using (var con = new SqlConnection(connectionString))
                {
                    var cmd = new SqlCommand("proc_mpr_report_dashboard_by_caption", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "MisCode",
                        Value = miscode,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "Period",
                        Value = period,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "Year",
                        Value = latestyear,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "Level",
                        Value = level,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "MainCaption",
                        Value = v,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "TrendOrMonthly",
                        Value = "Trend",
                    });

                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var pts = new SubMixListInfo();
                        
                        pts.SubCaption = reader["SubCaption"] != DBNull.Value ? reader["SubCaption"].ToString() : "null";
                        pts.Amount = reader["Amount"] != DBNull.Value ? Convert.ToDouble(reader["Amount"].ToString()) : 0;
                        pts.Budget = reader["Budget"] != DBNull.Value ? Convert.ToDouble(reader["Budget"].ToString()) : 0;
                        pts.Period = reader["Period"] != DBNull.Value ? Convert.ToInt32(reader["Period"].ToString()) : 0;

                        newList.Add(pts);
                    }
                    con.Close();
                }  // using               

                var ob = new MixListInfo()
                {
                    subobj = newList.ToList(),
                    MainCaption = v
                };
                newList2.Add(ob);

            }  // foreach

            tlist.T1List = newList2.Where(x => x.MainCaption == d1).ToList();
            tlist.T2List = newList2.Where(x => x.MainCaption == d2).ToList();
            tlist.T3List = newList2.Where(x => x.MainCaption == d3).ToList();
            
            newList3.Add(tlist);

            res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, newList3);

            appLog.InfoFormat("about to return the result");

            return res;
        }

        //============================== trend for SubCaption ends =========================================================================

        // ============ each method starts ==========================         

        [HttpGet]
        [Route("landingpagechat")]
        public HttpResponseMessage LPChart(HttpRequestMessage request)
        {
            appLog.InfoFormat("Calling the LPChart() method");

            HttpResponseMessage res = null;
            
            var newList = new List<SubMixListInfo>();

            //var latestmonthyear = entityContext.IncomeSetUpDailySet.OrderByDescending(x => x.Year).Take(1);
            //int latestyear = Convert.ToInt32(latestmonthyear.Max(x => x.Year));
            //int period = Convert.ToInt32(latestmonthyear.Max(x => x.CurrentPeriod));

            int latestyear = Convert.ToInt32(System.Web.HttpContext.Current.Session["latestyear"]);
            int period = Convert.ToInt32(System.Web.HttpContext.Current.Session["currentperiod"]);

            appLog.InfoFormat("Latestyear: {0} and Latestperiod: {1} are gotten.", latestyear, period);

            //string miscode = System.Web.HttpContext.Current.Session["session_miscode"].ToString();
            //int level = Convert.ToInt32(System.Web.HttpContext.Current.Session["session_level"]);

            string miscode = Convert.ToString(System.Web.HttpContext.Current.Session["session_miscode"]);
            string currentlyselectedmiscode = Convert.ToString(System.Web.HttpContext.Current.Session["session_currentlyselectedmiscode"]);

            int level = Convert.ToInt32(System.Web.HttpContext.Current.Session["session_level"]);
            int currentlyselectedlevel = Convert.ToInt32(System.Web.HttpContext.Current.Session["session_currentlyselectedlevel"]);

            //if (currentlyselectedmiscode != "")
            //{
            //    miscode = "";
            //    miscode = currentlyselectedmiscode;
            //    level = currentlyselectedlevel;
            //}

            appLog.InfoFormat("level: {0} received", level);
            appLog.InfoFormat("miscode: {0} received", miscode);

            //var conf = entityContext.LandingPageConfigurationSet.Where(x => x.Deleted == false).ToList();
            var conf = entityContext.LandingPageConfigurationSet.Where(x => x.Deleted == false && x.DisplayCaptionObjectCode == "D1").Select(x => x.MainCaption).FirstOrDefault();

            
                using (var con = new SqlConnection(connectionString))
                {
                    var cmd = new SqlCommand("proc_mpr_report_dashboard_by_caption", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "MisCode",
                        Value = miscode,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "Period",
                        Value = period,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "Year",
                        Value = latestyear,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "Level",
                        Value = level,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "MainCaption",
                        Value = conf,
                    });

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "TrendOrMonthly",
                        Value = "monthly",
                    });

                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                    var pts = new SubMixListInfo();
                    
                    pts.SubCaption = reader["SubCaption"] != DBNull.Value ? reader["SubCaption"].ToString() : "null";
                    pts.Amount = reader["Amount"] != DBNull.Value ? Convert.ToDouble(reader["Amount"].ToString()) : 0;
                    pts.Budget = reader["Budget"] != DBNull.Value ? Convert.ToDouble(reader["Budget"].ToString()) : 0;
                    pts.Period = reader["Period"] != DBNull.Value ? Convert.ToInt32(reader["Period"].ToString()) : 0;

                    newList.Add(pts);
                    }
                    con.Close();                   
            }  // using               

            var newList2 = new chartLandingPageInfo()
            {
                data = newList.ToList(),
                name = conf
            };

            res = request.CreateResponse(HttpStatusCode.OK, newList2);

            appLog.InfoFormat("about to return the result");

            return res;
        }

        //=========== each method ends ====================

        //==================== for cards A starts ================================

        [HttpGet]
        [Route("cardsA/{valu}")]
        public HttpResponseMessage CardsAMtd(HttpRequestMessage request, string valu)
        {
            appLog.InfoFormat("Calling the CardsAMtd() method by LandingPage");

            HttpResponseMessage res = null;

            //Get the culture property of the thread.
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            //Create TextInfo object.
            TextInfo textInfo = cultureInfo.TextInfo;

            var newList = new List<DashboardMainCaptionListInfo>();

            int latestyear = Convert.ToInt32(System.Web.HttpContext.Current.Session["latestyear"]);
            int period = Convert.ToInt32(System.Web.HttpContext.Current.Session["currentperiod"]);

            string miscode = Convert.ToString(System.Web.HttpContext.Current.Session["session_miscode"]);
            string currentlyselectedmiscode = Convert.ToString(System.Web.HttpContext.Current.Session["session_currentlyselectedmiscode"]);

            int level = Convert.ToInt32(System.Web.HttpContext.Current.Session["session_level"]);
            int currentlyselectedlevel = Convert.ToInt32(System.Web.HttpContext.Current.Session["session_currentlyselectedlevel"]);

            //if (currentlyselectedmiscode != "")
            //{
            //    miscode = "";
            //    miscode = currentlyselectedmiscode;
            //    level = currentlyselectedlevel;
            //}

            appLog.InfoFormat("level: {0} received", level);
            appLog.InfoFormat("miscode: {0} received", miscode);

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("proc_mpr_report_dashboard_by_caption_6", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MisCode",
                    Value = miscode,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Period",
                    Value = period,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Year",
                    Value = latestyear,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Level",
                    Value = level,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "type",
                    Value = valu,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "TrendOrMonthly",
                    Value = "monthly",
                });

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var pts = new DashboardMainCaptionListInfo();

                    pts.MainCaption = reader["MainCaption"] != DBNull.Value ? reader["MainCaption"].ToString() : "null";
                    pts.Amount = reader["Amount"] != DBNull.Value ? Convert.ToDouble(reader["Amount"].ToString()) : 0;
                    pts.Budget = reader["Budget"] != DBNull.Value ? Convert.ToDouble(reader["Budget"].ToString()) : 0;
                    pts.Period = reader["Period"] != DBNull.Value ? Convert.ToInt32(reader["Period"].ToString()) : 0;

                    newList.Add(pts);
                }
                con.Close();
            } // using               

            res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, newList);

            appLog.InfoFormat("about to return the result");

            return res;
        }

        //==================== for cards A ends ================================


        //==================== for cards starts ================================

        [HttpGet]
        [Route("cards")]
        public HttpResponseMessage CardsMtd(HttpRequestMessage request)
        {
            appLog.InfoFormat("Calling the CardsMtd() method");

            HttpResponseMessage res = null;

            var newList = new List<CListInfo>();
            var newList2 = new List<CListInfo>();

            //var latestmonthyear = entityContext.IncomeSetUpDailySet.OrderByDescending(x => x.Year).Take(1);
            //int latestyear = Convert.ToInt32(latestmonthyear.Max(x => x.Year));
            //int period = Convert.ToInt32(latestmonthyear.Max(x => x.CurrentPeriod));

            int latestyear = Convert.ToInt32(System.Web.HttpContext.Current.Session["latestyear"]);
            int period = Convert.ToInt32(System.Web.HttpContext.Current.Session["currentperiod"]);

            appLog.InfoFormat("Latestyear: {0} and Latestperiod: {1} are gotten.", latestyear, period);

            string cad = "CARD";

            List<string> configuredCaptions = entityContext.LandingPageConfigurationSet.Where(x => x.Deleted == false && x.Type == cad.ToUpper()).Select(x => x.MainCaption).ToList();
            //List<string> mcp = new List<string>() { "D1", "D2", "D3" };

            //string miscode = System.Web.HttpContext.Current.Session["session_miscode"].ToString();        
            //int level = Convert.ToInt32(System.Web.HttpContext.Current.Session["session_level"]);

            string miscode = Convert.ToString(System.Web.HttpContext.Current.Session["session_miscode"]);
            string currentlyselectedmiscode = Convert.ToString(System.Web.HttpContext.Current.Session["session_currentlyselectedmiscode"]);

            int level = Convert.ToInt32(System.Web.HttpContext.Current.Session["session_level"]);
            int currentlyselectedlevel = Convert.ToInt32(System.Web.HttpContext.Current.Session["session_currentlyselectedlevel"]);

            //if (currentlyselectedmiscode != "")
            //{
            //    miscode = "";
            //    miscode = currentlyselectedmiscode;
            //    level = currentlyselectedlevel;
            //}

            appLog.InfoFormat("level: {0} received", level);
            appLog.InfoFormat("miscode: {0} received", miscode);

            using (var con = new SqlConnection(connectionString))
                {
                var cmd = new SqlCommand("proc_mpr_report_dashboard_by_caption_3", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MisCode",
                    Value = miscode,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Period",
                    Value = period,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Year",
                    Value = latestyear,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Level",
                    Value = level,
                });

                con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var pts = new CListInfo();
                      
                    pts.SubCaption = reader["SubCaption"] != DBNull.Value ? reader["SubCaption"].ToString() : "null";
                    pts.CurrentMonth = reader["CurrentMonth"] != DBNull.Value ? Convert.ToDouble(reader["CurrentMonth"].ToString()) : 0;
                    pts.Budget = reader["Budget"] != DBNull.Value ? Convert.ToDouble(reader["Budget"].ToString()) : 0;

                    newList.Add(pts);
                    }
                    con.Close();
                }  // using               
            
            var query = from a in newList
                        join b in configuredCaptions on a.SubCaption equals b
                        select a;

            res = request.CreateResponse<IEnumerable>(HttpStatusCode.OK, query);

            appLog.InfoFormat("about to return the result");

            return res;
        }

        //==================== for cards ends ================================


       


        protected override void Dispose(bool disposing)
        {
            entityContext.Dispose();
            base.Dispose(disposing);
        }
    }
}