namespace CosmeticShop.Model.Entities
{
    public class SearchProduct
    {
        public int SearchProductId { get; set; }
        public string Name { get; set; }

        public double StartPrice { get; set; }
        public double EndPrice { get; set; }
        public int CategoryId { get; set; }
        public bool IsAvailable { get; set; } = true;
        public bool IsSortByPriceRequired { get; set; }
    }
}
