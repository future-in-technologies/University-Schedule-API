using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using UniversitySchedule.Core.Abstractions;
using UniversitySchedule.DB;

namespace UniversitySchedule.DAL
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T>
                where T : EntityBase, new()
    {
        protected readonly UniversityScheduleContext Context;

        public EntityBaseRepository(UniversityScheduleContext context)
        {
            Context = context;
        }

        public T Add(T entity)
        {
            Context.Set<T>().Add(entity);
            return entity;
        }

        public IList<T> Find(Expression<Func<T, bool>> predicate, int skip = 0, int? take = null)
        {
            var query = Context.Set<T>().Where(predicate).Skip(skip);
            if (take.HasValue)
                query = query.Take(take.Value);
            return query.ToList();
        }

        public async Task<IList<T>> FindAsync(Expression<Func<T, bool>> predicate = null, int? skip = null, int? take = null)
        {
            var query = Context.Set<T>().AsQueryable();

            if (predicate != null)
                query = query.Where(predicate);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);
            return await query.ToListAsync();
        }

        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().FirstOrDefault(predicate);
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate)
        {
            return await Context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            if (predicate != null)
                return Context.Set<T>().Any(predicate);
            return Context.Set<T>().Any();
        }
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            if (predicate != null)
                return await Context.Set<T>().AnyAsync(predicate);
            return await Context.Set<T>().AnyAsync();
        }


        public void Remove(T entity)
        {
            var dbEntityEntry = Context.Entry(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        public void Update(T entity)
        {
            var dbEntityEntry = Context.Entry(entity);
            dbEntityEntry.State = EntityState.Modified;
        }
    }
}
