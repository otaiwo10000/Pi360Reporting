using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPR.Report.Core
{
    public class mprFintrakMenu_ObjectListInfo
    {
        public string MenuList { get; set; }
       public List<mprFintrakMenu_SubObjectListInfo> subobj {get; set;}
    }

    public class mprFintrakMenu_SubObjectListInfo
    {
        public string ReportPAth { get; set; }
        public string ParameterKey { get; set; }
        public string ReportTitle { get; set; }
        public string UIsrefState { get; set; }
        public int ID { get; set; }
        public int? Position { get; set; }
        public string urlMenu { get; set; }
      
    }
}