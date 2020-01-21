
using MPR.Report.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace MPR.Report.Core.Entities
{
    public partial class UserMIS : IIdentifiableEntity
    {
        public int UserMisId { get; set; }

        public string LoginID { get; set; }

        public string ProfitCenterDefinitionCode { get; set; }

        public string ProfitCenterMisCode { get; set; }

        public int EntityId
        {
            get
            {
                return UserMisId;
            }
        }
    }
}
