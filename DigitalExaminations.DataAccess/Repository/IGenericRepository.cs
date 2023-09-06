﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigitalExaminations.DataAccess.Repository
{
    public interface IGenericRepository<T> : IDisposable
    {



        IEnumerable<T> GetAll(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");

        T GetByID(object id);

        Task<T>

        GetByIdAsync(object id);
        void Add(T entity);

        Task<T> AddAsync(T entity);

        void DeleteByID(object id);
        void Delete(T entityToDelete);
        Task<T> DeleteAsync(T entityToDelete);

        void Update(T entityToUpdate);

        Task<T> UpdateAsync(T entityToUpdate);

    }
}
