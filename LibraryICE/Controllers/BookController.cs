using LibraryICE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LibraryICE.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string searchTerm, string selectedType)
        {
            var books = _context.Book.Include(b => b.Type).AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                books = books.Where(b => b.Title.Contains(searchTerm));
            }

            if (!string.IsNullOrEmpty(selectedType))
            {
                books = books.Where(b => b.Type.Type == selectedType);
            }

            var bookTypes = await _context.BookType
                .Select(bt => bt.Type)
                .Distinct()
                .ToListAsync();

            ViewBag.BookTypes = new SelectList(bookTypes);

            return View(await books.ToListAsync());
        }

        public async Task<IActionResult> Create()
        {
            var types = await _context.BookType.ToListAsync();
            ViewBag.BookTypes = new SelectList(types, "TypeID", "Type");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var types = await _context.BookType.ToListAsync();
            ViewBag.BookTypes = new SelectList(types, "TypeID", "Type", book.TypeID);
            return View(book);
        }
    }
}
