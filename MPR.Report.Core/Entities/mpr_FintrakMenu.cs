
using MPR.Report.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace MPR.Report.Core.Entities
{ 

    public partial class mpr_FintrakMenu
    {
        public int ID { get; set; }

       // [StringLength(50)]
        public string FieldID { get; set; }

        //[StringLength(50)]
        public string MenuList { get; set; }

        //[StringLength(250)]
        public string ReportPAth { get; set; }

        //[StringLength(50)]
        public string ParameterKey { get; set; }

        public bool? ExpandStatus { get; set; }

        //[StringLength(30)]
        public string SpecialControl { get; set; }

        //[StringLength(300)]
        public string ReportTitle { get; set; }

        //[StringLength(300)]
        public string ViewerPath { get; set; }

        public bool? Visible { get; set; }

        //[StringLength(300)]
        public string ViewRights { get; set; }

        public int? Position { get; set; }

        public string UIsrefState { get; set; }
        public string urlMenu { get; set; }

        //public int EntityId { get; }

        public int EntityId
        {
            get
            {
                return ID;
            }
        }
    }
}
