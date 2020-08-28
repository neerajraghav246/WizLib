using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using WizLib_Model.Models;

namespace WizLib_DataAccess.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
        }

        //public DbSet<Category> Categories { get; set; }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Book> Books { get; set; }

        public DbSet<BookDetail> BookDetails { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }

        public DbSet<Fluent_BookDetail> Fluent_BookDetails { get; set; }
        public DbSet<Fluent_Book> Fluent_Books { get; set; }
        public DbSet<Fluent_Author> Fluent_Authors { get; set; }
        public DbSet<Fluent_Publisher> Fluent_Publishers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //we configure fluent api
            //category table configuration
            modelBuilder.Entity<Category>().ToTable("tbl_category");
            modelBuilder.Entity<Category>().Property(x => x.Name).HasColumnName("CategoryName");

            //composite key
            modelBuilder.Entity<BookAuthor>().HasKey(ba => new { ba.Author_Id, ba.Book_Id });

            //Fluent_BookDetail configuration
            modelBuilder.Entity<Fluent_BookDetail>().HasKey(x => x.BookDetail_Id);
            modelBuilder.Entity<Fluent_BookDetail>().Property(x => x.NumberOfChapters).IsRequired();

            //Fluent_Book configuration
            modelBuilder.Entity<Fluent_Book>().HasKey(x => x.Book_Id);
            modelBuilder.Entity<Fluent_Book>().Property(x => x.Title).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Fluent_Book>().Property(x => x.ISBN).IsRequired().HasMaxLength(15);
            modelBuilder.Entity<Fluent_Book>().Property(x => x.Price).IsRequired();

            //Fluent_Book configuration
            modelBuilder.Entity<Fluent_Author>().HasKey(x => x.Author_Id);
            modelBuilder.Entity<Fluent_Author>().Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Fluent_Author>().Property(x => x.LastName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Fluent_Author>().Property(x => x.Address).HasMaxLength(250);
            modelBuilder.Entity<Fluent_Author>().Ignore(x => x.FullName);

            //Fluent_Publisher configuration
            modelBuilder.Entity<Fluent_Publisher>().HasKey(x => x.Publisher_Id);
            modelBuilder.Entity<Fluent_Publisher>().Property(x => x.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Fluent_Publisher>().Property(x => x.Location).IsRequired().HasMaxLength(250);

            //base.OnModelCreating(modelBuilder);
        }
    }
}
