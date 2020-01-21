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
    public class SetUpRepository : IDataRepository
    {
        //MPRContext context = new MPRContext();
        MPRContext2 context2 = new MPRContext2();
        protected void Add(SetUp b)
        {
            context2.SetUpSet.Add(b);
            context2.SaveChanges();
        }

        protected void Edit(SetUp b)
        {
            context2.Entry(b).State = System.Data.Entity.EntityState.Modified;
        }

        public SetUp FindById(int Id)
        {
            var result = (from r in context2.SetUpSet where r.SetupId == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetAllSetUps()
        {
            return context2.SetUpSet;
        }
        protected void Remove(int Id)
        {
            SetUp p = context2.SetUpSet.Find(Id);
            context2.SetUpSet.Remove(p);
            context2.SaveChanges();
        }

        public SetUp GetSetUp()
        {
            using (MPRContext2 entityContext = new MPRContext2())
            {
                SetUp query = (from a in entityContext.SetUpSet
                               select a).FirstOrDefault();

                return query;
            }
        }

    }
} 

