
using MPR.Report.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace MPR.Report.Core.Entities
{
    public partial class LandingPageConfiguration : IIdentifiableEntity
    {
        //[DataMember]
        //[Browsable(false)]
        public int LandingPageConfigurationId { get; set; }

        //[DataMember]
        //[Required]
        public string DisplayCaptionObjectCode { get; set; }

        public string MainCaption { get; set; }
        public bool Deleted { get; set; }
        public string Type { get; set; }

        public int EntityId
        {
            get
            {
                return LandingPageConfigurationId;
            }
        }
    }
}
