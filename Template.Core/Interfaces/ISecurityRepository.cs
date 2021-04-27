using Template.Core.Entities;
using System.Threading.Tasks;

namespace Template.Core.Interfaces
{
    public interface ISecurityRepository : IRepository<Security>
    {
        Task<Security> GetLoginByCredentials(UserLogin login);
    }
}