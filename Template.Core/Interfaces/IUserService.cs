using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Template.Core.CustomEntities;
using Template.Core.Entities;
using Template.Core.QueryFilters;

namespace Template.Core.Interfaces
{
    public interface IUserService
    {
        PagedList<User> GetUsers(UserQueryFilter filters);

        Task InsertUser(User user);

        Task<User> LoginUser(User user);

        Task<bool> DeleteUser(long id);

    }
}
