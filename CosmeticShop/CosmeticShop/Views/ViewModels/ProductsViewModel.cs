using System.Collections.Generic;
using CosmeticShop.Model.Entities;
using CosmeticShop.WebApp.Data.Models;

namespace CosmeticShop.WebApp.Views.ViewModels
{
    public class ProductsViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public SearchProductsParams SearchParams { get; set; }
        public string ProductCategory { get; set; }
    }
}
