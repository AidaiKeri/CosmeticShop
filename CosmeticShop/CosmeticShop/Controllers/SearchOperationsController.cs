using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CosmeticShop.Model.Context;
using CosmeticShop.Model.Entities;

namespace CosmeticShop.WebApp.Controllers
{
    public class SearchOperationsController : Controller
    {
        private readonly AppDbContext _context;

        public SearchOperationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: SearchOperations
        public async Task<IActionResult> Index()
        {
              return View(await _context.SearchOperations.ToListAsync());
        }

        // GET: SearchOperations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SearchOperations == null)
            {
                return NotFound();
            }

            var searchOperation = await _context.SearchOperations
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (searchOperation == null)
            {
                return NotFound();
            }

            return View(searchOperation);
        }

        // GET: SearchOperations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SearchOperations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,Name,LastName,PhoneNumber,Email,IsSortByDateRequired,IsOrderActive")] SearchOperation searchOperation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(searchOperation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(searchOperation);
        }

        // GET: SearchOperations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SearchOperations == null)
            {
                return NotFound();
            }

            var searchOperation = await _context.SearchOperations.FindAsync(id);
            if (searchOperation == null)
            {
                return NotFound();
            }
            return View(searchOperation);
        }

        // POST: SearchOperations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,Name,LastName,PhoneNumber,Email,IsSortByDateRequired,IsOrderActive")] SearchOperation searchOperation)
        {
            if (id != searchOperation.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(searchOperation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SearchOperationExists(searchOperation.OrderId))
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
            return View(searchOperation);
        }

        // GET: SearchOperations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SearchOperations == null)
            {
                return NotFound();
            }

            var searchOperation = await _context.SearchOperations
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (searchOperation == null)
            {
                return NotFound();
            }

            return View(searchOperation);
        }

        // POST: SearchOperations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SearchOperations == null)
            {
                return Problem("Entity set 'AppDbContext.SearchOperations'  is null.");
            }
            var searchOperation = await _context.SearchOperations.FindAsync(id);
            if (searchOperation != null)
            {
                _context.SearchOperations.Remove(searchOperation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SearchOperationExists(int id)
        {
          return _context.SearchOperations.Any(e => e.OrderId == id);
        }
    }
}
