
using MPR.Report.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace MPR.Report.Core.Entities
{
    public partial class IncomeSetUpDaily : IIdentifiableEntity
    {
        //public int IncomeSetUpDailyId { get; set; }
        public int ID { get; set; }

        public int CurrentPeriod { get; set; }

        public int Year { get; set; }

       
        public int EntityId
        {
            get
            {
                return ID;
            }
        }
    }
}
