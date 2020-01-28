using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Questor.Core.Data.Entities.Base;

namespace Questor.Core.Data
{
    public interface IAsyncRepository<T, TId> where T : BaseEntity<TId>
    {
        Task<T> FindAsync(TId id);
        
        Task<IReadOnlyList<T>> GetAllAsync();
        
        Task<T> AddAsync(T entity);
        
        Task UpdateAsync(T entity);
        
        Task DeleteAsync(T entity);
        
        IEnumerable<T> GetList(Func<T, bool> predicate);
    }
}