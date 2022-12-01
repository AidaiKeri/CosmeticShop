using CosmeticShop.Data.Context;
using CosmeticShop.Model.Entities;

namespace CosmeticShop.Data.Repositories
{
    public class CartRepository : ICartRepository
    {
        public void Add(CartItem item)
        {
            using(var context = new CartContext())
            {
                context.Add(item);
                context.SaveChanges();
            }
        }

        public void Delete(CartItem item)
        {
            using (var context = new CartContext())
            {
                context.Remove(item);
                context.SaveChanges();
            }
        }

        public void DeleteById(int id)
        {
            using (var context = new CartContext())
            {
                var cartItem = context.CartItems.Find(id);
                context.Remove(cartItem);
                context.SaveChanges();
            }
        }

        public IEnumerable<CartItem> GetAll()
        {
            using (var context = new CartContext())
            {
                return context.CartItems;
            }
        }
    }
}
