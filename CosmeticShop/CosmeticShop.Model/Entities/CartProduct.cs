namespace CosmeticShop.Model.Entities
{
    public class CartProduct : Product
    {
        public virtual List<CartItem> CartItems { get; set; }
    }
}
