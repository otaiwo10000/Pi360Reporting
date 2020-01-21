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
    public partial class Viewer019 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //string reportId = Request.QueryString["id"].ToString();
                    //string Year = Request.QueryString["year"].ToString();
                    //string Period = Request.QueryString["period"].ToString();
                    //string Path = Request.QueryString["path"].ToString();
                    //string RunDate = Request.QueryString["rundate"].ToString();

                    string reportId = Request.QueryString["id"].ToString();
                    string Direction = Request.QueryString["direction"].ToString();
                    string Path = Request.QueryString["path"].ToString();
                    string Period = Request.QueryString["period"].ToString();
                    string ProductType = Request.QueryString["producttype"].ToString();
                    string Ranking = Request.QueryString["ranking"].ToString();
                    string State = Request.QueryString["state"].ToString();
                    string Top = Request.QueryString["top"].ToString();
                    string Type = Request.QueryString["type"].ToString();
                    string Year = Request.QueryString["year"].ToString();
                    //string RunDate = Request.QueryString["rundate"].ToString();
                    string StartDate = Request.QueryString["startdate"].ToString();
                    string EndDate = Request.QueryString["enddate"].ToString();

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

                
                    reportViewer019.ServerReport.ReportServerUrl = new Uri(reportServerUrl);
                    reportViewer019.ServerReport.ReportServerCredentials = new ReportCredentials(userName, password, domain);
                    reportViewer019.ServerReport.ReportPath = string.Format(reportPath, reportId);
                    //reportViewer.ServerReport.ReportPath = "/reportPath/reportId";
                    reportViewer019.ProcessingMode = ProcessingMode.Remote;
                    reportViewer019.ShowCredentialPrompts = false;


                    ReportParameter[] reportParameter = new ReportParameter[13];
                    reportParameter[0] = new ReportParameter("MisCode", MisCode);
                    reportParameter[1] = new ReportParameter("Level", Level);
                    reportParameter[2] = new ReportParameter("Direction", Direction);
                    reportParameter[3] = new ReportParameter("Path", Path);
                    reportParameter[4] = new ReportParameter("Period", Period);
                    reportParameter[5] = new ReportParameter("ProductType", ProductType);
                    reportParameter[6] = new ReportParameter("Ranking", Ranking);
                    reportParameter[7] = new ReportParameter("State", State);
                    reportParameter[8] = new ReportParameter("Top", Top);
                    reportParameter[9] = new ReportParameter("Year", Year);
                    reportParameter[10] = new ReportParameter("Type", Type);
                    //reportParameter[11] = new ReportParameter("RunDate", RunDate);
                    reportParameter[11] = new ReportParameter("StartDate", StartDate);
                    reportParameter[12] = new ReportParameter("EndDate", EndDate);


                    //==== NOTE: for report on server, use the below ============
                    reportViewer019.ServerReport.SetParameters(reportParameter);

                    reportViewer019.ServerReport.Refresh();

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
