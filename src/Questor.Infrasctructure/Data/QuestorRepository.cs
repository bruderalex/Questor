﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Questor.Core.Data;
using Questor.Core.Data.Entities.Base;

namespace Questor.Infrasctructure.Data
{
    public class QuestorRepository<T, TId> : IAsyncRepository<T, TId> where T : BaseEntity<TId>
    {
        protected readonly QuestorContext _context;
        
        public QuestorRepository(QuestorContext context)
        {
            this._context = context;
        }
        
        public virtual async Task<T> FindAsync(TId id)
        {
            return await this._context.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await this._context.Set<T>().ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await this._context.Set<T>().AddAsync(entity);
            await this._context.SaveChangesAsync();
            
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            this._context.Entry(entity).State = EntityState.Modified;
            await this._context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            this._context.Set<T>().Remove(entity);
            await this._context.SaveChangesAsync();
        }

        public IEnumerable<T> GetList(Func<T, bool> predicate)
        {
            return this._context.Set<T>().Where(predicate).ToList();
        }
    }
}