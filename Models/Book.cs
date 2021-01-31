using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;


namespace Bookstore.Models
{
    public class Book
    {

        [Display(Name = "Слика")]
        public string Picture { get; set; }
        [Required]
        [Key]
        public int BooksID { get; set; }
        public int Authorid { get; set; }
        [Display(Name = "Наслов")]
        public string Title { get; set; }
        [Display(Name = "Оригинален наслов")]
        public string OriginalTitle { get; set; }
        [Display(Name = "Жанр")]
        public string Genre { get; set; }
        [Display(Name = "Краток опис")]
        public string Synopsis { get; set; }
        [Display(Name = "Број на страни")]
        public int NumberofPages { get; set; }
        [Display(Name = "Цена")]
        public int Price { get; set; }
        [Display(Name = "Оригинална дата на излегување")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        [Display(Name = "Издавачка куќа")]
        public string Publisher { get; set; }
        [ForeignKey("Authorid")]
        [Display(Name = "Автор")]
        public Author Author {get; set;}
        public ICollection<Movie> Movie { get; set; }
        public ICollection<User> Users { get; set; }
       
    }
}