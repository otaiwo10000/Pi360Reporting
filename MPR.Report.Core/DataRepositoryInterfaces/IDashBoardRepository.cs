 using MPR.Report.Core.Entities;
using MPR.Report.Core.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MPR.Report.Core.IRepositoryInterfaces
{
    public interface IDashboardRepository
    {
        //void Add(TeamStructure b);
        //void Edit(TeamStructure b);
        //void Remove(int Id);
        //ITeamStructureRepository FindById(int Id);
        //IEnumerable GetTeamStructure();

        IEnumerable DashboardMixMtd(string param);
        IEnumerable DashboardTrendMtd(string param);
        IEnumerable DashboardTrendMtd2(string param);
        IEnumerable DashboardMainCaptionMtd();
    }     
}
