﻿using System;
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
    public partial class Viewer101 : System.Web.UI.Page
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

                    string Channel = Request.QueryString["channel"].ToString();
                    string Currency = Request.QueryString["currency"].ToString();
                    //string TeamName = Request.QueryString["teamname"].ToString();

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
                    string reportPath = ConfigurationManager.AppSettings["ReportPathChannels"];


                    reportViewer101.ServerReport.ReportServerUrl = new Uri(reportServerUrl);
                    reportViewer101.ServerReport.ReportServerCredentials = new ReportCredentials(userName, password, domain);
                    reportViewer101.ServerReport.ReportPath = string.Format(reportPath, reportId);
                    //reportViewer.ServerReport.ReportPath = "/reportPath/reportId";
                    reportViewer101.ProcessingMode = ProcessingMode.Remote;
                    reportViewer101.ShowCredentialPrompts = false;


                    ReportParameter[] reportParameter = new ReportParameter[6];
                    reportParameter[0] = new ReportParameter("MisCode", MisCode);
                    reportParameter[1] = new ReportParameter("Level", Level);
                    reportParameter[2] = new ReportParameter("StartDate", StartDate);
                    reportParameter[3] = new ReportParameter("EndDate", EndDate);
                    reportParameter[4] = new ReportParameter("Channel", Channel);
                    reportParameter[5] = new ReportParameter("Currency", Currency);
                    //reportParameter[6] = new ReportParameter("TeamName", TeamName);


                    //==== NOTE: for report on server, use the below ============
                    reportViewer101.ServerReport.SetParameters(reportParameter);

                    reportViewer101.ServerReport.Refresh();

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
