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
    public partial class Viewer036 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {                  
                    string reportId = Request.QueryString["id"].ToString();                  
                    string benchmarkAmount = Request.QueryString["benchmarkamount"].ToString();
                    string report = Request.QueryString["report"].ToString();                    
                    
                    var currentDate = Request.QueryString["currentdate"].ToString();
                    //var StartDate = Request.QueryString["startdate"];
                    ////DateTime StartDate3 = DateTime.ParseExact(StartDate, "ddd MMM dd yyyy HH:mm:ss 'GMT+0000 (GMT Standard Time)'", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime CurrentDate2 = DateTime.ParseExact(currentDate, "ddd MMM dd yyyy HH:mm:ss 'GMT 0100 (West Africa Standard Time)'", System.Globalization.CultureInfo.InvariantCulture);
                    currentDate = CurrentDate2.ToString("yyyy-MM-dd");

                    var  previousDate = Request.QueryString["previousdate"].ToString();
                    //var EndDate = Request.QueryString["enddate"];
                    DateTime PreviousDate2 = DateTime.ParseExact(previousDate, "ddd MMM dd yyyy HH:mm:ss 'GMT 0100 (West Africa Standard Time)'", System.Globalization.CultureInfo.InvariantCulture);
                    previousDate = PreviousDate2.ToString("yyyy-MM-dd");

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

                
                    reportViewer036.ServerReport.ReportServerUrl = new Uri(reportServerUrl);
                    reportViewer036.ServerReport.ReportServerCredentials = new ReportCredentials(userName, password, domain);
                    reportViewer036.ServerReport.ReportPath = string.Format(reportPath, reportId);
                    //reportViewer.ServerReport.ReportPath = "/reportPath/reportId";
                    reportViewer036.ProcessingMode = ProcessingMode.Remote;
                    reportViewer036.ShowCredentialPrompts = false;


                    ReportParameter[] reportParameter = new ReportParameter[6];
                    reportParameter[0] = new ReportParameter("MisCode", MisCode);
                    reportParameter[1] = new ReportParameter("Level", Level);
                    reportParameter[2] = new ReportParameter("currentDate", currentDate);
                    reportParameter[3] = new ReportParameter("previousDate", previousDate);
                    reportParameter[4] = new ReportParameter("benchmarkAmount", benchmarkAmount);
                    reportParameter[5] = new ReportParameter("report", report);                  

                    //==== NOTE: for report on server, use the below ============
                    reportViewer036.ServerReport.SetParameters(reportParameter);

                    reportViewer036.ServerReport.Refresh();

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
