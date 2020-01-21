 using MPR.Report.Core.Entities;
using MPR.Report.Core.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MPR.Report.Core.IRepositoryInterfaces
{
    public interface ITeamStructureRepository
    {
        //void Add(TeamStructure b);
        //void Edit(TeamStructure b);
        //void Remove(int Id);
        //ITeamStructureRepository FindById(int Id);
        //IEnumerable GetTeamStructure();
        IEnumerable<TeamStructure> GetTeamStructureByMISCodeLevelYear();
        //IEnumerable<TeamStructure> GetSelectedTeamStructureByMISCodeLevelYear(string dir, string reg, string div, string brh, string acct);
        IEnumerable<TeamStructure> GetSelectedTeamStructureMisCode(string searchvalue);
        IEnumerable<TeamStructureData> GetTeamStructureBySelectedMisCodeAndYear(string selectedcode, string selectedyear);
        YearPeriodData GetLatestYearAndPeriod();

        IEnumerable<RegionData> GetRegByDir(string reg_dir);
        IEnumerable<DivisionData> GetDivByReg(string div_reg);
        IEnumerable<BranchData> GetBrhByDiv(string brh_div);
        IEnumerable<AccountOfficerData> GetAcctByBrh(string acct_brh);

        IEnumerable<DirectorateData> GetDirectorates();
        IEnumerable<RegionData> GetRegions();
        IEnumerable<DivisionData> GetDivisions();
        IEnumerable<BranchData> GetBranches();
        IEnumerable<AccountOfficerData> GetAccountOfficers();
    } 
}
