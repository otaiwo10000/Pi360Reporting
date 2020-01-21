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


namespace MPR.Report.Data.Business.DataRepositories
{
    public class DashboardCustomerCRMRepository : IDataRepository
    {
        MPRContext2 entityContext = new MPRContext2();

        string connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;
        public List<customerCRM> DashboardCustomerCRMMtd(string param)
        {
            //Get the culture property of the thread.
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            //Create TextInfo object.
            TextInfo textInfo = cultureInfo.TextInfo;

            var newList = new List<customerCRM>();
            
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

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("income_dashboard_customer_analysis", con);
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
                    ParameterName = "Caption",
                    Value = param,
                });

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var pts = new customerCRM();

                    ////tb.AccountOfficer_Code = reader["AccountOfficer_Code"] != DBNull.Value ? reader["AccountOfficer_Code"].ToString() : "default";
                    //pts.MainCaption = reader["MainCaption"] != DBNull.Value ? reader["MainCaption"].ToString() : "null";
                    //pts.Amount = reader["Amount"] != DBNull.Value ? Convert.ToDouble(reader["Amount"].ToString()) : 0;
                    //pts.Budget = reader["Budget"] != DBNull.Value ? Convert.ToDouble(reader["Budget"].ToString()) : 0;
                    //pts.Period = reader["Period"] != DBNull.Value ? Convert.ToInt32(reader["Period"].ToString()) : 0;

                    pts.AccountNo = reader["AccountNo"] != DBNull.Value ? reader["AccountNo"].ToString() : "null";
                    pts.Amount = reader["Amount"] != DBNull.Value ? Convert.ToDouble(reader["Amount"].ToString()) : 0;
                    pts.Name = reader["Name"] != DBNull.Value ? reader["Name"].ToString() : "null";
                    //string acctname = reader["Name"] != DBNull.Value ? reader["Name"].ToString() : "null";
                    //pts.Name = acctname.Substring(0, 12);

                    newList.Add(pts);
                }
                con.Close();
            }
            return newList;
        }

    }
} 

