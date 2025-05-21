using LibraryICE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LibraryICE.Controllers
{
    public class LoanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoanController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string searchTerm, string selectedType)
        {
            var loans = _context.Loan.Include(l => l.Book).ThenInclude(b => b.Type).AsQueryable();

            // Filtering by Book Title (search)
            if (!string.IsNullOrEmpty(searchTerm))
            {
                loans = loans.Where(l => l.Book.Title.Contains(searchTerm));
            }

            // Filtering by Book Type classification
            if (!string.IsNullOrEmpty(selectedType))
            {
                loans = loans.Where(l => l.Book.Type.Type == selectedType);
            }

            // Get distinct BookTypes for dropdown
            var bookTypes = await _context.BookType.Select(bt => bt.Type).Distinct().ToListAsync();
            ViewBag.BookTypes = new SelectList(bookTypes);

            return View(await loans.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["Books"] = _context.Book.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Loan loan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Books"] = _context.Book.ToList();
            return View(loan);
        }
    }
}
