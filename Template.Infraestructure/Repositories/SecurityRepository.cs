using Microsoft.EntityFrameworkCore;
using Template.Core.Entities;
using Template.Core.Interfaces;
using System.Threading.Tasks;
using Template.Infraestructure.Repositories;
using Template.Infraestructure.Data;

namespace Template.Infrastructure.Repositories
{
    public class SecurityRepository : BaseRepository<Security>, ISecurityRepository
    {
        public SecurityRepository(TemplateContext context) : base(context) { }

        public async Task<Security> GetLoginByCredentials(UserLogin login)
        {
            return await _entities.FirstOrDefaultAsync(x => x.User == login.User);
        }
    }
}
