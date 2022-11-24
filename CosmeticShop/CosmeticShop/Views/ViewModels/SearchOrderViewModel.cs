using CosmeticShop.Model.Entities;
using CosmeticShop.WebApp.Data.Models;
using System.Collections.Generic;

namespace CosmeticShop.WebApp.Views.ViewModels
{
    public class SearchOrderViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
        public SearchOrderParams SearchParams { get; set; }
    }
}
