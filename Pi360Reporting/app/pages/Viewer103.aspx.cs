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
    public partial class Viewer103 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    string reportId = Request.QueryString["id"].ToString();                 

                    var StartDate = Request.QueryString["startdate"];
                    //DateTime StartDate3 = DateTime.ParseExact(StartDate, "ddd MMM dd yyyy HH:mm:ss 'GMT+0000 (GMT Standard Time)'", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime StartDate2 = DateTime.ParseExact(StartDate, "ddd MMM dd yyyy HH:mm:ss 'GMT 0100 (West Africa Standard Time)'", System.Globalization.CultureInfo.InvariantCulture);
                    StartDate = StartDate2.ToString("yyyy-MM-dd");
                   
                    var EndDate = Request.QueryString["enddate"];
                    DateTime EndDate2 = DateTime.ParseExact(EndDate, "ddd MMM dd yyyy HH:mm:ss 'GMT 0100 (West Africa Standard Time)'", System.Globalization.CultureInfo.InvariantCulture);
                    EndDate = EndDate2.ToString("yyyy-MM-dd");

                    //string TeamName = Request.QueryString["teamname"].ToString();
                    //string Currency = Request.QueryString["currency"].ToString();

                    string Level = Convert.ToString(System.Web.HttpContext.Current.Session["session_level"]);
                    string currentlyselectedlevel = Convert.ToString(System.Web.HttpContext.Current.Session["session_currentlyselectedlevel"]);

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
                    string reportPath = ConfigurationManager.AppSettings["ReportPathChannels"];


                    reportViewer103.ServerReport.ReportServerUrl = new Uri(reportServerUrl);
                    reportViewer103.ServerReport.ReportServerCredentials = new ReportCredentials(userName, password, domain);
                    reportViewer103.ServerReport.ReportPath = string.Format(reportPath, reportId);
                    //reportViewer.ServerReport.ReportPath = "/reportPath/reportId";
                    reportViewer103.ProcessingMode = ProcessingMode.Remote;
                    reportViewer103.ShowCredentialPrompts = false;


                    ReportParameter[] reportParameter = new ReportParameter[3];
                    //reportParameter[0] = new ReportParameter("MisCode", MisCode);
                    reportParameter[0] = new ReportParameter("Level", Level);
                    reportParameter[1] = new ReportParameter("StartDate", StartDate);
                    reportParameter[2] = new ReportParameter("EndDate", EndDate);
                    //reportParameter[3] = new ReportParameter("TeamName", TeamName);
                    //reportParameter[5] = new ReportParameter("Currency", Currency);


                    //==== NOTE: for report on server, use the below ============
                    reportViewer103.ServerReport.SetParameters(reportParameter);

                    reportViewer103.ServerReport.Refresh();

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
