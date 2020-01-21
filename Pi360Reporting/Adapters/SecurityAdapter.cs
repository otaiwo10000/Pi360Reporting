using log4net;
using MPR.Report.Data.Business;
using Pi360Reporting.Models.AccountModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace Pi360Reporting.Adapters
{
    //public class SecurityAdapter
    //{
    //}

    [Export(typeof(ISecurityAdapter))]
    //[PartCreationPolicy(CreationPolicy.NonShared)]
    public class SecurityAdapter : ISecurityAdapter
    {
        public static readonly ILog appLog = LogManager.GetLogger("File1Appender");
        private MPRContext3 mx3 = new MPRContext3();

        //public void Initialize()
        //{
        //    var securityMode = ConfigurationManager.AppSettings["SecurityMode"].ToString();

        //    if (securityMode == "UP")
        //    {
        //        if (!WebSecurity.Initialized)
        //            WebSecurity.InitializeDatabaseConnection("FintrakCoreDBConnection", "cor_usersetup", "UserSetupId", "LoginID", autoCreateTables: true);

        //        InitializeRolesAndUsers();
        //    }

        //}

        ////public bool Login(string loginID, string password, string companyCode, bool rememberMe)
        //public bool Login(string username, string password)
        //{
        //    bool success = false;
        //    bool canContinue = false;

        //    //canContinue = coreService.IsNewSystem(companyCode);
        //    //if (!canContinue)
        //    //    throw new Exception("Enter your company code to login.");

        //    //var securityMode = ConfigurationManager.AppSettings["SecurityMode"].ToString();
        //    //if (securityMode == "UP")
        //    //{
        //    //    success = WebSecurity.Login(loginID, password, persistCookie: false);
        //    //}
        //    //else
        //    //{
        //        if (Membership.ValidateUser(username, password))
        //        {
        //            FormsAuthentication.SetAuthCookie(username, false);
        //            success = true;
        //        }
        //   // }

        //    if (success)
        //    {
        //        AccountLoginModel serializeModel = new AccountLoginModel();
        //        serializeModel.CompanyCode = companyCode;

        //        JavaScriptSerializer serializer = new JavaScriptSerializer();

        //        string userData = serializer.Serialize(serializeModel);

        //        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, loginID, DateTime.Now, DateTime.Now.AddHours(10), false, userData);

        //        string encTicket = FormsAuthentication.Encrypt(authTicket);
        //        HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
        //        faCookie.Expires = false ? authTicket.Expiration : DateTime.Now.AddHours(10);
        //        HttpContext.Current.Response.Cookies.Add(faCookie);

        //        var user = coreService.GetUserSetupByLoginID(loginID);
        //        if (user != null)
        //        {
        //            user.LatestConnection = DateTime.Now;
        //            coreService.UpdateUserSetupProfile(user);

        //            //EventLogger.LogInformation(string.Format("User: {0} login successfull. - {1}", loginID, DateTime.Now.ToLongDateString()), Constants.FINTRAK_ENTERPRISE);
        //        }
        //    }
        //    else
        //    // EventLogger.LogWarning(string.Format("User: {0} login operation failed. - {1}", loginID, DateTime.Now.ToLongDateString()), Constants.FINTRAK_ENTERPRISE);
        //    if (success != true)
        //        throw new Exception("Wrong UserName or Password.");
        //    return success;
        //}



        //public void LogOut()
        //{
        //    var securityMode = ConfigurationManager.AppSettings["SecurityMode"].ToString();
        //    if (securityMode == "UP")
        //        WebSecurity.Logout();
        //    else
        //        FormsAuthentication.SignOut();
        //}


        //public bool Login(string username, string password)
        //{       
        //    if (Membership.ValidateUser(username, password))
        //    {
        //        FormsAuthentication.SetAuthCookie(username, false);

        //        return true;
        //    }

        //    return false;
        //}


        //public bool Login(string username, string password)
        //{

        //    if (userSetUpValidation(username))
        //    {
        //        appLog.InfoFormat("User setup validation for user {0} passed.", username);
        //        if (Membership.ValidateUser(username, password))
        //        {
        //            FormsAuthentication.SetAuthCookie(username, false);

        //            AccountLoginModel serializeModel = new AccountLoginModel();
        //            //serializeModel.CompanyCode = companyCode;

        //            JavaScriptSerializer serializer = new JavaScriptSerializer();

        //            string userData = serializer.Serialize(serializeModel);

        //            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, username, DateTime.Now, DateTime.Now.AddHours(10), false, userData);

        //            string encTicket = FormsAuthentication.Encrypt(authTicket);
        //            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
        //            faCookie.Expires = false ? authTicket.Expiration : DateTime.Now.AddHours(10);
        //            HttpContext.Current.Response.Cookies.Add(faCookie);

        //            appLog.InfoFormat("Membership.ValidateUser(username, password) is passed.");

        //            return true;
        //        }
        //    }
        //    return false;
        //}

        public bool Login(string username, string password)
        {
            string clientcode = Convert.ToString(ConfigurationManager.AppSettings["ClientCode"]).Trim().ToUpper();

            if (userSetUpValidation(username))
            {
                appLog.InfoFormat("User setup validation for user {0} passed.", username);
                //if (Membership.ValidateUser(username, password))

                if (clientcode == "ABP")
                {
                    appLog.InfoFormat("client code is: ABP");
                    // Get the specific provider
                    MembershipProvider domainProvider = Membership.Providers["Domain1ADMembershipProvider"];
                    appLog.InfoFormat("getting Domain1ADMembershipProvider as configured in the web.config");
                    //MembershipProvider domainProvider2 = Membership.Providers["Domain2ADMembershipProvider"];
                    // appLog.InfoFormat("getting Domain2ADMembershipProvider as configured in the web.config");
                    // validate the user
                    //if (domainProvider.ValidateUser(username, password) || domainProvider2.ValidateUser(username, password))
                    if (domainProvider.ValidateUser(username, password))
                    {
                        appLog.InfoFormat("validating the two domain providers at a time");

                        FormsAuthentication.SetAuthCookie(username, false);

                        AccountLoginModel serializeModel = new AccountLoginModel();
                        //serializeModel.CompanyCode = companyCode;

                        JavaScriptSerializer serializer = new JavaScriptSerializer();

                        string userData = serializer.Serialize(serializeModel);

                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, username, DateTime.Now, DateTime.Now.AddHours(10), false, userData);

                        string encTicket = FormsAuthentication.Encrypt(authTicket);
                        HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                        faCookie.Expires = false ? authTicket.Expiration : DateTime.Now.AddHours(10);
                        HttpContext.Current.Response.Cookies.Add(faCookie);

                        appLog.InfoFormat("Membership.ValidateUser(username, password) is passed.");


                        //////============ testing AD user detail ========================================
                        //string connection = ConfigurationManager.ConnectionStrings["TestDomain1ConnectionString"].ToString();
                        //appLog.InfoFormat("declaring connection with connection name: TestDomain1ConnectionString");

                        //System.DirectoryServices.DirectorySearcher dssearch = new System.DirectoryServices.DirectorySearcher(connection);
                        //appLog.InfoFormat("calling DirectorySearcher(x) method to pass the AD connection to the declared DirectorySearcher property: dssearch");
                        ////dssearch.Filter = username;
                        ////dssearch.Filter = "fintrakbusiness";
                        ////dssearch.Filter = "(CN=" + Session["username"].ToString() + ")";
                        ////dssearch.Filter = "(CN=MyName)";
                        ////dssearch.Filter = "(sAMAccountName=" + txtusername.Text + ")";
                        ////dssearch.Filter = "(CN=" + "fintrack" + ")";
                        //dssearch.Filter = "(sAMAccountName=" + "okwarachi" + ")";
                        //appLog.InfoFormat("passing sAMAccountName fintrack to dssearch.Filter.");

                        //System.DirectoryServices.SearchResult sresult = dssearch.FindOne();
                        //appLog.InfoFormat("calling FindOne()");
                        //System.DirectoryServices.DirectoryEntry dsresult = sresult.GetDirectoryEntry();
                        //appLog.InfoFormat("calling GetDirectoryEntry()");


                        ////string firstname = dsresult.Properties["givenName"][0].ToString();
                        //string firstname = Convert.ToString(dsresult.Properties["givenName"].Value);
                        //appLog.InfoFormat("Given Name is: {0}", firstname );
                        ////string lastname = dsresult.Properties["sn"][0].ToString();  //sn means surname
                        //string lastname = Convert.ToString(dsresult.Properties["sn"].Value);  //sn means surname
                        //appLog.InfoFormat("Surname is: {0}", lastname);
                        ////accountModel.UserSetup.Name = firstname + " " + lastname;


                        //string empid = Convert.ToString(dsresult.Properties["employeeID"].Value);
                        //appLog.InfoFormat("Employee Id is: {0}", empid);
                        //string empno = Convert.ToString(dsresult.Properties["employeeNumber"].Value);
                        //appLog.InfoFormat("Employee Number is: {0}", empno);
                        //string mail = Convert.ToString(dsresult.Properties["mail"].Value);
                        //appLog.InfoFormat("Email is: {0}", mail);
                        //////accountModel.UserSetup.StaffID = empid + "" + empno;

                        ////appLog.InfoFormat("SessionVariables for user: {0} below.", username);
                        //////============ testing AD user detail ends ===================================


                        return true;
                    }
                }

                else
                {
                    if (Membership.ValidateUser(username, password))
                    {
                        //MembershipUser userinfo = Membership.GetUser(username);
                        //appLog.InfoFormat("providername {0}", userinfo.ProviderName);
                        //appLog.InfoFormat("email {0}", userinfo.Email);
                        //appLog.InfoFormat("username {0}", userinfo.UserName);
                        //appLog.InfoFormat("ProviderUserKey {0}", userinfo.ProviderUserKey);
                      

                        FormsAuthentication.SetAuthCookie(username, false);

                        AccountLoginModel serializeModel = new AccountLoginModel();
                        //serializeModel.CompanyCode = companyCode;

                        JavaScriptSerializer serializer = new JavaScriptSerializer();

                        string userData = serializer.Serialize(serializeModel);

                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, username, DateTime.Now, DateTime.Now.AddHours(10), false, userData);

                        string encTicket = FormsAuthentication.Encrypt(authTicket);
                        HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                        faCookie.Expires = false ? authTicket.Expiration : DateTime.Now.AddHours(10);
                        HttpContext.Current.Response.Cookies.Add(faCookie);

                        appLog.InfoFormat("domainProvider.ValidateUser(username, password) is passed.");

                        return true;
                    }
                }
            }
            return false;
        }

        public bool userSetUpValidation(string username)
        {
            using (MPRContext3 mx3 = new MPRContext3())
            {
                //var user = db.Applicants.Where(a => a.UserEmailAddress.Equals(username) && a.password.Equals(password)).SingleOrDefault();
                var user = mx3.CorUserSetUpSet.Where(a => a.LoginID.Trim().ToUpper().Equals(username.ToUpper().Trim())).FirstOrDefault();

                if (user != null)
                {
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

        public UserDetailModel UserDetailMtd(string username)
        {
            string clientcode = Convert.ToString(ConfigurationManager.AppSettings["ClientCode"]).Trim().ToUpper();

            //string connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

            string connectionString = "";
            if (clientcode == "ABP")
            {
                 connectionString = ConfigurationManager.ConnectionStrings["FintrakDB2ndConnection"].ConnectionString;
            }
            else
            {
                connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;
            }

            var pts = new UserDetailModel();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("mpr_get_user_details", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "UserName",
                    Value = username,
                });

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
             
                while (reader.Read())
                {
                    //pts.loginID = reader["LoginId"] != DBNull.Value ? reader["LoginId"].ToString() : "null";
                    //pts.MISCode = reader["MisCode"] != DBNull.Value ? reader["MisCode"].ToString() : "";
                    //pts.MISLevel = reader["MisLevel"] != DBNull.Value ? reader["MisLevel"].ToString() : "";
                    //pts.MISName = reader["MisName"] != DBNull.Value ? reader["MisName"].ToString() : "";

                    System.Web.HttpContext.Current.Session["session_loggedinID"] = reader["LoginId"] != DBNull.Value ? reader["LoginId"].ToString() : "null";
                    System.Web.HttpContext.Current.Session["session_loggedinmiscode"] = reader["MisCode"] != DBNull.Value ? reader["MisCode"].ToString() : "";
                    System.Web.HttpContext.Current.Session["session_loggedinmislevel"] = reader["MisLevel"] != DBNull.Value ? reader["MisLevel"].ToString() : "";
                    System.Web.HttpContext.Current.Session["session_loggedinmisname"] = reader["MisName"] != DBNull.Value ? reader["MisName"].ToString() : "";
                }
                con.Close();
            }
            return pts;
        }      

    }

    ////--------Authorization starts --------------------------

    ////public class CustomAuthorizeAttribute2 : System.Web.Mvc.AuthorizeAttribute
    ////{       
    ////    public override void OnAuthorization(System.Web.Mvc.AuthorizationContext filterContext)
    ////    {
    ////        //string lcode = System.Web.HttpContext.Current.Session["session_levelcode"].ToString();
    ////        string lcode = "";

    ////        //if (String.IsNullOrEmpty(lcode))
    ////        if (lcode=="")
    ////        {
    ////            filterContext.Result = new System.Web.Mvc.RedirectToRouteResult(new
    ////                 System.Web.Routing.RouteValueDictionary(new { controller = "Error", action = "AccessDenied by role" }));
    ////            // base.OnAuthorization(filterContext); //returns to login url

    ////        }               
    ////    }
    ////}

    ////public class MyCustomFilter : System.Web.Http.Filters.FilterAttribute
    ////{
    ////    public void OnAuthorization(System.Web.Mvc.AuthorizationContext filterContext)
    ////    {
    ////        string lcode = "hh";
    ////        if (lcode == "hh")
    ////        {
    ////            filterContext.Result = new System.Web.Mvc.RedirectToRouteResult(new
    ////                    System.Web.Routing.RouteValueDictionary(new { controller = "Error", action = "AccessDenied by Access Right" }));
    ////        }
    ////    }
    ////}

    ////public class AuthorizationPrivilegeFilter : System.Web.Mvc.ActionFilterAttribute
    ////{
    ////    public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
    ////    {
    ////            var lcode = "";
    ////            if (lcode == "")
    ////            {
    ////                filterContext.Result = new System.Web.Mvc.RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary{{ "controller", "Account" },
    ////                                      { "action", "Login" }

    ////                                     });
    ////            }
    ////        base.OnActionExecuting(filterContext);
    ////    }
    ////}

    
    //public class AuthenticationFilter : System.Web.Http.Filters.AuthorizationFilterAttribute
    //{
    //    // public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext filterContext)
    //    public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext filterContext)
    //    {       
    //            string t = "ss";
    //        if (t == "ss")
    //        {
    //            //filterContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);

    //            //base.OnAuthorization(Unauthorized);

    //            filterContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);

    //            //throw new System.Security.SecurityException("Access Denied");
    //        }
    //        //base.OnAuthorization(Unauthorized);
    //    }
    //}

    //public class RedirectAttribute : System.Web.Http.Filters.ActionFilterAttribute
    //{
    //    public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
    //    {
    //        var response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Redirect);
    //        response.Headers.Location = new Uri("https://www.stackoverflow.com");
    //        actionContext.Response = response;
    //    }
    //}

    //public class CustomActionFilter : System.Web.Http.Filters.ActionFilterAttribute, System.Web.Http.Filters.IActionFilter
    //{
    //    public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext filterContext)
    //    {

    //        string checking = Convert.ToString(System.Web.HttpContext.Current.Session["usermisid2"]);

    //        //if (checking == "" || checking == null)
    //        if (string.IsNullOrEmpty(checking))
    //        {
    //            System.Web.Security.FormsAuthentication.SignOut();  // NOTE: "System.Web.Security" is used to call FormsAuthentication.SignOut().
    //            System.Web.HttpContext.Current.Session.Abandon(); // it clears the session at the end of request.
    //                                                              //Session.RemoveAll();
    //            System.Web.HttpContext.Current.Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", "")); //This line create a new cookie. The 1st var is the name of the new cookie. the 2nd var is the value of the new cookie

    //            //also check the "Global.asax.cs" for clearing of browser cache.

    //            //return RedirectToAction("Login", "Account");
    //            ////filterContext.Result = new System.Web.Mvc.RedirectToRouteResult(new
    //            // //  System.Web.Routing.RouteValueDictionary(new { controller = "Account", action = "Login" }));

    //            //string relativePath = "/account/login";
    //            //Uri relativeUri = new Uri(relativePath, UriKind.Relative);

                

    //            var response = filterContext.Request.CreateResponse(System.Net.HttpStatusCode.Redirect);
    //            response.Headers.Location = new Uri("http://localhost:43056/access/login");
    //            //response.Headers.Location = relativeUri;
    //            filterContext.Response = response;
    //        }
    //        base.OnActionExecuting(filterContext);
    //    }
    //}

   

    ////--------Authorization ends --------------------------
}