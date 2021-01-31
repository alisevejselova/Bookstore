using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookstore.Models
{
    public class Movie
    {

        [Display(Name = "Слика")]
        public string Picture { get; set; }
        [Required]
        [Key]
        public int MovieID { get; set; }
        [Display(Name = "Наслов")]
        public string Title { get; set; }
        public int BookId { get; set; }
        [Display(Name = "Оригинална дата на излегување")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        [Display(Name = "Опис")]
        public string Synopsis { get; set; }
        public string Trailer { get; set; }
        [Display(Name = "Оцена")]
        public string Rating { get; set; }
        [ForeignKey("BookId")]
        [Display(Name = "Книга")]
        public Book Book { get; set; }
        
    }
}