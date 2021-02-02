using Bookstore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Data
{
    public class BookstoreContext : IdentityDbContext<AppUser>
    {
        public BookstoreContext(DbContextOptions<BookstoreContext> options) : base(options)
        {
        }

        public DbSet<Book> Book { get; set; }
        public DbSet<Movie> Movie{ get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<AppUser> AppUser{ get; set; }
        public DbSet<User> User{ get; set; }
     

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().ToTable("Book")
              .HasOne(b => b.Author)
            .WithMany(i => i.Book)
            .HasForeignKey(b => b.Authorid).OnDelete(DeleteBehavior.NoAction);
            
             modelBuilder.Entity<Author>().ToTable("Author");
           

            modelBuilder.Entity<Movie>().ToTable("Movie")
              .HasOne(b => b.Book)
            .WithMany(i => i.Movie)
            .HasForeignKey(b => b.BookId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>().ToTable("User")
            .Property(e => e.UserID)
            .ValueGeneratedOnAdd();

            base.OnModelCreating(modelBuilder);
        }
    }
}