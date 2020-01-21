
using MPR.Report.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace MPR.Report.Core.Entities
{
    public partial class CorUserSetUp : IIdentifiableEntity
    {
        public int UserSetupId { get; set; }

        public string LoginID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string StaffID { get; set; }
        public string PhotoUrl { get; set; }
        public bool IsReportUser { get; set; }
        public string CompanyCode { get; set; }
        public int EntityId
        {
            get
            {
                return UserSetupId;
            }
        }
    }
}
