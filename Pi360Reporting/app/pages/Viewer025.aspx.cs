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
    public partial class Viewer025 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {                    
                    string reportId = Request.QueryString["id"].ToString();
                    string Year = Request.QueryString["year"].ToString();
                    string Period = Request.QueryString["period"].ToString();
                    string Report = Request.QueryString["report"].ToString();
                    string Band = Request.QueryString["band"].ToString();
                    string Path = Request.QueryString["path"].ToString();


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


                    reportViewer025.ServerReport.ReportServerUrl = new Uri(reportServerUrl);
                    reportViewer025.ServerReport.ReportServerCredentials = new ReportCredentials(userName, password, domain);
                    reportViewer025.ServerReport.ReportPath = string.Format(reportPath, reportId);
                    //reportViewer.ServerReport.ReportPath = "/reportPath/reportId";
                    reportViewer025.ProcessingMode = ProcessingMode.Remote;
                    reportViewer025.ShowCredentialPrompts = false;


                    ReportParameter[] reportParameter = new ReportParameter[7];
                    reportParameter[0] = new ReportParameter("MisCode", MisCode);
                    reportParameter[1] = new ReportParameter("Period", Period);
                    reportParameter[2] = new ReportParameter("Year", Year);
                    reportParameter[3] = new ReportParameter("Level", Level);
                    reportParameter[4] = new ReportParameter("Path", Path);
                    reportParameter[5] = new ReportParameter("Report", Report);
                    reportParameter[6] = new ReportParameter("Band", Band);

                    //reportParameter[8] = new ReportParameter("NRFF", NRFF);                  

                    //==== NOTE: for report on server, use the below ============
                    reportViewer025.ServerReport.SetParameters(reportParameter);

                    reportViewer025.ServerReport.Refresh();

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
