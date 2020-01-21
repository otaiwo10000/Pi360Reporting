
using MPR.Report.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace MPR.Report.Core.Entities
{
    public partial class SetUp : IIdentifiableEntity
    {
        public int SetupId { get; set; }

        public string Year { get; set; }

        public int Period { get; set; }

       
        public int EntityId
        {
            get
            {
                return SetupId;
            }
        }
    }
}
