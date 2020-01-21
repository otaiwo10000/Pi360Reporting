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
    
    public partial class MPRContext2 : DbContext
    {
       
        public MPRContext2()
            : base("name=FintrakDBConnection")
        {
        }

        public DbSet<TeamStructure> TeamStructureSet { get; set; }
       // public DbSet<TeamStructureWMB> TeamStructureWMBSet { get; set; }
        public DbSet<TeamStructureABP> TeamStructureABPSet { get; set; }
        public DbSet<TeamStructureALL> TeamStructureALLSet { get; set; }
        public DbSet<SetUp> SetUpSet { get; set; }
        public DbSet<UserMIS> UserMISSet { get; set; }
        public DbSet<mpr_FintrakMenu> mpr_FintrakMenuSet { get; set; }
        public DbSet<TeamDefinition> TeamDefinitionSet { get; set; }
        public DbSet<LandingPageConfiguration> LandingPageConfigurationSet { get; set; }
        public DbSet<IncomeSetUpDaily> IncomeSetUpDailySet { get; set; }
        public DbSet<DashboardConfiguration> DashboardConfigurationSet { get; set; }
        public DbSet<DashboardSubCaptionConfiguration> DashboardSubCaptionConfigurationSet { get; set; }
        public DbSet<Reportstatus> ReportstatusSet { get; set; }
        public DbSet<MPRReportStatus> MPRReportStatusSet { get; set; }
        public DbSet<OnBoardingUsers> OnBoardingUsersSet { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Ignore<PropertyChangedEventHandler>();
            modelBuilder.Ignore<ExtensionDataObject>();
            modelBuilder.Ignore<IIdentifiableEntity>();

          

            //BSheet
            modelBuilder.Entity<BSheet>().HasKey<int>(e => e.BSheetId).Ignore(e => e.EntityId);
           // modelBuilder.Entity<BSheet>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<BSheet>().ToTable("mpr_BSheet");

            //team structure
            modelBuilder.Entity<TeamStructure>().HasKey(x => new { x.teambankid, x.teamgroupid });
            // modelBuilder.Entity<mpr_FintrakMenu>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<TeamStructure>().ToTable("vw_TeamBankGroupReport");

            ////team structure
            //modelBuilder.Entity<TeamStructureWMB>().HasKey<int>(e => e.Team_StructureId).Ignore(e => e.EntityId);
            //// modelBuilder.Entity<mpr_FintrakMenu>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<TeamStructureWMB>().ToTable("Mpr_Team_Structure");

            //team structure
            modelBuilder.Entity<TeamStructureABP>().HasKey<int>(e => e.Team_StructureId).Ignore(e => e.EntityId);
            // modelBuilder.Entity<mpr_FintrakMenu>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<TeamStructureABP>().ToTable("Team");

            //team structure
            modelBuilder.Entity<TeamStructureALL>().HasKey<int>(e => e.Team_StructureId).Ignore(e => e.EntityId);
            // modelBuilder.Entity<mpr_FintrakMenu>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<TeamStructureALL>().ToTable("Mpr_Team_Structure");

            //Set up
            modelBuilder.Entity<SetUp>().HasKey<int>(e => e.SetupId).Ignore(e => e.EntityId);
            // modelBuilder.Entity<mpr_FintrakMenu>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<SetUp>().ToTable("mpr_setup");

            //user mis
            modelBuilder.Entity<UserMIS>().HasKey<int>(e => e.UserMisId).Ignore(e => e.EntityId);
            // modelBuilder.Entity<mpr_FintrakMenu>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<UserMIS>().ToTable("mpr_usermis");

            //mpr fintrak menu
            modelBuilder.Entity<mpr_FintrakMenu>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            // modelBuilder.Entity<mpr_FintrakMenu>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<mpr_FintrakMenu>().ToTable("FintrakMenu");

            //team definition
            modelBuilder.Entity<TeamDefinition>().HasKey<int>(e => e.TeamDefinitionId).Ignore(e => e.EntityId);
            // modelBuilder.Entity<mpr_FintrakMenu>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<TeamDefinition>().ToTable("mpr_team_definition");

            //Landingpage Configuration
            modelBuilder.Entity<LandingPageConfiguration>().HasKey<int>(e => e.LandingPageConfigurationId).Ignore(e => e.EntityId);
            // modelBuilder.Entity<mpr_FintrakMenu>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<LandingPageConfiguration>().ToTable("Mpr_LandingPageConfiguration");

            //Income SetUp Daily
            modelBuilder.Entity<IncomeSetUpDaily>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            // modelBuilder.Entity<mpr_FintrakMenu>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IncomeSetUpDaily>().ToTable("income_setup_daily");
            //modelBuilder.Entity<IncomeSetUpDaily>().ToTable("income_setup_");

            //Dashboard Configuration
            modelBuilder.Entity<DashboardConfiguration>().HasKey<int>(e => e.DashboardConfigurationId).Ignore(e => e.EntityId);
            // modelBuilder.Entity<mpr_FintrakMenu>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<DashboardConfiguration>().ToTable("Mpr_DashboardConfiguration");

            //Dashboard SubCaption Configuration
            modelBuilder.Entity<DashboardSubCaptionConfiguration>().HasKey<int>(e => e.DashboardSubcaptionConfigurationId).Ignore(e => e.EntityId);
            // modelBuilder.Entity<mpr_FintrakMenu>().Property(c => c.RowVersion).IsRowVersi
            modelBuilder.Entity<DashboardSubCaptionConfiguration>().ToTable("Mpr_DashboardSubCaptionConfiguration");

            //Report Status
            modelBuilder.Entity<Reportstatus>().HasKey<int>(e => e.Sno).Ignore(e => e.EntityId);
            // modelBuilder.Entity<mpr_FintrakMenu>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Reportstatus>().ToTable("ReportStatus");

            //MPRReport Status
            modelBuilder.Entity<MPRReportStatus>().HasKey<int>(e => e.MPRReportStatusId).Ignore(e => e.EntityId);
            // modelBuilder.Entity<mpr_FintrakMenu>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MPRReportStatus>().ToTable("MPRReportStatus");

            //OnBoarding Users
            modelBuilder.Entity<OnBoardingUsers>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            //modelBuilder.Entity<OnBoardingUsers>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<OnBoardingUsers>().ToTable("OnBoardingUsers");
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