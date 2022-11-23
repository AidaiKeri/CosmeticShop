using CosmeticShop.Data;
using CosmeticShop.Model.Entities;

namespace E_Shop_Cosmetic.Data.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> GetOrderByIdWithDetailsOrDefault(int id);
    }
}
