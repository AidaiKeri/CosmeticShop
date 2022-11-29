using CosmeticShop.Model.Context;
using CosmeticShop.Model.Entities;
using CosmeticShop.WebApp.Data;
using CosmeticShop.WebApp.Data.Models;
using CosmeticShop.WebApp.Data.Specifications;
using CosmeticShop.WebApp.Views.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CosmeticShop.WebApp.Controllers
{
    public class ProductsController : Controller
    {
        //private readonly AppDbContext _context;

        //public ProductsController(AppDbContext context)
        //{
        //    _context = context;
        //}


        private readonly IProductsRepository _cosmeticProductsRepository;
        private readonly IRepository<Category> _categoriesRepository;
        private readonly ILogger _logger;

        public ProductsController(IProductsRepository products, IRepository<Category> category, ILogger<ProductsController> logger)
        {
            _cosmeticProductsRepository = products;
            _categoriesRepository = category;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Product(int id)
        {
            ViewBag.Title = "Продукт";
            _logger.LogInformation("Products\\Product is executed");
            var product = await _cosmeticProductsRepository.GetById(id);

            if (product is not null)
            {
                return View(product);
            }

            return NoContent();
        }

        public async Task<IActionResult> ViewProducts()
        {
            var viewModel = new ProductsViewModel
            {
                Products = await _cosmeticProductsRepository.GetAll(
                    new ProductSpecification().
                             IncludeCategory().
                             WithoutTracking()),

                ProductCategory = "Косметика",
                SearchParams = new SearchProductsParams()
            };

            _logger.LogInformation("Products\\ViewProducts is executed");

            var categories = await _categoriesRepository.GetAll();

            ViewBag.Categories = new SelectList(categories, "Id", "CategoryName");
            ViewBag.Title = "Вывод продуктов";

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Search(SearchProductsParams searchParams)
        {
            ViewBag.Title = "Поиск";
            var searchSpecification = new ProductSpecification().IncludeCategory();

            if (searchParams.StartPrice is not null && searchParams.EndPrice is not null)
            {
                searchSpecification.WhereInPriceRange(searchParams.StartPrice.Value, searchParams.EndPrice.Value);
            }

            var isPrimeKeyUsed = false;

            if (searchParams.SearchProductId is not null)
            {
                searchSpecification.WhereId(searchParams.SearchProductId.Value);
                isPrimeKeyUsed = true;
            }

            if (!isPrimeKeyUsed)
            {

                if (searchParams.Name is not null)
                {
                    searchSpecification.WhereName(searchParams.Name);
                }

                if (searchParams.IsSortByPriceRequired)
                {
                    searchSpecification.SortByPrice();
                }

                if (searchParams.CategoryId is not null)
                {
                    searchSpecification.WhereCategoryId(searchParams.CategoryId.Value);
                }
            }

            searchSpecification.WhereAvailable(searchParams.IsAvailable)
                               .WithoutTracking();

            ViewBag.Title = "Искомый товар";
            ViewBag.Categories = new SelectList(await _categoriesRepository.GetAll(), "Id", "CategoryName");

            var viewModel = new ProductsViewModel
            {
                Products = await _cosmeticProductsRepository.GetAll(searchSpecification),
                ProductCategory = "Косметика",
                SearchParams = new SearchProductsParams()
            };

            _logger.LogInformation("Products\\Search is executed");

            if (!viewModel.Products.Any())
            {
                _logger.LogWarning("Search unsuccesful!");
            }

            return View(viewModel);
        }

        [Authorize(Roles = IdentityRoleConstants.Admin)]
        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
            ViewBag.Title = "Добавление продукта";
            ViewBag.Categories = new SelectList(await _categoriesRepository.GetAll(), "Id", "CategoryName");

            return View();
        }

        [Authorize(Roles = IdentityRoleConstants.Admin)]
        [HttpPost]
        public async Task<IActionResult> AddProduct(Product newProduct)
        {
            newProduct.Price = Math.Round(newProduct.Price, 2);

            await _cosmeticProductsRepository.Add(newProduct);

            return RedirectToAction("ViewProducts", "Products");
        }

        [Authorize(Roles = IdentityRoleConstants.Admin)]
        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            ViewBag.Title = "Изменение продукта";
            var searchResult = await _cosmeticProductsRepository.GetById(id);

            if (searchResult is null)
            {
                return NoContent();
            }

            ViewBag.Categories = new SelectList(await _categoriesRepository.GetAll(), "Id", "CategoryName");

            return View(searchResult);
        }

        [Authorize(Roles = IdentityRoleConstants.Admin)]
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            product.Price = Math.Round(product.Price, 2);
            await _cosmeticProductsRepository.Update(product);

            return RedirectToAction("ViewProducts", "Products");
        }

        [Authorize(Roles = IdentityRoleConstants.Admin)]
        [HttpGet]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            ViewBag.Title = "Удаление продукта";
            var searchResult = await _cosmeticProductsRepository.GetById(id);

            if (searchResult is null)
            {
                return NoContent();
            }

            return View(searchResult);
        }

        [Authorize(Roles = IdentityRoleConstants.Admin)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int id, IFormCollection collection)
        {
            var product = await _cosmeticProductsRepository.GetById(id);
            await _cosmeticProductsRepository.Delete(product);

            return RedirectToAction("ViewProducts", "Products");
        }
        //// GET: Products
        //public async Task<IActionResult> Index()
        //{
        //    var appDbContext = _context.Product.Include(p => p.Category);
        //    return View(await appDbContext.ToListAsync());
        //}

        //// GET: Products/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.Product == null)
        //    {
        //        return NotFound();
        //    }

        //    var product = await _context.Product
        //        .Include(p => p.Category)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(product);
        //}

        //// GET: Products/Create
        //public IActionResult Create()
        //{
        //    ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName");
        //    return View();
        //}

        //// POST: Products/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Name,ShortDescription,LongDescription,ImageURL,Price,IsFavorite,IsAvailable,CategoryId,Id")] Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(product);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", product.CategoryId);
        //    return View(product);
        //}

        //// GET: Products/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.Product == null)
        //    {
        //        return NotFound();
        //    }

        //    var product = await _context.Product.FindAsync(id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", product.CategoryId);
        //    return View(product);
        //}

        //// POST: Products/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Name,ShortDescription,LongDescription,ImageURL,Price,IsFavorite,IsAvailable,CategoryId,Id")] Product product)
        //{
        //    if (id != product.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(product);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ProductExists(product.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", product.CategoryId);
        //    return View(product);
        //}

        //// GET: Products/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Product == null)
        //    {
        //        return NotFound();
        //    }

        //    var product = await _context.Product
        //        .Include(p => p.Category)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(product);
        //}

        //// POST: Products/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Product == null)
        //    {
        //        return Problem("Entity set 'AppDbContext.Product'  is null.");
        //    }
        //    var product = await _context.Product.FindAsync(id);
        //    if (product != null)
        //    {
        //        _context.Product.Remove(product);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool ProductExists(int id)
        //{
        //  return _context.Product.Any(e => e.Id == id);
        //}
    }
}
