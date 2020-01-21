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
using System.ComponentModel.DataAnnotations;
using log4net;
//using Pi360Reporting.ReportParams;

namespace Pi360Reporting.app.pages
{
    public partial class Viewer028 : System.Web.UI.Page
    {
        public static readonly ILog appLog = LogManager.GetLogger("File1Appender");
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

                    string Year = Request.QueryString["year"].ToString();

                    if (Year=="0")
                    {
                        reportViewer028.ShowReportBody = false;
                    }

                    else
                    {
                        string reportId = Request.QueryString["id"].ToString();
                        //string Year = Request.QueryString["year"].ToString();
                        ////string Period = Request.QueryString["period"].ToString();
                        string StartDate = Request.QueryString["startdate"];
                        string EndDate = Request.QueryString["enddate"];

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

                        reportViewer028.ServerReport.ReportServerUrl = new Uri(reportServerUrl);
                        reportViewer028.ServerReport.ReportServerCredentials = new ReportCredentials(userName, password, domain);
                        reportViewer028.ServerReport.ReportPath = string.Format(reportPath, reportId);
                        //reportViewer.ServerReport.ReportPath = "/reportPath/reportId";
                        reportViewer028.ProcessingMode = ProcessingMode.Remote;
                        reportViewer028.ShowCredentialPrompts = false;


                        ReportParameter[] reportParameter = new ReportParameter[5];
                        reportParameter[0] = new ReportParameter("MisCode", MisCode);
                        reportParameter[1] = new ReportParameter("Year", Year);
                        reportParameter[2] = new ReportParameter("Level", Level);
                        reportParameter[3] = new ReportParameter("StartDate", StartDate);
                        reportParameter[4] = new ReportParameter("EndDate", EndDate);


                        //==== NOTE: for report on server, use the below ============
                        reportViewer028.ServerReport.SetParameters(reportParameter);

                        reportViewer028.ServerReport.Refresh();

                        ////==== NOTE: for report on local machine, use the below ============
                        //reportViewer.LocalReport.SetParameters(reportParameter);
                        //reportViewer.LocalReport.Refresh();
                    }
                }
            }
            catch (Exception ex)
            {
                //string customerrormsg = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["CustomErrorMessage"]);

                //throw ex;
                appLog.Error(ex);
                throw;
                //throw new Exception("Something happen. Please, contact the system admin");

                //Response.Clear();
                ////ex = Server.GetLastError();
                //Response.Write(customerrormsg);
                ////Response.Write(ex.Message);
                // Server.ClearError();

            }

        }

    }   
}
