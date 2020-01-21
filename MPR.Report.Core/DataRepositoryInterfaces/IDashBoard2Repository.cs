 using MPR.Report.Core.Entities;
using MPR.Report.Core.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MPR.Report.Core.IRepositoryInterfaces
{
    public interface IDashboard2Repository
    {
        //void Add(TeamStructure b);
        //void Edit(TeamStructure b);
        //void Remove(int Id);
        //ITeamStructureRepository FindById(int Id);
        //IEnumerable GetTeamStructure();

        IEnumerable Dashboard2Ratio(string param1, string param2, string param3, string param4,
            string param5, string param6, string param7, string param8, string param9);

    } 
}
