using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;
using Services.Specifications;



namespace Persistance.Repositories
{
    public class GenericRepository<TEntity, TKey>(StoreDbContext _dbContext) : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public async Task AddAsync(TEntity entity) => await  _dbContext.Set<TEntity>().AddAsync(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbContext.Set<TEntity>().ToListAsync();



        public async Task<TEntity?> GetByIdAsync(TKey id) => await _dbContext.Set<TEntity>().FindAsync(id);
        //------------------------------------------------------------------------------------------------

        #region WithSpecifications
        public async Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> specifications)
        {
            return await SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(),specifications).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications)
        {
            return await SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(),specifications).ToListAsync();

        }
        #endregion
        //------------------------------------------------------------------------------------------------
        public void Remove(TEntity entity) => _dbContext.Set<TEntity>().Remove(entity);
        public void Update(TEntity entity) => _dbContext.Set<TEntity>().Update(entity);

    }
}
