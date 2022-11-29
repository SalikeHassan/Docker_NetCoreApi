using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TestProject.Domain.Entities;

namespace TestProject.Infrastrcuture.EntityConfiguration
{
    internal class ReaderCommentEntityConfiguration : IEntityTypeConfiguration<ReaderComment>
    {
        public void Configure(EntityTypeBuilder<ReaderComment> builder)
        {
            builder.Property(x => x.Comment)
                .HasColumnType("nvarchar(MAX)");

            builder.ToTable("ReaderComment", "blog");
        }
    }
}
