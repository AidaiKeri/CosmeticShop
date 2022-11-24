using CosmeticShop.Model.Context;
using CosmeticShop.Model.Entities;
using CosmeticShop.WebApp.Data.Specifications;
using Microsoft.EntityFrameworkCore;

namespace CosmeticShop.WebApp.Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductsRepository
    {
        public ProductRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }

        public async Task<Product> GetProductByName(string name)
        {
            var collection = await GetAll();

            return collection.FirstOrDefault(n => n.Name.Equals(name));
        }

        public async Task<Product> GetProductByPrice(int price)
        {
            var collection = await GetAll();

            return collection.FirstOrDefault(n => n.Price == price);
        }

        public Task<IReadOnlyList<Product>> GetProductsByIds(IEnumerable<int> ids)
        {
            var specification = new ProductSpecification(prod => ids.Contains(prod.Id));
            return GetAll(specification);
        }

    }
}
