using MPR.Report.Core.Entities;
using MPR.Report.Core.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MPR.Report.Core.IRepositoryInterfaces
{
    public interface IOnBoardingUsersRepository
    {
        void Add(OnBoardingUsers b);
        void Edit(OnBoardingUsers b);
        void Remove(int Id);
        IOnBoardingUsersRepository FindById(int Id);
        IEnumerable GetAllOnBoardingUsers();

        OnBoardingUsers GetOnBoardingUsers();
    } 
}
