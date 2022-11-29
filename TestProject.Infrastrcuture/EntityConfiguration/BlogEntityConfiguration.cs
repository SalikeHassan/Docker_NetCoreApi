using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TestProject.Domain.Entities;

namespace TestProject.Infrastrcuture.EntityConfiguration
{
    internal class BlogEntityConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.Property(x => x.Title)
                .HasColumnType("nvarchar(300)")
                .IsRequired();

            builder.Property(x => x.Url)
                .HasColumnType("nvarchar(5000)")
                .IsRequired();

            builder.HasMany(x => x.ReaderComments)
                .WithOne(x => x.Blog)
                .HasForeignKey(x => x.BlogId);

            builder.ToTable("Blog", "blog");
        }
    }
}
