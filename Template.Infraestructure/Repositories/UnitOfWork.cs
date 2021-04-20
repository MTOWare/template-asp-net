using System.Threading.Tasks;
using Template.Core.Interfaces;
using Template.Infraestructure.Data;

namespace Template.Infraestructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TemplateContext _context;
        private readonly IUserRepository _userRepository;

        public UnitOfWork(TemplateContext context)
        {
            _context = context;
        }

        public IUserRepository UserRepository => _userRepository ?? new UserRepository(_context);

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
