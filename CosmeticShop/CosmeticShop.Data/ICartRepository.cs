using CosmeticShop.Model.Entities;

namespace CosmeticShop.Data
{
    public interface ICartRepository
    {
        public void Add(CartItem item);
        public void Delete(CartItem item);
        public void DeleteById(int id);
        public IEnumerable<CartItem> GetAll();
    }
}
