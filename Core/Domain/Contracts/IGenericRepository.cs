using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        //GetALL
        Task<IEnumerable<TEntity>> GetAllAsync();

        //Get By ID
        Task<TEntity?> GetByIdAsync(TKey id);

        #region WithSpecifications
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications);
        Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> specifications);
        Task<int> CountAsync(ISpecifications<TEntity, TKey> specifications);
        #endregion


        Task AddAsync(TEntity entity);
        //Update
        void Update(TEntity entity);
        //Delete
        void Remove(TEntity entity);
    }
}
