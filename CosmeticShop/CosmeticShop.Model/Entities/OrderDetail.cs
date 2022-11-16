using CosmeticShop.Model.AbstractClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticShop.Model.Entities
{
    public class OrderDetail : Entity
    {
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public double PriceOnOrderTime { get; set; }
        public double TotalPrice { get; set; }
        public uint Amount { get; set; }
        public int OrderId { get; set; }
    }
}
