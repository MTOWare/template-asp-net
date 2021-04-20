using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Template.Core.Entities;

namespace Template.Core.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetUserByEmail(string email);

        Task<IEnumerable<User>> GetUserByToken(string token);

        Task<IEnumerable<User>> Login(string email, string password);

    }
}
