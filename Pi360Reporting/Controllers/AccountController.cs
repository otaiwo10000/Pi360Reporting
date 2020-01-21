using log4net;
using MPR.Report.Core.Entities;
using MPR.Report.Data.Business;
using Pi360Reporting.Adapters;
using Pi360Reporting.Models.AccountModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace Pi360Reporting.Controllers
{
    public class AccountController : Controller
    {
        public static readonly ILog appLog = LogManager.GetLogger("File1Appender");

        //[System.ComponentModel.Composition.ImportingConstructor]
        //public AccountController(Adapters.ISecurityAdapter securityAdapter)
        //{
        //    _SecurityAdapter = securityAdapter;
        //}

        Adapters.SecurityAdapter _SecurityAdapter = new Adapters.SecurityAdapter();

        SecurityAdapter securityobj = new SecurityAdapter();


        private MPRContext3 mx3 = new MPRContext3();
        private MPRContext2 mx2 = new MPRContext2();
        //private MPRContext mx = new MPRContext();

       
        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {           
            var v = "NA";

            try
            {
                var securityMode = ConfigurationManager.AppSettings["SecurityMode"].ToString().ToUpper();

                if (securityMode == ("UP"))
                {
                    if (userValidation(username, password))
                    {
                        appLog.InfoFormat("uservalidation for user: {0} is passed", username);
                        //if (userReportAppValidation(username))
                        if (Convert.ToBoolean(System.Web.HttpContext.Current.Session["session_isreportuser"]) == true)
                        {
                            appLog.InfoFormat("the user: {0} is a report user", username);
                            if (userMISValidation(username))
                            {
                                appLog.InfoFormat("userMISValidation(username) method is passed");

                                SessionVariablesMethod(username);

                                appLog.InfoFormat("SessionVariablesMethod(username) method is passed");

                                var urlBuilder = new UrlHelper(Request.RequestContext);
                                var url = urlBuilder.Action("Index", "Home");
                                //return Json(new { status = "success", redirectUrl = url, JsonRequestBehavior.AllowGet });
                                appLog.InfoFormat("{0}{1}{2}", "user: ", username, " successfully logged on");

                                return Json(new { v = "success", redirectUrl = url, JsonRequestBehavior.AllowGet });
                            }
                            else //if (!userMISValidation(username))
                            {
                                appLog.InfoFormat("{0}{1}{2}", "user: ", username, " is not mapped to UserMIS on Pi360 app");

                                return Json(new { v = "notusermis", JsonRequestBehavior.AllowGet });
                            }
                        }
                        else //if (!userReportAppValidation(username))
                        {
                            appLog.InfoFormat("{0}{1}{2}", "user: ", username, " is not a report user.");

                            return Json(new { v = "notreportuser", JsonRequestBehavior.AllowGet });
                        }
                    }

                    else //if (!userValidation(username, password))
                    {
                        SecurityAdapter securityobj = new SecurityAdapter();
                        if (!securityobj.userSetUpValidation(username))
                        {
                            appLog.InfoFormat("{0}{1}{2}", "user: ", username, " is not set up on Pi360 app.");

                            return Json(new { v = "notOnPi360", JsonRequestBehavior.AllowGet });
                        }
                        else
                        {
                            appLog.InfoFormat("{0}{1}{2}", "user: ", username, " failed logged in");
                            //return Json(app, JsonRequestBehavior.AllowGet);
                            return Json(new { v = "fail", JsonRequestBehavior.AllowGet });
                        }                      
                    }                    
                } // end of 1st if

                //------------ for Active Directory Authentication ----------------------------
                else
                {
                    if (_SecurityAdapter.Login(username, password))
                    {
                        appLog.InfoFormat("_SecurityAdapter.Login(username, password) method is passed for user: {0}", username);
                        //if (userReportAppValidation(username))
                        if (Convert.ToBoolean(System.Web.HttpContext.Current.Session["session_isreportuser"]) == true)
                        {
                            appLog.InfoFormat("the user: {0} is a report user", username);
                            if (userMISValidation(username))
                            {
                                appLog.InfoFormat("userMISValidation(username) method is passed");

                                SessionVariablesMethod(username);

                                appLog.InfoFormat("SessionVariablesMethod(username) method is passed");

                                var urlBuilder = new UrlHelper(Request.RequestContext);
                                var url = urlBuilder.Action("Index", "Home");

                                //return Json(new { status = "success", redirectUrl = url, JsonRequestBehavior.AllowGet });
                                appLog.InfoFormat("{0}{1}{2}", "user: ", username, " successfully logged on");

                                return Json(new { v = "success", redirectUrl = url, JsonRequestBehavior.AllowGet });
                                //return RedirectToAction("Index", "Home");
                            }
                            else //if (!userMISValidation(username))
                            {
                                appLog.InfoFormat("{0}{1}{2}", "user: ", username, " is not mapped to UserMIS on Pi360 app");

                                return Json(new { v = "notusermis", JsonRequestBehavior.AllowGet });
                            }
                        }
                        else //if (!userReportAppValidation(username))
                        {
                            appLog.InfoFormat("{0}{1}{2}", "user: ", username, " is not a report user.");

                            return Json(new { v = "notreportuser", JsonRequestBehavior.AllowGet });
                        }                                                                   
                    }
                    else //if (!_SecurityAdapter.Login(username, password))
                    {
                        //SecurityAdapter securityobj = new SecurityAdapter();
                        if (!securityobj.userSetUpValidation(username))
                        {
                            appLog.InfoFormat("{0}{1}{2}", "user: ", username, " is not set up on Pi360 app.");

                            return Json(new { v = "notOnPi360", JsonRequestBehavior.AllowGet });
                        }
                        else
                        {
                            appLog.InfoFormat("{0}{1}{2}", "user: ", username, " failed logged in");
                            //return Json(app, JsonRequestBehavior.AllowGet);
                            return Json(new { v = "fail", JsonRequestBehavior.AllowGet });
                        }
                    }                   
                } //end of else

            }  //end of try

            
            catch (Exception ex)
            {
                //Service1.job1Log.Info(string.Format("{0}{1}{2}", rowAffected, " ", "rows affected."));
                appLog.InfoFormat("{0}{1}", "Message exception: ", ex.Message);
                appLog.InfoFormat("{0}{1}", "InnerException exception: ", ex.InnerException.Message);
                //appLog.InfoFormat("{0}{1}", "Stack Trace: ", ex.StackTrace);
            }

            //finally
            //{
            //    Dispose(true);
            //}

            return View();
        }


        public bool userValidation(string username, string password)
        {
            using (MPRContext3 mx3 = new MPRContext3())
            {
                //var user = db.Applicants.Where(a => a.UserEmailAddress.Equals(username) && a.password.Equals(password)).SingleOrDefault();
                var user = mx3.CorUserSetUpSet.Where(a => a.LoginID.Trim().ToUpper().Equals(username.ToUpper()) && password.Equals("@password")).FirstOrDefault();

                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(username, false);

                    AccountLoginModel serializeModel = new AccountLoginModel();
                    //serializeModel.CompanyCode = companyCode;

                    JavaScriptSerializer serializer = new JavaScriptSerializer();

                    string userData = serializer.Serialize(serializeModel);

                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, username, DateTime.Now, DateTime.Now.AddHours(10), false, userData);

                    string encTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    faCookie.Expires = false ? authTicket.Expiration : DateTime.Now.AddHours(10);
                    System.Web.HttpContext.Current.Response.Cookies.Add(faCookie);
                    
                    System.Web.HttpContext.Current.Session["session_userfullname"] = user.Name;
                    System.Web.HttpContext.Current.Session["session_loggedinUser"] = user.Name;
                    System.Web.HttpContext.Current.Session["session_userstaffid"] = user.StaffID;
                    System.Web.HttpContext.Current.Session["session_photourlpath"] = user.PhotoUrl;
                    System.Web.HttpContext.Current.Session["session_isreportuser"] = user.IsReportUser;

                    return true;
                }
            }
            return false;
        }

        //////public bool userSetUpValidation(string username)
        //////{
        //////    using (MPRContext3 mx3 = new MPRContext3())
        //////    {
        //////        //var user = db.Applicants.Where(a => a.UserEmailAddress.Equals(username) && a.password.Equals(password)).SingleOrDefault();
        //////        var user = mx3.CorUserSetUpSet.Where(a => a.LoginID.Equals(username)).FirstOrDefault();

        //////        if (user != null)
        //////        {
        //////            System.Web.HttpContext.Current.Session["userfullname"] = user.Name;
        //////            System.Web.HttpContext.Current.Session["userstaffid"] = user.StaffID;
        //////            System.Web.HttpContext.Current.Session["photourlpath"] = user.PhotoUrl;

        //////            return true;
        //////        }
        //////    }
        //////    return false;
        //////}

        public bool userMISValidation(string username)
        {
            using (MPRContext2 mx2 = new MPRContext2())
            {
                string clientcode = Convert.ToString(ConfigurationManager.AppSettings["ClientCode"]).Trim().ToUpper();
                ////var user = db.Applicants.Where(a => a.UserEmailAddress.Equals(username) && a.password.Equals(password)).SingleOrDefault();
                ////var user = mx2.UserMISSet.Where(a => a.LoginID.Equals(username)).FirstOrDefault();

                //var userMISObj = mx2.UserMISSet.Where(x => x.LoginID.Trim().ToUpper() == username.ToUpper()).FirstOrDefault();

                UserMIS userMISObj = null;

                if (clientcode == "ABP")
                {
                    using (MPRContextSecondFintrakDB dbObj = new MPRContextSecondFintrakDB())
                    {
                        userMISObj = dbObj.UserMISSet.Where(x => x.LoginID.Trim().ToUpper() == username.ToUpper()).FirstOrDefault();
                    }
                }
                else
                {
                    userMISObj = mx2.UserMISSet.Where(x => x.LoginID.Trim().ToUpper() == username.ToUpper()).FirstOrDefault();
                }
                appLog.InfoFormat("{0}{1}", "userMISObj gotten from the 'userMISValidation(string username) method' using loginId: ", username);                

                if (userMISObj != null)
                {
                    System.Web.HttpContext.Current.Session["ProfitCenterDefinitionCode"] = userMISObj.ProfitCenterDefinitionCode;
                    System.Web.HttpContext.Current.Session["ProfitCenterMisCode"] = userMISObj.ProfitCenterMisCode;
                    System.Web.HttpContext.Current.Session["UserMisId"] = userMISObj.UserMisId;

                    return true;
                }
            }
            return false;
        }

        //public bool userReportAppValidation(string username)
        //{
        //    using (MPRContext3 mx3 = new MPRContext3())
        //    {
        //        //var user = db.Applicants.Where(a => a.UserEmailAddress.Equals(username) && a.password.Equals(password)).SingleOrDefault();
        //        var user = mx3.CorUserSetUpSet.Where(a => a.IsReportUser == true && a.LoginID.Equals(username.Trim())).FirstOrDefault();

        //        if (user != null)
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}
        public void SessionVariablesMethod(string username)
        {
            appLog.InfoFormat("SessionVariables for user: {0} below.", username);
            //int latestyear = mx2.TeamStructureSet.Max(x => x.Year);
            //appLog.InfoFormat("{0}{1}", "latestyear gotten: ", latestyear);

            var latestmonthyear = mx2.IncomeSetUpDailySet.OrderByDescending(x => x.Year).Take(1);
                     
            int latestyear = Convert.ToInt32(latestmonthyear.Max(x=>x.Year));
            System.Web.HttpContext.Current.Session["latestyear"] = latestyear;
            appLog.InfoFormat("{0}{1}", "latestyear gotten: ", latestyear);

            int currentperiod = Convert.ToInt32(latestmonthyear.Max(x => x.CurrentPeriod));
            System.Web.HttpContext.Current.Session["currentperiod"] = currentperiod;
            appLog.InfoFormat("{0}{1}", "currentperiod gotten: ", currentperiod);

            //List<TeamDefinition> teamDefinition = mx2.TeamDefinitionSet.Where(x => x.Year == latestyear.ToString()).GroupBy(x => x.Code).FirstOrDefault().ToList();
            //var teamdefinition = mx2.TeamDefinitionSet.Where(x => x.Year == latestyear.ToString()).ToList();
            //appLog.InfoFormat("{0}{1}", "teamDefinition list gotten using latest year: ", latestyear);

            ////var userMISObj = mx2.UserMISSet.Where(x => x.LoginID == username).FirstOrDefault();
            ////appLog.InfoFormat("{0}{1}", "userMISObj gotten using loginId: ", username);

            ////int level = teamDefinition.Where(x => x.Code == userMISObj.ProfitCenterDefinitionCode).Select(x => x.Level).FirstOrDefault();
            //int level = teamDefinition.Where(x => x.Code == System.Web.HttpContext.Current.Session["ProfitCenterDefinitionCode"].ToString()).Select(x => x.Level).FirstOrDefault(); //gotten from usermis ProfitCenterDefinitionCode.
            //appLog.InfoFormat("{0}{1}", "level gotten: ", level);

            var lcode = Convert.ToString(System.Web.HttpContext.Current.Session["ProfitCenterDefinitionCode"]);
            //NOTE: The variable below is intentionally defined as string to confirm if the output below is empty otherwise, if it is defined as 0 and the output returned o, it causes a great issue bcos BNK is already set to 0 in the team_definition table. it causes all users to take on level = 0. so, i decided to check the output first. 

            //int level = mx2.TeamDefinitionSet.Where(x => x.Year == latestyear.ToString() && x.Code == lcode).Select(x => x.Level).FirstOrDefault();
            //string level_string_forChecking = mx2.TeamDefinitionSet.Where(x => x.Year == latestyear.ToString() && x.Code.Trim().ToUpper() == lcode.Trim().ToUpper()).Select(x => x.Level).FirstOrDefault().ToString();
            var ob = mx2.TeamDefinitionSet.Where(x => x.Year == latestyear.ToString() && x.Code.Trim().ToUpper() == lcode.Trim().ToUpper()).FirstOrDefault();
            //string level_string_forChecking = "";

            int level = -1;
            //if (string.IsNullOrEmpty(level_string_forChecking))
            //{
            //    level = level;
            //}
            //else
            //{
            //    level = Convert.ToInt32(level_string_forChecking);
            //}

            if(ob == null)
            {
                level = level;
            }
            else
            {
                level = ob.Level;
            }

            appLog.InfoFormat("level {0} is gotten", level);

            System.Web.HttpContext.Current.Session["session_level"] = Convert.ToString(level);
            //var a1 = System.Web.HttpContext.Current.Session["session_level"].ToString();
            appLog.InfoFormat("session_level gotten");

            //System.Web.HttpContext.Current.Session["session_levelcode"] = Convert.ToString(userMISObj.ProfitCenterDefinitionCode);
            System.Web.HttpContext.Current.Session["session_levelcode"] = System.Web.HttpContext.Current.Session["ProfitCenterDefinitionCode"];
            //var a2 = System.Web.HttpContext.Current.Session["session_levelcode"].ToString();
            appLog.InfoFormat("session_levelcode gotten");

            //System.Web.HttpContext.Current.Session["session_miscode"] = Convert.ToString(userMISObj.ProfitCenterMisCode);
            System.Web.HttpContext.Current.Session["session_miscode"] = System.Web.HttpContext.Current.Session["ProfitCenterMisCode"];
            //var a3 = System.Web.HttpContext.Current.Session["session_miscode"].ToString();
            appLog.InfoFormat("session_miscode gotten");

            //System.Web.HttpContext.Current.Session["usermisid2"] = Convert.ToString(userMISObj.UserMisId);
            System.Web.HttpContext.Current.Session["usermisid2"] = System.Web.HttpContext.Current.Session["UserMisId"];
            appLog.InfoFormat("usermisid2 gotten");


            ////////System.Web.HttpContext.Current.Session["session_initiallyselectedmiscode"] = Convert.ToString(userMISObj.ProfitCenterMisCode);

            securityobj.UserDetailMtd(username);

            //    MPR.Report.Data.Business.DataRepositories.Mpr_FintrakMenuRepository menuObj = new MPR.Report.Data.Business.DataRepositories.Mpr_FintrakMenuRepository();
            //menuObj.Getmpr_FintrakMenuList();
            //appLog.InfoFormat("Getmpr_FintrakMenuList() passed");
        }

        public ActionResult LogOut()
        {
            //System.Web.Security.FormsAuthentication.SignOut();  // NOTE: "System.Web.Security" is used to call FormsAuthentication.SignOut().
            //System.Web.HttpContext.Current.Session.Abandon(); // it clears the session at the end of request.
            ////Session.RemoveAll();
            //System.Web.HttpContext.Current.Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", "")); //This line create a new cookie. The 1st var is the name of the new cookie. the 2nd var is the value of the new cookie

            System.Web.HttpContext.Current.Session.Clear();
            System.Web.HttpContext.Current.Session.Abandon(); // it clears the session at the end of request.
            System.Web.Security.FormsAuthentication.SignOut();  // NOTE: "System.Web.Security" is used to call FormsAuthentication.SignOut().

        //"ASP.Net_SessionId" is a cookie which is used to identify the users session on the server.The session being an area on the server which can be used to store data in between http requests. ...ASPXAUTH is a cookie to identify if the user is authenticated (that is, has their identity been verified).
        //also check the "Global.asax.cs" for clearing of browser cache.

        //SessionID: The SessionID property is used to uniquely identify a browser with session data on the server. The SessionID value is randomly generated by ASP.NET and stored in a non-expiring session cookie in the browser. The SessionID value is then sent in a cookie with each request to the ASP.NET application.

            return RedirectToAction("Login", "Account");
        }

        //public ActionResult CustomSessionTimeOut()
        //{
        //    string sessionstatus = 
        //}

        //public ActionResult CustomSessionTimeOut()
        //{
        //    //string sv = System.Web.HttpContext.Current.Session["session_loggedinID"].ToString();
        //    string ret = "0";
        //    string test = Convert.ToString(System.Web.HttpContext.Current.Session["session_loggedinID"]);

        //    if (string.IsNullOrEmpty(Convert.ToString(System.Web.HttpContext.Current.Session["session_loggedinID"])))
        //    //if (ret=="0")
        //    {
        //        System.Web.HttpContext.Current.Session.Clear();
        //        System.Web.HttpContext.Current.Session.Abandon(); // it clears the session at the end of request.
        //        System.Web.Security.FormsAuthentication.SignOut();  // NOTE: "System.Web.Security" is used to call FormsAuthentication.SignOut().

        //        //System.Web.HttpContext.Current.Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", "")); //This line create a new cookie. The 1st var is the name of the new cookie. the 2nd var is the value of the new cookie

        //        ////var urlBuilder = new UrlHelper(Request.RequestContext);
        //        ////var url = urlBuilder.Action("Login", "Account");
        //        ////return Json(new { ret = "1", redirectUrl = url, JsonRequestBehavior.AllowGet });


        //        return RedirectToAction("Login", "Account");
        //    }

        //   return Json(new { ret, JsonRequestBehavior.AllowGet });
        //   //return RedirectToAction("Login", "Account");
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

