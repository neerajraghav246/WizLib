using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WizLib_Model.Models;

namespace WizLib_DataAccess.FluentConfig
{
    public class FluentBookDetailConfig : IEntityTypeConfiguration<Fluent_BookDetail>
    {
        public void Configure(EntityTypeBuilder<Fluent_BookDetail> builder)
        {
            //Fluent_BookDetail configuration
            builder.HasKey(x => x.BookDetail_Id);
            builder.Property(x => x.NumberOfChapters).IsRequired();
        }
    }
}
