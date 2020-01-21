
using MPR.Report.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace MPR.Report.Core.Entities
{
    public partial class DashboardConfiguration : IIdentifiableEntity
    {
        //[DataMember]
        //[Browsable(false)]
        public int DashboardConfigurationId { get; set; }

        //[DataMember]
        //[Required]

        public string MainCaption { get; set; }
        public string SubCaption { get; set; }
        public string DisplayCaptionObjectCode { get; set; }
        public string Dashbard { get; set; }
        public bool Deleted { get; set; }
        public string Type { get; set; }

        public int EntityId
        {
            get
            {
                return DashboardConfigurationId;
            }
        }
    }
}
