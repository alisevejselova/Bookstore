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
  
    public class AuthorController : Controller
    {
        private readonly BookstoreContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AuthorController(BookstoreContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["Filter"] = searchString;

            var authors = from a in _context.Author
                          select a;
            if (!String.IsNullOrEmpty(searchString))
            {
                 authors = authors.Where(s => s.FirstName.Contains(searchString)
                    || s.LastName.Contains(searchString));

               // authors = authors.Where(s => s.FullName.Contains(searchString));
            }
            return View(await authors.ToListAsync());
        }
   

        public async Task<IActionResult> Details(int? id)
{
    if (id == null)
    {
        return NotFound();
    }
    
      var author = await _context.Author
        .FirstOrDefaultAsync(m => m.AuthorID == id);

    if (author == null)
    {
        return NotFound();
    }

    return View(author);
}
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
        return View();
        }
         [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(AuthorViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);

                Author author = new Author
                {
                    Picture = uniqueFileName,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    DateofBirth = model.DateofBirth,
                    DateofDeath = model.DateofBirth,
                    Biography = model.Biography,                    
                    Rewards = model.Rewards,
                    Book=model.Book,
                    Books=model.Books
                };

                _context.Add(author);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }


        // GET: Author/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Author.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }

              AuthorViewModel vm = new AuthorViewModel
                {
                    AuthorID = author.AuthorID,
                    FirstName = author.FirstName,
                    LastName = author.LastName,
                    DateofBirth = author.DateofBirth,
                    DateofDeath = author.DateofBirth,
                    Biography = author.Biography,
                    Rewards = author.Rewards,
                    Book=author.Book
                };
            return View(vm);
        }

        // POST: Author/Edit/5
        [Authorize(Roles = "Admin")]

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        
            public async Task<IActionResult> EditPost(int? id, AuthorViewModel vm)
        {
            if (id != vm.AuthorID)
            {
                return NotFound();
            }

       if (ModelState.IsValid)
       {
           try
                {
                    string uniqueFileName = UploadedFile(vm);

               Author author = new Author
               {
                    AuthorID = vm.AuthorID,
                    FirstName = vm.FirstName,
                    LastName = vm.LastName,
                    DateofBirth = vm.DateofBirth,
                    DateofDeath = vm.DateofBirth,
                    Biography = vm.Biography,
                    Picture = uniqueFileName,
                    Rewards = vm.Rewards,
                    Book=vm.Book
                };

                    _context.Update(author);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(vm.AuthorID))
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

        private void PopulateDropDownList(object selectedBook = null)
        {
            var booksQuery = from d in _context.Book
                                   orderby d.Title
                                   select d;
            ViewBag.Books = new SelectList(booksQuery.AsNoTracking(), "BooksId", "Title", selectedBook);
        }


        // GET: Author/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Author
                .FirstOrDefaultAsync(m => m.AuthorID == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: Author/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var author = await _context.Author.FindAsync(id);
            _context.Author.Remove(author);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
          [HttpPost]
     
        private bool AuthorExists(int id)
        {
            return _context.Author.Any(e => e.AuthorID == id);
        }

         private string UploadedFile(AuthorViewModel model)
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
         public async Task<IActionResult> MyBooks(int id)
        {
            IQueryable<Book> book = _context.Book;

            book = book.Where(s=>s.Authorid==id);
            
            ViewData["AuthorsName"] = _context.Author.Where(t => t.AuthorID == id).Select(t => t.FullName).FirstOrDefault();
            return View(book);
        }
             
    }
}