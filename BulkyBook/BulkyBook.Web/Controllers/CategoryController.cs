using BulkyBook.Web.Data;
using BulkyBook.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _context.Categories;
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
            _context.Categories.Add(category);

            // Save the new category to database
            _context.SaveChanges();

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
            var categoryFind = _context.Categories.Find(id);
            //var categoryFirst = _context.Categories.FirstOrDefault(c => c.Id == id);
            //var categorySingle = _context.Categories.SingleOrDefault(c => c.Id == id);

            if (categoryFind == null)
            {
                return NotFound();
            }

            return View(categoryFind);
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
            _context.Categories.Update(category);

            // Save the update category to database
            _context.SaveChanges();

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
            var category = _context.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            // Find the category by id
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            // Remove category
            _context.Categories.Remove(category);

            // Save the removed category to database
            _context.SaveChanges();

            // Add information message
            TempData["Success"] = "Category deleted successfully.";

            // Redirect to Index action (Category list)
            return RedirectToAction("Index");
        }
    }
}
