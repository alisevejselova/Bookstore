using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Bookstore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Bookstore.Data;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Bookstore.ViewModels;

namespace Bookstore.Controllers
{
  
    public class BookController : Controller
    {
        private readonly BookstoreContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BookController(BookstoreContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index(string searchString)
        {         
            ViewData["CurrentFilter"] = searchString;
           
            var books = from b in _context.Book
                   select b;
            if (!String.IsNullOrEmpty(searchString))
            {
                 books = books.Where(s => s.Title.Contains(searchString));
            }
          
         return View(await books.AsNoTracking().ToListAsync());
        }
        public async Task<IActionResult> Thriller()
        {         
          return View(await _context.Book.ToListAsync());
        }
          public async Task<IActionResult> Biography()
        {         
          return View(await _context.Book.ToListAsync());
        }
           public async Task<IActionResult> Classics()
        {         
          return View(await _context.Book.ToListAsync());
        }
           public async Task<IActionResult> Comedy()
        {         
          return View(await _context.Book.ToListAsync());
        }
           public async Task<IActionResult> Fantasy()
        {         
          return View(await _context.Book.ToListAsync());
        }
        public async Task<IActionResult> Mystery()
        {         
          return View(await _context.Book.ToListAsync());
        }
        public async Task<IActionResult> Mythology()
        {         
          return View(await _context.Book.ToListAsync());
        }
        public async Task<IActionResult> Novel()
        {         
          return View(await _context.Book.ToListAsync());
        }
        public async Task<IActionResult> Poetry()
        {         
          return View(await _context.Book.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
             return NotFound();
             }

            var book = await _context.Book
            .Include(s => s.Author)
            .AsNoTracking()
             .FirstOrDefaultAsync(m => m.BooksID == id);

            if (book == null)
             {
                 return NotFound();
             }

         return View(book);
        }
        // GET: Book/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            PopulateDropDownList();
            return View();
        }
         [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        /*       public async Task<IActionResult> Create([Bind("Title, OriginalTitle, Genre, Synopsis, NumberofPages, Picture, Price, ReleaseDate, Publisher, Movie, Author, Authorid")]Book book)
               {
                   if (ModelState.IsValid)
                   {

                       _context.Add(book);
                       await _context.SaveChangesAsync();
                       return RedirectToAction(nameof(Index));
                   } 
                 PopulateDropDownList(book.Authorid);          
                  return View();
               }
       */
        public async Task<IActionResult> Create(BookViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);

                Book book = new Book
                {
                    Picture = uniqueFileName,
                    Title = model.Title,
                    OriginalTitle = model.OriginalTitle,
                    Genre = model.Genre,
                    Synopsis = model.Synopsis,
                    NumberofPages = model.NumberofPages,
                    Price = model.Price,
                    ReleaseDate = model.ReleaseDate,
                    Publisher = model.Publisher,
                    Movie = model.Movie,
                    Authorid = model.Authorid
                };
                
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           
            return View();
        }

        // GET: Book/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

             BookViewModel vm = new BookViewModel
                {
                    BooksID = book.BooksID,
                    Title = book.Title,
                    OriginalTitle = book.OriginalTitle,
                    Genre = book.Genre,
                    Synopsis = book.Synopsis,
                    NumberofPages = book.NumberofPages,
                    Price=book.Price,
                    ReleaseDate=book.ReleaseDate,
                    Publisher = book.Publisher,
                    Movie= book.Movie,
                    Author=book.Author,
                    Authorid=book.Authorid
                };
            ViewData["Authorid"] = new SelectList(_context.Author, "Authorid", "FullName", book.Authorid);

            return View(vm);
        }

        // POST: Book/Edit/5
      
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditPost(int? id, BookViewModel vm)
        {
            if (id != vm.BooksID)
            {
                return NotFound();
            }

       if (ModelState.IsValid)
       {
           try
                {
                    string uniqueFileName = UploadedFile(vm);

               Book book = new Book
                {
                    BooksID = vm.BooksID,
                    Title = vm.Title,
                    OriginalTitle = vm.OriginalTitle,
                    Genre = vm.Genre,
                    Synopsis = vm.Synopsis,
                    NumberofPages = vm.NumberofPages,
                    Picture = uniqueFileName,
                    Price=vm.Price,
                    ReleaseDate=vm.ReleaseDate,
                    Publisher = vm.Publisher,
                    Movie= vm.Movie,
                    Author=vm.Author,
                    Authorid=vm.Authorid
                };

                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(vm.BooksID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
       }
             return View(vm);
          }

        

        private void PopulateDropDownList(object selectedauthor = null)
        {
            var authorQuery = from d in _context.Author
                                   orderby d.FirstName
                                   select d;
            ViewBag.Authorid = new SelectList(authorQuery.AsNoTracking(), "AuthorID", "FullName", selectedauthor);
        }


        // GET: Book/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(c => c.Author)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.BooksID == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Book.FindAsync(id);
            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
          [HttpPost]
     
        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.BooksID == id);
        }
          private string UploadedFile(BookViewModel model)
        {
            string uniqueFileName = null;

            if (model.Picture != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "pictures");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.Picture.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Picture.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
       
        
        public IActionResult Order(int id)
        {
            // TempData["ID"] = id;
            ViewData["BookID"] = _context.Book.Where(t => t.BooksID == id).Select(t => t.BooksID).FirstOrDefault();
            ViewData["BookName"] = _context.Book.Where(t => t.BooksID == id).Select(t => t.Title).FirstOrDefault();
            ViewData["BookPrice"] = _context.Book.Where(t => t.BooksID == id).Select(t => t.Price).FirstOrDefault();
            string imageURL = "~/pictures/" + _context.Book.Where(t => t.BooksID == id).Select(t => t.Picture).FirstOrDefault();
            ViewData["Image"] = imageURL;
            return View();
        } 
 
         [HttpPost]
           [ValidateAntiForgeryToken]
         public async Task<IActionResult> Order([Bind("FullName, PhoneNumber, Address, City,BooksID, BookTitle")] User order)
         {
            if (ModelState.IsValid)
             {
                 _context.Add(order);
                 await _context.SaveChangesAsync();
                 return RedirectToAction(nameof(Text));
             }

             return View();
         } 

        
        public IActionResult Text()
        {
            return View();
        }

        public IActionResult OrderedBooks()
            {
       
             IQueryable<User> orderitem = _context.User;
                 return View(orderitem);
        }

     
    

    }
}