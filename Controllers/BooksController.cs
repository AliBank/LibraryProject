using LibraryAppWeb.Data;
using LibraryAppWeb.Models;
using LibraryAppWeb.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryAppWeb.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext dBContext;

        public BooksController(ApplicationDbContext dbContext) 
        {
            this.dBContext = dbContext;
        }    
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBookViewModel viewModel)
        {
            if (viewModel.PublishedYear > DateTime.Now.Year)
            {
                throw new InvalidOperationException("Published year cannot be greater than the current year.");
            }

            var book = new Book() 
            {  Title = viewModel.Title,
               Author = viewModel.Author,
               PublishedYear = viewModel.PublishedYear,
               Genre = viewModel.Genre
            };

            await dBContext.Books.AddAsync(book);
            await dBContext.SaveChangesAsync();

            return RedirectToAction("List", "Books");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var books = await dBContext.Books.ToListAsync();

            return View(books);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var book = await dBContext.Books.FindAsync(id);

            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Book viewModel)
        {
           var book = await dBContext.Books.FindAsync(viewModel.BookId);

            if(book is not null)
            {
                book.Title = viewModel.Title;   
                book.Author = viewModel.Author;
                book.PublishedYear = viewModel.PublishedYear;
                book.Genre = viewModel.Genre;

                await dBContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Books");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var book = await dBContext.Books.AsNoTracking().FirstOrDefaultAsync(x => x.BookId == id);

            if(book is not null)
            {
                dBContext.Books.Remove(book);
                await dBContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Books");
        }
    }
}
