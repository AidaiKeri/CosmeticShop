using CosmeticShop.Model.AbstractClasses;

namespace CosmeticShop.Model.Entities
{
    public class Category : Entity
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public List<Product> ProductsOfCurrentCategory { get; set; }
    }
}
