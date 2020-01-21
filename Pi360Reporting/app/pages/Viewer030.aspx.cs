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
//using Pi360Reporting.ReportParams;

namespace Pi360Reporting.app.pages
{
    public partial class Viewer030 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {                    
                    string reportId = Convert.ToString(Request.QueryString["id"]);
                    string Year = Convert.ToString(Request.QueryString["year"]);
                    string StartDate = Convert.ToString(Request.QueryString["startdate"]);
                    string EndDate = Convert.ToString(Request.QueryString["enddate"]);
                    string Metric = Convert.ToString(Request.QueryString["metric"]);
                    //string MetricType = Convert.ToString(Request.QueryString["metrictype"]);
                    //string ReportType = Convert.ToString(Request.QueryString["reporttype"]);
                    string Path = Convert.ToString(Request.QueryString["path"]);
                    string AnalysisType = Convert.ToString(Request.QueryString["analysistype"]);
                    //string AnalysisCategory = Convert.ToString(Request.QueryString["analysiscategory"]);
                    string SortOrder = Convert.ToString(Request.QueryString["sortorder"]);


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


                    reportViewer030.ServerReport.ReportServerUrl = new Uri(reportServerUrl);
                    reportViewer030.ServerReport.ReportServerCredentials = new ReportCredentials(userName, password, domain);
                    reportViewer030.ServerReport.ReportPath = string.Format(reportPath, reportId);
                    //reportViewer.ServerReport.ReportPath = "/reportPath/reportId";
                    reportViewer030.ProcessingMode = ProcessingMode.Remote;
                    reportViewer030.ShowCredentialPrompts = false;


                    ReportParameter[] reportParameter = new ReportParameter[9];
                    reportParameter[0] = new ReportParameter("MisCode", MisCode);
                    reportParameter[1] = new ReportParameter("Level", Level);
                    reportParameter[2] = new ReportParameter("Year", Year);
                    reportParameter[3] = new ReportParameter("StartDate", StartDate);
                    reportParameter[4] = new ReportParameter("EndDate", EndDate);
                    reportParameter[5] = new ReportParameter("Metric", Metric);
                    //reportParameter[6] = new ReportParameter("MetricType", MetricType);
                    //reportParameter[7] = new ReportParameter("ReportType", ReportType);
                    reportParameter[6] = new ReportParameter("Path", Path);
                    reportParameter[7] = new ReportParameter("AnalysisType", AnalysisType);
                    //reportParameter[10] = new ReportParameter("AnalysisCategory", AnalysisCategory);
                    reportParameter[8] = new ReportParameter("SortOrder", SortOrder);


                    //==== NOTE: for report on server, use the below ============
                    reportViewer030.ServerReport.SetParameters(reportParameter);

                    reportViewer030.ServerReport.Refresh();

                    ////==== NOTE: for report on local machine, use the below ============
                    //reportViewer.LocalReport.SetParameters(reportParameter);
                    //reportViewer.LocalReport.Refresh();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }   
}
