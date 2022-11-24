using Abp.Domain.Entities;
using CosmeticShop.Data;
using CosmeticShop.Model.Entities;

namespace CosmeticShop.WebApp.Data
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        public Task<IReadOnlyList<TEntity>> GetAll();
        public Task<IReadOnlyList<TEntity>> GetAll(ISpecification<TEntity> specification);
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task Delete(TEntity entity);
        Task<TEntity> GetById(int id);
    }
}
