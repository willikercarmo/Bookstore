using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _context;

        public CategoryController(IUnitOfWork context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _context.Category.GetAll();
            return View(objCategoryList);
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            // Validate if Name field is equal to DisplayOrder field
            if (category.Name == category.DisplayOrder.ToString())
            {
                // Set a custom error message
                ModelState.AddModelError("CustomError", "The DisplayOrder cannot exactly match the Name.");
            }

            // Validate if the object is valid 
            if (!ModelState.IsValid) return View(category);

            // Add the new category
            _context.Category.Add(category);

            // Save the new category to database
            _context.Save();

            // Add information message
            TempData["Success"] = "Category created successfully.";

            // Redirect to Index action (Category list)
            return RedirectToAction("Index");
        }

        // GET
        public IActionResult Edit(int? id)
        {
            // Validate if id is null or zero
            if (id == null || id == 0)
            {
                return NotFound();
            }

            // Get category by id from database
            // var categoryFind = _context.Categories.Find(id);
            var categoryFirst = _context.Category.GetFirstOrDefault(c => c.Id == id);
            //var categorySingle = _context.Categories.SingleOrDefault(c => c.Id == id);

            if (categoryFirst == null)
            {
                return NotFound();
            }

            return View(categoryFirst);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            // Validate if Name field is equal to DisplayOrder field
            if (category.Name == category.DisplayOrder.ToString())
            {
                // Set a custom error message
                ModelState.AddModelError("CustomError", "The DisplayOrder cannot exactly match the Name.");
            }

            // Validate if the object is valid 
            if (!ModelState.IsValid) return View(category);

            // Update the new category
            _context.Category.Update(category);

            // Save the update category to database
            _context.Save();

            // Add information message
            TempData["Success"] = "Category updated successfully.";

            // Redirect to Index action (Category list)
            return RedirectToAction("Index");
        }

        // GET
        public IActionResult Delete(int? id)
        {
            // Validate if id is null or zero
            if (id == null || id == 0)
            {
                return NotFound();
            }

            // Get category by id from database
            var categoryFirst = _context.Category.GetFirstOrDefault(c => c.Id == id);

            if (categoryFirst == null)
            {
                return NotFound();
            }

            return View(categoryFirst);
        }

        // POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            // Find the category by id
            var categoryFirst = _context.Category.GetFirstOrDefault(c => c.Id == id);
            if (categoryFirst == null)
            {
                return NotFound();
            }
            // Remove category
            _context.Category.Remove(categoryFirst);

            // Save the removed category to database
            _context.Save();

            // Add information message
            TempData["Success"] = "Category deleted successfully.";

            // Redirect to Index action (Category list)
            return RedirectToAction("Index");
        }
    }
}
