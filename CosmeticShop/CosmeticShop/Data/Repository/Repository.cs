using CosmeticShop.Model.AbstractClasses;
using CosmeticShop.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace CosmeticShop.WebApp.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected AppDbContext Context { get; set; }
        private DbSet<TEntity> EntitySet => Context.Set<TEntity>();

        public Repository(AppDbContext appDbContext)
        {
            Context = appDbContext;
        }

        public async Task<TEntity> Add(TEntity entity)
        {

            ExceptionHelper.ThrowIfObjectWasNull(entity, nameof(entity), nameof(Add));

            await Context.AddAsync(entity);
            await Context.SaveChangesAsync();

            return entity;
        }


        public Task Delete(TEntity entity)
        {
            ExceptionHelper.ThrowIfObjectWasNull(entity, nameof(entity), nameof(Delete));

            EntitySet.Remove(entity);
            return Context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<TEntity>> GetAll()
        {
            return await EntitySet.ToListAsync();
        }

        public async Task<IReadOnlyList<TEntity>> GetAll(ISpecification<TEntity> specification)
        {
            return await ApplySpecification(specification).ToListAsync();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await EntitySet.FindAsync(id);
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            ExceptionHelper.ThrowIfObjectWasNull(entity, nameof(entity), nameof(Update));

            var updateResult = EntitySet.Update(entity).Entity;
            await Context.SaveChangesAsync();

            return updateResult;
        }

        protected IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
        {
            return SpecificationEvaluator.ApplySpecification(Context.Set<TEntity>().AsQueryable(), spec);
        }
    }
}
