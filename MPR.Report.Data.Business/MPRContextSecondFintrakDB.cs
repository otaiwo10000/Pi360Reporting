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
    
    public partial class MPRContextSecondFintrakDB : DbContext
    {
       
        public MPRContextSecondFintrakDB()
            : base("name=FintrakDB2ndConnection")
        {
        }

       
        public DbSet<UserMIS> UserMISSet { get; set; }       


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Ignore<PropertyChangedEventHandler>();
            modelBuilder.Ignore<ExtensionDataObject>();
            modelBuilder.Ignore<IIdentifiableEntity>();

          

            //user mis
            modelBuilder.Entity<UserMIS>().HasKey<int>(e => e.UserMisId).Ignore(e => e.EntityId);
            // modelBuilder.Entity<mpr_FintrakMenu>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<UserMIS>().ToTable("mpr_usermis");
        }

                }            
}









//public class MyContext : DbContext
//{
//    protected override void OnModelCreating(DbModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<Model1>()
//            .HasRequired(e => e.Model2)
//            .WithMany(e => e.Model1s)
//            .HasForeignKey(e => new { e.fk_one, e.fk_two })
//            .WillCascadeOnDelete(false);
//    }
//}