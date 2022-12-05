using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CosmeticShop.Data.Context;
using CosmeticShop.Model.Entities;
using CosmeticShop.Data;
using CosmeticShop.Model.Context;

namespace CosmeticShop.WebApp.Controllers
{
    public class CartItemsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ICartRepository _repository;

        public CartItemsController(AppDbContext context,
            ICartRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        // GET: CartItems
        public async Task<IActionResult> Index()
        {
            var user = _context.Users.FirstOrDefault();
            var product = _context.Product.Find(2);
            try
            {
                var cartItem = new CartItem
                {
                    Product = (CartProduct)product,
                    User = (CartUser)user,
                    ProductId = product.Id,
                    UserId = user.Id,
                };
                _repository.Add(cartItem);
                var a = _repository.GetAll().ToList();

                return NotFound();
            }
            catch(NullReferenceException ex)
            {
 
                return Problem(ex.Message);

            }
            
        }
    }
}
