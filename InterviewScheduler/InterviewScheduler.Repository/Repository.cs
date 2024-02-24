using InterviewScheduler.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InterviewScheduler.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private InterviewSchedulerDbContext _context;
        private readonly DbSet<T> _dbSet;
        public IQueryable<T> Table { get; }

        public Repository(InterviewSchedulerDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
            Table = _dbSet;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await Table.ToListAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message + " Inner: " + ex.InnerException?.Message + " conn:" + _context.Database.GetConnectionString());
            }

        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await Table.Where(predicate).AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            await Task.Run(() => { 
                var existing=_context.Entry(entity);
                if (existing.State==EntityState.Detached) {
                    _context.Attach(entity);
                    _context.Entry(entity).State = EntityState.Modified;
                }
                 
            });
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            await Task.Run(() =>
            {
                _dbSet.Remove(entity);
            });
        }

        public async Task BulkInsertAsync(IList<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        public  void Save()
        {
             _context.SaveChanges();
        }
    }
}
