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
    //public partial class MPRContext : DbContext
    //{
    //    //const string SOLUTION_NAME = "FIN_MPR";

    //    //AuditManager _auditManager;

    //    //public MPRContext()
    //    //    : base(GetDataConnection())
    //    //{
    //    //    System.Data.Entity.Database.SetInitializer<MPRContext>(null);

    //    //    _auditManager = new AuditManager(GetDataConnection());
    //    //}

    //    public MPRContext()
    //        : base("name=FintrakDailyConnection")
    //    {
    //    }


    //    //public MPRContext(string connectionString)
    //    //    //: base(connectionString)
    //    //    : base(connectionString = "FintrakReportEntities")
    //    //{
    //    //    System.Data.Entity.Database.SetInitializer<MPRContext>(null);
    //    //    //_auditManager = new AuditManager(connectionString);
    //    //}

        
    //    //MPR
    //    public DbSet<BSheet> BSheetSet { get; set; }
    //    public DbSet<mpr_FintrakMenu> mpr_FintrakMenuSet { get; set; }
        



    //    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    //    {
    //        modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

    //        modelBuilder.Ignore<PropertyChangedEventHandler>();
    //        modelBuilder.Ignore<ExtensionDataObject>();
    //        modelBuilder.Ignore<IIdentifiableEntity>();

          

    //        //BSheet
    //        modelBuilder.Entity<BSheet>().HasKey<int>(e => e.BSheetId).Ignore(e => e.EntityId);
    //       // modelBuilder.Entity<BSheet>().Property(c => c.RowVersion).IsRowVersion();
    //        modelBuilder.Entity<BSheet>().ToTable("mpr_BSheet");

    //        //mpr fintrak menu
    //        modelBuilder.Entity<mpr_FintrakMenu>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
    //        // modelBuilder.Entity<mpr_FintrakMenu>().Property(c => c.RowVersion).IsRowVersion();
    //        modelBuilder.Entity<mpr_FintrakMenu>().ToTable("FintrakMenu");

    //    }

    //    //public override int SaveChanges()
    //    //{
    //    //    try
    //    //    {
    //    //        if (ChangeTracker.HasChanges())
    //    //        {
    //                //var entries = this.ChangeTracker.Entries();

    //                //foreach (DbEntityEntry entry in entries)
    //                //{
    //                //    if (entry.Entity != null)
    //                //    {
    //                //        if (entry.State == EntityState.Added)
    //                //        {
    //                //            //entry is Added 

    //                //            var model = (IExtensibleDataObject)entry.Entity;
    //                //            //model.CreatedBy = DataConnector.LoginName;
    //                //            //model.CreatedOn = DateTime.Now;
    //                //            //model.UpdatedBy = DataConnector.LoginName;
    //                //            //model.UpdatedOn = DateTime.Now;
    //                //        }
    //                //        else if (entry.State == EntityState.Deleted)
    //                //        {
    //                //            //entry in deleted

    //                //        }
    //                //        else
    //                //        {
    //                //            //entry is modified
    //                //            var model = (IExtensibleDataObject)entry.Entity;
    //                //            //model.UpdatedBy = DataConnector.LoginName;
    //                //            //model.UpdatedOn = DateTime.Now;
    //                //        }

    //                //        _auditManager.AddAudit(entry, DataConnector.LoginName);
    //                //    }
    //                //}
    //            }

    //          //  _auditManager.Save();

    //    //        return base.SaveChanges();
    //    //    }
    //    //    catch (DbUpdateException e)
    //    //    {
    //    //        var innerEx = e.InnerException;
    //    //        while (innerEx.InnerException != null)
    //    //            innerEx = innerEx.InnerException;

    //    //        throw new Exception(innerEx.Message);
    //    //    }
    //    //    catch (DbEntityValidationException e)
    //    //    {
    //    //        var sb = new StringBuilder();

    //    //        foreach (var entry in e.EntityValidationErrors)
    //    //        {
    //    //            foreach (var error in entry.ValidationErrors)
    //    //            {
    //    //                sb.AppendLine(string.Format("{0}-{1}-{2}", entry.Entry.Entity, error.PropertyName, error.ErrorMessage));
    //    //            }
    //    //        }

    //    //        throw new Exception(sb.ToString());
    //    //    }
    //    //    catch (Exception e)
    //    //    {
    //    //        var innerEx = e.InnerException;
    //    //        while (innerEx.InnerException != null)
    //    //            innerEx = innerEx.InnerException;

    //    //        throw new Exception(innerEx.Message);
    //    //    }

    //    //}

    //    //public static string GetDataConnection()
    //    //{
    //    //    string connectionString = "";

    //        //if (!string.IsNullOrEmpty(DataConnector.CompanyCode) && !string.IsNullOrEmpty(SOLUTION_NAME))
    //        //{
    //        //    systemContract.IDatabaseRepository databaseRepository = new systemCore.DatabaseRepository();
    //        //    var companydbs = databaseRepository.GetDatabases().Where(c => c.Database.CompanyCode == DataConnector.CompanyCode && (c.Solution.Name == SOLUTION_NAME || c.Solution.Name == "CORE"));

    //        //    DatabaseInfo companydb = null;

    //        //    if (companydbs == null)
    //        //        throw new Exception("Unable to load database.");
    //        //    else
    //        //    {
    //        //        companydb = companydbs.Where(c => c.Solution.Name == SOLUTION_NAME).FirstOrDefault();

    //        //        if (companydb == null)
    //        //            companydb = companydbs.FirstOrDefault();
    //        //    }

    //        //    //connectionString="Data Source=10.0.0.18\FintrakSQL2014;Initial Catalog=FintrakDB;User =sa;Password=sqluser10$;Integrated Security=False"
    //        //    connectionString = string.Format("Data Source= {0};Initial Catalog={1};User ={2};Password={3};Integrated Security={4}", companydb.Database.ServerName, companydb.Database.DatabaseName, companydb.Database.UserName, companydb.Database.Password, companydb.Database.IntegratedSecurity);
    //        //}
    //       // connectionString = string.Format("Data Source= {0};Initial Catalog={1};User ={2};Password={3};Integrated Security={4}", companydb.Database.ServerName, companydb.Database.DatabaseName, companydb.Database.UserName, companydb.Database.Password, companydb.Database.IntegratedSecurity);

    //      //  return connectionString;
    //   // }

    ////}
}
