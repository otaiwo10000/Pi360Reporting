﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MPR.Report.Core.Interfaces
{
    public interface IDataRepository
    {

    }

    public interface IDataRepository<T> : IDataRepository
       where T : class, IIdentifiableEntity, new()
    {
        T Add(T entity);

        void Remove(T entity);

        void Remove(int id);

        T Update(T entity);

        IEnumerable<T> Get();

        T Get(int id);
    }
}
