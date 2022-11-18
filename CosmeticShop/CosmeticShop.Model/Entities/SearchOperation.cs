using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticShop.Model.Entities
{
    public class SearchOperation
    {
        [Key]
        public int OrderId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        [EmailAddress(ErrorMessage = "Неправильный адрес")]
        public string Email { get; set; }
        public bool IsSortByDateRequired { get; set; }
        public bool IsOrderActive { get; set; } = true;
    }
}
