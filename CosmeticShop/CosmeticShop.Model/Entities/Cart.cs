namespace CosmeticShop.Model.Entities
{
    public class CartItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public virtual CartProduct Product { get; set; }
        public int UserId { get; set; }
        public virtual CartUser User { get; set; }
    }
}
