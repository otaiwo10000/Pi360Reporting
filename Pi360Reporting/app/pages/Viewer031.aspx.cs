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
    public partial class Viewer031 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {                    
                    string reportId = Convert.ToString(Request.QueryString["id"]);
                    string Year = Convert.ToString(Request.QueryString["year"]);
                    string Period = Convert.ToString(Request.QueryString["period"]);
                    string Unit = Convert.ToString(Request.QueryString["unit"]);
                    string LoginID = Convert.ToString(System.Web.HttpContext.Current.Session["session_loggedinID"]);

                    ////Unit = Unit.Replace("AMPERSANDXTER", "&");
                    ////Unit = Unit.Replace("DOTXTER", ".");


                    //string Level = Convert.ToString(System.Web.HttpContext.Current.Session["session_level"]);
                    //string currentlyselectedlevel = Convert.ToString(System.Web.HttpContext.Current.Session["session_currentlyselectedlevel"]);

                    //string MisCode = Convert.ToString(System.Web.HttpContext.Current.Session["session_miscode"]);
                    //string currentlyselectedmiscode = Convert.ToString(System.Web.HttpContext.Current.Session["session_currentlyselectedmiscode"]);

                    ////if (!string.IsNullOrEmpty(Convert.ToString(currentlyselectedmiscode)))
                    //if (!string.IsNullOrEmpty(Convert.ToString(currentlyselectedmiscode)) || currentlyselectedmiscode.ToLower() != "")
                    //{
                    //    MisCode = "";
                    //    MisCode = currentlyselectedmiscode;
                    //    Level = currentlyselectedlevel;
                    //}                       
                   
                    string reportServerUrl = ConfigurationManager.AppSettings["ReportServerURL"];
                    string domain = ConfigurationManager.AppSettings["rsDomain"];
                    string userName = ConfigurationManager.AppSettings["rsUserName"];
                    string password = ConfigurationManager.AppSettings["rsPassword"];
                    string reportPath = ConfigurationManager.AppSettings["ReportPath"];


                    reportViewer031.ServerReport.ReportServerUrl = new Uri(reportServerUrl);
                    reportViewer031.ServerReport.ReportServerCredentials = new ReportCredentials(userName, password, domain);
                    reportViewer031.ServerReport.ReportPath = string.Format(reportPath, reportId);
                    //reportViewer.ServerReport.ReportPath = "/reportPath/reportId";
                    reportViewer031.ProcessingMode = ProcessingMode.Remote;
                    reportViewer031.ShowCredentialPrompts = false;


                    ReportParameter[] reportParameter = new ReportParameter[4];
                    //reportParameter[0] = new ReportParameter("MisCode", MisCode);
                    //reportParameter[1] = new ReportParameter("Level", Level);
                    reportParameter[0] = new ReportParameter("Year", Year);
                    reportParameter[1] = new ReportParameter("Period", Period);
                    reportParameter[2] = new ReportParameter("LoginID", LoginID);
                    reportParameter[3] = new ReportParameter("Unit", Unit);                    


                    //==== NOTE: for report on server, use the below ============
                    reportViewer031.ServerReport.SetParameters(reportParameter);

                    reportViewer031.ServerReport.Refresh();

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
