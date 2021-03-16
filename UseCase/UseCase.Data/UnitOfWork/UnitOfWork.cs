using UseCase.Data.Context;
using UseCase.Data.Model;
using UseCase.Data.Repositories;
using System;

namespace UseCase.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private bool disposed;
        private readonly UseCaseContext _useCaseContext;
        private IGenericRepository<User> _users;
        private IGenericRepository<Customer> _customers;
        private IGenericRepository<Invoice> _invoices;



        public UnitOfWork(UseCaseContext context)
        {
            _useCaseContext = context;
        }

        public IGenericRepository<User> Users => _users ?? (_users = new GenericRepository<User>(_useCaseContext));
  
        public IGenericRepository<Invoice> Invoces => _invoices ?? (_invoices = new GenericRepository<Invoice>(_useCaseContext));

        public void Save()
        {
            _useCaseContext.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _useCaseContext.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }
    }
}