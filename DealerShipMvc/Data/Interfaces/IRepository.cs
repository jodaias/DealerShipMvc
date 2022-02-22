using DealerShipMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DealerShipMvc.Data.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);

        Task<TEntity> GetByIdAsync(object id);

        IQueryable<TEntity> GetAll();

        Task UpdateAsync(TEntity entity);
        
        Task RemoveAsync(TEntity entity);
        
        Task FlushAsync();
    }
}
