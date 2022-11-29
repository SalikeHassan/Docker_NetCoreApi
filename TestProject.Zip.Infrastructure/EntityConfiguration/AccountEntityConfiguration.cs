using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestProject.ZipPay.Domain.Entities;

namespace TestProject.ZipPay.Infrastructure.EntityConfiguration
{
    /// <summary>
    /// Class defines the schema of table using entity class and fluent api methods
    /// </summary>
    internal class AccountEntityConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.Property(x => x.AccountNumber)
                 .HasColumnType("nvarchar(36)")
                 .IsRequired();

            builder.Property(x => x.AccountType)
                .HasColumnType("nvarchar(10)");

            //1-1 relation between user and account
            builder.HasOne(x => x.User)
              .WithOne(x => x.Account)
              .HasForeignKey<Account>(x => x.UserId)
              .HasConstraintName("FK_User_Account_UserId")
              .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Account", "credit");
        }
    }
}
