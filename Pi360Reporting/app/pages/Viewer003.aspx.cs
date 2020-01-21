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
    public partial class Viewer003 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        //public void Pag(string pat, string typ, string cur )
         {
            try
            {
                if (!IsPostBack)
                {
                    //var RunDate = Request.QueryString["rundate"];
                    ////DateTime StartDate3 = DateTime.ParseExact(StartDate, "ddd MMM dd yyyy HH:mm:ss 'GMT+0000 (GMT Standard Time)'", System.Globalization.CultureInfo.InvariantCulture);
                    //DateTime rdate = DateTime.ParseExact(RunDate, "ddd MMM dd yyyy HH:mm:ss 'GMT 0100 (West Africa Standard Time)'", System.Globalization.CultureInfo.InvariantCulture);
                    //RunDate = rdate.ToString("yyyy-MM-dd");


                    string reportId = Request.QueryString["id"].ToString();
                    string Year = Request.QueryString["year"].ToString();
                    string Period = Request.QueryString["period"].ToString();
                    string Path = Request.QueryString["path"].ToString();
                    string RunDate = Request.QueryString["rundate"];

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
                   
                    //string Path = Request.QueryString["pat"].ToString();
                    //string Type = Request.QueryString["typ"].ToString();
                    //string Currency = Request.QueryString["cur"].ToString();

                    //string NRFF = Request.QueryString["nrff"].ToString();                   

                    string reportServerUrl = ConfigurationManager.AppSettings["ReportServerURL"];
                    string domain = ConfigurationManager.AppSettings["rsDomain"];
                    string userName = ConfigurationManager.AppSettings["rsUserName"];
                    string password = ConfigurationManager.AppSettings["rsPassword"];
                    string reportPath = ConfigurationManager.AppSettings["ReportPath"];

                    //string MisCode = "totalbank";
                    //string Period = "6";
                    //string Year = "2018";
                    // string Level = "0";
                    //string Path = "Balancesheet";
                    // string Type = "bs";
                    //string Currency = "naira";



                    reportViewer003.ServerReport.ReportServerUrl = new Uri(reportServerUrl);
                    reportViewer003.ServerReport.ReportServerCredentials = new ReportCredentials(userName, password, domain);
                    reportViewer003.ServerReport.ReportPath = string.Format(reportPath, reportId);
                    //reportViewer.ServerReport.ReportPath = "/reportPath/reportId";
                    reportViewer003.ProcessingMode = ProcessingMode.Remote;
                    reportViewer003.ShowCredentialPrompts = false;


                    ReportParameter[] reportParameter = new ReportParameter[6];
                    reportParameter[0] = new ReportParameter("MisCode", MisCode);
                    reportParameter[1] = new ReportParameter("Period", Period);
                    reportParameter[2] = new ReportParameter("Year", Year);
                    reportParameter[3] = new ReportParameter("Level", Level);
                    reportParameter[4] = new ReportParameter("Path", Path);
                    reportParameter[5] = new ReportParameter("RunDate", RunDate);

                    //reportParameter[8] = new ReportParameter("NRFF", NRFF);                  

                    //==== NOTE: for report on server, use the below ============
                    reportViewer003.ServerReport.SetParameters(reportParameter);

                    reportViewer003.ServerReport.Refresh();

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
