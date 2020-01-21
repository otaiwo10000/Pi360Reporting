using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPR.Report.Core
{
    public class MixListInfo
    {
        public string MainCaption { get; set; }
        public List<SubMixListInfo> subobj { get; set; }
    }

    public class SubCaptionTrendListInfo
    {
        public string SubCaption { get; set; }
        public List<ABPTrendListInfo> abpList { get; set; }
    }

    public class SubMixListInfo
    {
        public string SubCaption { get; set; }
        public double Amount { get; set; }
        public double Budget { get; set; }
        public int Period { get; set; }
    }

    public class ABPTrendListInfo
    {
        public double Amount { get; set; }
        public double Budget { get; set; }
        public int Period { get; set; }
    }
    public class MainMixListInfo
    {
        public string MainCaption { get; set; }
        public double Amount { get; set; }
        public double Budget { get; set; }
        public int Period { get; set; }
    }
    public class MixInfo
    {
        public string MainCaption { get; set; }
        public string SubCaption { get; set; }
        public double Amount { get; set; }
        public double Budget { get; set; }
        public int Period { get; set; }
    }

    public class DListInfo
    {
        public List<MixListInfo> D1List { get; set; }
        public List<MixListInfo> D2List { get; set; }
        public List<MixListInfo> D3List { get; set; }
    }

    public class chartLandingPageInfo
    {
        public string name { get; set; } // represent maincaption
        public List<SubMixListInfo> data { get; set; }  // represent other columns
    }

    public class TMainListInfo
    {
        public List<MixInfo> T1MainList { get; set; }
        public List<MixInfo> T2MainList { get; set; }
        public List<MixInfo> T3MainList { get; set; }
    }
    public class TListInfo
    {
        public List<MixListInfo> T1List { get; set; }
        public List<MixListInfo> T2List { get; set; }
        public List<MixListInfo> T3List { get; set; }
    }

    public class CListInfo
    {
        public string SubCaption { get; set; }
        public double CurrentMonth { get; set; }  // i.e Amount
        public double Budget { get; set; }
    }

}