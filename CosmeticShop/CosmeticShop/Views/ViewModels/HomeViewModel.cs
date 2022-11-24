using CosmeticShop.Model.Entities;
using System.Collections.Generic;
namespace CosmeticShop.WebApp.Views.ViewModels
{
    public class HomeViewModel
    {
        public string Message { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
