using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookstore.Models
{
    public class Author
    {

        [Display(Name = "Слика")]
        public string Picture { get; set; }
        [Required]
        [Key]
        public int AuthorID { get; set; }
        [Display(Name = "Име")]
        public string FirstName { get; set; }
        [Display(Name = "Презиме")]
        public string LastName { get; set; }
        [Display(Name = "Дата на раѓање")]
        [DataType(DataType.Date)]
        public DateTime DateofBirth { get; set; }
        [Display(Name = "Дата на умирање")]
        [DataType(DataType.Date)]
        public DateTime? DateofDeath { get; set; }
        [Display(Name = "Биографија")]
        public string Biography { get; set; }
        [Display(Name = "Награди")]
        public string Rewards { get; set; }
        [Display(Name = "Книга")]
        public string Books {get; set;}
        
        [Display(Name = "Име и презиме")]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
       
       public ICollection<Book> Book {get; set;}
    }
}