using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Bookstore.Models;

namespace Bookstore.ViewModels
{
    public class BookViewModel
    {

          public int BooksID { get; set; }
        public int Authorid { get; set; }
        public string Title { get; set; }
        [Display(Name = "Original Title")]
        public string OriginalTitle { get; set; }
        public string Genre { get; set; }
        public string Synopsis { get; set; }
        [Display(Name = "Number of Pages")]
        public int NumberofPages { get; set; }
        public int Price { get; set; }
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }
        public string Publisher { get; set; }
        [ForeignKey("Authorid")]
        public Author Author {get; set;}
        public ICollection<Movie> Movie { get; set; }
        public ICollection<Author> Authors {get; set;}      
        public IFormFile Picture { get; set; }
    }
}
