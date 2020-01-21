using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPR.Report.Core
{
    //public class MixListBarChartInfo
    //{
    //    public string MainCaption { get; set; }
    //    public List<SubMixListBarChartInfo> subobj { get; set; }
    //}

    public class SubCaptionDashboardTrendListInfo
    {
        public string SubCaption { get; set; }
        //public List<DashboardTrendListInfo> abpList { get; set; }
        public List<SubTrendDashboardListInfo> abpList { get; set; }
    }

    //public class SubMixListBarChartInfo
    public class SubTrendDashboardListInfo
    {
        public string SubCaption { get; set; }
        public double Amount { get; set; }
        public double Budget { get; set; }
        public int Period { get; set; }
    }

    public class DashboardTrendListInfo
    {
        public double Amount { get; set; }
        public double Budget { get; set; }
        public int Period { get; set; }
    }

    public class DashboardMainCaptionListInfo
    {
        public string MainCaption { get; set; }
        public double Amount { get; set; }
        public double Budget { get; set; }
        public int Period { get; set; }
    }
    public class ChartMixInfo
    {
        public string MainCaption { get; set; }
        public string SubCaption { get; set; }
        public double Amount { get; set; }
        public double Budget { get; set; }
        public int Period { get; set; }
    }

    public class SubCaptionByCurrencyInfo
    {
        public string lcy { get; set; }
        public string fcy { get; set; }
        public double Amount { get; set; }
    }

    //public class DashboardListInfo
    //{
    //    public List<MixListBarChartInfo> D1List { get; set; }
    //    public List<MixListBarChartInfo> D2List { get; set; }
    //    public List<MixListBarChartInfo> D3List { get; set; }
    //}

    //public class chartLandingPageInfo
    //{
    //    public string name { get; set; } // represent maincaption
    //    public List<SubMixListInfo> data { get; set; }  // represent other columns
    //}

    //public class TMainListInfo
    //{
    //    public List<MixInfo> T1MainList { get; set; }
    //    public List<MixInfo> T2MainList { get; set; }
    //    public List<MixInfo> T3MainList { get; set; }
    //}
    //public class TListInfo
    //{
    //    public List<MixListInfo> T1List { get; set; }
    //    public List<MixListInfo> T2List { get; set; }
    //    public List<MixListInfo> T3List { get; set; }
    //}

    public class DashboardMainCaptionCardsListInfo
    {
        public string MainCaption { get; set; }
        public double CurrentMonth { get; set; }  // i.e Amount
        public double Budget { get; set; }
    }

    public class DashboardSubCaptionCardsListInfo
    {
        public string SubCaption { get; set; }
        public double CurrentMonth { get; set; }  // i.e Amount
        public double Budget { get; set; }
    }

    //public class BarChartInfo
    //{
    //    public string MainCaption { get; set; }
    //    public string SubCaption { get; set; }
    //}

}