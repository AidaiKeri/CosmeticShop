using CosmeticShop.Model.Entities;

namespace CosmeticShop.WebApp.Data
{
    public interface IProductsRepository
    {
        Task<IReadOnlyList<Product>> GetProductsByIds(IEnumerable<int> ids);
        Task<Product> GetProductByName(string name);
        Task<Product> GetProductByPrice(int price);
    }
}
