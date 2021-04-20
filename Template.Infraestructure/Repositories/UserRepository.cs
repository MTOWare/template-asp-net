using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Core.Entities;
using Template.Core.Interfaces;
using Template.Infraestructure.Data;

namespace Template.Infraestructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(TemplateContext context) : base(context) { }

        public async Task<IEnumerable<User>> GetUserByEmail(string email)
        {
            return await _entities.Where(x => x.Email == email).ToListAsync();
        }

        public async Task<IEnumerable<User>> GetUserByToken(string token)
        {
            return await _entities.Where(x => x.Token == token).ToListAsync();
        }

        public async Task<IEnumerable<User>> Login(string email, string password)
        {
            return await _entities.Where(x => x.Email == email).Where(x => x.Password == password).Where(x => x.Status == "1").ToListAsync();
        }

    }
}
