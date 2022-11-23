using CosmeticShop.Model.Entities;

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
