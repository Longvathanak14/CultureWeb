using CultureWeb.Data;
using CultureWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CultureWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin , SuperUser")]
    public class ProductPricesController : Controller
    {
        private ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductPricesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public ViewResult List(string search)
        {
            var model = from m in _context.ProductPrices.Include(c => c.Products) select m;

            if (!string.IsNullOrEmpty(search))
            {
                model = model.Where(s => s.Products.Name!.Contains(search) || s.Products.Name_kh!.Contains(search));

            }
            return View("Index", model);
        }

        public IActionResult Index()
        {
            return View(_context.ProductPrices.Include(c => c.Products).OrderByDescending(p => p.Id).ToList());
        }

      
        //Get Create method
        [HttpGet]
        public IActionResult Create()
        {
            // Get a list of products that are not associated with any product price
            var availableProducts = _context.Products
                .Where(p => !_context.ProductPrices.Any(pp => pp.ProductId == p.Id))
                .ToList();

            ViewData["ProId"] = new SelectList(availableProducts, "Id", "Name");
            ViewData["ProId_kh"] = new SelectList(availableProducts, "Id", "Name_kh");

            return View();
        }

        //Post Create method
        [HttpPost]
        public IActionResult Create(ProductPrice productPrice)
        {
           
            // Check if the selected product is already associated with a product price
            if (_context.ProductPrices.Any(pp => pp.ProductId == productPrice.ProductId))
            {
                ViewBag.message = "This product is already associated with a product price.";
                ViewData["ProId"] = new SelectList(_context.Products.ToList(), "Id", "Name");
                ViewData["ProId_kh"] = new SelectList(_context.Products.ToList(), "Id", "Name_kh");
                return View(productPrice);
            }

            _context.ProductPrices.Add(productPrice);
            _context.SaveChanges();
            TempData["StatusMessage"] = "YourCreatedSuccessfully";

            return RedirectToAction(nameof(Index));
           

            // If the model state is not valid, return to the create view with errors
            ViewData["ProId"] = new SelectList(_context.Products.ToList(), "Id", "Name");
            ViewData["ProId_kh"] = new SelectList(_context.Products.ToList(), "Id", "Name_kh");

            return View(productPrice);
        }

        //GET Edit Action Method

        public ActionResult Edit(int? id)
        {
            ViewData["ProId"] = new SelectList(_context.Products.ToList(), "Id", "Name");
            ViewData["ProId_kh"] = new SelectList(_context.Products.ToList(), "Id", "Name_kh");

            if (id == null)
            {
                return NotFound();
            }

            var productPrices = _context.ProductPrices.Include(c => c.Products)
                .FirstOrDefault(c => c.Id == id);
            if (productPrices == null)
            {
                return NotFound();
            }
            return View(productPrices);
        }

        //POST Edit Action Method
        [HttpPost]
        public IActionResult Edit(ProductPrice productPrice)
        {
           
            _context.ProductPrices.Update(productPrice);
            _context.SaveChanges();
            TempData["StatusMessage"] = "YourEditedSuccessfully";

            return RedirectToAction(nameof(Index));         

        }

        //GET Details Action Method
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var productPrice = _context.ProductPrices
                                .Include(c => c.Products)
                                .ThenInclude(c => c.PurchaseDetails)
                                .FirstOrDefault(c => c.Id == id);

            if (productPrice == null)
            {
                return NotFound();
            }
            return View(productPrice);
        }

        //GET Delete Action Method
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productPrice = _context.ProductPrices.Include(c => c.Products).Where(c => c.Id == id).FirstOrDefault();
            if (productPrice == null)
            {
                return NotFound();
            }
            return View(productPrice);
        }

        //POST Delete Action Method

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productPrice = _context.ProductPrices.FirstOrDefault(c => c.Id == id);
            if (productPrice == null)
            {
                return NotFound();
            }

            _context.ProductPrices.Remove(productPrice);
            _context.SaveChanges();
            TempData["StatusMessage"] = "YourDeletedSuccessfully";


            return RedirectToAction(nameof(Index));
        }
    }
}
