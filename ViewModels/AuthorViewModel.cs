using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Bookstore.Models;

namespace Bookstore.ViewModels
{
    public class AuthorViewModel
    {

         public int AuthorID { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Date of Birth")]
        public DateTime DateofBirth { get; set; }
        [Display(Name = "Date of Death")]
        public DateTime? DateofDeath { get; set; }
        public string Biography { get; set; }
        public string Rewards { get; set; }
        
        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
        public ICollection<Book> Book {get; set;}        
        public IFormFile Picture { get; set; }
        public string Books {get; set;}
    }
}
