namespace CosmeticShop.Model.Entities
{
    public class CartUser : User
    {
        public virtual List<CartItem> CartItems { get; set; }
    }
}
