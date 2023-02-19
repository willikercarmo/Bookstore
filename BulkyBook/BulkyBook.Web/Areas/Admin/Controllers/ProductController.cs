using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBook.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _context;

        public ProductController(IUnitOfWork context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET
        public IActionResult Upsert(int? id)
        {
            var product = new Product();
            var categoryList = _context.Category.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                });
            var coverTypeList = _context.CoverType.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                });

            if (id == null || id == 0)
            {
                // Create product
                ViewBag.CategoryList = categoryList;
                ViewData["CoverTypeList"] = coverTypeList;
                return View(product);
            }
            else
            {
                // Update product
            }

            return View(product);
        }
    }
}
