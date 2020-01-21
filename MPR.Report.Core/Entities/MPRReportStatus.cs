
using MPR.Report.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace MPR.Report.Core.Entities
{
    public partial class MPRReportStatus : IIdentifiableEntity
    {
        public int MPRReportStatusId { get; set; }
        public int Year { get; set; }
        //public int Period { get; set; }
        public string Period { get; set; }

        public string Status { get; set; }

       
        public int EntityId
        {
            get
            {
                return MPRReportStatusId;
            }
        }
    }
}
