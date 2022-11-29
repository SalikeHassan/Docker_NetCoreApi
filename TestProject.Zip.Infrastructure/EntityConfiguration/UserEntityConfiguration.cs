using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestProject.ZipPay.Domain.Entities;

namespace TestProject.ZipPay.Infrastructure.EntityConfiguration
{
    /// <summary>
    /// Class defines the schema of table using entity class and fluent api methods
    /// </summary>
    internal class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.FirstName)
                 .HasColumnType("nvarchar(250)")
                 .IsRequired();

            builder.Property(x => x.MiddleName)
                .HasColumnType("nvarchar(250)");

            builder.Property(x => x.LastName)
                .HasColumnType("nvarchar(250)")
                .IsRequired();

            builder.Property(x => x.EmailId)
                .HasColumnType("nvarchar(500)")
                .IsRequired();

            //Non cluster unique index created over email id
            //Column value use frequently for email id validation
            builder.HasIndex(x=>x.EmailId)
                    .IsUnique()
                    .IsClustered(false)
                    .HasDatabaseName("NonClusterIndex_Email");

            builder.Property(x => x.Gender)
                .HasColumnType("nvarchar(12)")
                .IsRequired();

            builder.Property(x => x.Salary)
                .HasColumnType("money")
                .IsRequired();

            builder.Property(x => x.Expense)
                .HasColumnType("money")
                .IsRequired();

            builder.ToTable("User", "credit");
        }
    }
}
