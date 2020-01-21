using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPR.Report.Core
{
    public class SessionVariableInfo
    {
        public int Level { get; set; }
        //public string LeveCode { get; set; }
        public string MISCode { get; set; }
        public string Username { get; set; }
        public string LogOnUserFullName { get; set; }
        public string Email { get; set; }
        public string PhotoUrlPath { get; set; }
        public string StaffId { get; set; }
        public string ReportUser { get; set; }
        public string Levelcode { get; set; }
    }
}