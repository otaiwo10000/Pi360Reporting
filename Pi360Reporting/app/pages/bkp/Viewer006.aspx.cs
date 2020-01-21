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
    public partial class Viewer006 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        //public void Pag(string pat, string typ, string cur )
        {
            try
            {
                if (!IsPostBack)
                {                    
                    string reportId = Request.QueryString["id"].ToString();
                    string Currency = Request.QueryString["currency"].ToString();
                    string Direction = Request.QueryString["direction"].ToString();
                    string LevelSelect = Request.QueryString["levelselect"].ToString();
                    string Office = Request.QueryString["office"].ToString();
                    string Path = Request.QueryString["path"].ToString();
                    string Period = Request.QueryString["period"].ToString();
                    string Staff = Request.QueryString["staff"].ToString();
                    string Top = Request.QueryString["top"].ToString();
                    string Type = Request.QueryString["type"].ToString();
                    string Unit = Request.QueryString["unit"].ToString();
                    string Year = Request.QueryString["year"].ToString();
                   
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

                   

                    reportViewer006.ServerReport.ReportServerUrl = new Uri(reportServerUrl);
                    reportViewer006.ServerReport.ReportServerCredentials = new ReportCredentials(userName, password, domain);
                    reportViewer006.ServerReport.ReportPath = string.Format(reportPath, reportId);
                    //reportViewer.ServerReport.ReportPath = "/reportPath/reportId";
                    reportViewer006.ProcessingMode = ProcessingMode.Remote;
                    reportViewer006.ShowCredentialPrompts = false;


                    ReportParameter[] reportParameter = new ReportParameter[13];
                    reportParameter[0] = new ReportParameter("MisCode", MisCode);
                    reportParameter[1] = new ReportParameter("Level", Level);
                    reportParameter[2] = new ReportParameter("Currency", Currency);
                    reportParameter[3] = new ReportParameter("Direction", Direction);
                    reportParameter[4] = new ReportParameter("LevelSelect", LevelSelect);
                    reportParameter[5] = new ReportParameter("Office", Office);
                    reportParameter[6] = new ReportParameter("Path", Path);
                    reportParameter[7] = new ReportParameter("Period", Period);
                    reportParameter[8] = new ReportParameter("Staff", Staff);
                    reportParameter[9] = new ReportParameter("Top", Top);
                    reportParameter[10] = new ReportParameter("Type", Type);
                    reportParameter[11] = new ReportParameter("Unit", Unit);
                    reportParameter[12] = new ReportParameter("Year", Year);

                    //==== NOTE: for report on server, use the below ============
                    reportViewer006.ServerReport.SetParameters(reportParameter);


                    reportViewer006.ServerReport.Refresh();

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
