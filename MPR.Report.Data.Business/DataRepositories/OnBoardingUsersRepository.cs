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
    public class OnBoardingUsersRepository : IDataRepository
    {
        //MPRContext context = new MPRContext();
        MPRContext2 context2 = new MPRContext2();
        //protected void Add(OnBoardingUsers b)
        public void Add(OnBoardingUsers b)
        {
            context2.OnBoardingUsersSet.Add(b);
            context2.SaveChanges();
        }

        //protected void Edit(OnBoardingUsers b)
        public void Edit(OnBoardingUsers b)
        {
            context2.Entry(b).State = System.Data.Entity.EntityState.Modified;
        }

        public OnBoardingUsers FindById(int Id)
        {
            var result = (from r in context2.OnBoardingUsersSet where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetAllOnBoardingUsers()
        {
            return context2.OnBoardingUsersSet;
        }
        //protected void Remove(int Id)
        public void Remove(int Id)
        {
            OnBoardingUsers p = context2.OnBoardingUsersSet.Find(Id);
            context2.OnBoardingUsersSet.Remove(p);
            context2.SaveChanges();
        }

        public OnBoardingUsers GetOnBoardingUsers()
        {
            using (MPRContext2 entityContext = new MPRContext2())
            {
                OnBoardingUsers query = (from a in entityContext.OnBoardingUsersSet
                                         select a).FirstOrDefault();

                return query;
            }
        }

    }
} 

