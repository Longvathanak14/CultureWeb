
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using CultureWeb.Data;
using CultureWeb.Models;
using System.Data;
using Microsoft.AspNetCore.Localization;

namespace CultureWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin , SuperUser")]
    public class DashboardController : Controller
    {

        private ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public DashboardController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Dashboard()
        {
            decimal totalOrderAmount = CalculateTotalOrderAmount();
            decimal totalPurchaseAmount = CalculateTotalPurchaseAmount();
            int productCount = _context.Products.Count();
            int blogCount = _context.Blogs.Count();
            int userCount = _context.ApplicationUsers.Count();
            int OrderCount = _context.Orders.Count();

            // Pass  to the view using ViewBag
            ViewBag.ProductCount = productCount;
            ViewBag.BlogCount = blogCount;
            ViewBag.UserCount = userCount;
            ViewBag.OrderCount = OrderCount;
            ViewBag.TotalAmount = totalOrderAmount;
            ViewBag.TotalPurchaseAmount = totalPurchaseAmount;


            return View();
        }
        public decimal CalculateTotalOrderAmount()
        {
            decimal totalAmount = _context.Orders
                .SelectMany(order => order.OrderDetails)
                .Sum(orderDetail => orderDetail.Product.Price);

            return totalAmount;
        }

        public decimal CalculateTotalPurchaseAmount()
        {
            decimal totalAmount = _context.Purchases
                .SelectMany(order => order.PurchaseDetails)
                .Sum(orderDetail => orderDetail.CostPrice*orderDetail.QtyPurchase);

            return totalAmount;
        }

        //ChangeLanguage
        [HttpPost]
        public IActionResult ChangeLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions { Expires = DateTimeOffset.Now.AddDays(1) });
            return LocalRedirect(returnUrl);
        }

    }
}
