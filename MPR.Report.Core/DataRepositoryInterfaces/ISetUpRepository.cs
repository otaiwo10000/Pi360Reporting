using MPR.Report.Core.Entities;
using MPR.Report.Core.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MPR.Report.Core.IRepositoryInterfaces
{
    public interface ISetUpRepository
    {
        void Add(SetUp b);
        void Edit(SetUp b);
        void Remove(int Id);
        ISetUpRepository FindById(int Id);
        IEnumerable GetAllSetUps();

        SetUp GetSetUp();
    } 
}
