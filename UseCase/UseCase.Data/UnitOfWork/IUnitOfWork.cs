using UseCase.Data.Model;
using UseCase.Data.Repositories;

namespace UseCase.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<User> Users { get; }
        IGenericRepository<Invoice> Invoces { get; }

        void Save();
    }
}