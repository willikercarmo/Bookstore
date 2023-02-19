using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBook.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(IUnitOfWork context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET
        public IActionResult Upsert(int? id)
        {
            var model = new ProductViewModel()
            {
                Product = new Product()
                {
                    Title = "Microsoft Word",
                    Author = "Bill Gates",
                    Description= "Description",
                    ISBN = "ISBN",
                    ListPrice = 10,
                    Price = 30,
                    Price50 = 25,
                    Price100 = 20,
                },
                CategoryList = _context.Category.GetAll()
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                CoverTypeList = _context.CoverType.GetAll()
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
            };

            if (id == null || id == 0)
            {
                // Create product
                //ViewBag.CategoryList = categoryList;
                //ViewData["CoverTypeList"] = coverTypeList;
                return View(model);
            }
            else
            {
                // Update product
            }

            return View(model);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductViewModel model, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString();
                    var upload = Path.Combine(wwwRootPath, @"images\products");
                    var extension = Path.GetExtension(file.FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    model.Product.ImageUrl = @"\images\products" + fileName + extension;
                }

                _context.Product.Add(model.Product);
                _context.Save();
                TempData["Success"] = "Product created successfully.";
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

    }
}
