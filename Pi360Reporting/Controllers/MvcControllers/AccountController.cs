using MPR.Report.Data.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Pi360Reporting.Controllers.MvcControllers
{
    ////[Export]
    ////[PartCreationPolicy(CreationPolicy.NonShared)]
    ////[RoutePrefix("account")]
    //public class Account2Controller : Controller
    //{
    //    public Account2Controller()
    //    {

    //    }

    //    private MPRContext3 mx3 = new MPRContext3();
    //    private MPRContext2 mx2 = new MPRContext2();

    //    //[HttpGet]
    //    ////[Route("login/{username}/{password}")]
    //    ////public ActionResult Login(HttpRequestMessage request, string username, string password)
    //    //public ActionResult Login()
    //    //{
    //    //    return View();
    //    //}

    //    //public ActionResult Login(string username, string password)
    //    //{
    //    //    var v = "NA";


    //    //    //HttpResponseMessage res = null;

    //    //    //var userObj = mx3.CorUserSetUpSet.Where(x => x.UserSetupId == 0).FirstOrDefault();

    //    //    //     userObj = mx3.CorUserSetUpSet.Where(x => x.LoginID == username).FirstOrDefault();

    //    //    if (userValidation(username, password))
    //    //    {
    //    //        var urlBuilder = new UrlHelper(Request.RequestContext);

    //    //        var userMISObj = mx2.UserMISSet.Where(x => x.LoginID == username).FirstOrDefault();

    //    //        System.Web.HttpContext.Current.Session["session_levelcode"] = Convert.ToString(userMISObj.ProfitCenterDefinitionCode);
    //    //        //string ggg = Convert.ToString(System.Web.HttpContext.Current.Session["slevel2"]);
    //    //        System.Web.HttpContext.Current.Session["session_miscode"] = Convert.ToString(userMISObj.ProfitCenterMisCode);
    //    //        System.Web.HttpContext.Current.Session["usermisid2"] = Convert.ToString(userMISObj.UserMisId);

    //    //        string uu = Convert.ToString(System.Web.HttpContext.Current.Session["session_levelcode"]);

    //    //        //HttpContext.Current.Items.Add("sessionlevel", userMISObj.ProfitCenterDefinitionCode);
    //    //        //HttpContext.Current.Items.Add("sessionacctofficer", userMISObj.ProfitCenterMisCode);
    //    //        //HttpContext.Current.Items.Add("sessionusermis", userMISObj.UserMisId);

    //    //        //HttpContext.Current.Items["slevel"] = Convert.ToString(userMISObj.ProfitCenterDefinitionCode);
    //    //        //HttpContext.Current.Items["sMISCode"] = Convert.ToString(userMISObj.ProfitCenterMisCode);

    //    //        ////string a = Convert.ToString(HttpContext.Current.Items["slevel"]);
    //    //        ////string b = Convert.ToString(HttpContext.Current.Items["sMISCode"]);

    //    //        ////string c = Convert.ToString(HttpContext.Current.Items["sessionlevel"]);
    //    //        ////string d = Convert.ToString(HttpContext.Current.Items["sessionacctofficer"]);

    //    //        //string m = Convert.ToString(System.Web.HttpContext.Current.Session["level2"]);
    //    //        //string n = Convert.ToString(System.Web.HttpContext.Current.Session["accountofficercode2"]);
    //    //        ////System.Web.HttpSessionStateBase.Session["sslevel"] = Convert.ToString(userMISObj.ProfitCenterDefinitionCode);

    //    //        //var session = HttpContext.Current.Session;
    //    //        //if(session != null)
    //    //        //{
    //    //        //    if (session["slevel"] == null)
    //    //        //    {
    //    //        //        session["slevel"] = Convert.ToString(userMISObj.ProfitCenterMisCode);
    //    //        //    }
    //    //        //}
    //    //        //string kk = Convert.ToString(session["slevel"]);



    //    //        //v = "1";
    //    //        //res = request.CreateResponse(HttpStatusCode.OK, v);
    //    //        //return RedirectToAction("Index", "Home");

    //    //        //var url = urlBuilder.Action("staffs", "Staff");
    //    //        //var url = urlBuilder.Action("Index", "Home");
    //    //        var url = urlBuilder.Action("Index", "Home");
    //    //        //var url = urlBuilder.Action("ceo2dashboard", "home/index");
    //    //        //var url = urlBuilder.Action("ceo2dashboard", "index");
    //    //        //return Json(new { status = "success", redirectUrl = url, JsonRequestBehavior.AllowGet });
    //    //        return Json(new { v = "success", redirectUrl = url, JsonRequestBehavior.AllowGet });
    //    //    }

    //    //    else if (!userValidation(username, password))
    //    //    {
    //    //        return Json(new { v = "fail" });
    //    //        //v = "username or password not correct";
    //    //        //res = request.CreateResponse(HttpStatusCode.ExpectationFailed, v);
    //    //        //return View();
    //    //    }

    //    //    else
    //    //    {
    //    //        return Json(new { v = "NA" });
    //    //        //res = request.CreateResponse(HttpStatusCode.ExpectationFailed, v);
    //    //        //return View();
    //    //    }
    //    //}

    //    //public bool userValidation(string username, string password)
    //    //{
    //    //    //throw new NotImplementedException();
    //    //    // Check if this is a valid user.
    //    //    //using (MPRContext2 mx2 = new MPRContext2())
    //    //    using (MPRContext3 mx3 = new MPRContext3())
    //    //    {
    //    //        //var user = db.Applicants.Where(a => a.UserEmailAddress.Equals(username) && a.password.Equals(password)).SingleOrDefault();
    //    //        var user = mx3.CorUserSetUpSet.Where(a => a.LoginID.Equals(username) && password.Equals("@password")).FirstOrDefault();


    //    //        if (user != null)
    //    //        {
    //    //            // Store the user temporarily in the context for this request.
    //    //            //HttpContext.Current.Items.Add("User", user);
    //    //            return true;
    //    //        }
    //    //    }
    //    //    return false;
    //    //}


    //    //protected override void Dispose(bool disposing)
    //    //{
    //    //    mx3.Dispose();
    //    //    base.Dispose(disposing);
    //    //}


    //}
}