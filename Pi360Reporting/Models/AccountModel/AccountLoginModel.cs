using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pi360Reporting.Models.AccountModel
{
    public class AccountLoginModel
    {
        //public string CompanyCode { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        //public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }

    public class UserDetailModel
    {
        public string loginID { get; set; }
        public string MISCode { get; set; }
        public string MISLevel { get; set; }
        public string MISName { get; set; }
    }
}