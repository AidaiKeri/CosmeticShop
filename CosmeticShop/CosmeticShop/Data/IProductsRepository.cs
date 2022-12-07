using Abp.Domain.Repositories;
using CosmeticShop.Data;
using CosmeticShop.Model.Entities;

namespace CosmeticShop.WebApp.Data
{
    public interface IProductsRepository : IRepository<Product>
    {
        Task<IReadOnlyList<Product>> GetProductsByIds(IEnumerable<int> ids);
        Task<Product> GetProductByName(string name);
        Task<Product> GetProductByPrice(int price);
    }
}
