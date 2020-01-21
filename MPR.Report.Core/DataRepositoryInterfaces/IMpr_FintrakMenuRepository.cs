using MPR.Report.Core.Entities;
using MPR.Report.Core.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MPR.Report.Core.IRepositoryInterfaces
{
    public interface IMpr_FintrakMenuRepository
    {
        void Add(mpr_FintrakMenu b);
        void Edit(mpr_FintrakMenu b);
        void Remove(int Id);
        IMpr_FintrakMenuRepository FindById(int Id);
        IEnumerable Getmpr_FintrakMenus();
        IEnumerable Getmpr_FintrakMenuList();
        IEnumerable Getmpr_FintrakMenuList(string finallevelcode);
        mpr_FintrakMenu GetFintrakMenuObject(string searchvalue);
    } 
}
