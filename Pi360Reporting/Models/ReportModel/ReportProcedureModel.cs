using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pi360Reporting.Models.AccountModel
{
    public class ReportProcedureModel
    {      
        public string fiName { get; set; }
    }

    public class rundateModel
    {
        //public DateTime RunDate { get; set; }
        public string RunDate { get; set; }
    }


    public class reportStatusModel
    {
        public int Year { get; set; }
        public int Period { get; set; }
        public string ReportStatus { get; set; }
        public string loggedinmiscode { get; set; }
        public string loggedindefcode { get; set; }
    }

    public class ScoreCardMetricsModel
    {
        public string Metric { get; set; }
        public int Position { get; set; }
    }

    public class ScoreCardRolesModel
    {
        public string Caption { get; set; }
        public int Position { get; set; }
    }

    public class YearModel
    {
        public string value { get; set; }
        public string name { get; set; }
    }

    public class PeriodModel
    {
        public string value { get; set; }
        public string name { get; set; }
    }

    public class YearPeriodModel
    {
        public List<YearModel> yList { get; set; }
        public List<PeriodModel> pList { get; set; }
    }

    public class MPRReportStatusModel
    {
        public int Year { get; set; }
        //public int Period { get; set; }
        public string Period { get; set; }
        public string ReportStatus { get; set; }
        public string loggedinmiscode { get; set; }
        public string loggedindefcode { get; set; }
    }

    //public class MPRReportStatus_2Model
    //{
    //    public int Year { get; set; }
    //    public string Period { get; set; }
    //    public string ReportStatus { get; set; }
    //    public string loggedinmiscode { get; set; }
    //    public string loggedindefcode { get; set; }
    //}

}