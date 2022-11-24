using CosmeticShop.Data;
using CosmeticShop.Model.Entities;

namespace CosmeticShop.Data
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> GetOrderByIdWithDetailsOrDefault(int id);
    }
}
