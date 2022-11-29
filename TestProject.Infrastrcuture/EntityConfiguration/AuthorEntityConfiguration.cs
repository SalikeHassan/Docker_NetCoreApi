using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TestProject.Domain.Entities;

namespace TestProject.Infrastrcuture.EntityConfiguration
{
    internal class AuthorEntityConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.Property(x => x.FirstName)
                .HasColumnType("nvarchar(300)")
                .IsRequired();

            builder.Property(x => x.FirstName)
                .HasColumnType("nvarchar(300)");

            builder.Property(x => x.LastName)
                .HasColumnType("nvarchar(300)")
                .IsRequired();

            builder.Property(x => x.Gender)
                .HasColumnType("varchar(11)")
                .IsRequired();

            builder.ToTable("Author", "blog");
        }
    }
}
