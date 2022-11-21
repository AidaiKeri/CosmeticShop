using CosmeticShop.Model.Entities;

namespace CosmeticShop.WebApp.Views
{
    public class HomeViewModel
    {
        public string Message { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
