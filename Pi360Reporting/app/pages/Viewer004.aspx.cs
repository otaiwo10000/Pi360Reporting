using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Pi360Reporting.ReportsCredentials;
using Microsoft.Reporting.WebForms;
using Pi360Reporting.Controllers.ApiControllers;
using MPR.Report.Core.Entities;
//using Pi360Reporting.ReportParams;

namespace Pi360Reporting.app.pages
{
    //[OtherMethods.AccessMth]
    public partial class Viewer004 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        //public void Pag(string pat, string typ, string cur )
        {
            try
            {
                if (!IsPostBack)
                {
                    MPR.Report.Data.Business.MPRContext2 entityContext = new MPR.Report.Data.Business.MPRContext2();

                    string query = "SELECT * FROM MPRReportStatus";

                    MPRReportStatus MPRReportStatusObj = entityContext.Database.SqlQuery<MPRReportStatus>(query).FirstOrDefault();

                    string Period = Request.QueryString["period"].ToString();

                    //string currentPeriod = Convert.ToString((System.Web.HttpContext.Current.Session["currentperiod"]));
                    //string rStatusPeriod = MPRReportStatusObj.Period.ToString().Trim();
                    //string[] rStatusPeriod = MPRReportStatusObj.Period.ToString().Trim().Split(',');
                    List<string> rStatusPeriod = MPRReportStatusObj.Period.ToString().Trim().Split(',').ToList();

                    string Year = Request.QueryString["year"].ToString();
                    //string currentYear = Convert.ToString((System.Web.HttpContext.Current.Session["latestyear"]));
                    string rStatusYear = MPRReportStatusObj.Year.ToString().Trim();

                    //string currentstatus = Request.QueryString["currentstatus"].ToString();
                    string rStatus = MPRReportStatusObj.Status.ToUpper().Trim();

                    //string loggedinusermiscode = Convert.ToString((System.Web.HttpContext.Current.Session["ProfitCenterMisCode"]));
                    //loggedinusermiscode = loggedinusermiscode.ToUpper();
                    string loggedinuserdefcode = Convert.ToString((System.Web.HttpContext.Current.Session["ProfitCenterDefinitionCode"]));
                    loggedinuserdefcode = loggedinuserdefcode.ToUpper().Trim();

                    string unlockreportfor = ConfigurationManager.AppSettings["UnLockReportFor"];
                    unlockreportfor = unlockreportfor.ToUpper();


                    //if (currentPeriod == Period && currentYear == Year && currentstatus.ToUpper() == "OFF" && !unlockreportfor.Contains(loggedinuserdefcode))
                    //if (rStatusPeriod == Period && rStatusYear == Year && rStatus == "OFF" && !unlockreportfor.Contains(loggedinuserdefcode))
                    if ((rStatusPeriod.IndexOf(Period) != -1) && rStatusYear == Year && rStatus == "OFF" && !unlockreportfor.Contains(loggedinuserdefcode))
                    {
                        reportViewer004.ShowReportBody = false;
                    }


                    else
                    {
                        string reportId = Request.QueryString["id"].ToString();
                        string Currency = Request.QueryString["currency"].ToString();
                        string Path = Request.QueryString["path"].ToString();
                        string Type = Request.QueryString["type"].ToString();

                        //string MisCode = "totalbank";

                        string Level = Convert.ToString(System.Web.HttpContext.Current.Session["session_level"]);
                        string currentlyselectedlevel = Convert.ToString(System.Web.HttpContext.Current.Session["session_currentlyselectedlevel"]);

                        string MisCode = Convert.ToString(System.Web.HttpContext.Current.Session["session_miscode"]);
                        string currentlyselectedmiscode = Convert.ToString(System.Web.HttpContext.Current.Session["session_currentlyselectedmiscode"]);

                        //if (!string.IsNullOrEmpty(Convert.ToString(currentlyselectedmiscode)))
                        if (!string.IsNullOrEmpty(Convert.ToString(currentlyselectedmiscode)) || currentlyselectedmiscode.ToLower() != "")
                        {
                            MisCode = "";
                            MisCode = currentlyselectedmiscode;
                            Level = currentlyselectedlevel;
                        }

                        string reportServerUrl = ConfigurationManager.AppSettings["ReportServerURL"];
                        string domain = ConfigurationManager.AppSettings["rsDomain"];
                        string userName = ConfigurationManager.AppSettings["rsUserName"];
                        string password = ConfigurationManager.AppSettings["rsPassword"];
                        string reportPath = ConfigurationManager.AppSettings["ReportPath"];


                        reportViewer004.ServerReport.ReportServerUrl = new Uri(reportServerUrl);
                        reportViewer004.ServerReport.ReportServerCredentials = new ReportCredentials(userName, password, domain);
                        reportViewer004.ServerReport.ReportPath = string.Format(reportPath, reportId);
                       // reportViewer004.ServerReport.ReportPath = string.Format(reportPath, "Balancesheet");
                        reportViewer004.ProcessingMode = ProcessingMode.Remote;
                        reportViewer004.ShowCredentialPrompts = false;


                        ReportParameter[] reportParameter = new ReportParameter[7];
                        reportParameter[0] = new ReportParameter("MisCode", MisCode);
                        reportParameter[1] = new ReportParameter("Period", Period);
                        reportParameter[2] = new ReportParameter("Year", Year);
                        reportParameter[3] = new ReportParameter("Level", Level);
                        reportParameter[4] = new ReportParameter("Path", Path);
                        reportParameter[5] = new ReportParameter("Type", Type);
                        reportParameter[6] = new ReportParameter("Currency", Currency);

                        //ReportParameter[] reportParameter = new ReportParameter[7];
                        //reportParameter[0] = new ReportParameter("MisCode", "bnk");
                        //reportParameter[1] = new ReportParameter("Period", "8");
                        //reportParameter[2] = new ReportParameter("Year", "2019");
                        //reportParameter[3] = new ReportParameter("Level", "0");
                        //reportParameter[4] = new ReportParameter("Path", "");
                        //reportParameter[5] = new ReportParameter("Type", "BS");
                        //reportParameter[6] = new ReportParameter("Currency", "naira");

                        //reportParameter[8] = new ReportParameter("NRFF", NRFF);                  

                        //==== NOTE: for report on server, use the below ============
                        reportViewer004.ServerReport.SetParameters(reportParameter);

                        reportViewer004.ServerReport.Refresh();

                        ////==== NOTE: for report on local machine, use the below ============
                        //reportViewer.LocalReport.SetParameters(reportParameter);
                        //reportViewer.LocalReport.Refresh();
                    } //end of else
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }   
}
