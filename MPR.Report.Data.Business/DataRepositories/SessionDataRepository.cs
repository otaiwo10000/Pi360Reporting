using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using MPR.Report.Core;
using MPR.Report.Core.Entities;
using MPR.Report.Core.Interfaces;
using MPR.Report.Core.IRepositoryInterfaces;
using System.Configuration;
using System.Globalization;
using System.Threading;
using System.Data.SqlClient;
//using Pi360Reporting.Models.DataInfo;
//using Pi360Reporting.Models.DataSubdata;

namespace MPR.Report.Data.Business.DataRepositories
{
    public class SessionDataRepository : IDataRepository
    {        
        public SessionVariableInfo SessionDataMtd()
        {
            //Get the culture property of the thread.
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            //Create TextInfo object.
            TextInfo textInfo = cultureInfo.TextInfo;

            SessionVariableInfo sessiondataObj = new SessionVariableInfo();

            string LogOnUserFullName = Convert.ToString(System.Web.HttpContext.Current.Session["session_userfullname"]);

            int Level = Convert.ToInt32(System.Web.HttpContext.Current.Session["session_level"]);
            int currentlyselectedlevel = Convert.ToInt32(System.Web.HttpContext.Current.Session["session_currentlyselectedlevel"]);

            string MisCode = Convert.ToString(System.Web.HttpContext.Current.Session["session_miscode"]);
            string currentlyselectedmiscode = Convert.ToString(System.Web.HttpContext.Current.Session["session_currentlyselectedmiscode"]);

            
            string reportuser = Convert.ToString(System.Web.HttpContext.Current.Session["session_loggedinmisname"]);
            string currentlyselectedmisname = Convert.ToString(System.Web.HttpContext.Current.Session["session_currentlyselectedmisname"]);

            //if (!string.IsNullOrEmpty(Convert.ToString(currentlyselectedmiscode)))
            if (currentlyselectedmiscode.ToLower() != "")
            {
                MisCode = "";
                MisCode = currentlyselectedmiscode;
                Level = currentlyselectedlevel;
                reportuser = currentlyselectedmisname;
            }

            sessiondataObj.Level = Level;
            sessiondataObj.MISCode = MisCode;
            sessiondataObj.LogOnUserFullName = Convert.ToString(System.Web.HttpContext.Current.Session["session_userfullname"]);
            sessiondataObj.ReportUser = reportuser;
            sessiondataObj.StaffId = Convert.ToString(System.Web.HttpContext.Current.Session["session_userstaffid"]);
            sessiondataObj.PhotoUrlPath = Convert.ToString(System.Web.HttpContext.Current.Session["session_photourlpath"]);
            sessiondataObj.Levelcode = Convert.ToString(System.Web.HttpContext.Current.Session["session_levelcode"]);


            return sessiondataObj;
        }
       
    }
} 

