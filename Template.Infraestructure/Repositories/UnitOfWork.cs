using System.Threading.Tasks;
using Template.Core.Interfaces;
using Template.Infraestructure.Data;
using Template.Infrastructure.Repositories;

namespace Template.Infraestructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TemplateContext _context;
        private readonly IUserRepository _userRepository;
        
        private readonly ISecurityRepository _securityRepository;

        public UnitOfWork(TemplateContext context)
        {
            _context = context;
        }

        public IUserRepository UserRepository => _userRepository ?? new UserRepository(_context);

        public ISecurityRepository SecurityRepository => _securityRepository ?? new SecurityRepository(_context);
        
        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
