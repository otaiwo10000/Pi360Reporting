
using MPR.Report.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace MPR.Report.Core.Entities
{
    public partial class Reportstatus : IIdentifiableEntity
    {
        public int Sno { get; set; }

        public string ReportStatus { get; set; }

       
        public int EntityId
        {
            get
            {
                return Sno;
            }
        }
    }
}
