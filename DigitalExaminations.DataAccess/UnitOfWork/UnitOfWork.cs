using DigitalExaminations.DataAccess.Data;
using DigitalExaminations.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalExaminations.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context = null;
        private bool disposed;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public IGenericRepository<T> GenericRepository<T>() where T : class
        {
            IGenericRepository<T> repo = new GenericRepository<T>(_context);
            return repo;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
