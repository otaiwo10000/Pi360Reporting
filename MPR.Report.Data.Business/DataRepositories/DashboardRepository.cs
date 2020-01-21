using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using MPR.Report.Core;
using MPR.Report.Core.Entities;
using MPR.Report.Core.Interfaces;
using MPR.Report.Core.IRepositoryInterfaces;
using System.Configuration;
using System.Globalization;
using System.Threading;
using System.Data.SqlClient;
//using Pi360Reporting.Models.DataInfo;
//using Pi360Reporting.Models.DataSubdata;

namespace MPR.Report.Data.Business.DataRepositories
{
    public class DashboardRepository : IDataRepository
    {
        MPRContext2 entityContext = new MPRContext2();

        string connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

        //======================= mix starts =======================================================================

        public List<ChartMixInfo> DashboardMixMtd(string param)
        {
            //Get the culture property of the thread.
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            //Create TextInfo object.
            TextInfo textInfo = cultureInfo.TextInfo;
            
            var newList = new List<ChartMixInfo>();

            //var latestmonthyear = (from a in entityContext.IncomeSetUpDailySet

            //                       where a.Year == entityContext.IncomeSetUpDailySet.Max(x => x.Year)
            //                       //select a);
            //                       select new
            //                       {
            //                           Year = a.Year,
            //                           CurrentPeriod = a.CurrentPeriod
            //                       })
            //                   .AsEnumerable().Select(x => new IncomeSetUpDaily
            //                   {
            //                       Year = x.Year,
            //                       CurrentPeriod = x.CurrentPeriod
            //                   })
            //                  .ToList();

            //var latestmonthyear = entityContext.IncomeSetUpDailySet.OrderByDescending(x => x.Year).Take(1);
            //int latestyear = latestmonthyear.Select(x => x.Year).FirstOrDefault();
            //int period = latestmonthyear.Max(x => x.CurrentPeriod);

            int latestyear = Convert.ToInt32(System.Web.HttpContext.Current.Session["latestyear"]);
            int period = Convert.ToInt32(System.Web.HttpContext.Current.Session["currentperiod"]);

            string miscode = Convert.ToString(System.Web.HttpContext.Current.Session["session_miscode"]);
            string currentlyselectedmiscode = Convert.ToString(System.Web.HttpContext.Current.Session["session_currentlyselectedmiscode"]);


            int level = Convert.ToInt32(System.Web.HttpContext.Current.Session["session_level"]);
            int currentlyselectedlevel = Convert.ToInt32(System.Web.HttpContext.Current.Session["session_currentlyselectedlevel"]);

            //string currentlyselectedlevel = Convert.ToString(System.Web.HttpContext.Current.Session["session_currentlyselectedlevel"]);
            //string currentlyselectedlevel_string = (System.Web.HttpContext.Current.Session["session_currentlyselectedlevel"]).ToString();

            if (currentlyselectedmiscode != "")
            {
                miscode = "";
                miscode = currentlyselectedmiscode;
                level = currentlyselectedlevel;
            }

            //var query = menu2.Where(x => x.MenuList == p).Select(x => new mprFintrakMenu_SubObjectListInfo { ReportPAth = x.ReportPAth, ParameterKey = x.ParameterKey, ReportTitle = x.ReportTitle, UIsrefState = x.UIsrefState, ID = x.ID });
            //var conf = entityContext.LandingPageConfigurationSet.Where(x => x.Deleted == false).ToList();
            //var conf = entityContext.DashboardConfigurationSet.Where(x => x.Deleted == false && x.DisplayCaptionObjectCode == "D3").Select(x => x.SubCaption).ToList();
            //List<BarChartInfo> conf = entityContext.DashboardConfigurationSet.Where(x => x.Deleted == false && x.DisplayCaptionObjectCode == "D3").Select(x => new BarChartInfo { MainCaption = x.MainCaption, SubCaption = x.SubCaption }).ToList();

            //string param = entityContext.DashboardConfigurationSet.Where(x => x.Deleted == false && x.DisplayCaptionObjectCode == "D1").Select(x => x.MainCaption).FirstOrDefault();

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
                    Value = param,
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
                    var pts = new ChartMixInfo();

                    ////pts.MPR_Processing_Id = int.Parse(reader["MPR_Processing_Id"].ToString());
                    ////pts.DateTimeToStart = (DateTime)reader["DateTimeToStart"];
                   
                    pts.SubCaption = reader["SubCaption"] != DBNull.Value ? reader["SubCaption"].ToString() : "null";
                    pts.Amount = reader["Amount"] != DBNull.Value ? Convert.ToDouble(reader["Amount"].ToString()) : 0;
                    pts.Budget = reader["Budget"] != DBNull.Value ? Convert.ToDouble(reader["Budget"].ToString()) : 0;
                    pts.Period = reader["Period"] != DBNull.Value ? Convert.ToInt32(reader["Period"].ToString()) : 0;

                    newList.Add(pts);
                }
                con.Close();
            }  
            return newList;
        }


        //============================== mix ends ===========================================================================

        //=======================  trend for subcaption starts =======================================================================

        public List<SubCaptionDashboardTrendListInfo> DashboardTrendMtd(string param)
        {
            var newList2 = new List<SubCaptionDashboardTrendListInfo>();

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

            //var latestmonthyear = entityContext.IncomeSetUpDailySet.OrderByDescending(x => x.Year).Take(1);
            //int latestyear = latestmonthyear.Select(x => x.Year).FirstOrDefault();
            //int period = latestmonthyear.Max(x => x.CurrentPeriod);

            int latestyear = Convert.ToInt32(System.Web.HttpContext.Current.Session["latestyear"]);
            int period = Convert.ToInt32(System.Web.HttpContext.Current.Session["currentperiod"]);

            string miscode = Convert.ToString(System.Web.HttpContext.Current.Session["session_miscode"]);
            string currentlyselectedmiscode = Convert.ToString(System.Web.HttpContext.Current.Session["session_currentlyselectedmiscode"]);


            int level = Convert.ToInt32(System.Web.HttpContext.Current.Session["session_level"]);
            int currentlyselectedlevel = Convert.ToInt32(System.Web.HttpContext.Current.Session["session_currentlyselectedlevel"]);

            if (currentlyselectedmiscode != "")
            {
                miscode = "";
                miscode = currentlyselectedmiscode;
                level = currentlyselectedlevel;
            }

            var newList = new List<SubTrendDashboardListInfo>();

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
                    Value = param,
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
                    var pts = new SubTrendDashboardListInfo();
                    
                    pts.SubCaption = reader["SubCaption"] != DBNull.Value ? reader["SubCaption"].ToString() : "null";
                    pts.Amount = reader["Amount"] != DBNull.Value ? Convert.ToDouble(reader["Amount"].ToString()) : 0;
                    pts.Budget = reader["Budget"] != DBNull.Value ? Convert.ToDouble(reader["Budget"].ToString()) : 0;
                    pts.Period = reader["Period"] != DBNull.Value ? Convert.ToInt32(reader["Period"].ToString()) : 0;

                    newList.Add(pts);
                }
                con.Close();
            }  // using               

            List<string> sub = newList.Select(x => x.SubCaption).Distinct().ToList();

            foreach (var vv in sub)
            {
                //var query = menu2.Where(x => x.MenuList == p).Select(x => new mprFintrakMenu_SubObjectListInfo { ReportPAth = x.ReportPAth, ParameterKey = x.ParameterKey, ReportTitle = x.ReportTitle, UIsrefState = x.UIsrefState, ID = x.ID });
                var query = newList.Where(x => x.SubCaption == vv).Select(x => new SubTrendDashboardListInfo { Amount = x.Amount, Budget = x.Budget, Period = x.Period }).GroupBy(x => x.Period).Select(x => x.FirstOrDefault()).ToList();

                var ob = new SubCaptionDashboardTrendListInfo()
                {
                    abpList = query.ToList(),
                    SubCaption = vv
                };
                newList2.Add(ob);
            }

            return newList2;
        }


        public List<DashboardMainCaptionListInfo> DashboardTrendMtd2(string param)
        {
            var newList2 = new List<DashboardMainCaptionListInfo>();

            //var latestmonthyear = (from a in entityContext.IncomeSetUpDailySet

            //                       where a.Year == entityContext.IncomeSetUpDailySet.Max(x => x.Year)
            //                       //select a);
            //                       select new
            //                       {
            //                           Year = a.Year,
            //                           CurrentPeriod = a.CurrentPeriod
            //                       })
            //                   .AsEnumerable().Select(x => new IncomeSetUpDaily
            //                   {
            //                       Year = x.Year,
            //                       CurrentPeriod = x.CurrentPeriod
            //                   })
            //                  .ToList();

            //var latestmonthyear = entityContext.IncomeSetUpDailySet.OrderByDescending(x => x.Year).Take(1);
            //int latestyear = latestmonthyear.Select(x => x.Year).FirstOrDefault();
            //int period = latestmonthyear.Max(x => x.CurrentPeriod);

            int latestyear = Convert.ToInt32(System.Web.HttpContext.Current.Session["latestyear"]);
            int period = Convert.ToInt32(System.Web.HttpContext.Current.Session["currentperiod"]);

            string miscode = Convert.ToString(System.Web.HttpContext.Current.Session["session_miscode"]);
            string currentlyselectedmiscode = Convert.ToString(System.Web.HttpContext.Current.Session["session_currentlyselectedmiscode"]);


            int level = Convert.ToInt32(System.Web.HttpContext.Current.Session["session_level"]);
            int currentlyselectedlevel = Convert.ToInt32(System.Web.HttpContext.Current.Session["session_currentlyselectedlevel"]);

            if (currentlyselectedmiscode != "")
            {
                miscode = "";
                miscode = currentlyselectedmiscode;
                level = currentlyselectedlevel;
            }

            var newList = new List<DashboardMainCaptionListInfo>();

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("proc_mpr_report_dashboard_by_caption_2", con);  //changed from _4 to _2
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
                    Value = param,
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
                    var pts = new DashboardMainCaptionListInfo();
                    
                    pts.MainCaption = reader["MainCaption"] != DBNull.Value ? reader["MainCaption"].ToString() : "null";
                    pts.Amount = reader["Amount"] != DBNull.Value ? Convert.ToDouble(reader["Amount"].ToString()) : 0;
                    pts.Budget = reader["Budget"] != DBNull.Value ? Convert.ToDouble(reader["Budget"].ToString()) : 0;
                    pts.Period = reader["Period"] != DBNull.Value ? Convert.ToInt32(reader["Period"].ToString()) : 0;

                    newList.Add(pts);
                }
                con.Close();
            }  // using               
           
            return newList;
        }

        //============================== trend for subcaption ends ===========================================================================

        //==================== for cards starts ================================

        public List<DashboardMainCaptionListInfo> DashboardCardMainCaptionMtd()
        {
            //Get the culture property of the thread.
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            //Create TextInfo object.
            TextInfo textInfo = cultureInfo.TextInfo;

            var newList = new List<DashboardMainCaptionListInfo>();

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

            //var latestmonthyear = entityContext.IncomeSetUpDailySet.OrderByDescending(x => x.Year).Take(1);
            //int latestyear = latestmonthyear.Select(x => x.Year).FirstOrDefault();
            //int period = latestmonthyear.Max(x => x.CurrentPeriod);

            int latestyear = Convert.ToInt32(System.Web.HttpContext.Current.Session["latestyear"]);
            int period = Convert.ToInt32(System.Web.HttpContext.Current.Session["currentperiod"]);

            string miscode = Convert.ToString(System.Web.HttpContext.Current.Session["session_miscode"]);
            string currentlyselectedmiscode = Convert.ToString(System.Web.HttpContext.Current.Session["session_currentlyselectedmiscode"]);


            int level = Convert.ToInt32(System.Web.HttpContext.Current.Session["session_level"]);
            int currentlyselectedlevel = Convert.ToInt32(System.Web.HttpContext.Current.Session["session_currentlyselectedlevel"]);

            if (currentlyselectedmiscode != "")
            {
                miscode = "";
                miscode = currentlyselectedmiscode;
                level = currentlyselectedlevel;
            }

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("proc_mpr_report_ratios_2B", con);
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

                //cmd.Parameters.Add(new SqlParameter
                //{
                //    ParameterName = "TrendOrMonthly",
                //    Value = "monthly",
                //});

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
            }
            return newList;
        }


        // ----------------------------------------------------------------------------------------------------------
        public List<DashboardMainCaptionCardsListInfo> DashboardMainCaptionCardsMtd(string param)
        {
            var newList = new List<DashboardMainCaptionCardsListInfo>();
            var newList2 = new List<DashboardMainCaptionCardsListInfo>();

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

            //var latestmonthyear = entityContext.IncomeSetUpDailySet.OrderByDescending(x => x.Year).Take(1);
            //int latestyear = latestmonthyear.Select(x => x.Year).FirstOrDefault();
            //int period = latestmonthyear.Max(x => x.CurrentPeriod);

            int latestyear = Convert.ToInt32(System.Web.HttpContext.Current.Session["latestyear"]);
            int period = Convert.ToInt32(System.Web.HttpContext.Current.Session["currentperiod"]);

            string cad = "CARD";

            List<string> configuredCaptions = entityContext.LandingPageConfigurationSet.Where(x => x.Deleted == false && x.Type == cad.ToUpper()).Select(x => x.MainCaption).ToList();
            
            param = "";

            string miscode = System.Web.HttpContext.Current.Session["session_miscode"].ToString();
            string currentlyselectedmiscode = Convert.ToString(System.Web.HttpContext.Current.Session["session_currentlyselectedmiscode"]);

            int level = Convert.ToInt32(System.Web.HttpContext.Current.Session["session_level"]);
            int currentlyselectedlevel = Convert.ToInt32(System.Web.HttpContext.Current.Session["session_currentlyselectedlevel"]);

            if (currentlyselectedmiscode != "")
            {
                miscode = "";
                miscode = currentlyselectedmiscode;
                level = currentlyselectedlevel;
            }

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("proc_mpr_report_dashboard_by_caption_4", con);
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
                    Value = param,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "TrendOrMonthly",
                    Value = "Monthly",
                });

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var cap = new DashboardMainCaptionCardsListInfo();
                       
                    cap.MainCaption = reader["MainCaption"] != DBNull.Value ? reader["MainCaption"].ToString() : "null";
                    cap.CurrentMonth = reader["currentmonth"] != DBNull.Value ? Convert.ToInt32(reader["currentmonth"].ToString()) : 0;
                    cap.Budget = reader["Budget"] != DBNull.Value ? Convert.ToDouble(reader["Budget"].ToString()) : 0;

                    newList.Add(cap);
                }
                con.Close();
            }  // using               
            
            return newList;
        }

        //==================== for cards ends ================================

    }
} 

