using log4net;
using MPR.Report.Core.Entities;
using MPR.Report.Data.Business;
using Pi360Reporting.Models.AccountModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pi360Reporting.Controllers
{
    //[OtherMethods.CustomActionFilter]
    //[Authorize]
    public class OnBoardingUsersController : Controller
    {
        public static readonly ILog appLog = LogManager.GetLogger("File1Appender");

        private MPRContext3 mx3 = new MPRContext3();
        private MPRContext2 mx2 = new MPRContext2();       


        [HttpGet]
        public ActionResult OnBoardingUserAdd()
        {
            //ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpPost]
        public ActionResult OnBoardingUserAdd(OnBoardingUsers user)
        //public ActionResult OnBoardingUserAdd()
        {
            try
            {

            

            //OnBoardingUsers user = new OnBoardingUsers();
            //user.Email = "t@gmail.com";  //unique
            //user.FirstName = "Taiwo";
            //user.LastName = "oye";
            //user.MISCode = "miscode123";    //unique
            //user.StaffId = "staffid001";   //unique
            //user.TeamDefinitionCode = "tdefcode";
            //user.UserName = "fintrak";

            //ViewBag.Message = "Your application description page.";
            MPR.Report.Data.Business.DataRepositories.OnBoardingUsersRepository h = new MPR.Report.Data.Business.DataRepositories.OnBoardingUsersRepository();
            h.Add(user);


            //return View();
            return Json(new { v = "success", JsonRequestBehavior.AllowGet });
            }

            catch(Exception ex)
            {
                appLog.InfoFormat("{0}{1}", "InnerException exception: ", ex.InnerException.Message);
            }

            return View();
        }


        public ActionResult teamDefinitionBind()
        {
            var latestmonthyear = mx2.IncomeSetUpDailySet.OrderByDescending(x => x.Year).Take(1);
            int latestyear = latestmonthyear.Select(x => x.Year).FirstOrDefault();

            //SelectList countryList = new SelectList(db.Countries.OrderByDescending(i => i.CountryName), "CountryId", "CountryName");
            //SelectList teamDefinitionList = new SelectList(mx2.TeamDefinitionSet.Where(x=>x.Year == latestyear.ToString()).OrderByDescending(i => i.Name).GroupBy(x => x.Code), "Code", "Name");
            //SelectList teamDefinitionList = new SelectList(mx2.TeamDefinitionSet.Where(x => x.Year == latestyear.ToString()).OrderByDescending(i => i.Name).GroupBy(x => x.TeamDefinitionId), "TeamDefinitionId", "Name");

            var teamDefinitionList = (from a in mx2.TeamDefinitionSet
                                     where a.Year == latestyear.ToString()

                     select new
                     {
                         Code = a.Code,
                         Name = a.Name
                     })
                    .AsEnumerable().Select(x => new TeamDefinition
                    {
                        Code = x.Code,
                        Name = x.Name
                    })
                    .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                    .OrderBy(x => x.Name)
                    .ToList();


            //var teamDefinitionList = mx2.TeamDefinitionSet
            //                        .Select(x => new TeamDefinition { Name = x.Name, Code = x.Code })
            //                        .Where(x => x.Year == latestyear.ToString()).OrderByDescending(i => i.Name).GroupBy(x => x.Code);

            return Json(teamDefinitionList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult misBind(string code, string search)
        {
            var latestmonthyear = mx2.IncomeSetUpDailySet.OrderByDescending(x => x.Year).Take(1);
            int latestyear = latestmonthyear.Select(x => x.Year).FirstOrDefault();

            code = code.ToUpper().Trim();
            List<NameCodeModel> misList = null;

            //var segmentcaption = bsINOtherInformations
            //                     .Select(x => new SegmentCaption { Segment = x.Segment, OtherCaption = x.OtherCaption, MainCaption = x.MainCaption, Currency = x.Currency })
            //                     .GroupBy(x => new { x.Segment, x.OtherCaption, x.MainCaption, x.Currency }).Select(o => o.FirstOrDefault());

            switch (code)
            {
                case "DIV":
                    misList = mx2.TeamStructureALLSet.Where(x => x.Year == latestyear && (x.DivisionName.StartsWith(search) || x.Division_Code.StartsWith(search)))
                                   .Select(x => new NameCodeModel { Name = x.DivisionName, Code = x.Division_Code })                                  
                                   .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                                   .OrderByDescending(x => x.Name).ToList();                 
                    break;

                case "GRP":
                    misList = mx2.TeamStructureALLSet.Where(x => x.Year == latestyear && (x.GroupName.StartsWith(search) || x.Group_Code.StartsWith(search)))
                                  .Select(x => new NameCodeModel { Name = x.GroupName, Code = x.Group_Code })
                                  .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                                  .OrderByDescending(x => x.Name).ToList();
                    break;

                case "REG":
                    misList = mx2.TeamStructureALLSet.Where(x => x.Year == latestyear && (x.RegionName.StartsWith(search) || x.Region_Code.StartsWith(search)))
                                  .Select(x => new NameCodeModel { Name = x.RegionName, Code = x.Region_Code })
                                  .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                                  .OrderByDescending(x => x.Name).ToList();
                    break;

                case "BRH":
                    misList = mx2.TeamStructureALLSet.Where(x => x.Year == latestyear && (x.BranchName.StartsWith(search) || x.Branch_Code.StartsWith(search)))
                                 .Select(x => new NameCodeModel { Name = x.BranchName, Code = x.Branch_Code })
                                 .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                                 .OrderByDescending(x => x.Name).ToList();
                    break;

                case "TEM":
                    misList = mx2.TeamStructureALLSet.Where(x => x.Year == latestyear && (x.TeamName.StartsWith(search) || x.Team_Code.StartsWith(search)))
                                .Select(x => new NameCodeModel { Name = x.TeamName, Code = x.Team_Code })
                                .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                                .OrderByDescending(x => x.Name).ToList();
                    break;

                case "ACCT":
                    misList = mx2.TeamStructureALLSet.Where(x => x.Year == latestyear && (x.AccountofficerName.StartsWith(search) || x.Accountofficer_Code.StartsWith(search)))
                                .Select(x => new NameCodeModel { Name = x.AccountofficerName, Code = x.Accountofficer_Code })
                                .GroupBy(x => x.Code).Select(o => o.FirstOrDefault())
                                .OrderByDescending(x => x.Name).ToList();
                    break;

                    //case "ACCT":
                    //    misList = new SelectList(mx2.TeamStructureSet.Where(x => x.Year == latestyear).OrderBy(i => i.AccountofficerName), "Accountofficer_Code", "AccountofficerName");
                    //    break;
            }

            return Json(misList, JsonRequestBehavior.AllowGet);

            //List<string> intList1 = new List<string>() { "taiwo", "kenny", "idowu" };
            //return Json(intList1, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult MISCode(string search)
        //{
        //    var latestmonthyear = mx2.IncomeSetUpDailySet.OrderByDescending(x => x.Year).Take(1);
        //    int latestyear = latestmonthyear.Select(x => x.Year).FirstOrDefault();

        //    var teamstructureList = mx2.TeamStructureSet
        //                            .Select(x => new TeamStructure { Name = x.Name, Code = x.Code })
        //                            .Where(x => x.Year == latestyear.ToString()).OrderByDescending(i => i.Name).GroupBy(x => x.Code);

        //    return Json(teamstructureList, JsonRequestBehavior.AllowGet);

        //}

        //public ActionResult misBind(string code)
        //{
        //    var latestmonthyear = mx2.IncomeSetUpDailySet.OrderByDescending(x => x.Year).Take(1);
        //    int latestyear = latestmonthyear.Select(x => x.Year).FirstOrDefault();

        //    //SelectList stateList = new SelectList(db.States.Where(x => x.CountryId == cid).OrderByDescending(i => i.StateName), "StateId", "StateName");

        //    code = code.ToUpper().Trim();
        //        SelectList misList = null;

        //        switch (code)
        //        {
        //            case "DIV":
        //                misList = new SelectList(mx2.TeamStructureSet.Where(x=>x.Year == latestyear).OrderByDescending(i => i.DivisionName), "Division_Code", "DivisionName");
        //                break;

        //            case "GRP":
        //                misList = new SelectList(mx2.TeamStructureSet.Where(x => x.Year == latestyear).OrderByDescending(i => i.GroupName), "Group_Code", "GroupName");
        //                break;

        //            case "REG":
        //                misList = new SelectList(mx2.TeamStructureSet.Where(x => x.Year == latestyear).OrderByDescending(i => i.RegionName), "RegionName", "Region_Code");
        //                break;

        //            case "BRH":
        //                misList = new SelectList(mx2.TeamStructureSet.Where(x => x.Year == latestyear).OrderByDescending(i => i.BranchName), "Branch_Code", "BranchName");
        //                break;

        //            case "TEM":
        //                misList = new SelectList(mx2.TeamStructureSet.Where(x => x.Year == latestyear).OrderByDescending(i => i.TeamName), "Team_Code", "TeamName");
        //                break;

        //            case "ACCT":
        //                misList = new SelectList(mx2.TeamStructureSet.Where(x => x.Year == latestyear).OrderByDescending(i => i.BranchName), "Branch_Code", "BranchName");
        //                break;

        //            //case "ACCT":
        //            //    misList = new SelectList(mx2.TeamStructureSet.Where(x => x.Year == latestyear).OrderBy(i => i.AccountofficerName), "Accountofficer_Code", "AccountofficerName");
        //            //    break;
        //    }

        //    return Json(misList, JsonRequestBehavior.AllowGet);

        //    //List<string> intList1 = new List<string>() { "taiwo", "kenny", "idowu" };

        //    //return Json(intList1, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult accountofficerBind(string branchcode)
        {
            var latestmonthyear = mx2.IncomeSetUpDailySet.OrderByDescending(x => x.Year).Take(1);
            int latestyear = latestmonthyear.Select(x => x.Year).FirstOrDefault();

            branchcode = branchcode.Trim().ToUpper();

            //SelectList countryList = new SelectList(db.Countries.OrderByDescending(i => i.CountryName), "CountryId", "CountryName");
            SelectList accountofficerList = new SelectList(mx2.TeamStructureSet.Where(x=>x.Branch_Code.Trim().ToUpper() == branchcode && x.Year == latestyear).OrderByDescending(i => i.AccountofficerName).GroupBy(x => x.Accountofficer_Code), "Accountofficer_Code", "AccountofficerName");

            return Json(accountofficerList, JsonRequestBehavior.AllowGet);
        }


        protected override void Dispose(bool disposing)
        {
            mx3.Dispose();
            base.Dispose(disposing);
        }



    }
}

// List<int> IDs_of_currentSubCaptions = System.Web.HttpContext.Current.Session["IDs_of_currentSubCaptions"];
// List<int> IDs_of_currentSubCaptions = (List<int>)System.Web.HttpContext.Current.Session["IDs_of_currentSubCaptions"];

////System.Web.HttpContext.Current.Session["IDs_of_currentSubCaptions"] = IDs_of_currentSubCaptions;
//// System.Web.HttpSessionStateBase.Session["ids"] = IDs_of_currentSubCaptions;

