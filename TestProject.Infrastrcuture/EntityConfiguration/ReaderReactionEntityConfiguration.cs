using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TestProject.Domain.Entities;

namespace TestProject.Infrastrcuture.EntityConfiguration
{
    internal class ReaderReactionEntityConfiguration : IEntityTypeConfiguration<ReaderReaction>
    {
        public void Configure(EntityTypeBuilder<ReaderReaction> builder)
        {
            builder.Property(x => x.LikeCount)
                .HasColumnType("int");

            builder.Property(x => x.DislikeCount)
                .HasColumnType("int");

            builder.HasOne(x => x.Blog)
                 .WithOne(x => x.ReaderReaction)
                 .HasForeignKey<ReaderReaction>(x => x.BlogId);

            builder.ToTable("ReaderReaction", "blog");
        }
    }
}
