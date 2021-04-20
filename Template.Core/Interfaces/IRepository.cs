using System.Collections.Generic;
using System.Threading.Tasks;
using Template.Core.Entities;

namespace Template.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        Task<T> GetById(long id);
        Task Add(T entity);
        void Update(T entity);
        Task Delete(long id);
    }
}
