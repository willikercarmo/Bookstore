using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _context;

        public CoverTypeController(IUnitOfWork context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var coverTypes = _context.CoverType.GetAll();
            return View(coverTypes);
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType coverType)
        {
            // Validate if the object is valid 
            if (!ModelState.IsValid) return View(coverType);

            // Add the new Cover Type
            _context.CoverType.Add(coverType);

            // Save the new Cover Type to database
            _context.Save();

            // Add information message
            TempData["Success"] = "Cover Type created successfully.";

            // Redirect to Index action (Cover Type list)
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

            // Get CoverType by id from database
            var coverType = _context.CoverType.GetFirstOrDefault(c => c.Id == id);

            if (coverType == null)
            {
                return NotFound();
            }

            return View(coverType);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType coverType)
        {
            // Validate if the object is valid 
            if (!ModelState.IsValid) return View(coverType);

            // Update the new Cover Type
            _context.CoverType.Update(coverType);

            // Save the update Cover Type to database
            _context.Save();

            // Add information message
            TempData["Success"] = "Cover Type updated successfully.";

            // Redirect to Index action (Cover Type list)
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

            // Get Cover Type by id from database
            var coverType = _context.CoverType.GetFirstOrDefault(c => c.Id == id);

            if (coverType == null)
            {
                return NotFound();
            }

            return View(coverType);
        }

        // POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            // Find the Cover Type by id
            var coverType = _context.CoverType.GetFirstOrDefault(c => c.Id == id);
            if (coverType == null)
            {
                return NotFound();
            }
            // Remove Cover Type
            _context.CoverType.Remove(coverType);

            // Save the removed Cover Type to database
            _context.Save();

            // Add information message
            TempData["Success"] = "Cover Type deleted successfully.";

            // Redirect to Index action (Cover Type list)
            return RedirectToAction("Index");
        }
    }
}
