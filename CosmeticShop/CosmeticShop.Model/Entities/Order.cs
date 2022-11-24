using CosmeticShop.Model.AbstractClasses;
using System.ComponentModel.DataAnnotations;

namespace CosmeticShop.Model.Entities
{
    public class Order : BaseEntity
    {
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Неверный адрес!")]
        public string Email { get; set; }
        public string Address { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public string Information { get; set; }
        [Required(ErrorMessage = "Нет цены!")]
        public double TotalPrice { get; set; }
        public bool IsOrderActive { get; set; }
        public User User { get; set; }
        public int? UserId { get; set; }
        public string ProductsString { get; set; }
       
        public DateTime OrderDate { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
