using CosmeticShop.Model.Entities;

namespace CosmeticShop.WebApp.Data
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderByIdWithDetailsOrDefault(int id);
    }
}
