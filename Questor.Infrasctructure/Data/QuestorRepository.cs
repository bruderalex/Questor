using System.Collections.Generic;
using System.Threading.Tasks;
using Questor.Core.Data;
using Questor.Core.Data.Entities.Base;

namespace Questor.Infrasctructure.Data
{
    public class QuestorRepository<T> : IAsyncRepository<T> where T : BaseEntity
    {
        protected readonly QuestorContext _context;
        
        public QuestorRepository(QuestorContext context)
        {
            this._context = context;
        }
        
        public Task<T> FindAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyList<T>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<T> AddAsync(T entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<T> UpdateAsync(T entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<T> DeleteAsync(T entity)
        {
            throw new System.NotImplementedException();
        }
    }
}