using System.Collections.Generic;
using System.Threading.Tasks;
using Questor.Core.Data.Entities.Base;

namespace Questor.Core.Data
{
    public interface IAsyncRepository<T> where T : BaseEntity
    {
        Task<T> FindAsync(int id);
        
        Task<IReadOnlyList<T>> GetAllAsync();
        
        Task<T> AddAsync(T entity);
        
        Task UpdateAsync(T entity);
        
        Task DeleteAsync(T entity);
    }
}