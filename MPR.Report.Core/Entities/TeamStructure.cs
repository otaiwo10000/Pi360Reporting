
using MPR.Report.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace MPR.Report.Core.Entities
{
    //public partial class TeamStructure : IIdentifiableEntity
    public partial class TeamStructure
    {
        //[DataMember]
        //[Browsable(false)]
        [Key, Column(Order = 0)]
        public int teambankid { get; set; }
        
        [Key, Column(Order = 1)]
        public int teamgroupid { get; set; }

        //[DataMember]
        //[Required]
        public string Accountofficer_Code { get; set; }
        public string AccountofficerName { get; set; }

        public string Team_Code { get; set; }
        public string TeamName { get; set; }
       
        public string Branch_Code { get; set; }
        public string BranchName { get; set; }
    
        public string Region_Code { get; set; }
        public string RegionName { get; set; }

        public string Division_Code { get; set; }
        public string DivisionName { get; set; }

        public string DIRECTORATECODE { get; set; }
        public string DIRECTORATENAME { get; set; }

        public string Zone_Code { get; set; }
        public string ZoneName { get; set; }

        public string Group_Code { get; set; }
        public string GroupName { get; set; }

        public int Year { get; set; }

        public string staff_id { get; set; }

        public string unit_code { get; set; }
        public string unitname { get; set; }

        public int Period { get; set; }

        //public int EntityId
        //{
        //    get
        //    {
        //        return Team_StructureId;
        //    }
        //}
    }
}
