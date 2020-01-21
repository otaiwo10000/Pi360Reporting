using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//namespace Pi360Reporting.Models.DataInfo
namespace MPR.Report.Core
{
    public class TotalbankData
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
    public class DirectorateData
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class RegionData
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
    public class DivisionData
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
    public class BranchData
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
    public class AccountOfficerData
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class TeamData
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class ZoneData
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class GroupData
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class SegmentData
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class SuperSegmentData
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class SectorData
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class YearPeriodData
    {
        //public string Year { get; set; }
        public int Year { get; set; }
        public int Period { get; set; }
    }
    public class TeamStructureData
    {
        public List<TotalbankData> TotalbankList { get; set; }
        public List<DirectorateData> DirectorateList { get; set; }
        public List<RegionData> RegionList { get; set; }
        public List<DivisionData> DivisionList { get; set; }
        public List<BranchData> BranchList { get; set; }
        public List<AccountOfficerData> AccountOfficerList { get; set; }
        public List<TeamData> TeamList { get; set; }
        public List<ZoneData> ZoneList { get; set; }
        public List<GroupData> GroupList { get; set; }
        public List<SegmentData> SegmentList { get; set; }
        public List<SuperSegmentData> SuperSegmentList { get; set; }
        public List<SectorData> SectorList { get; set; }
    }
}