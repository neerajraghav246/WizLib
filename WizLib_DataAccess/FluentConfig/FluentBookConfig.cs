using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WizLib_Model.Models;

namespace WizLib_DataAccess.FluentConfig
{
    public class FluentBookConfig : IEntityTypeConfiguration<Fluent_Book>
    {
        public void Configure(EntityTypeBuilder<Fluent_Book> builder)
        {
            //Fluent_Book configuration
            builder.HasKey(x => x.Book_Id);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
            builder.Property(x => x.ISBN).IsRequired().HasMaxLength(15);
            builder.Property(x => x.Price).IsRequired();
            //one to one relationship b/w Fluent_Book and Fluent_BookDetail
            builder.HasOne(b => b.Fluent_BookDetail)
                .WithOne(b => b.Fluent_Book)
                .HasForeignKey<Fluent_Book>("BookDetail_Id");
            //one to many relationship b/w Fluent_Book and Fluent_Publisher
            builder.HasOne(p => p.Fluent_Publisher)
                .WithMany(b => b.Fluent_Books)
                .HasForeignKey(b => b.Publisher_Id);
        }
    }
}
