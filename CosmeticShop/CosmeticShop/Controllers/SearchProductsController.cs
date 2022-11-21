using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CosmeticShop.Model.Context;
using CosmeticShop.Model.Entities;

namespace CosmeticShop.WebApp.Controllers
{
    public class SearchProductsController : Controller
    {
        private readonly AppDbContext _context;

        public SearchProductsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: SearchProducts
        public async Task<IActionResult> Index()
        {
              return View(await _context.SerachProducts.ToListAsync());
        }

        // GET: SearchProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SerachProducts == null)
            {
                return NotFound();
            }

            var searchProduct = await _context.SerachProducts
                .FirstOrDefaultAsync(m => m.SearchProductId == id);
            if (searchProduct == null)
            {
                return NotFound();
            }

            return View(searchProduct);
        }

        // GET: SearchProducts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SearchProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SearchProductId,Name,StartPrice,EndPrice,CategoryId,IsAvailable,IsSortByPriceRequired")] SearchProduct searchProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(searchProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(searchProduct);
        }

        // GET: SearchProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SerachProducts == null)
            {
                return NotFound();
            }

            var searchProduct = await _context.SerachProducts.FindAsync(id);
            if (searchProduct == null)
            {
                return NotFound();
            }
            return View(searchProduct);
        }

        // POST: SearchProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SearchProductId,Name,StartPrice,EndPrice,CategoryId,IsAvailable,IsSortByPriceRequired")] SearchProduct searchProduct)
        {
            if (id != searchProduct.SearchProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(searchProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SearchProductExists(searchProduct.SearchProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(searchProduct);
        }

        // GET: SearchProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SerachProducts == null)
            {
                return NotFound();
            }

            var searchProduct = await _context.SerachProducts
                .FirstOrDefaultAsync(m => m.SearchProductId == id);
            if (searchProduct == null)
            {
                return NotFound();
            }

            return View(searchProduct);
        }

        // POST: SearchProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SerachProducts == null)
            {
                return Problem("Entity set 'AppDbContext.SerachProducts'  is null.");
            }
            var searchProduct = await _context.SerachProducts.FindAsync(id);
            if (searchProduct != null)
            {
                _context.SerachProducts.Remove(searchProduct);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SearchProductExists(int id)
        {
          return _context.SerachProducts.Any(e => e.SearchProductId == id);
        }
    }
}
