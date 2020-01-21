using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPR.Report.Core
{
    public class ratio
    {
        public double Amount { get; set; }
        public double Budget { get; set; }
    }

    public class subcaptions
    {
        public string SubCaption { get; set; }
        public double Budget { get; set; }
        public double Amount { get; set; }
        public int Period { get; set; }
    }

    public class customerCRM
    {
        public string Name { get; set; }
        public string AccountNo { get; set; }
        public double Amount { get; set; }      
    }

}