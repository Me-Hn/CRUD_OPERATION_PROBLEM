using CRUD_OPERATION.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRUD_OPERATION.Controllers
{
    public class ProductController : Controller
    {
        private readonly CRUD_OPERATION.Models.AppContext _context1;

        public ProductController(CRUD_OPERATION.Models.AppContext context1)
        {
            _context1 = context1;

        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            // Populate the dropdown list with categories
            ViewBag.CategoryId = new SelectList(_context1.categories.ToList(), "CategoryId", "Name");
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product pro)
        {
            
                // Add the product to the database
                await _context1.products.AddAsync(pro);
                await _context1.SaveChangesAsync(); // Use SaveChangesAsync for async operations
                //return RedirectToAction(nameof(Index)); // Redirect to the Index action after successful creation
                return View();
            

            // If we got this far, something failed; redisplay form with the current product data
            ViewBag.CategoryId = new SelectList(_context1.categories.ToList(), "CategoryId", "Name", pro.CategoryId);
            return View(pro);
        }

    }
}
