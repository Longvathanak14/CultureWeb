using CultureWeb;
using Grpc.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CultureWeb.Areas.Admin.Models;

using CultureWeb.Data;
using CultureWeb.Models;
using CultureWeb.Utility;
using System.Net;
using X.PagedList;
using Newtonsoft.Json.Schema;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace CultureWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin , SuperUser")]
    public class PurchasesController : Controller
    {
        private ApplicationDbContext _db;
        public PurchasesController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var purchase = _db.Purchases.OrderByDescending(o => o.Id).ToList();
            return View(_db.Purchases.Include(o => o.User).OrderByDescending(o => o.Id).ToList());
        }


        //Get Create purchase method
        //[HttpGet]
        //public IActionResult Create()
        //{
            
        //    ViewBag.Suppliers = _db.Suppliers.ToList();
        //    ViewBag.Purchase = _db.PurchaseDetails.ToList();
        //    ViewBag.Products = _db.Products.ToList();
        //    ViewBag.ProductCount = ViewBag.Products.Count;

        //    List<Products> products = HttpContext.Session.Get<List<Products>>("products");
        //    if (products == null)
        //    {
        //        products = new List<Products>();
        //    }
        //    //var productInPurchase = products.FirstOrDefault(c => c.Id == _db.Products.Id);
        //    //ViewBag.ProductInPurchase = productInPurchase;

        //    Dictionary<int, int> productQuantities = new Dictionary<int, int>();

        //    foreach (var product in products)
        //    {
        //        if (productQuantities.ContainsKey(product.Id))
        //        {
        //            productQuantities[product.Id] += 1; // Increment quantity for existing product
        //        }
        //        else
        //        {
        //            productQuantities[product.Id] = 1; // Initialize quantity for new product
        //        }
        //    }

        //    List<Products> aggregatedProducts = new List<Products>();
        //    foreach (var kvp in productQuantities)
        //    {
        //        int productId = kvp.Key;
        //        int quantity = kvp.Value;

        //        // Find the first occurrence of the product in the list
        //        var product = products.FirstOrDefault(p => p.Id == productId);

        //        if (product != null)
        //        {
        //            product.Qty = quantity; // Set the aggregated quantity

        //            // Check if the product is already added to the aggregated list
        //            if (!aggregatedProducts.Any(p => p.Id == productId))
        //            {
        //                aggregatedProducts.Add(product);
        //            }
        //        }
        //    }
        //        return View(aggregatedProducts);
        //} 

        [HttpGet]
        public IActionResult Create()
        {
            
            ViewBag.Suppliers = _db.Suppliers.ToList();
            ViewBag.Purchase = _db.PurchaseDetails.ToList();
            ViewBag.Products = _db.Products.ToList();
            ViewBag.ProductCount = ViewBag.Products.Count;

            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if (products == null)
            {
                products = new List<Products>();
            }
            return View(products);
        }

        // POST: Create purchase
        //[HttpPost]
        //public IActionResult Create(Purchase purchase)
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        //    ViewBag.Suppliers = _db.Suppliers.ToList();
        //    ViewBag.Purchase = _db.PurchaseDetails.ToList();
        //    ViewBag.Products = _db.Products.ToList();
        //    ViewBag.ProductCount = ViewBag.Products.Count;
           
        //    List<Products> products = HttpContext.Session.Get<List<Products>>("products");
        //    if (products != null)
        //    {

        //        Dictionary<int, int> productQuantities = new Dictionary<int, int>();

        //        foreach (var product in products)
        //        {
        //            if (productQuantities.ContainsKey(product.Id))
        //            {
        //                productQuantities[product.Id] += 1; // Increment quantity for existing product
        //            }
        //            else
        //            {
        //                productQuantities[product.Id] = 1; // Initialize quantity for new product
        //            }
        //        }

        //        //List<Products> aggregatedProducts = new List<Products>();
        //        foreach (var kvp in productQuantities)
        //        {

        //            int productId = kvp.Key;
        //            int quantity = kvp.Value;
        //            var product = products.FirstOrDefault(p => p.Id == productId);
        //            if (product != null)
        //            {

        //                PurchaseDetail purchaseDetails = new PurchaseDetail();
        //                purchaseDetails.PorductId = productId;
        //                purchaseDetails.QtyPurchase = quantity;
        //                // Initialize the PurchaseDetails property if it's null
        //                if (purchase.PurchaseDetails == null)
        //                {
        //                    purchase.PurchaseDetails = new List<PurchaseDetail>();
        //                }

        //                purchase.PurchaseDetails.Add(purchaseDetails);
        //                //anOrder.OrderDetails.Add(orderDetails);

        //                var databaseProduct = _db.Products.FirstOrDefault(p => p.Id == product.Id);
        //                if (databaseProduct != null)
        //                {
        //                    databaseProduct.Qty += quantity; // Subtract the ordered quantity
        //                    _db.Update(databaseProduct);
        //                }

        //            }


        //        }

        //    }

        //    purchase.UserId = userId;
        //     purchase.PurchaseNo = GetPurchaseNo();
        //     purchase.PurchaseDate = DateTime.Now;
        //    _db.Purchases.Add(purchase);
        //    _db.SaveChanges();
        //    HttpContext.Session.Set("products", new List<Products>());
        //    TempData["StatusMessage"] = "CreatedSuccessfully";
        //    return RedirectToAction(nameof(Index));
        //}
    
        [HttpPost]
        public IActionResult Create(Purchase purchase , int quantity ,int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ViewBag.Suppliers = _db.Suppliers.ToList();
            ViewBag.Purchase = _db.PurchaseDetails.ToList();
            ViewBag.Products = _db.Products.ToList();
            ViewBag.ProductCount = ViewBag.Products.Count;

            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if (products != null)
            {
                foreach (var product in products)
                {
                    //product.Qty = quantity;
                    PurchaseDetail purchaseDetails = new PurchaseDetail();
                    purchaseDetails.PorductId = productId;
                    purchaseDetails.QtyPurchase = quantity;
                    // Initialize the PurchaseDetails property if it's null
                    if (purchase.PurchaseDetails == null)
                    {
                        purchase.PurchaseDetails = new List<PurchaseDetail>();
                    }

                    purchase.PurchaseDetails.Add(purchaseDetails);
                    //anOrder.OrderDetails.Add(orderDetails);

                    var databaseProduct = _db.Products.FirstOrDefault(p => p.Id == product.Id);
                    if (databaseProduct != null)
                    {
                        databaseProduct.Qty += quantity; // Subtract the ordered quantity
                        _db.Update(databaseProduct);
                    }
                }
            }
            purchase.UserId = userId;
            purchase.PurchaseNo = GetPurchaseNo();
            purchase.PurchaseDate = DateTime.Now;
            _db.Purchases.Add(purchase);
            _db.SaveChanges();
            HttpContext.Session.Set("products", new List<Products>());
            TempData["StatusMessage"] = "CreatedSuccessfully";
            return RedirectToAction(nameof(Index));
          

           
        }

        // GET: purchase Details
        [HttpGet]
        public ActionResult Details(int id)
        {
            var purchase = _db.Purchases.Include(o => o.Suppliers)
                                        .Include(o => o.User)
                                        .Include(o => o.PurchaseDetails)
                                        .ThenInclude(p => p.Product)
                                        .FirstOrDefault(o => o.Id == id);
            return View(purchase);
        }

        // GET: purchase
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var purchase = _db.Purchases.Include(o => o.Suppliers)
                                      .Include(o => o.User)
                                      .Include(o => o.PurchaseDetails)
                                      .ThenInclude(p => p.Product)
                                      .FirstOrDefault(o => o.Id == id);
            return View(purchase);
        }

        // POST: purchase
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var purchase = _db.Purchases.FirstOrDefault(o => o.Id == id);
                if (purchase == null)
                {
                    return NotFound();
                }

                _db.Purchases.Remove(purchase);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public string GetPurchaseNo()
        {
            int rowCount = _db.Purchases.ToList().Count() + 1;
            return rowCount.ToString("000");
        }


        [HttpPost]
        public ActionResult AddToPurchase(int? id, int quantity)
        {

            if (id == null)
            {
                return NotFound();
            }

            var product = _db.Products.Include(c => c.SubCategories).FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                // Store the original quantity in the session
                HttpContext.Session.SetInt32($"originalQty_{id}", quantity);

                // Add the selected quantity of the product to the cart
                List<Products> products = HttpContext.Session.Get<List<Products>>("products");
                Products productInPurchase = null;
                if (products == null)
                {
                    products = new List<Products>();
                }
                else
                {
                    productInPurchase = products.FirstOrDefault(c => c.Id == id);
                    ViewBag.ProductInPurchase = productInPurchase;
                }
                            
                products.Add(product);

                HttpContext.Session.Set("products", products);

            }
            return Redirect(Request.Headers["Referer"].ToString());
        }

        [HttpPost]
        public IActionResult RemoveQty(int? id, int removeQuantity)
        {
            if (id == null)
            {
                return NotFound();
            }
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.Id == id);
                if (product != null)
                {
                    // reStore the original quantity in the session
                    //int originalQuantity = HttpContext.Session.GetInt32($"originalQty_{id}") ?? 1;
                    HttpContext.Session.SetInt32($"originalQty_{id}", removeQuantity);

                    // Remove the product from the cart
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }

            return RedirectToAction(nameof(Create));
        }

        [HttpPost]
        public IActionResult AddQty(int? id, int addQuantity)
        {
            if (id == null)
            {
                return NotFound();
            }
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.Id == id);
                if (product != null)
                {
                    // Store the original quantity in the session
                    //int originalQuantity = HttpContext.Session.GetInt32($"originalQty_{id}") ?? 1;
                    HttpContext.Session.SetInt32($"originalQty_{id}", addQuantity);

                    // Add the product
                    products.Add(product);
                    HttpContext.Session.Set("products", products);
                }
            }

            return Redirect(Request.Headers["Referer"].ToString());
        }

        //GET Remove from cart action method
        //[ActionName("Remove")]
        //public IActionResult RemoveFromPurchase(int? id)
        //{
        //    List<Products> products = HttpContext.Session.Get<List<Products>>("products");
        //    if (products != null)
        //    {
        //        var product = products.FirstOrDefault(c => c.Id == id);
        //        if (product != null)
        //        {
        //            // Get the original quantity and add it back to the product
        //            int originalQuantity = HttpContext.Session.GetInt32($"originalQty_{id}") ?? 1;
        //            product.Qty += originalQuantity;

        //            _db.Update(product);
        //            _db.SaveChanges();

        //            // Remove the product from the cart
        //            products.Remove(product);
        //            HttpContext.Session.Set("products", products);
        //        }
        //    }
        //    return RedirectToAction(nameof(Create));
        //}

        [HttpPost]
        public IActionResult RemoveAll(int? productId)
        {
            if (productId == null)
            {
                return NotFound();
            }

            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.Id == productId);
                if (product != null)
                {
                    // Remove the original quantity session data
                    HttpContext.Session.Remove($"originalQty_{productId}");

                    // Remove all quantities of the product from the cart
                    products.RemoveAll(p => p.Id == productId);

                    TempData["StatusMessage"] = "Successfully removed from cart.";
                    HttpContext.Session.Set("products", products);
                }
            }

            return Redirect(Request.Headers["Referer"].ToString());
        }


    }
}
