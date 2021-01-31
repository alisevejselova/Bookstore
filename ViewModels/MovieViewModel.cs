using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Bookstore.Models;

namespace Bookstore.ViewModels
{
    public class MovieViewModel
    {

        public int MovieID { get; set; }
        public string Title { get; set; }
        public int BookId { get; set; }
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }
        public string Synopsis { get; set; }
        public string Trailer { get; set; }
        public string Rating { get; set; }
        [ForeignKey("BookId")]
        public Book Book { get; set; } 
        public IFormFile Picture { get; set; }
    }
}
