using System;
using System.Threading.Tasks;

namespace Template.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }

        ISecurityRepository SecurityRepository { get; }

        void SaveChanges();

        Task SaveChangesAsync();

    }
}
