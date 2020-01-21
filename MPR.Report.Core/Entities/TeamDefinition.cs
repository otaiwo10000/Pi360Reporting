
using MPR.Report.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace MPR.Report.Core.Entities
{
    public partial class TeamDefinition : IIdentifiableEntity
    {
       

        [System.ComponentModel.DataAnnotations.Key]
        [System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedAttribute(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]

        //[DataMember]
        //[Browsable(false)]
        public int TeamDefinitionId { get; set; }

        //[DataMember]
        //[Required]
        public string Code { get; set; }

        //[DataMember]
        //[Required]
        public string Name { get; set; }

        //[DataMember]
        //[Required]
        public int Position { get; set; }

        //[DataMember]
        //[Required]
        public int Level { get; set; }

        //[DataMember]
        //[Required]
        public bool CanClassified { get; set; }

        //[DataMember]
        //[Required]
        public bool CanUseStaffId { get; set; }

        //[DataMember]
        public Nullable<int> Period { get; set; }

        //[DataMember]
        public string CompanyCode { get; set; }

        //[DataMember]
        //[Required]
        public string Year { get; set; }

        public int EntityId
        {
            get
            {
                return TeamDefinitionId;
            }
        }
    }
}
