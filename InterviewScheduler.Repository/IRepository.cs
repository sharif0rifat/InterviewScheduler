using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InterviewScheduler.Repository
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Table { get; }

        Task<T> AddAsync(T entity);
        Task BulkInsertAsync(IList<T> entities);
        Task DeleteAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetByIdAsync(object id);
        void Save();
        Task SaveAsync();
        Task<T> UpdateAsync(T entity);
    }
}
