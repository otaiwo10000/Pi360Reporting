using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPR.Report.Core;
using MPR.Report.Core.Entities;
using MPR.Report.Core.Interfaces;
using MPR.Report.Core.IRepositoryInterfaces;

namespace MPR.Report.Data.Business.DataRepositories
{
   
    public class Mpr_FintrakMenuRepository : IDataRepository
    {
        //MPRContext context = new MPRContext();
        MPRContext2 context2 = new MPRContext2();
        public void Add(mpr_FintrakMenu b)
        {
            context2.mpr_FintrakMenuSet.Add(b);
            context2.SaveChanges();
        }

        public void Edit(mpr_FintrakMenu b)
        {
            context2.Entry(b).State = System.Data.Entity.EntityState.Modified;
        }

        public mpr_FintrakMenu FindById(int Id)
        {
            var result = (from r in context2.mpr_FintrakMenuSet where r.ID == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetMpr_FintrakMenus() { return context2.mpr_FintrakMenuSet; }
        public void Remove(int Id) { mpr_FintrakMenu p = context2.mpr_FintrakMenuSet.Find(Id); context2.mpr_FintrakMenuSet.Remove(p); context2.SaveChanges(); }

        ////public IEnumerable<MPR.Report.Data.Business.mprFintrakMenu_ObjectListInfo> Getmpr_FintrakMenuList()
        //public IEnumerable<mpr_FintrakMenu> Getmpr_FintrakMenuList()
        //{
        //    var menus = context.mpr_FintrakMenuSet;
        //    //var viewrights = menus.Select(x => x.ViewRights);
        //    var teamdefcode = Convert.ToString(System.Web.HttpContext.Current.Session["session_levelcode"]);
        //    //var menubylevel = menus

        //     var menu2 = menus.Where(a => a.ViewRights.Contains(teamdefcode));

        //    return menu2.ToList();
        //}


        public IEnumerable<MPR.Report.Core.mprFintrakMenu_ObjectListInfo> Getmpr_FintrakMenuList()
        {
            string mprvar = "mpr";

            var newList = new List<mprFintrakMenu_ObjectListInfo>();

            var menus = context2.mpr_FintrakMenuSet.Where(x => x.Visible == true && (x.MenuList != "" || x.MenuList != null) && x.urlMenu.Trim().ToUpper() == mprvar.ToUpper());
            //var viewrights = menus.Select(x => x.ViewRights);
            var teamdefcode = Convert.ToString(System.Web.HttpContext.Current.Session["session_levelcode"]);
            //var menubylevel = menus

            var menu2 = menus.Where(a => a.ViewRights.Contains(teamdefcode));

            var parentmenu = menu2.GroupBy(x => x.MenuList).Select(o => o.FirstOrDefault()).Select(x => x.MenuList);

            foreach(var p in parentmenu)
            {
                //.Select(x => new BSCaption_2 { CaptionCode = x.CaptionCode, CaptionName = x.CaptionName });
                //var rp = mx.mpr_FintrakMenuSet.Where(x => x.MenuList == item).Select(y => y.ReportPAth).ToList();
                var query = menu2.Where(x => x.MenuList == p).Select(x=> new mprFintrakMenu_SubObjectListInfo { ReportPAth = x.ReportPAth, ParameterKey = x.ParameterKey, ReportTitle = x.ReportTitle, UIsrefState = x.UIsrefState, ID = x.ID, Position = x.Position, urlMenu = x.urlMenu});

                var ob = new mprFintrakMenu_ObjectListInfo()
                {
                    //subobj = query.OrderBy(x=>x.Position).ToList(),
                    subobj = query.ToList(),
                    MenuList = p
                };

                newList.Add(ob);
            }

            return newList;
        }

        public IEnumerable<MPR.Report.Core.mprFintrakMenu_ObjectListInfo> Getmpr_FintrakMenuList(string finallevelcode)
        {
            string mprvar = "mpr";

            var newList = new List<mprFintrakMenu_ObjectListInfo>();

            //var menus = context2.mpr_FintrakMenuSet.Where(x => x.Visible == true && (x.MenuList != "" || x.MenuList != null));
            var menus = context2.mpr_FintrakMenuSet.Where(x => x.Visible == true && (x.MenuList != "" || x.MenuList != null) && x.urlMenu.Trim().ToUpper() == mprvar.ToUpper());

            //var teamdefcode = Convert.ToString(System.Web.HttpContext.Current.Session["session_levelcode"]);
            var teamdefcode = finallevelcode;

            var menu2 = menus.Where(a => a.ViewRights.Contains(teamdefcode));

            var parentmenu = menu2.GroupBy(x => x.MenuList.Trim()).Select(o => o.FirstOrDefault()).Select(x => x.MenuList.Trim());

            foreach (var p in parentmenu)
            {
                //.Select(x => new BSCaption_2 { CaptionCode = x.CaptionCode, CaptionName = x.CaptionName });
                //var rp = mx.mpr_FintrakMenuSet.Where(x => x.MenuList == item).Select(y => y.ReportPAth).ToList();
                //var query = menu2.Where(x => x.MenuList == p).Select(x => new mprFintrakMenu_SubObjectListInfo { ReportPAth = x.ReportPAth, ParameterKey = x.ParameterKey, ReportTitle = x.ReportTitle, UIsrefState = x.UIsrefState, ID = x.ID });
                var query = menu2.Where(x => x.MenuList == p).Select(x => new mprFintrakMenu_SubObjectListInfo { ReportPAth = x.ReportPAth, ParameterKey = x.ParameterKey, ReportTitle = x.ReportTitle, UIsrefState = x.UIsrefState, ID = x.ID, Position = x.Position, urlMenu = x.urlMenu });

                var ob = new mprFintrakMenu_ObjectListInfo()
                {
                    //subobj = query.OrderBy(x => x.Position).ToList(),
                    subobj = query.ToList(),
                    MenuList = p
                };

                newList.Add(ob);
            }

            return newList;
        }

        public mpr_FintrakMenu GetFIntrakMenuOBJ(string searchvalue)
        {
            var menuObj = context2.mpr_FintrakMenuSet.Where(x => x.ParameterKey == searchvalue).FirstOrDefault();

            return menuObj;
        }

        public IEnumerable<mpr_FintrakMenu> MobilityMth()
        {
            //string mobilityvar = "mobility";

            //List<mpr_FintrakMenu> newList = new List<mpr_FintrakMenu>();
            //newList = context2.mpr_FintrakMenuSet.Where(x => x.Visible == true && (x.MenuList != "" || x.MenuList != null) && x.urlMenu.Trim().ToUpper() == mobilityvar.ToUpper()).ToList();

            //return newList;


            string mobilityvar = "mobility";

            List<mpr_FintrakMenu> menus = new List<mpr_FintrakMenu>();
            menus = context2.mpr_FintrakMenuSet.Where(x => x.Visible == true && (x.MenuList != "" || x.MenuList != null) && x.urlMenu.Trim().ToUpper() == mobilityvar.ToUpper()).ToList();
            var teamdefcode = Convert.ToString(System.Web.HttpContext.Current.Session["session_levelcode"]);
            menus = menus.Where(a => a.ViewRights.Contains(teamdefcode)).ToList();

            return menus;
        }

        public IEnumerable<mpr_FintrakMenu> EchannelMth()
        {
            // string echannelvar = "echannel";

            // //var newList = new List<mpr_FintrakMenu>();
            //var newList = context2.mpr_FintrakMenuSet.Where(x => x.Visible == true && (x.MenuList != "" || x.MenuList != null) && x.urlMenu.Trim().ToUpper() == echannelvar.ToUpper()).ToList();

            // return newList;


            string echannelvar = "echannel";

            var menus = context2.mpr_FintrakMenuSet.Where(x => x.Visible == true && (x.MenuList != "" || x.MenuList != null) && x.urlMenu.Trim().ToUpper() == echannelvar.ToUpper()).ToList();
            var teamdefcode = Convert.ToString(System.Web.HttpContext.Current.Session["session_levelcode"]);
            menus = menus.Where(a => a.ViewRights.Contains(teamdefcode)).ToList();

            return menus;
        }

    }
} 

