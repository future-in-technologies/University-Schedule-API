using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UniversitySchedule.Core.Abstractions;

namespace UniversitySchedule.Core.Abstractions
{
    public interface IEntityBaseRepository<T>
        where T : EntityBase, new()
    {
        T Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        T GetSingle(Expression<Func<T, bool>> predicate);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate);
        IList<T> Find(Expression<Func<T, bool>> predicate, int skip = 0, int? take = null);
        Task<IList<T>> FindAsync(Expression<Func<T, bool>> predicate = null, int? skip = null, int? take = null);
        bool Any(Expression<Func<T, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

    }
}
