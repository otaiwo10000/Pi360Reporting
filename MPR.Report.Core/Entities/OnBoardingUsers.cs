
using MPR.Report.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace MPR.Report.Core.Entities
{
    public partial class OnBoardingUsers : IIdentifiableEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StaffId { get; set; }
        public string TeamDefinitionCode { get; set; }
        public string MISCode { get; set; }

        public int EntityId
        {
            get
            {
                return Id;
            }
        }
    }
}
