using CosmeticShop.Model.AbstractClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticShop.Model.Entities
{
    public class Product : Entity
    {
        [Required(ErrorMessage = "Имя товара обязательно!")]
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }

        [Url(ErrorMessage = "Неверный формат")]
        
        public string ImageURL { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Цена не указана!")]
        public double Price { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsAvailable { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
