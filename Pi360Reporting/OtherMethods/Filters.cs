using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;


namespace Pi360Reporting.OtherMethods
{ 
    public class CustomActionFilter : System.Web.Mvc.ActionFilterAttribute, System.Web.Mvc.IActionFilter
    {
        public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {

            string checking = Convert.ToString(System.Web.HttpContext.Current.Session["usermisid2"]);

            //if (checking == "" || checking == null)
            if (string.IsNullOrEmpty(checking))
            {
                System.Web.Security.FormsAuthentication.SignOut();  // NOTE: "System.Web.Security" is used to call FormsAuthentication.SignOut().
                System.Web.HttpContext.Current.Session.Abandon(); // it clears the session at the end of request.
                                                                  //Session.RemoveAll();
                System.Web.HttpContext.Current.Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", "")); //This line create a new cookie. The 1st var is the name of the new cookie. the 2nd var is the value of the new cookie

                //also check the "Global.asax.cs" for clearing of browser cache.

                //return RedirectToAction("Login", "Account");
                filterContext.Result = new System.Web.Mvc.RedirectToRouteResult(new
                  System.Web.Routing.RouteValueDictionary(new { controller = "Account", action = "Login" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class CheckSessionTimeOutAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            var context = filterContext.HttpContext;
            if (context.Session != null)
            {
                if (context.Session.IsNewSession)
                {
                    string sessionCookie = context.Request.Headers["Cookie"];
                    if ((sessionCookie != null) && (sessionCookie.IndexOf("ASP.NET&#95;SessionId") >= 0))
                    {
                        System.Web.Security.FormsAuthentication.SignOut();
                        string redirectTo = "~/Account/Login";
                        if (!string.IsNullOrEmpty(context.Request.RawUrl))
                        {
                            redirectTo = string.Format("~/Account/Login?ReturnUrl={0}", HttpUtility.UrlEncode(context.Request.RawUrl));
                        }
                        filterContext.HttpContext.Response.Redirect(redirectTo, true);
                    }
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }

    public class AccessMth: System.Web.Http.AuthorizeAttribute
    {      
        protected override bool IsAuthorized(HttpActionContext context)
        {
            int i = 4;
            // if ( /*check if user OK or not*/)
            if (i < 3)
            {
                return true;// or false
            }
            return false;
        }
    }

    ////public class AllowOnlyCertainUsers2 : System.Web.Http.Filters.AuthorizationFilterAttribute
    ////{
    ////    protected override bool OnAuthorization(HttpActionContext context)
    ////    {
    ////        // if ( /*check if user OK or not*/)
    ////        if (2 > 3)
    ////        {
    ////            return true;// or false
    ////        }
    ////        return true;
    ////    }
    ////}

}