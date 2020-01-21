using MPR.Report.Core.Entities;
using MPR.Report.Data.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pi360Reporting.Controllers
{
    //[OtherMethods.CustomActionFilter]
    [Authorize]
  
    public class HomeController : Controller
    {
        //public ActionResult Index()
        //{
        //    return View();
        //}
        private MPRContext3 mx3 = new MPRContext3();
        private MPRContext2 mx2 = new MPRContext2();
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        //private MPRContext mx = new MPRContext();

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        //[OtherMethods.CheckSessionTimeOutAttribute]
        [HttpGet]
        public ActionResult Index()
        {
            //ViewBag.Message = "Your application description page.";

            return View();
        }

        //public ActionResult sessionTimeOut()
        //{
        //    string session_loggedinID = Convert.ToString(System.Web.HttpContext.Current.Session["session_loggedinID"]);
        //    var url = "";
        //    var v = "1";

        //    if (session_loggedinID == "" || session_loggedinID is null)
        //    {
        //        v = "ab";
        //        var urlBuilder = new UrlHelper(Request.RequestContext);
        //        url = urlBuilder.Action("Login", "Account");
        //        System.Web.HttpContext.Current.Session.Clear();
        //        System.Web.HttpContext.Current.Session.Abandon(); // it clears the session at the end of request.
        //        System.Web.Security.FormsAuthentication.SignOut();

        //        //return Json(new { v = v, redirectUrl = url, JsonRequestBehavior.AllowGet });
        //        return RedirectToAction("Login", "Account");
        //    }

        //    //return Json(new { v = v, redirectUrl = url, JsonRequestBehavior.AllowGet });
        //    return Json(new { v = v, JsonRequestBehavior.AllowGet });
        //}

        //public ActionResult TestAutoComplete(string fname)
        //{

        //    var umis = mx2.UserMISSet.Select(x => x.LoginID).ToList();

        //    return Json(umis, JsonRequestBehavior.AllowGet);
        //}

        [HttpGet]
        public ActionResult LayoutMenus()
        {
           // ViewBag.Title = "Home Page";
           // //var preparedMenus = new List<MenuModel>();

           // var list = new List<mpr_FintrakMenu>();

           // var newList = new List<mprFinTrackMenuList>();

           // //string query = "SELECT distinct MenuList FROM mpr_FintrakMenu where MenuList is not null and MenuList  <> '' order by MenuList asc";

           // //string CS = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakReportEntities"].ConnectionString;
           // //System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(CS);

           // try
           // {
           //     //connection.Open();

           //     //var menus = mx.Database.SqlQuery<string>(query);
           //     var menus = mx.mpr_FintrakMenuSet.Where(x => x.MenuList != null || x.MenuList != "").Select(x => x.MenuList).Distinct();

           //     foreach (var item in menus)
           //     {
           //         var rp = mx.mpr_FintrakMenuSet.Where(x => x.MenuList == item).Select(y => y.ReportPAth).ToList();

           //         var dr = new mprFinTrackMenuList()
           //         {
           //             reportpath = rp,
           //             menulist = item
           //         };

           //         newList.Add(dr);
           //     }
           //     //connection.Close();
           //     //connection.Dispose();
           //     ViewBag.Menus = newList;
           //     //return View();
           //     return Json(newList, JsonRequestBehavior.AllowGet);
           // }

           // catch (Exception ex)
           // {
           //     throw ex;
           // }

           // //finally
           // //{
           // //    connection.Close();
           // //    connection.Dispose();
           // //}

           //// ViewBag.Menus = newList;
            return View();
        }


        //private MPRContext3 mx3 = new MPRContext3();

        //[HttpPost]
        //public ActionResult Login(string username, string password)
        //{
        //    var userObj = mx3.CorUserSetUpSet.Where(x => x.LoginID == username).FirstOrDefault();



        //    if (userObj.LoginID != "" && password=="@password")
        //    {
        //        var userMISObj = mx3.UserMISSet.Where(x=>x.LoginID == userObj.LoginID).FirstOrDefault();

        //        Session["level"] = userMISObj.ProfitCenterDefinitionCode;
        //        Session["accountofficercode"] = userMISObj.ProfitCenterMisCode;
        //        Session["usermisid"] = userMISObj.UserMisId;

        //        return RedirectToAction("Home", "Index");
        //    }

        //    else if (userObj.LoginID != "" && password != "@password")
        //    {
        //        ViewBag.res = "username or password not correct";
        //    }

        //    else
        //    {
        //        ViewBag.res = "Something goes wrong";
        //    }

        //    return View();
        //}


        //[HttpGet]
        //public ActionResult LogIn()
        //{
        //    return View();
        //}
        //public ActionResult Login(string username, string password)
        //{
        //    var v = "NA";


        //    //HttpResponseMessage res = null;

        //    //var userObj = mx3.CorUserSetUpSet.Where(x => x.UserSetupId == 0).FirstOrDefault();

        //    //     userObj = mx3.CorUserSetUpSet.Where(x => x.LoginID == username).FirstOrDefault();

        //    if (userValidation(username, password))
        //    {
        //        var urlBuilder = new UrlHelper(Request.RequestContext);

        //        var userMISObj = mx2.UserMISSet.Where(x => x.LoginID == username).FirstOrDefault();

        //        System.Web.HttpContext.Current.Session["session_levelcode"] = Convert.ToString(userMISObj.ProfitCenterDefinitionCode);
        //        //string ggg = Convert.ToString(System.Web.HttpContext.Current.Session["slevel2"]);
        //        System.Web.HttpContext.Current.Session["session_miscode"] = Convert.ToString(userMISObj.ProfitCenterMisCode);
        //        System.Web.HttpContext.Current.Session["usermisid2"] = Convert.ToString(userMISObj.UserMisId);

        //        string uu = Convert.ToString(System.Web.HttpContext.Current.Session["session_levelcode"]);

        //        //HttpContext.Current.Items.Add("sessionlevel", userMISObj.ProfitCenterDefinitionCode);
        //        //HttpContext.Current.Items.Add("sessionacctofficer", userMISObj.ProfitCenterMisCode);
        //        //HttpContext.Current.Items.Add("sessionusermis", userMISObj.UserMisId);

        //        //HttpContext.Current.Items["slevel"] = Convert.ToString(userMISObj.ProfitCenterDefinitionCode);
        //        //HttpContext.Current.Items["sMISCode"] = Convert.ToString(userMISObj.ProfitCenterMisCode);

        //        ////string a = Convert.ToString(HttpContext.Current.Items["slevel"]);
        //        ////string b = Convert.ToString(HttpContext.Current.Items["sMISCode"]);

        //        ////string c = Convert.ToString(HttpContext.Current.Items["sessionlevel"]);
        //        ////string d = Convert.ToString(HttpContext.Current.Items["sessionacctofficer"]);

        //        //string m = Convert.ToString(System.Web.HttpContext.Current.Session["level2"]);
        //        //string n = Convert.ToString(System.Web.HttpContext.Current.Session["accountofficercode2"]);
        //        ////System.Web.HttpSessionStateBase.Session["sslevel"] = Convert.ToString(userMISObj.ProfitCenterDefinitionCode);

        //        //var session = HttpContext.Current.Session;
        //        //if(session != null)
        //        //{
        //        //    if (session["slevel"] == null)
        //        //    {
        //        //        session["slevel"] = Convert.ToString(userMISObj.ProfitCenterMisCode);
        //        //    }
        //        //}
        //        //string kk = Convert.ToString(session["slevel"]);



        //        //v = "1";
        //        //res = request.CreateResponse(HttpStatusCode.OK, v);
        //        return RedirectToAction("Index", "Home");

        //        //var url = urlBuilder.Action("staffs", "Staff");
        //        //var url = urlBuilder.Action("Index", "Home");
        //        //var url = urlBuilder.Action("Index", "Home");
        //        //var url = urlBuilder.Action("ceo2dashboard", "home/index");
        //        //var url = urlBuilder.Action("ceo2dashboard", "index");
        //        //return Json(new { status = "success", redirectUrl = url, JsonRequestBehavior.AllowGet });
        //       // return Json(new { v = "success", redirectUrl = url, JsonRequestBehavior.AllowGet });
        //    }

        //    else if (!userValidation(username, password))
        //    {
        //        return Json(new { v = "fail" });
        //        //v = "username or password not correct";
        //        //res = request.CreateResponse(HttpStatusCode.ExpectationFailed, v);
        //        //return View();
        //    }

        //    else
        //    {
        //        return Json(new { v = "NA" });
        //        //res = request.CreateResponse(HttpStatusCode.ExpectationFailed, v);
        //        //return View();
        //    }
        //}

        //public bool userValidation(string username, string password)
        //{
        //    //throw new NotImplementedException();
        //    // Check if this is a valid user.
        //    //using (MPRContext2 mx2 = new MPRContext2())
        //    using (MPRContext3 mx3 = new MPRContext3())
        //    {
        //        //var user = db.Applicants.Where(a => a.UserEmailAddress.Equals(username) && a.password.Equals(password)).SingleOrDefault();
        //        var user = mx3.CorUserSetUpSet.Where(a => a.LoginID.Equals(username) && password.Equals("@password")).FirstOrDefault();


        //        if (user != null)
        //        {
        //            // Store the user temporarily in the context for this request.
        //            //HttpContext.Current.Items.Add("User", user);
        //            return true;
        //        }
        //    }
        //    return false;
        //}


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

