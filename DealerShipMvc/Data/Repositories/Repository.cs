using System;
using DealerShipMvc.Data.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DealerShipMvc.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DealerShipMvcContext _context;

        protected Repository(DealerShipMvcContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task AddAsync(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);

            FlushAsync();
            return Task.CompletedTask;
        }

        public Task<TEntity> GetByIdAsync(object id)
        {
            return _context.FindAsync<TEntity>(id).AsTask();
        }


        public Task RemoveAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);

            FlushAsync();

            return Task.CompletedTask;
        }

        public Task UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            FlushAsync();
            return Task.CompletedTask;
        }

        public Task FlushAsync()
        {
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public IQueryable<TEntity> GetAll()
        {
         return _context.Set<TEntity>();
        }
    }
}
