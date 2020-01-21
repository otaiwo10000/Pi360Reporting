using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using MPR.Report.Core;
using MPR.Report.Core.Entities;
using MPR.Report.Core.Interfaces;
using MPR.Report.Core.IRepositoryInterfaces;
using System.Configuration;
using System.Data.SqlClient;
//using Pi360Reporting.Models.DataInfo;
//using Pi360Reporting.Models.DataSubdata;

namespace MPR.Report.Data.Business.DataRepositories
{
    public class TeamStructureRepository : IDataRepository
    {
        string connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;
        MPRContext2 context2 = new MPRContext2();
        //public void Add(TeamStructure b)
        //{
        //    context2.TeamStructureSet.Add(b);
        //    context2.SaveChanges();
        //}

        //public void Edit(TeamStructure b)
        //{
        //    context2.Entry(b).State = System.Data.Entity.EntityState.Modified;
        //}

        //public TeamStructure FindById(int Id)
        //{
        //    var result = (from r in context2.TeamStructureSet where r.teambankid == Id select r).FirstOrDefault();
           
        //    return result;
        //}

        //public IEnumerable GetTeamStructure()
        //{
        //    return context2.TeamStructureSet;
        //}
        //public void Remove(int Id)
        //{
        //    TeamStructure p = context2.TeamStructureSet.Find(Id);
        //    context2.TeamStructureSet.Remove(p);
        //    context2.SaveChanges();
        //}

// ================================ on pageload starts =======================================================
        public IEnumerable<TeamStructureData> GetTeamStructureByMISCodeLevelYear()
        {
            var teamstructure = new List<TeamStructure>();

            //using (MPRContext2 entityContext = new MPRContext2())
            using (var con = new SqlConnection(connectionString))
            {
                //var latestmonthyear = entityContext.IncomeSetUpDailySet.OrderByDescending(x => x.Year).Take(1);
                //int latestyear = latestmonthyear.Select(x => x.Year).FirstOrDefault();
                ////int period = latestmonthyear.Max(x => x.CurrentPeriod);

                var teamstructureListObj = new TeamStructureData();
                var teamStructureDataList = new List<TeamStructureData>();

                string levelcode = Convert.ToString(System.Web.HttpContext.Current.Session["session_levelcode"]);
                string miscode = Convert.ToString(System.Web.HttpContext.Current.Session["session_miscode"]);

                //var teamstructure = new List<TeamStructure>();

                var cmd = new SqlCommand("spp_getteamstructureForLatestYear", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var pts = new TeamStructure();
                    
                    pts.DIRECTORATECODE = reader["DIRECTORATECODE"] != DBNull.Value ? reader["DIRECTORATECODE"].ToString() : "";
                    pts.DIRECTORATENAME = reader["DIRECTORATENAME"] != DBNull.Value ? reader["DIRECTORATENAME"].ToString() : "";
                    pts.Region_Code = reader["Region_Code"] != DBNull.Value ? reader["Region_Code"].ToString() : "";
                    pts.RegionName = reader["RegionName"] != DBNull.Value ? reader["RegionName"].ToString() : "";
                    pts.Division_Code = reader["Division_Code"] != DBNull.Value ? reader["Division_Code"].ToString() : "";
                    pts.DivisionName = reader["DivisionName"] != DBNull.Value ? reader["DivisionName"].ToString() : "";
                    pts.Branch_Code = reader["Branch_Code"] != DBNull.Value ? reader["Branch_Code"].ToString() : "";
                    pts.BranchName = reader["BranchName"] != DBNull.Value ? reader["BranchName"].ToString() : "";
                    pts.Accountofficer_Code = reader["Accountofficer_Code"] != DBNull.Value ? reader["Accountofficer_Code"].ToString() : "";
                    pts.AccountofficerName = reader["AccountofficerName"] != DBNull.Value ? reader["AccountofficerName"].ToString() : "";
                    pts.staff_id = reader["staff_id"] != DBNull.Value ? reader["staff_id"].ToString() : "";
                    pts.Year = reader["Year"] != DBNull.Value ? Convert.ToInt32(reader["Year"].ToString()) : 0;
                    pts.teambankid = reader["teambankid"] != DBNull.Value ? Convert.ToInt32(reader["teambankid"].ToString()) : 0;
                    pts.teamgroupid = reader["teamgroupid"] != DBNull.Value ? Convert.ToInt32(reader["teamgroupid"].ToString()) : 0;

                    teamstructure.Add(pts);
                }
                con.Close();


                if (levelcode.ToUpper() == "BNK" || levelcode.ToUpper() == "SA" || levelcode.ToUpper() == "PRO" || levelcode.ToUpper() == "MD")
                {
                    //teamstructure = (from a in entityContext.TeamStructureSet
                    //                 where a.Year == latestyear
                    //                 select a
                    //               )
                    //       .GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault())
                    //      .ToList();
                   
                    teamstructure = teamstructure.GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault()).ToList();

                    System.Web.HttpContext.Current.Session["session_tem_bnk"] = teamstructure;

                    teamstructureListObj.TotalbankList = (from a in teamstructure

                                                          select new TotalbankData()
                                                          {
                                                              Code = "BNK",
                                                              Name = "Total Bank"
                                                          }).Take(1).ToList();
                    //.GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                    // .OrderBy(x => x.Name)
                    // .ToList();

                    ////teamstructureListObj.TotalbankList.Add(new TotalbankData() { Code = "BNK", Name = "Total Bank" });
                    ////list2.Add(new Test() { A = 3, B = "Sarah" });

                    //TotalbankData totalbankObj = new TotalbankData()
                    //{
                    //    Code = "BNK",
                    //    Name = "Total bank"
                    //};
                    //teamstructureListObj.TotalbankList.Add(totalbankObj);


                    teamstructureListObj.DirectorateList = (from a in teamstructure

                                                            select new DirectorateData()
                                                            {
                                                                Code = a.DIRECTORATECODE,
                                                                Name = a.DIRECTORATENAME
                                                            })
                            .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                            .OrderBy(x => x.Name)
                           .ToList();

                    teamstructureListObj.RegionList = (from a in teamstructure

                                                       select new RegionData()
                                                       {
                                                           Code = a.Region_Code,
                                                           Name = a.RegionName
                                                       })
                               .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                               .OrderBy(x => x.Name)
                              .ToList();

                    teamstructureListObj.DivisionList = (from a in teamstructure

                                                         select new DivisionData()
                                                         {
                                                             Code = a.Division_Code,
                                                             Name = a.DivisionName
                                                         })
                               .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                               .OrderBy(x => x.Name)
                              .ToList();

                    teamstructureListObj.BranchList = (from a in teamstructure

                                                       select new BranchData()
                                                       {
                                                           Code = a.Branch_Code,
                                                           Name = a.BranchName
                                                       })
                               .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                               .OrderBy(x => x.Name)
                              .ToList();

                    teamstructureListObj.AccountOfficerList = (from a in teamstructure

                                                               select new AccountOfficerData()
                                                               {
                                                                   Code = a.Accountofficer_Code,
                                                                   Name = a.AccountofficerName
                                                               })
                               .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                               .OrderBy(x => x.Name)
                              .ToList();

                    teamStructureDataList.Add(teamstructureListObj);
                    //return teamStructureDataList;
                }  // end if

                else if (levelcode.ToUpper() == "DIR")
                {
                    //teamstructure = (from a in entityContext.TeamStructureSet
                    //                 where a.Year == latestyear && a.DIRECTORATECODE == miscode
                    //                 select a
                    //              )
                    //      .GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault())
                    //     .ToList();

                    teamstructure = teamstructure.Where(x=>x.DIRECTORATECODE == miscode).GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault()).ToList();

                    System.Web.HttpContext.Current.Session["session_tem_dir"] = teamstructure;

                    teamstructureListObj.DirectorateList = (from a in teamstructure where a.DIRECTORATECODE == miscode

                                                       select new DirectorateData()
                                                       {
                                                           Code = a.DIRECTORATECODE,
                                                           Name = a.DIRECTORATENAME
                                                       })
                                   .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                                   .OrderBy(x => x.Name)
                                  .ToList();

                    teamstructureListObj.RegionList = (from a in teamstructure

                                                       select new RegionData()
                                                       {
                                                           Code = a.Region_Code,
                                                           Name = a.RegionName
                                                       })
                                   .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                                   .OrderBy(x => x.Name)
                                  .ToList();

                    teamstructureListObj.DivisionList = (from a in teamstructure

                                                         select new DivisionData()
                                                         {
                                                             Code = a.Division_Code,
                                                             Name = a.DivisionName
                                                         })
                               .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                               .OrderBy(x => x.Name)
                              .ToList();

                    teamstructureListObj.BranchList = (from a in teamstructure

                                                       select new BranchData()
                                                       {
                                                           Code = a.Branch_Code,
                                                           Name = a.BranchName
                                                       })
                               .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                               .OrderBy(x => x.Name)
                              .ToList();

                    teamstructureListObj.AccountOfficerList = (from a in teamstructure

                                                               select new AccountOfficerData()
                                                               {
                                                                   Code = a.Accountofficer_Code,
                                                                   Name = a.AccountofficerName
                                                               })
                               .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                               .OrderBy(x => x.Name)
                              .ToList();

                    teamStructureDataList.Add(teamstructureListObj);
                    //return teamStructureDataList;
                }  // end else if

                else if (levelcode.ToUpper() == "REG")
                {
                    //teamstructure = (from a in entityContext.TeamStructureSet
                    //                 where a.Year == latestyear && a.Region_Code == miscode
                    //                 select a
                    //             )
                    //    .GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault())
                    //    .ToList();

                    teamstructure = teamstructure.Where(x => x.Region_Code == miscode).GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault()).ToList();

                    System.Web.HttpContext.Current.Session["session_tem_reg"] = teamstructure;

                    teamstructureListObj.RegionList = (from a in teamstructure where a.Region_Code == miscode

                                                         select new RegionData()
                                                         {
                                                             Code = a.Region_Code,
                                                             Name = a.RegionName
                                                         })
                                  .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                                  .OrderBy(x => x.Name)
                                 .ToList();

                    teamstructureListObj.DivisionList = (from a in teamstructure

                                                         select new DivisionData()
                                                         {
                                                             Code = a.Division_Code,
                                                             Name = a.DivisionName
                                                         })
                                   .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                                   .OrderBy(x => x.Name)
                                  .ToList();

                    teamstructureListObj.BranchList = (from a in teamstructure

                                                       select new BranchData()
                                                       {
                                                           Code = a.Branch_Code,
                                                           Name = a.BranchName
                                                       })
                               .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                               .OrderBy(x => x.Name)
                              .ToList();

                    teamstructureListObj.AccountOfficerList = (from a in teamstructure

                                                               select new AccountOfficerData()
                                                               {
                                                                   Code = a.Accountofficer_Code,
                                                                   Name = a.AccountofficerName
                                                               })
                               .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                               .OrderBy(x => x.Name)
                              .ToList();

                    teamStructureDataList.Add(teamstructureListObj);
                    //return teamStructureDataList;
                }  // end else if

                else if (levelcode.ToUpper() == "DIV")
                {
                    //teamstructure = (from a in entityContext.TeamStructureSet
                    //                 where a.Year == latestyear && a.Division_Code == miscode
                    //                 select a
                    //            )
                    //   .GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault())
                    //   .ToList();

                    teamstructure = teamstructure.Where(x=>x.Division_Code == miscode).GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault()).ToList();

                    System.Web.HttpContext.Current.Session["session_tem_div"] = teamstructure;

                    teamstructureListObj.DivisionList = (from a in teamstructure where a.Division_Code == miscode

                                                       select new DivisionData()
                                                       {
                                                           Code = a.Division_Code,
                                                           Name = a.DivisionName
                                                       })
                                   .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                                   .OrderBy(x => x.Name)
                                  .ToList();

                    teamstructureListObj.BranchList = (from a in teamstructure

                                                       select new BranchData()
                                                       {
                                                           Code = a.Branch_Code,
                                                           Name = a.BranchName
                                                       })
                                   .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                                   .OrderBy(x => x.Name)
                                  .ToList();

                    teamstructureListObj.AccountOfficerList = (from a in teamstructure

                                                               select new AccountOfficerData()
                                                               {
                                                                   Code = a.Accountofficer_Code,
                                                                   Name = a.AccountofficerName
                                                               })
                               .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                               .OrderBy(x => x.Name)
                              .ToList();

                    teamStructureDataList.Add(teamstructureListObj);
                    //return teamStructureDataList;
                }  // end else if

                else if (levelcode.ToUpper() == "BRH")
                {
                    //teamstructure = (from a in entityContext.TeamStructureSet
                    //                 where a.Year == latestyear && a.Branch_Code == miscode
                    //                 select a
                    //           )
                    //  .GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault())
                    //  .ToList();

                    teamstructure = teamstructure.Where(x=>x.Branch_Code == miscode).GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault()).ToList();

                    System.Web.HttpContext.Current.Session["session_tem_brh"] = teamstructure;

                    teamstructureListObj.BranchList = (from a in teamstructure where a.Branch_Code == miscode

                                                               select new BranchData()
                                                               {
                                                                   Code = a.Branch_Code,
                                                                   Name = a.BranchName
                                                               })
                                  .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                                  .OrderBy(x => x.Name)
                                 .ToList();

                    teamstructureListObj.AccountOfficerList = (from a in teamstructure

                                                               select new AccountOfficerData()
                                                               {
                                                                   Code = a.Accountofficer_Code,
                                                                   Name = a.AccountofficerName
                                                               })
                                   .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                                   .OrderBy(x => x.Name)
                                  .ToList();

                    teamStructureDataList.Add(teamstructureListObj);
                    //return teamStructureDataList;
                }  // end else if

                else if (levelcode.ToUpper() == "ACCT")
                {                    
                    teamstructure = teamstructure.Where(x => x.Accountofficer_Code == miscode).GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault()).ToList();

                    //System.Web.HttpContext.Current.Session["session_tem_brh"] = teamstructure;

                    teamstructureListObj.AccountOfficerList = (from a in teamstructure

                                                               select new AccountOfficerData()
                                                               {
                                                                   Code = a.Accountofficer_Code,
                                                                   Name = a.AccountofficerName
                                                               })
                                   .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                                   .OrderBy(x => x.Name)
                                  .ToList();

                    teamStructureDataList.Add(teamstructureListObj);
                    //return teamStructureDataList;
                }  // end else if

                return teamStructureDataList;
            }
        }


        //public IEnumerable<TeamStructure> GetTeamStructureByMISCodeLevelYear()
        //{
        //    using (MPRContext2 entityContext = new MPRContext2())
        //    {
        //        string latestyear = entityContext.TeamStructureSet.Max(x => x.Year);
        //        //List<int> periodsForLatestYear = entityContext.TeamStructureSet.Where(x => x.Year == latestyear).Select(x => x.Period).ToList();
        //        //int period = periodsForLatestYear.Max();

        //        string levelcode = Convert.ToString(System.Web.HttpContext.Current.Session["session_levelcode"]);
        //        string miscode = Convert.ToString(System.Web.HttpContext.Current.Session["session_miscode"]);

        //        var query = new List<TeamStructure>();


        //        if (levelcode == "BNK")
        //        {
        //            query = (from a in entityContext.TeamStructureSet
        //                     where a.Year == latestyear


        //                     select new
        //                     {
        //                         DIRECTORATECODE = a.DIRECTORATECODE,
        //                         DIRECTORATENAME = a.DIRECTORATENAME,
        //                         Region_Code = a.Region_Code,
        //                         RegionName = a.RegionName,
        //                         Division_Code = a.Division_Code,
        //                         DivisionName = a.DivisionName,
        //                         Branch_Code = a.Branch_Code,
        //                         BranchName = a.BranchName,
        //                         Accountofficer_Code = a.Accountofficer_Code,
        //                         AccountofficerName = a.AccountofficerName
        //                     })
        //                            .AsEnumerable().Select(x => new TeamStructure
        //                            {
        //                                DIRECTORATECODE = x.DIRECTORATECODE,
        //                                DIRECTORATENAME = x.DIRECTORATENAME,
        //                                Region_Code = x.Region_Code,
        //                                RegionName = x.RegionName,
        //                                Division_Code = x.Division_Code,
        //                                DivisionName = x.DivisionName,
        //                                Branch_Code = x.Branch_Code,
        //                                BranchName = x.BranchName,
        //                                Accountofficer_Code = x.Accountofficer_Code,
        //                                AccountofficerName = x.AccountofficerName
        //                            })
        //                            .GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault())
        //                            //.OrderBy(x => x.Name)
        //                            .ToList();
        //            System.Web.HttpContext.Current.Session["session_tem_bnk"] = query;

        //        }

        //        else if (levelcode == "DIR")
        //        {                    
        //            query = (from a in entityContext.TeamStructureSet
        //                     where a.Year == latestyear && a.DIRECTORATECODE == miscode

        //                     select new
        //                     {
        //                         Region_Code = a.Region_Code,
        //                         RegionName = a.RegionName,
        //                         Division_Code = a.Division_Code,
        //                         DivisionName = a.DivisionName,
        //                         Branch_Code = a.Branch_Code,
        //                         BranchName = a.BranchName,
        //                         Accountofficer_Code = a.Accountofficer_Code,
        //                         AccountofficerName = a.AccountofficerName
        //                     })
        //                            .AsEnumerable().Select(x => new TeamStructure
        //                            {
        //                                Region_Code = x.Region_Code,
        //                                RegionName = x.RegionName,
        //                                Division_Code = x.Division_Code,
        //                                DivisionName = x.DivisionName,
        //                                Branch_Code = x.Branch_Code,
        //                                BranchName = x.BranchName,
        //                                Accountofficer_Code = x.Accountofficer_Code,
        //                                AccountofficerName = x.AccountofficerName
        //                            })
        //                            .GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault())
        //                            //.OrderBy(x => x.Name)
        //                            .ToList();
        //            System.Web.HttpContext.Current.Session["session_tem_dir"] = query;
        //        }

        //        else if (levelcode == "REG")
        //        {
        //            query = (from a in entityContext.TeamStructureSet
        //                             where a.Year == latestyear && a.Region_Code == miscode

        //                             select new
        //                             {
        //                                 Division_Code = a.Division_Code,
        //                                 DivisionName = a.DivisionName,
        //                                 Branch_Code = a.Branch_Code,
        //                                 BranchName = a.BranchName,
        //                                 Accountofficer_Code = a.Accountofficer_Code,
        //                                 AccountofficerName = a.AccountofficerName
        //                             })
        //                            .AsEnumerable().Select(x => new TeamStructure
        //                            {
        //                                Division_Code = x.Division_Code,
        //                                DivisionName = x.DivisionName,
        //                                Branch_Code = x.Branch_Code,
        //                                BranchName = x.BranchName,
        //                                Accountofficer_Code = x.Accountofficer_Code,
        //                                AccountofficerName = x.AccountofficerName
        //                            })
        //                            .GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault())
        //                            //.OrderBy(x => x.Name)
        //                            .ToList();
        //            System.Web.HttpContext.Current.Session["session_tem_reg"] = query;
        //        }  // end else if

        //        else if (levelcode == "DIV")
        //        {                    
        //            query = (from a in entityContext.TeamStructureSet
        //                             where a.Year == latestyear && a.Division_Code == miscode
        //                             select new
        //                             {
        //                                 Branch_Code = a.Branch_Code,
        //                                 BranchName = a.BranchName,
        //                                 Accountofficer_Code = a.Accountofficer_Code,
        //                                 AccountofficerName = a.AccountofficerName
        //                             })
        //                             .AsEnumerable().Select(x => new TeamStructure
        //                             {
        //                                 Branch_Code = x.Branch_Code,
        //                                 BranchName = x.BranchName,
        //                                 Accountofficer_Code = x.Accountofficer_Code,
        //                                 AccountofficerName = x.AccountofficerName
        //                             })
        //                             //.GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
        //                             //.OrderBy(x => x.Name)
        //                             .ToList();
        //            System.Web.HttpContext.Current.Session["session_tem_div"] = query;
        //        }  // end else if

        //        else if (levelcode == "BRH")
        //        {                   
        //            query = (from a in entityContext.TeamStructureSet
        //                             where a.Year == latestyear && a.Branch_Code == miscode

        //                             select new
        //                             {
        //                                 Accountofficer_Code = a.Accountofficer_Code,
        //                                 AccountofficerName = a.AccountofficerName
        //                             })
        //                             .AsEnumerable().Select(x => new TeamStructure
        //                             {
        //                                 Accountofficer_Code = x.Accountofficer_Code,
        //                                 AccountofficerName = x.AccountofficerName
        //                             })
        //                             .GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault())
        //                             //.OrderBy(x => x.Name)
        //                             .ToList();
        //            System.Web.HttpContext.Current.Session["session_tem_brh"] = query;
        //        }
        //        return query;
        //    }
        //}

//======================================= on pageload end ==================================================================================================



//============= selection by the selected miscode and selected year starts ==================================================================================


        public IEnumerable<TeamStructureData> GetTeamStructureBySelectedMisCodeAndYear(string selectedcode, int selectedyear)
        {
            var teamstructure = new List<TeamStructure>();

            //using (MPRContext2 entityContext = new MPRContext2())
            using (var con = new SqlConnection(connectionString))
            {
                MPRContext2 entityContext = new MPRContext2();

                var latestmonthyear = entityContext.IncomeSetUpDailySet.OrderByDescending(x => x.Year).Take(1);
                int latestyear = latestmonthyear.Select(x => x.Year).FirstOrDefault();
                //int period = latestmonthyear.Max(x => x.CurrentPeriod);

                var teamstructureListObj = new TeamStructureData();
                var teamStructureDataList = new List<TeamStructureData>();

                string levelcode = Convert.ToString(System.Web.HttpContext.Current.Session["session_levelcode"]);
                string miscode = Convert.ToString(System.Web.HttpContext.Current.Session["session_miscode"]);

                var cmd = new SqlCommand("spp_getteamstructureByYear", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "year",
                    Value = selectedyear,
                });

                //var teamstructure = new List<TeamStructure>();

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var pts = new TeamStructure();

                    pts.DIRECTORATECODE = reader["DIRECTORATECODE"] != DBNull.Value ? reader["DIRECTORATECODE"].ToString() : "";
                    pts.DIRECTORATENAME = reader["DIRECTORATENAME"] != DBNull.Value ? reader["DIRECTORATENAME"].ToString() : "";
                    pts.Region_Code = reader["Region_Code"] != DBNull.Value ? reader["Region_Code"].ToString() : "";
                    pts.RegionName = reader["RegionName"] != DBNull.Value ? reader["RegionName"].ToString() : "";
                    pts.Division_Code = reader["Division_Code"] != DBNull.Value ? reader["Division_Code"].ToString() : "";
                    pts.DivisionName = reader["DivisionName"] != DBNull.Value ? reader["DivisionName"].ToString() : "";
                    pts.Branch_Code = reader["Branch_Code"] != DBNull.Value ? reader["Branch_Code"].ToString() : "";
                    pts.BranchName = reader["BranchName"] != DBNull.Value ? reader["BranchName"].ToString() : "";
                    pts.Accountofficer_Code = reader["Accountofficer_Code"] != DBNull.Value ? reader["Accountofficer_Code"].ToString() : "";
                    pts.AccountofficerName = reader["AccountofficerName"] != DBNull.Value ? reader["AccountofficerName"].ToString() : "";
                    pts.staff_id = reader["staff_id"] != DBNull.Value ? reader["staff_id"].ToString() : "";
                    pts.Year = reader["Year"] != DBNull.Value ? Convert.ToInt32(reader["Year"].ToString()) : 0;
                    pts.teambankid = reader["teambankid"] != DBNull.Value ? Convert.ToInt32(reader["teambankid"].ToString()) : 0;
                    pts.teamgroupid = reader["teamgroupid"] != DBNull.Value ? Convert.ToInt32(reader["teamgroupid"].ToString()) : 0;

                    teamstructure.Add(pts);
                }
                con.Close();

                if (levelcode.ToUpper() == "BNK" || levelcode.ToUpper() == "SA" || levelcode.ToUpper() == "PRO" || levelcode.ToUpper() == "MD")
                {
                    if (selectedyear == latestyear)
                    {
                        teamstructure = (List<TeamStructure>)System.Web.HttpContext.Current.Session["session_tem_bnk"];
                    }
                    else
                    {
                        //teamstructure = (from a in entityContext.TeamStructureSet
                        //                 where a.Year == selectedyear
                        //                 select a
                        //                      )
                        //              .GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault())
                        //             .ToList();                    

                        teamstructure = teamstructure.GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault()).ToList();
                    }

                    if (selectedcode.ToLower() == "bnk" || selectedcode.ToLower() == "totalbank" || selectedcode.ToLower() == "total bank")
                    {
                        teamstructure = teamstructure.ToList();
                    }
                    else
                    {
                        teamstructure = (from a in teamstructure
                                         where a.DIRECTORATECODE == selectedcode || a.Region_Code == selectedcode || a.Division_Code == selectedcode ||
                                         a.Branch_Code == selectedcode || a.Accountofficer_Code == selectedcode
                                         select a).ToList();
                    }

                    teamstructureListObj.TotalbankList = (from a in teamstructure

                                                          select new TotalbankData()
                                                          {
                                                              Code = "BNK",
                                                              Name = "Total Bank"
                                                          }).Take(1).ToList();
                    //.GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                    // .OrderBy(x => x.Name)
                    // .ToList();

                    teamstructureListObj.DirectorateList = (from a in teamstructure

                                                           select new DirectorateData()
                                                            {
                                                                Code = a.DIRECTORATECODE,
                                                                Name = a.DIRECTORATENAME
                                                            })
                            .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                            .OrderBy(x => x.Name)
                           .ToList();

                    teamstructureListObj.RegionList = (from a in teamstructure

                                                       select new RegionData()
                                                       {
                                                           Code = a.Region_Code,
                                                           Name = a.RegionName
                                                       })
                               .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                               .OrderBy(x => x.Name)
                              .ToList();

                    teamstructureListObj.DivisionList = (from a in teamstructure

                                                         select new DivisionData()
                                                         {
                                                             Code = a.Division_Code,
                                                             Name = a.DivisionName
                                                         })
                               .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                               .OrderBy(x => x.Name)
                              .ToList();

                    teamstructureListObj.BranchList = (from a in teamstructure

                                                       select new BranchData()
                                                       {
                                                           Code = a.Branch_Code,
                                                           Name = a.BranchName
                                                       })
                               .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                               .OrderBy(x => x.Name)
                              .ToList();

                    teamstructureListObj.AccountOfficerList = (from a in teamstructure

                                                               select new AccountOfficerData()
                                                               {
                                                                   Code = a.Accountofficer_Code,
                                                                   Name = a.AccountofficerName
                                                               })
                               .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                               .OrderBy(x => x.Name)
                              .ToList();

                    teamStructureDataList.Add(teamstructureListObj);
                    //return teamStructureDataList;
                }  // end if

                else if (levelcode.ToUpper() == "DIR")
                {
                    if (selectedyear == latestyear)
                    {
                        teamstructure = (List<TeamStructure>)System.Web.HttpContext.Current.Session["session_tem_dir"];
                    }
                    else
                    {
                        //teamstructure = (from a in entityContext.TeamStructureSet
                        //                 where a.Year == selectedyear && a.DIRECTORATECODE == miscode
                        //                 select a
                        //                      )
                        //              .GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault())
                        //             .ToList();

                        teamstructure = teamstructure.Where(x=>x.DIRECTORATECODE == miscode).GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault()).ToList();
                    }

                    teamstructure = (from a in teamstructure
                                     where a.DIRECTORATECODE == selectedcode || a.Region_Code == selectedcode || a.Division_Code == selectedcode ||
                                     a.Branch_Code == selectedcode || a.Accountofficer_Code == selectedcode
                                     select a).ToList();

                   
                    teamstructureListObj.RegionList = (from a in teamstructure

                                                       select new RegionData()
                                                       {
                                                           Code = a.Region_Code,
                                                           Name = a.RegionName
                                                       })
                                   .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                                   .OrderBy(x => x.Name)
                                  .ToList();

                    teamstructureListObj.DivisionList = (from a in teamstructure

                                                         select new DivisionData()
                                                         {
                                                             Code = a.Division_Code,
                                                             Name = a.DivisionName
                                                         })
                               .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                               .OrderBy(x => x.Name)
                              .ToList();

                    teamstructureListObj.BranchList = (from a in teamstructure

                                                       select new BranchData()
                                                       {
                                                           Code = a.Branch_Code,
                                                           Name = a.BranchName
                                                       })
                               .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                               .OrderBy(x => x.Name)
                              .ToList();

                    teamstructureListObj.AccountOfficerList = (from a in teamstructure

                                                               select new AccountOfficerData()
                                                               {
                                                                   Code = a.Accountofficer_Code,
                                                                   Name = a.AccountofficerName
                                                               })
                               .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                               .OrderBy(x => x.Name)
                              .ToList();

                    teamStructureDataList.Add(teamstructureListObj);
                    //return teamStructureDataList;
                }  // end else if

                else if (levelcode.ToUpper() == "REG")
                {
                    if (selectedyear == latestyear)
                    {
                        teamstructure = (List<TeamStructure>)System.Web.HttpContext.Current.Session["session_tem_reg"];
                    }
                    else
                    {
                        //teamstructure = (from a in entityContext.TeamStructureSet
                        //                 where a.Year == selectedyear && a.Region_Code == miscode
                        //                 select a
                        //                      )
                        //              .GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault())
                        //             .ToList();

                        teamstructure = teamstructure.Where(x=>x.Region_Code == miscode).GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault()).ToList();
                    }

                    teamstructure = (from a in teamstructure
                                     where a.DIRECTORATECODE == selectedcode || a.Region_Code == selectedcode || a.Division_Code == selectedcode ||
                                     a.Branch_Code == selectedcode || a.Accountofficer_Code == selectedcode
                                     select a).ToList();

                   teamstructureListObj.DivisionList = (from a in teamstructure

                                                         select new DivisionData()
                                                         {
                                                             Code = a.Division_Code,
                                                             Name = a.DivisionName
                                                         })
                                   .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                                   .OrderBy(x => x.Name)
                                  .ToList();

                    teamstructureListObj.BranchList = (from a in teamstructure

                                                       select new BranchData()
                                                       {
                                                           Code = a.Branch_Code,
                                                           Name = a.BranchName
                                                       })
                               .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                               .OrderBy(x => x.Name)
                              .ToList();

                    teamstructureListObj.AccountOfficerList = (from a in teamstructure

                                                               select new AccountOfficerData()
                                                               {
                                                                   Code = a.Accountofficer_Code,
                                                                   Name = a.AccountofficerName
                                                               })
                               .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                               .OrderBy(x => x.Name)
                              .ToList();

                    teamStructureDataList.Add(teamstructureListObj);
                    //return teamStructureDataList;
                }  // end else if

                else if (levelcode.ToUpper() == "DIV")
                {
                    if (selectedyear == latestyear)
                    {
                        teamstructure = (List<TeamStructure>)System.Web.HttpContext.Current.Session["session_tem_div"];
                    }
                    else
                    {
                        //teamstructure = (from a in entityContext.TeamStructureSet
                        //                 where a.Year == selectedyear && a.Division_Code == miscode
                        //                 select a
                        //                      )
                        //              .GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault())
                        //             .ToList();

                        teamstructure = teamstructure.Where(x=>x.Division_Code == miscode).GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault()).ToList();
                    }

                    teamstructure = (from a in teamstructure
                                     where a.DIRECTORATECODE == selectedcode || a.Region_Code == selectedcode || a.Division_Code == selectedcode ||
                                     a.Branch_Code == selectedcode || a.Accountofficer_Code == selectedcode
                                     select a).ToList();

                     teamstructureListObj.BranchList = (from a in teamstructure

                                                       select new BranchData()
                                                       {
                                                           Code = a.Branch_Code,
                                                           Name = a.BranchName
                                                       })
                                   .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                                   .OrderBy(x => x.Name)
                                  .ToList();

                    teamstructureListObj.AccountOfficerList = (from a in teamstructure

                                                               select new AccountOfficerData()
                                                               {
                                                                   Code = a.Accountofficer_Code,
                                                                   Name = a.AccountofficerName
                                                               })
                               .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                               .OrderBy(x => x.Name)
                              .ToList();

                    teamStructureDataList.Add(teamstructureListObj);
                    //return teamStructureDataList;
                }  // end else if

                else if (levelcode.ToUpper() == "BRH")
                {
                    if (selectedyear == latestyear)
                    {
                        teamstructure = (List<TeamStructure>)System.Web.HttpContext.Current.Session["session_tem_brh"];
                    }
                    else
                    {
                        //teamstructure = (from a in entityContext.TeamStructureSet
                        //                 where a.Year == selectedyear && a.Branch_Code == miscode
                        //                 select a
                        //                      )
                        //              .GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault())
                        //             .ToList();

                        teamstructure = teamstructure.Where(x=>x.Branch_Code == miscode).GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault()).ToList();
                    }

                    teamstructure = (from a in teamstructure
                                     where a.DIRECTORATECODE == selectedcode || a.Region_Code == selectedcode || a.Division_Code == selectedcode ||
                                     a.Branch_Code == selectedcode || a.Accountofficer_Code == selectedcode
                                     select a).ToList();

                    teamstructureListObj.AccountOfficerList = (from a in teamstructure

                                                               select new AccountOfficerData()
                                                               {
                                                                   Code = a.Accountofficer_Code,
                                                                   Name = a.AccountofficerName
                                                               })
                                   .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                                   .OrderBy(x => x.Name)
                                  .ToList();

                    teamStructureDataList.Add(teamstructureListObj);
                    //return teamStructureDataList;
                }  // end else if
                return teamStructureDataList;
            }
        }






        //public IEnumerable<TeamStructure> GetTeamStructureBySelectedMisCodeAndYear(string selectedcode, string selectedyear)
        //{
        //    using (MPRContext2 entityContext = new MPRContext2())
        //    {
        //        string latestyear = entityContext.TeamStructureSet.Max(x => x.Year);

        //        string levelcode = Convert.ToString(System.Web.HttpContext.Current.Session["session_levelcode"]);
        //    //string miscode = Convert.ToString(System.Web.HttpContext.Current.Session["session_miscode"]);

        //    //var teamstructureListObj = new TeamStructureData();
        //    //var teamStructureDataList = new List<TeamStructureData>();

        //    var teamstructure = new List<TeamStructure>();
        //        var query = new List<TeamStructure>();      


        //        if (levelcode == "BNK")
        //        {
        //            if (selectedyear == latestyear)
        //            {
        //                teamstructure = (List<TeamStructure>)System.Web.HttpContext.Current.Session["session_tem_bnk"];
        //            }
        //            else
        //            {
        //                teamstructure = (from a in entityContext.TeamStructureSet
        //                                 where a.Year == selectedyear
        //                                 select a
        //                                      )
        //                              .GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault())
        //                             .ToList();
        //            }


        //            query = (from a in teamstructure
        //                             where a.DIRECTORATECODE == selectedcode || a.Region_Code == selectedcode || a.Division_Code == selectedcode ||
        //                             a.Branch_Code == selectedcode || a.Accountofficer_Code == selectedcode


        //                             select new
        //                            {
        //                                DIRECTORATECODE = a.DIRECTORATECODE,
        //                                DIRECTORATENAME = a.DIRECTORATENAME,
        //                                Region_Code = a.Region_Code,
        //                                RegionName = a.RegionName,
        //                                Division_Code = a.Division_Code,
        //                                DivisionName = a.DivisionName,
        //                                Branch_Code = a.Branch_Code,
        //                                BranchName = a.BranchName,
        //                                Accountofficer_Code = a.Accountofficer_Code,
        //                                AccountofficerName = a.AccountofficerName
        //                            })
        //                            .AsEnumerable().Select(x => new TeamStructure
        //                            {
        //                                DIRECTORATECODE = x.DIRECTORATECODE,
        //                                DIRECTORATENAME = x.DIRECTORATENAME,
        //                                Region_Code = x.Region_Code,
        //                                RegionName = x.RegionName,
        //                                Division_Code = x.Division_Code,
        //                                DivisionName = x.DivisionName,
        //                                Branch_Code = x.Branch_Code,
        //                                BranchName = x.BranchName,
        //                                Accountofficer_Code = x.Accountofficer_Code,
        //                                AccountofficerName = x.AccountofficerName
        //                            })
        //                            //.GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
        //                            //.OrderBy(x => x.Name)
        //                            .ToList();
        //        }  

        //        else if (levelcode == "DIR")
        //        {
        //            if (selectedyear == latestyear)
        //            {
        //                teamstructure = (List<TeamStructure>)System.Web.HttpContext.Current.Session["session_tem_dir"];
        //            }
        //            else
        //            {
        //                teamstructure = (from a in entityContext.TeamStructureSet
        //                                 where a.Year == selectedyear
        //                                 select a
        //                                      )
        //                              .GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault())
        //                             .ToList();
        //            }

        //            query = (from a in teamstructure
        //                             where a.DIRECTORATECODE == selectedcode || a.Region_Code == selectedcode ||
        //                            a.Division_Code == selectedcode || a.Branch_Code == selectedcode ||
        //                                                     a.Accountofficer_Code == selectedcode

        //                             select new
        //                             {                                        
        //                                 Region_Code = a.Region_Code,
        //                                 RegionName = a.RegionName,
        //                                 Division_Code = a.Division_Code,
        //                                 DivisionName = a.DivisionName,
        //                                 Branch_Code = a.Branch_Code,
        //                                 BranchName = a.BranchName,
        //                                 Accountofficer_Code = a.Accountofficer_Code,
        //                                 AccountofficerName = a.AccountofficerName
        //                             })
        //                            .AsEnumerable().Select(x => new TeamStructure
        //                            {                                                               
        //                                Region_Code = x.Region_Code,
        //                                RegionName = x.RegionName,
        //                                Division_Code = x.Division_Code,
        //                                DivisionName = x.DivisionName,
        //                                Branch_Code = x.Branch_Code,
        //                                BranchName = x.BranchName,
        //                                Accountofficer_Code = x.Accountofficer_Code,
        //                                AccountofficerName = x.AccountofficerName
        //                            })
        //                            //.GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
        //                            //.OrderBy(x => x.Name)
        //                            .ToList();
        //        } 

        //        else if (levelcode == "REG")
        //        {
        //            if (selectedyear == latestyear)
        //            {
        //                teamstructure = (List<TeamStructure>)System.Web.HttpContext.Current.Session["session_tem_reg"];
        //            }
        //            else
        //            {
        //                teamstructure = (from a in entityContext.TeamStructureSet
        //                                 where a.Year == selectedyear
        //                                 select a
        //                                      )
        //                              .GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault())
        //                             .ToList();
        //            }

        //            teamstructure = (from a in teamstructure
        //                             where a.DIRECTORATECODE == selectedcode || a.Region_Code == selectedcode ||
        //                            a.Division_Code == selectedcode || a.Branch_Code == selectedcode ||
        //                                                     a.Accountofficer_Code == selectedcode

        //                             select new
        //                             {
        //                                 Division_Code = a.Division_Code,
        //                                 DivisionName = a.DivisionName,
        //                                 Branch_Code = a.Branch_Code,
        //                                 BranchName = a.BranchName,
        //                                 Accountofficer_Code = a.Accountofficer_Code,
        //                                 AccountofficerName = a.AccountofficerName
        //                             })
        //                            .AsEnumerable().Select(x => new TeamStructure
        //                            {                                        
        //                                Division_Code = x.Division_Code,
        //                                DivisionName = x.DivisionName,
        //                                Branch_Code = x.Branch_Code,
        //                                BranchName = x.BranchName,
        //                                Accountofficer_Code = x.Accountofficer_Code,
        //                                AccountofficerName = x.AccountofficerName
        //                            })
        //                            //.GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
        //                            //.OrderBy(x => x.Name)
        //                            .ToList();
        //        }  // end else if

        //        else if (levelcode == "DIV")
        //        {
        //            if (selectedyear == latestyear)
        //            {
        //                teamstructure = (List<TeamStructure>)System.Web.HttpContext.Current.Session["session_tem_div"];
        //            }
        //            else
        //            {
        //                teamstructure = (from a in entityContext.TeamStructureSet
        //                                 where a.Year == selectedyear
        //                                 select a
        //                                      )
        //                              .GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault())
        //                             .ToList();
        //            }

        //            teamstructure = (from a in teamstructure
        //                             where a.DIRECTORATECODE == selectedcode || a.Region_Code == selectedcode ||
        //                            a.Division_Code == selectedcode || a.Branch_Code == selectedcode ||
        //                                                     a.Accountofficer_Code == selectedcode

        //                             select new
        //                             {
        //                                 Branch_Code = a.Branch_Code,
        //                                 BranchName = a.BranchName,
        //                                 Accountofficer_Code = a.Accountofficer_Code,
        //                                 AccountofficerName = a.AccountofficerName
        //                             })
        //                             .AsEnumerable().Select(x => new TeamStructure
        //                             {
        //                                 Branch_Code = x.Branch_Code,
        //                                 BranchName = x.BranchName,
        //                                 Accountofficer_Code = x.Accountofficer_Code,
        //                                 AccountofficerName = x.AccountofficerName
        //                             })
        //                             //.GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
        //                             //.OrderBy(x => x.Name)
        //                             .ToList();
        //        }  // end else if

        //        else if (levelcode == "BRH")
        //        {
        //            if (selectedyear == latestyear)
        //            {
        //                teamstructure = (List<TeamStructure>)System.Web.HttpContext.Current.Session["session_tem_brh"];
        //            }
        //            else
        //            {
        //                teamstructure = (from a in entityContext.TeamStructureSet
        //                                 where a.Year == selectedyear
        //                                 select a
        //                                      )
        //                              .GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault())
        //                             .ToList();
        //            }

        //            teamstructure = (from a in teamstructure
        //                             where a.DIRECTORATECODE == selectedcode || a.Region_Code == selectedcode ||
        //                            a.Division_Code == selectedcode || a.Branch_Code == selectedcode ||
        //                                                     a.Accountofficer_Code == selectedcode

        //                             select new
        //                             {
        //                                 Accountofficer_Code = a.Accountofficer_Code,
        //                                 AccountofficerName = a.AccountofficerName
        //                             })
        //                             .AsEnumerable().Select(x => new TeamStructure
        //                             {
        //                                 Accountofficer_Code = x.Accountofficer_Code,
        //                                 AccountofficerName = x.AccountofficerName
        //                             })
        //                             //.GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
        //                             //.OrderBy(x => x.Name)
        //                             .ToList();
        //        } 
        //        return query;
        //    }            
        //}




        //============= selection by the selected miscode and selected year  ends ==================================================================================




        //============= selection by a miscode starts ==================================================================================

        public IEnumerable<TeamStructureData> GetSelectedTeamStructureMisCode(string searchvalue)
        {
            using (MPRContext2 entityContext = new MPRContext2())
            {
                //int latestyear = entityContext.TeamStructureSet.Max(x => x.Year);

                var latestmonthyear = entityContext.IncomeSetUpDailySet.OrderByDescending(x => x.Year).Take(1);
                int latestyear = latestmonthyear.Select(x => x.Year).FirstOrDefault();
                //int period = latestmonthyear.Max(x => x.CurrentPeriod);

                var teamstructureListObj = new TeamStructureData();
                var teamStructureDataList = new List<TeamStructureData>();

                string levelcode = Convert.ToString(System.Web.HttpContext.Current.Session["session_levelcode"]);
                string miscode = Convert.ToString(System.Web.HttpContext.Current.Session["session_miscode"]);


                if (levelcode.ToUpper() == "BNK" || levelcode.ToUpper() == "SA" || levelcode.ToUpper() == "PRO" || levelcode.ToUpper() == "MD")
                {
                    teamstructureListObj.DirectorateList = (from a in entityContext.TeamStructureSet
                                                            where a.Year == latestyear

                                                            select new DirectorateData()
                                                            {
                                                                Code = a.DIRECTORATECODE,
                                                                Name = a.DIRECTORATENAME
                                                            })
                        .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                        .OrderBy(x => x.Name)
                       .ToList();

                    teamstructureListObj.RegionList = (from a in entityContext.TeamStructureSet
                                                       where a.Year == latestyear && a.Division_Code == searchvalue

                                                       select new RegionData()
                                                       {
                                                           Code = a.Region_Code,
                                                           Name = a.RegionName
                                                       })
                               .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                               .OrderBy(x => x.Name)
                              .ToList();

                    teamstructureListObj.DivisionList = (from a in entityContext.TeamStructureSet
                                                         where a.Year == latestyear && a.Region_Code == searchvalue

                                                         select new DivisionData()
                                                         {
                                                             Code = a.Division_Code,
                                                             Name = a.DivisionName
                                                         })
                               .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                               .OrderBy(x => x.Name)
                              .ToList();

                    teamstructureListObj.BranchList = (from a in entityContext.TeamStructureSet
                                                       where a.Year == latestyear && a.Division_Code == searchvalue

                                                       select new BranchData()
                                                       {
                                                           Code = a.Branch_Code,
                                                           Name = a.BranchName
                                                       })
                               .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                               .OrderBy(x => x.Name)
                              .ToList();

                    teamstructureListObj.AccountOfficerList = (from a in entityContext.TeamStructureSet
                                                               where a.Year == latestyear && a.Branch_Code == searchvalue

                                                               select new AccountOfficerData()
                                                               {
                                                                   Code = a.Accountofficer_Code,
                                                                   Name = a.AccountofficerName
                                                               })
                               .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                               .OrderBy(x => x.Name)
                              .ToList();

                    teamStructureDataList.Add(teamstructureListObj);
                    //return teamStructureDataList;
                }  // end if

                else if (levelcode.ToUpper() == "DIR")
                {
                    teamstructureListObj.RegionList = (from a in entityContext.TeamStructureSet
                                                       where a.Year == latestyear

                                                       select new RegionData()
                                                       {
                                                           Code = a.Region_Code,
                                                           Name = a.RegionName
                                                       })
                               .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                               .OrderBy(x => x.Name)
                              .ToList();

                    teamstructureListObj.DivisionList = (from a in entityContext.TeamStructureSet
                                                         where a.Year == latestyear && a.Region_Code == searchvalue

                                                         select new DivisionData()
                                                         {
                                                             Code = a.Division_Code,
                                                             Name = a.DivisionName
                                                         })
                               .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                               .OrderBy(x => x.Name)
                              .ToList();

                    teamstructureListObj.BranchList = (from a in entityContext.TeamStructureSet
                                                       where a.Year == latestyear && a.Division_Code == miscode

                                                       select new BranchData()
                                                       {
                                                           Code = a.Branch_Code,
                                                           Name = a.BranchName
                                                       })
                               .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                               .OrderBy(x => x.Name)
                              .ToList();

                    teamstructureListObj.AccountOfficerList = (from a in entityContext.TeamStructureSet
                                                               where a.Year == latestyear && a.Branch_Code == miscode

                                                               select new AccountOfficerData()
                                                               {
                                                                   Code = a.Accountofficer_Code,
                                                                   Name = a.AccountofficerName
                                                               })
                               .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                               .OrderBy(x => x.Name)
                              .ToList();

                    teamStructureDataList.Add(teamstructureListObj);
                    //return teamStructureDataList;
                }  // end else if

                else if (levelcode.ToUpper() == "REG")
                {
                    teamstructureListObj.DivisionList = (from a in entityContext.TeamStructureSet
                                                         where a.Year == latestyear && a.Region_Code == miscode

                                                         select new DivisionData()
                                                         {
                                                             Code = a.Division_Code,
                                                             Name = a.DivisionName
                                                         })
                               .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                               .OrderBy(x => x.Name)
                              .ToList();

                    teamstructureListObj.BranchList = (from a in entityContext.TeamStructureSet
                                                       where a.Year == latestyear && a.Division_Code == searchvalue

                                                       select new BranchData()
                                                       {
                                                           Code = a.Branch_Code,
                                                           Name = a.BranchName
                                                       })
                               .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                               .OrderBy(x => x.Name)
                              .ToList();

                    teamstructureListObj.AccountOfficerList = (from a in entityContext.TeamStructureSet
                                                               where a.Year == latestyear && a.Branch_Code == miscode

                                                               select new AccountOfficerData()
                                                               {
                                                                   Code = a.Accountofficer_Code,
                                                                   Name = a.AccountofficerName
                                                               })
                               .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                               .OrderBy(x => x.Name)
                              .ToList();

                    teamStructureDataList.Add(teamstructureListObj);
                    //return teamStructureDataList;
                }  // end else if

                else if (levelcode.ToUpper() == "DIV")
                {
                    teamstructureListObj.BranchList = (from a in entityContext.TeamStructureSet
                                                       where a.Year == latestyear && a.Division_Code == miscode

                                                       select new BranchData()
                                                       {
                                                           Code = a.Branch_Code,
                                                           Name = a.BranchName
                                                       })
                               .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                               .OrderBy(x => x.Name)
                              .ToList();

                    teamstructureListObj.AccountOfficerList = (from a in entityContext.TeamStructureSet
                                                               where a.Year == latestyear && a.Branch_Code == miscode

                                                               select new AccountOfficerData()
                                                               {
                                                                   Code = a.Accountofficer_Code,
                                                                   Name = a.AccountofficerName
                                                               })
                               .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                               .OrderBy(x => x.Name)
                              .ToList();

                    teamStructureDataList.Add(teamstructureListObj);
                    //return teamStructureDataList;
                }  // end else if

                else if (levelcode == "BRH")
                {
                    teamstructureListObj.AccountOfficerList = (from a in entityContext.TeamStructureSet
                                                               where a.Year == latestyear

                                                               select new AccountOfficerData()
                                                               {
                                                                   Code = a.Accountofficer_Code,
                                                                   Name = a.AccountofficerName
                                                               })
                               .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                               .OrderBy(x => x.Name)
                              .ToList();

                    teamStructureDataList.Add(teamstructureListObj);
                    //return teamStructureDataList;
                }  // end else if
                return teamStructureDataList;
            }
        }

//=========================== selection by a miscode ends =====================================================================================

        public IEnumerable<RegionData> GetRegByDir(string reg_dir)
        {
            using (MPRContext2 entityContext = new MPRContext2())
            {
                // var teamstructureListObj = new TeamStructureData();
                var regionDataList = new List<RegionData>();

                //int latestyear = entityContext.TeamStructureSet.Max(x => x.Year);

                var latestmonthyear = entityContext.IncomeSetUpDailySet.OrderByDescending(x => x.Year).Take(1);
                int latestyear = latestmonthyear.Select(x => x.Year).FirstOrDefault();
                //int period = latestmonthyear.Max(x => x.CurrentPeriod);

                regionDataList = (from a in entityContext.TeamStructureSet
                                  where a.Year == latestyear && a.Division_Code == reg_dir

                                  select new RegionData()
                                  {
                                      Code = a.Region_Code,
                                      Name = a.RegionName
                                  })
                                   .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                                   .OrderBy(x => x.Name)
                                  .ToList();

                return regionDataList;
            }
        }

        public IEnumerable<DivisionData> GetDivByReg(string div_reg)
        {
            using (MPRContext2 entityContext = new MPRContext2())
            {
                // var teamstructureListObj = new TeamStructureData();
                var divisionDataList = new List<DivisionData>();

                //int latestyear = entityContext.TeamStructureSet.Max(x => x.Year);

                var latestmonthyear = entityContext.IncomeSetUpDailySet.OrderByDescending(x => x.Year).Take(1);
                int latestyear = latestmonthyear.Select(x => x.Year).FirstOrDefault();
                //int period = latestmonthyear.Max(x => x.CurrentPeriod);

                divisionDataList = (from a in entityContext.TeamStructureSet
                                    where a.Year == latestyear && a.Region_Code == div_reg

                                    select new DivisionData()
                                    {
                                        Code = a.Division_Code,
                                        Name = a.DivisionName
                                    })
                                   .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                                   .OrderBy(x => x.Name)
                                  .ToList();

                return divisionDataList;
            }
        }

        public IEnumerable<BranchData> GetBrhByDiv(string brh_div)
        {
            using (MPRContext2 entityContext = new MPRContext2())
            {
                // var teamstructureListObj = new TeamStructureData();
                var branchDataList = new List<BranchData>();

                //int latestyear = entityContext.TeamStructureSet.Max(x => x.Year);

                var latestmonthyear = entityContext.IncomeSetUpDailySet.OrderByDescending(x => x.Year).Take(1);
                int latestyear = latestmonthyear.Select(x => x.Year).FirstOrDefault();
                //int period = latestmonthyear.Max(x => x.CurrentPeriod);

                branchDataList = (from a in entityContext.TeamStructureSet
                                  where a.Year == latestyear && a.Division_Code == brh_div

                                  select new BranchData()
                                  {
                                      Code = a.Branch_Code,
                                      Name = a.BranchName
                                  })
                                   .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                                   .OrderBy(x => x.Name)
                                  .ToList();

                return branchDataList;
            }
        }

        public IEnumerable<AccountOfficerData> GetAcctByBrh(string acct_brh)
        {
            using (MPRContext2 entityContext = new MPRContext2())
            {
                // var teamstructureListObj = new TeamStructureData();
                var acctofficerDataList = new List<AccountOfficerData>();

                //int latestyear = entityContext.TeamStructureSet.Max(x => x.Year);

                var latestmonthyear = entityContext.IncomeSetUpDailySet.OrderByDescending(x => x.Year).Take(1);
                int latestyear = latestmonthyear.Select(x => x.Year).FirstOrDefault();
                //int period = latestmonthyear.Max(x => x.CurrentPeriod);

                acctofficerDataList = (from a in entityContext.TeamStructureSet
                                       where a.Year == latestyear && a.Branch_Code == acct_brh

                                       select new AccountOfficerData()
                                       {
                                           Code = a.Accountofficer_Code,
                                           Name = a.AccountofficerName
                                       })
                                   .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                                   .OrderBy(x => x.Name)
                                  .ToList();

                return acctofficerDataList;
            }
        }


        public YearPeriodData GetLatestYearAndPeriod()
        {
            using (MPRContext2 entityContext = new MPRContext2())
            {
                //int latestyear = entityContext.TeamStructureSet.Max(x => x.Year);
                //int period = entityContext.IncomeSetUpDailySet.Max(x => x.CurrentPeriod);

                var latestmonthyear = entityContext.IncomeSetUpDailySet.OrderByDescending(x => x.Year).Take(1);
                int latestyear = latestmonthyear.Select(x => x.Year).FirstOrDefault();
                int period = latestmonthyear.Max(x => x.CurrentPeriod);

                YearPeriodData ypObj = new YearPeriodData();
                ypObj.Year = latestyear;
                ypObj.Period = period;

                return ypObj;
            }
        }



    }
} 

