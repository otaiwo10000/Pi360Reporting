﻿using System;
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
    public class TeamStructureABPRepository : IDataRepository
    {
        string connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;
        MPRContext2 context2 = new MPRContext2();
       
// ================================ on pageload starts =======================================================
        public IEnumerable<TeamStructureData> GetTeamStructureByMISCodeLevelYear()
        {
            var teamstructure = new List<TeamStructureALL>();

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
                int level = int.Parse(Convert.ToString(System.Web.HttpContext.Current.Session["session_level"]));

                //var teamstructure = new List<TeamStructure>();

                //var cmd = new SqlCommand("spp_getteamstructureForLatestYearABP", con);
                var cmd = new SqlCommand("spp_getteamstructureForLatestYearALL", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var pts = new TeamStructureALL();

                    //pts.DIRECTORATECODE = reader["DIRECTORATECODE"] != DBNull.Value ? reader["DIRECTORATECODE"].ToString() : "";
                    //pts.DIRECTORATENAME = reader["DIRECTORATENAME"] != DBNull.Value ? reader["DIRECTORATENAME"].ToString() : "";
                    pts.SuperSegment_Code = reader["SuperSegment_Code"] != DBNull.Value ? reader["SuperSegment_Code"].ToString() : "";
                    pts.SuperSegmentName = reader["SuperSegmentName"] != DBNull.Value ? reader["SuperSegmentName"].ToString() : "";
                    pts.Segment_Code = reader["Segment_Code"] != DBNull.Value ? reader["Segment_Code"].ToString() : "";
                    pts.SegmentName = reader["SegmentName"] != DBNull.Value ? reader["SegmentName"].ToString() : "";

                    pts.Division_Code = reader["Division_Code"] != DBNull.Value ? reader["Division_Code"].ToString() : "";
                    pts.DivisionName = reader["DivisionName"] != DBNull.Value ? reader["DivisionName"].ToString() : "";

                    pts.Group_Code = reader["Group_Code"] != DBNull.Value ? reader["Group_Code"].ToString() : "";
                    pts.GroupName = reader["GroupName"] != DBNull.Value ? reader["GroupName"].ToString() : "";
                    pts.Region_Code = reader["Region_Code"] != DBNull.Value ? reader["Region_Code"].ToString() : "";
                    pts.RegionName = reader["RegionName"] != DBNull.Value ? reader["RegionName"].ToString() : "";
                    //pts.Zone_Code = reader["Zone_Code"] != DBNull.Value ? reader["Zone_Code"].ToString() : "";
                    //pts.ZoneName = reader["ZoneName"] != DBNull.Value ? reader["ZoneName"].ToString() : "";

                    pts.Branch_Code = reader["Branch_Code"] != DBNull.Value ? reader["Branch_Code"].ToString() : "";
                    pts.BranchName = reader["BranchName"] != DBNull.Value ? reader["BranchName"].ToString() : "";
                    pts.Team_Code = reader["Team_Code"] != DBNull.Value ? reader["Team_Code"].ToString() : "";
                    pts.TeamName = reader["TeamName"] != DBNull.Value ? reader["TeamName"].ToString() : "";
                    pts.Accountofficer_Code = reader["Accountofficer_Code"] != DBNull.Value ? reader["Accountofficer_Code"].ToString() : "";
                    pts.AccountofficerName = reader["AccountofficerName"] != DBNull.Value ? reader["AccountofficerName"].ToString() : "";
                    
                    pts.staff_id = reader["staff_id"] != DBNull.Value ? reader["staff_id"].ToString() : "";
                    pts.Year = reader["Year"] != DBNull.Value ? Convert.ToInt32(reader["Year"].ToString()) : 0;
                    pts.Team_StructureId = reader["Team_StructureId"] != DBNull.Value ? Convert.ToInt32(reader["Team_StructureId"].ToString()) : 0;

                    teamstructure.Add(pts);
                }
                con.Close();


                //if (levelcode.ToUpper() == "BNK" || levelcode.ToUpper() == "SA" || levelcode.ToUpper() == "PRO" || levelcode.ToUpper() == "MD" || levelcode.ToUpper() == "TRE")
                if (level == 0)
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

                    teamstructureListObj.SuperSegmentList = (from a in teamstructure

                                                        select new SuperSegmentData()
                                                        {
                                                            Code = a.SuperSegment_Code,
                                                            Name = a.SuperSegmentName
                                                        })
                           .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                           .OrderBy(x => x.Name)
                          .ToList();

                    teamstructureListObj.SegmentList = (from a in teamstructure

                                                         select new SegmentData()
                                                         {
                                                             Code = a.Segment_Code,
                                                             Name = a.SegmentName
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

                    teamstructureListObj.GroupList = (from a in teamstructure

                                                      select new GroupData()
                                                      {
                                                          Code = a.Group_Code,
                                                          Name = a.GroupName
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

                    teamstructureListObj.BranchList = (from a in teamstructure

                                                       select new BranchData()
                                                       {
                                                           Code = a.Branch_Code,
                                                           Name = a.BranchName
                                                       })
                               .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                               .OrderBy(x => x.Name)
                              .ToList();

                    teamstructureListObj.TeamList = (from a in teamstructure

                                                     select new TeamData()
                                                     {
                                                         Code = a.Team_Code,
                                                         Name = a.TeamName
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
                }  // end if

                //else if (levelcode.ToUpper() == "SSG")
                else if (level == 8)
                {
                    teamstructure = teamstructure.Where(x => x.SuperSegment_Code == miscode).GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault()).ToList();

                    System.Web.HttpContext.Current.Session["session_tem_ssg"] = teamstructure;

                    teamstructureListObj.SuperSegmentList = (from a in teamstructure

                                                        select new SuperSegmentData()
                                                        {
                                                            Code = a.SuperSegment_Code,
                                                            Name = a.SuperSegmentName
                                                        })
                                  .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                                  .OrderBy(x => x.Name)
                                 .ToList();

                    teamstructureListObj.SegmentList = (from a in teamstructure

                                                        select new SegmentData()
                                                        {
                                                            Code = a.Segment_Code,
                                                            Name = a.SegmentName
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

                    teamstructureListObj.RegionList = (from a in teamstructure

                                                       select new RegionData()
                                                       {
                                                           Code = a.Region_Code,
                                                           Name = a.RegionName
                                                       })
                             .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                             .OrderBy(x => x.Name)
                            .ToList();

                    teamstructureListObj.GroupList = (from a in teamstructure

                                                      select new GroupData()
                                                      {
                                                          Code = a.Group_Code,
                                                          Name = a.GroupName
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

                    teamstructureListObj.TeamList = (from a in teamstructure

                                                     select new TeamData()
                                                     {
                                                         Code = a.Team_Code,
                                                         Name = a.TeamName
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
                }  // end else if

                //else if (levelcode.ToUpper() == "SEG")
                else if (level == 7)
                {
                    teamstructure = teamstructure.Where(x => x.Segment_Code == miscode).GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault()).ToList();

                    System.Web.HttpContext.Current.Session["session_tem_seg"] = teamstructure;

                    teamstructureListObj.SegmentList = (from a in teamstructure
                                                         //where a.Segment_Code == miscode

                                                         select new SegmentData()
                                                         {
                                                             Code = a.Segment_Code,
                                                             Name = a.SegmentName
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

                    teamstructureListObj.RegionList = (from a in teamstructure

                                                       select new RegionData()
                                                       {
                                                           Code = a.Region_Code,
                                                           Name = a.RegionName
                                                       })
                             .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                             .OrderBy(x => x.Name)
                            .ToList();

                    teamstructureListObj.GroupList = (from a in teamstructure

                                                      select new GroupData()
                                                      {
                                                          Code = a.Group_Code,
                                                          Name = a.GroupName
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

                    teamstructureListObj.TeamList = (from a in teamstructure

                                                     select new TeamData()
                                                     {
                                                         Code = a.Team_Code,
                                                         Name = a.TeamName
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
                }  // end else if

                //else if (levelcode.ToUpper() == "DIV")
                else if (level == 5)
                {
                    teamstructure = teamstructure.Where(x => x.Division_Code == miscode).GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault()).ToList();

                    System.Web.HttpContext.Current.Session["session_tem_div"] = teamstructure;

                    teamstructureListObj.DivisionList = (from a in teamstructure
                                                         //where a.Division_Code == miscode

                                                         select new DivisionData()
                                                         {
                                                             Code = a.Division_Code,
                                                             Name = a.DivisionName
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

                    teamstructureListObj.GroupList = (from a in teamstructure

                                                      select new GroupData()
                                                      {
                                                          Code = a.Group_Code,
                                                          Name = a.GroupName
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

                    teamstructureListObj.TeamList = (from a in teamstructure

                                                     select new TeamData()
                                                     {
                                                         Code = a.Team_Code,
                                                         Name = a.TeamName
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
                }  // end else if

                //else if (levelcode.ToUpper() == "GRP")
                else if (level == 3)
                {
                    teamstructure = teamstructure.Where(x => x.Group_Code == miscode).GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault()).ToList();

                    System.Web.HttpContext.Current.Session["session_tem_grp"] = teamstructure;

                    teamstructureListObj.GroupList = (from a in teamstructure
                                                         //where a.Group_Code == miscode

                                                         select new GroupData()
                                                         {
                                                             Code = a.Group_Code,
                                                             Name = a.GroupName
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

                    teamstructureListObj.BranchList = (from a in teamstructure

                                                      select new BranchData()
                                                      {
                                                          Code = a.Branch_Code,
                                                          Name = a.BranchName
                                                      })
                              .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                              .OrderBy(x => x.Name)
                             .ToList();

                    teamstructureListObj.TeamList = (from a in teamstructure

                                                     select new TeamData()
                                                     {
                                                         Code = a.Team_Code,
                                                         Name = a.TeamName
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
                }  // end else if

                //else if (levelcode.ToUpper() == "REG")
                else if (level == 4)
                {
                    teamstructure = teamstructure.Where(x => x.Region_Code == miscode).GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault()).ToList();

                    System.Web.HttpContext.Current.Session["session_tem_reg"] = teamstructure;

                    teamstructureListObj.RegionList = (from a in teamstructure
                                                      //where a.Region_Code == miscode

                                                      select new RegionData()
                                                      {
                                                          Code = a.Region_Code,
                                                          Name = a.RegionName
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

                    teamstructureListObj.TeamList = (from a in teamstructure

                                                     select new TeamData()
                                                     {
                                                         Code = a.Team_Code,
                                                         Name = a.TeamName
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
                }  // end else if

                //else if (levelcode.ToUpper() == "BRH")
                else if (level == 6)
                {
                    teamstructure = teamstructure.Where(x => x.Branch_Code == miscode).GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault()).ToList();

                    System.Web.HttpContext.Current.Session["session_tem_brh"] = teamstructure;                  

                    teamstructureListObj.BranchList = (from a in teamstructure

                                                       select new BranchData()
                                                       {
                                                           Code = a.Branch_Code,
                                                           Name = a.BranchName
                                                       })
                           .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                           .OrderBy(x => x.Name)
                          .ToList();

                    teamstructureListObj.TeamList = (from a in teamstructure

                                                     select new TeamData()
                                                     {
                                                         Code = a.Team_Code,
                                                         Name = a.TeamName
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
                }  // end else if


                //else if (levelcode.ToUpper() == "TEM")
                else if (level == 2)
                {
                    teamstructure = teamstructure.Where(x => x.Team_Code == miscode).GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault()).ToList();

                    System.Web.HttpContext.Current.Session["session_tem_tem"] = teamstructure;

                    teamstructureListObj.TeamList = (from a in teamstructure

                                                     select new TeamData()
                                                     {
                                                         Code = a.Team_Code,
                                                         Name = a.TeamName
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
                }  // end else if

                //else if (levelcode.ToUpper() == "ACCT")
                else if (level == 1)
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
                }  // end else if

                return teamStructureDataList;
            }
        }



//============= selection by the selected miscode and selected year starts ==================================================================================

        public IEnumerable<TeamStructureData> GetTeamStructureBySelectedMisCodeAndYear(string selectedcode, int selectedyear)
        {
            var teamstructure = new List<TeamStructureALL>();

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
                int level = int.Parse(Convert.ToString(System.Web.HttpContext.Current.Session["session_level"]));

                //var cmd = new SqlCommand("spp_getteamstructureByYearABP", con);
                var cmd = new SqlCommand("spp_getteamstructureByYearALL", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "year",
                    Value = selectedyear,
                });

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var pts = new TeamStructureALL();

                    //pts.DIRECTORATECODE = reader["DIRECTORATECODE"] != DBNull.Value ? reader["DIRECTORATECODE"].ToString() : "";
                    //pts.DIRECTORATENAME = reader["DIRECTORATENAME"] != DBNull.Value ? reader["DIRECTORATENAME"].ToString() : "";
                    pts.SuperSegment_Code = reader["SuperSegment_Code"] != DBNull.Value ? reader["SuperSegment_Code"].ToString() : "";
                    pts.SuperSegmentName = reader["SuperSegmentName"] != DBNull.Value ? reader["SuperSegmentName"].ToString() : "";
                    pts.Segment_Code = reader["Segment_Code"] != DBNull.Value ? reader["Segment_Code"].ToString() : "";
                    pts.SegmentName = reader["SegmentName"] != DBNull.Value ? reader["SegmentName"].ToString() : "";

                    pts.Division_Code = reader["Division_Code"] != DBNull.Value ? reader["Division_Code"].ToString() : "";
                    pts.DivisionName = reader["DivisionName"] != DBNull.Value ? reader["DivisionName"].ToString() : "";
                    pts.Group_Code = reader["Group_Code"] != DBNull.Value ? reader["Group_Code"].ToString() : "";
                    pts.GroupName = reader["GroupName"] != DBNull.Value ? reader["GroupName"].ToString() : "";
                    pts.Region_Code = reader["Region_Code"] != DBNull.Value ? reader["Region_Code"].ToString() : "";
                    pts.RegionName = reader["RegionName"] != DBNull.Value ? reader["RegionName"].ToString() : "";
                    //pts.Zone_Code = reader["Zone_Code"] != DBNull.Value ? reader["Zone_Code"].ToString() : "";
                    //pts.ZoneName = reader["ZoneName"] != DBNull.Value ? reader["ZoneName"].ToString() : "";

                    pts.Branch_Code = reader["Branch_Code"] != DBNull.Value ? reader["Branch_Code"].ToString() : "";
                    pts.BranchName = reader["BranchName"] != DBNull.Value ? reader["BranchName"].ToString() : "";
                    pts.Team_Code = reader["Team_Code"] != DBNull.Value ? reader["Team_Code"].ToString() : "";
                    pts.TeamName = reader["TeamName"] != DBNull.Value ? reader["TeamName"].ToString() : "";
                    pts.Accountofficer_Code = reader["Accountofficer_Code"] != DBNull.Value ? reader["Accountofficer_Code"].ToString() : "";
                    pts.AccountofficerName = reader["AccountofficerName"] != DBNull.Value ? reader["AccountofficerName"].ToString() : "";

                    pts.staff_id = reader["staff_id"] != DBNull.Value ? reader["staff_id"].ToString() : "";
                    pts.Year = reader["Year"] != DBNull.Value ? Convert.ToInt32(reader["Year"].ToString()) : 0;
                    pts.Team_StructureId = reader["Team_StructureId"] != DBNull.Value ? Convert.ToInt32(reader["Team_StructureId"].ToString()) : 0;

                    teamstructure.Add(pts);
                }
                con.Close();

                //if (levelcode.ToUpper() == "BNK" || levelcode.ToUpper() == "SA" || levelcode.ToUpper() == "PRO" || levelcode.ToUpper() == "MD" || levelcode.ToUpper() == "TRE")
                if (level == 0 )
                {
                    if (selectedyear == latestyear)
                    {
                        teamstructure = (List<TeamStructureALL>)System.Web.HttpContext.Current.Session["session_tem_bnk"];
                    }
                    else
                    {                        
                        teamstructure = teamstructure.GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault()).ToList();
                    }

                    if (selectedcode.ToLower() == "bnk" || selectedcode.ToLower() == "totalbank" || selectedcode.ToLower() == "total bank")
                    {
                        teamstructure = teamstructure.ToList();
                    }
                    else
                    {
                        teamstructure = (from a in teamstructure
                                         where a.SuperSegment_Code == selectedcode || a.Segment_Code == selectedcode || a.Division_Code == selectedcode || 
                                         a.Group_Code == selectedcode || a.Region_Code == selectedcode || a.Branch_Code == selectedcode || 
                                         a.Team_Code == selectedcode || a.Accountofficer_Code == selectedcode 
                                         select a).ToList();
                    }

                    teamstructureListObj.TotalbankList = (from a in teamstructure

                                                          select new TotalbankData()
                                                          {
                                                              Code = "BNK",
                                                              Name = "Total Bank"
                                                          }).Take(1).ToList();


                    teamstructureListObj.SuperSegmentList = (from a in teamstructure

                                                        select new SuperSegmentData()
                                                        {
                                                            Code = a.SuperSegment_Code,
                                                            Name = a.SuperSegmentName
                                                        })
                           .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                           .OrderBy(x => x.Name)
                          .ToList();

                    teamstructureListObj.SegmentList = (from a in teamstructure

                                                         select new SegmentData()
                                                         {
                                                             Code = a.Segment_Code,
                                                             Name = a.SegmentName
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

                    teamstructureListObj.GroupList = (from a in teamstructure

                                                      select new GroupData()
                                                      {
                                                          Code = a.Group_Code,
                                                          Name = a.GroupName
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

                    teamstructureListObj.BranchList = (from a in teamstructure

                                                     select new BranchData()
                                                     {
                                                         Code = a.Branch_Code,
                                                         Name = a.BranchName
                                                     })
                             .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                             .OrderBy(x => x.Name)
                            .ToList();

                    teamstructureListObj.TeamList = (from a in teamstructure

                                                       select new TeamData()
                                                       {
                                                           Code = a.Team_Code,
                                                           Name = a.TeamName
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
                }  // end if

                //else if (levelcode.ToUpper() == "SSG")
                else if (level == 8)
                {
                    if (selectedyear == latestyear)
                    {
                        teamstructure = (List<TeamStructureALL>)System.Web.HttpContext.Current.Session["session_tem_ssg"];
                    }
                    else
                    {
                        teamstructure = teamstructure.Where(x => x.SuperSegment_Code == miscode).GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault()).ToList();
                    }

                    teamstructure = (from a in teamstructure
                                     where a.SuperSegment_Code == selectedcode || a.Segment_Code == selectedcode || a.Division_Code == selectedcode ||
                                     a.Group_Code == selectedcode || a.Region_Code == selectedcode || a.Branch_Code == selectedcode || 
                                     a.Team_Code == selectedcode || a.Accountofficer_Code == selectedcode
                                     select a).ToList();

                    teamstructureListObj.SegmentList = (from a in teamstructure

                                                         select new SegmentData()
                                                         {
                                                             Code = a.Segment_Code,
                                                             Name = a.SegmentName
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

                    teamstructureListObj.GroupList = (from a in teamstructure

                                                      select new GroupData()
                                                      {
                                                          Code = a.Group_Code,
                                                          Name = a.GroupName
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

                    teamstructureListObj.BranchList = (from a in teamstructure

                                                       select new BranchData()
                                                       {
                                                           Code = a.Branch_Code,
                                                           Name = a.BranchName
                                                       })
                             .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                             .OrderBy(x => x.Name)
                            .ToList();

                    teamstructureListObj.TeamList = (from a in teamstructure

                                                     select new TeamData()
                                                     {
                                                         Code = a.Team_Code,
                                                         Name = a.TeamName
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
                }  // end else if

                //else if (levelcode.ToUpper() == "SEG")
                else if (level == 7)
                {
                    if (selectedyear == latestyear)
                    {
                        teamstructure = (List<TeamStructureALL>)System.Web.HttpContext.Current.Session["session_tem_seg"];
                    }
                    else
                    {
                        teamstructure = teamstructure.Where(x => x.Segment_Code == miscode).GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault()).ToList();
                    }

                    teamstructure = (from a in teamstructure
                                     where a.Segment_Code == selectedcode || a.Division_Code == selectedcode || a.Group_Code == selectedcode || 
                                     a.Region_Code == selectedcode || a.Branch_Code == selectedcode || a.Team_Code == selectedcode || 
                                     a.Accountofficer_Code == selectedcode
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

                    teamstructureListObj.GroupList = (from a in teamstructure

                                                      select new GroupData()
                                                      {
                                                          Code = a.Group_Code,
                                                          Name = a.GroupName
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

                    teamstructureListObj.BranchList = (from a in teamstructure

                                                       select new BranchData()
                                                       {
                                                           Code = a.Branch_Code,
                                                           Name = a.BranchName
                                                       })
                             .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                             .OrderBy(x => x.Name)
                            .ToList();

                    teamstructureListObj.TeamList = (from a in teamstructure

                                                     select new TeamData()
                                                     {
                                                         Code = a.Team_Code,
                                                         Name = a.TeamName
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
                }  // end else if

                //else if (levelcode.ToUpper() == "DIV")
                else if (level == 5)
                {
                    if (selectedyear == latestyear)
                    {
                        teamstructure = (List<TeamStructureALL>)System.Web.HttpContext.Current.Session["session_tem_div"];
                    }
                    else
                    {                        
                        teamstructure = teamstructure.Where(x=>x.Division_Code == miscode).GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault()).ToList();
                    }

                    teamstructure = (from a in teamstructure
                                     where a.Division_Code == selectedcode || a.Group_Code == selectedcode || a.Region_Code == selectedcode ||
                                     a.Branch_Code == selectedcode || a.Team_Code == selectedcode || a.Accountofficer_Code == selectedcode
                                     select a).ToList();

                    teamstructureListObj.GroupList = (from a in teamstructure

                                                      select new GroupData()
                                                      {
                                                          Code = a.Group_Code,
                                                          Name = a.GroupName
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

                    teamstructureListObj.BranchList = (from a in teamstructure

                                                     select new BranchData()
                                                     {
                                                         Code = a.Branch_Code,
                                                         Name = a.BranchName
                                                     })
                             .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                             .OrderBy(x => x.Name)
                            .ToList();

                    teamstructureListObj.TeamList = (from a in teamstructure

                                                     select new TeamData()
                                                     {
                                                         Code = a.Team_Code,
                                                         Name = a.TeamName
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
                }  // end else if

                //else if (levelcode.ToUpper() == "GRP")
                else if (level == 3)
                {
                    if (selectedyear == latestyear)
                    {
                        teamstructure = (List<TeamStructureALL>)System.Web.HttpContext.Current.Session["session_tem_grp"];
                    }
                    else
                    {
                        teamstructure = teamstructure.Where(x => x.Group_Code == miscode).GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault()).ToList();
                    }

                    teamstructure = (from a in teamstructure
                                     where a.Division_Code == selectedcode || a.Group_Code == selectedcode || a.Region_Code == selectedcode ||
                                     a.Branch_Code == selectedcode || a.Team_Code == selectedcode || a.Accountofficer_Code == selectedcode
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

                    teamstructureListObj.BranchList = (from a in teamstructure

                                                       select new BranchData()
                                                       {
                                                           Code = a.Branch_Code,
                                                           Name = a.BranchName
                                                       })
                             .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                             .OrderBy(x => x.Name)
                            .ToList();

                    teamstructureListObj.TeamList = (from a in teamstructure

                                                     select new TeamData()
                                                     {
                                                         Code = a.Team_Code,
                                                         Name = a.TeamName
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
                }  // end else if

                //else if (levelcode.ToUpper() == "REG")
                else if (level == 4)
                {
                    if (selectedyear == latestyear)
                    {
                        teamstructure = (List<TeamStructureALL>)System.Web.HttpContext.Current.Session["session_tem_reg"];
                    }
                    else
                    {
                        teamstructure = teamstructure.Where(x=>x.Region_Code == miscode).GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault()).ToList();
                    }

                    teamstructure = (from a in teamstructure
                                     where a.Division_Code == selectedcode || a.Group_Code == selectedcode || a.Region_Code == selectedcode ||
                                     a.Branch_Code == selectedcode || a.Team_Code == selectedcode || a.Accountofficer_Code == selectedcode
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

                    teamstructureListObj.TeamList = (from a in teamstructure

                                                     select new TeamData()
                                                     {
                                                         Code = a.Team_Code,
                                                         Name = a.TeamName
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
                }  // end else if

                //else if (levelcode.ToUpper() == "BRH")
                else if (level == 6)
                {
                    if (selectedyear == latestyear)
                    {
                        teamstructure = (List<TeamStructureALL>)System.Web.HttpContext.Current.Session["session_tem_brh"];
                    }
                    else
                    {
                        teamstructure = teamstructure.Where(x => x.Branch_Code == miscode).GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault()).ToList();
                    }

                    teamstructure = (from a in teamstructure
                                     where a.Division_Code == selectedcode || a.Group_Code == selectedcode || a.Region_Code == selectedcode ||
                                     a.Branch_Code == selectedcode || a.Team_Code == selectedcode || a.Accountofficer_Code == selectedcode
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

                    teamstructureListObj.TeamList = (from a in teamstructure

                                                     select new TeamData()
                                                     {
                                                         Code = a.Team_Code,
                                                         Name = a.TeamName
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
                }  // end else if


                //else if (levelcode.ToUpper() == "TEM")
                else if (level == 2)
                {
                    if (selectedyear == latestyear)
                    {
                        teamstructure = (List<TeamStructureALL>)System.Web.HttpContext.Current.Session["session_tem_tem"];
                    }
                    else
                    {
                        teamstructure = teamstructure.Where(x => x.Team_Code == miscode).GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault()).ToList();
                    }

                    teamstructure = (from a in teamstructure
                                     where a.Division_Code == selectedcode || a.Group_Code == selectedcode || a.Region_Code == selectedcode ||
                                     a.Branch_Code == selectedcode || a.Team_Code == selectedcode || a.Accountofficer_Code == selectedcode
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

    }
} 

