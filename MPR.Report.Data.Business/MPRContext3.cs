using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPR.Report.Core;
using MPR.Report.Core.Entities;
using MPR.Report.Core.Interfaces;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

namespace MPR.Report.Data.Business
{
    public partial class MPRContext3 : DbContext
    {
        
        public MPRContext3()
            : base("name=FintrakCoreDBConnection")
        {
        }
        
        public DbSet<CorUserSetUp> CorUserSetUpSet { get; set; }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Ignore<PropertyChangedEventHandler>();
            modelBuilder.Ignore<ExtensionDataObject>();
            modelBuilder.Ignore<IIdentifiableEntity>();         


            //user Set up
            modelBuilder.Entity<CorUserSetUp>().HasKey<int>(e => e.UserSetupId).Ignore(e => e.EntityId);
            // modelBuilder.Entity<mpr_FintrakMenu>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CorUserSetUp>().ToTable("cor_usersetup");           
        }
                }
    
}
