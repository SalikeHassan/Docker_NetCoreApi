using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TestProject.Domain.Entities;

namespace TestProject.Infrastrcuture.EntityConfiguration
{
    internal class BlogAuthorXrefEntityConfiguration : IEntityTypeConfiguration<BlogAuthorXref>
    {
        public void Configure(EntityTypeBuilder<BlogAuthorXref> builder)
        {
            builder.HasOne(x => x.Author)
                  .WithMany(x => x.BlogAuthorXrefs)
                  .HasForeignKey(x => x.AuthorId);

            builder.HasOne(x => x.Blog)
                  .WithMany(x => x.BlogAuthorXrefs)
                  .HasForeignKey(x => x.BlogId);

            builder.ToTable("BlogAuthorXref", "blog");
        }
    }
}
