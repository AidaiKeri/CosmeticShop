using CosmeticShop.Model.AbstractClasses;

namespace CosmeticShop.Model.Entities
{
    public class Role : Entity
    {
        public string Name { get; set; }
        public List<User> Users { get; set; }
        public Role()
        {
            Users = new List<User>();
        }
    }
}
