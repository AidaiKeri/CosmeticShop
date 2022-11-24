using CosmeticShop.Model.Context;
using CosmeticShop.Model.Entities;
using CosmeticShop.WebApp.Data.Specifications;
using E_Shop_Cosmetic.Data.Specifications;

namespace CosmeticShop.WebApp.Data.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }

        public async Task<Order> GetOrderByIdWithDetailsOrDefault(int id)
        {
            var spec = new OrderSpecification(id)
                .IncludeDetails("OrderDetails.Product.Category");

            var orders = await GetAll(spec);

            return orders.FirstOrDefault();
        }
    }
}
