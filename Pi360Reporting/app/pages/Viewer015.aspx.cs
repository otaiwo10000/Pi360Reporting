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
    public partial class Viewer015 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
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
                        reportViewer015.ShowReportBody = false;
                    }
                    else
                    {
                        string reportId = Request.QueryString["id"].ToString();
                        string Path = Request.QueryString["path"].ToString();
                        //string Period = Request.QueryString["period"].ToString();
                        string Currency = Request.QueryString["currency"].ToString();
                        string NRFF = Request.QueryString["nrff"].ToString();
                        //string Year = Request.QueryString["year"].ToString();

                        string Level = Convert.ToString(System.Web.HttpContext.Current.Session["session_level"]);
                        string currentlyselectedlevel = Convert.ToString(System.Web.HttpContext.Current.Session["session_currentlyselectedlevel"]);

                        string MisCode = Convert.ToString(System.Web.HttpContext.Current.Session["session_miscode"]);
                        string currentlyselectedmiscode = Convert.ToString(System.Web.HttpContext.Current.Session["session_currentlyselectedmiscode"]);

                        //if (!string.IsNullOrEmpty(Convert.ToString(currentlyselectedmiscode)))
                        //if (!string.IsNullOrEmpty(Convert.ToString(currentlyselectedmiscode)) || currentlyselectedmiscode.ToLower() != "")
                        if (currentlyselectedmiscode != "")
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


                        reportViewer015.ServerReport.ReportServerUrl = new Uri(reportServerUrl);
                        reportViewer015.ServerReport.ReportServerCredentials = new ReportCredentials(userName, password, domain);
                        reportViewer015.ServerReport.ReportPath = string.Format(reportPath, reportId);
                        //reportViewer.ServerReport.ReportPath = "/reportPath/reportId";
                        reportViewer015.ProcessingMode = ProcessingMode.Remote;
                        reportViewer015.ShowCredentialPrompts = false;


                        ReportParameter[] reportParameter = new ReportParameter[7];
                        reportParameter[0] = new ReportParameter("MisCode", MisCode);
                        reportParameter[1] = new ReportParameter("Level", Level);
                        reportParameter[2] = new ReportParameter("Path", Path);
                        reportParameter[3] = new ReportParameter("Period", Period);
                        reportParameter[4] = new ReportParameter("Currency", Currency);
                        reportParameter[5] = new ReportParameter("NRFF", NRFF);
                        reportParameter[6] = new ReportParameter("Year", Year);


                        //==== NOTE: for report on server, use the below ============
                        reportViewer015.ServerReport.SetParameters(reportParameter);

                        reportViewer015.ServerReport.Refresh();

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
