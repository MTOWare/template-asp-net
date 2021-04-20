using System;
using System.Threading.Tasks;

namespace Template.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }

        void SaveChanges();

        Task SaveChangesAsync();

    }
}
