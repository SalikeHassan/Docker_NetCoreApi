﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestProject.ZipPay.Infrastructure.Context;

#nullable disable

namespace TestProject.ZipPay.Infrastructure.Migrations
{
    [DbContext(typeof(ZipPayContext))]
    [Migration("20221126114336_ZippayTables")]
    partial class ZippayTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TestProject.ZipPay.Domain.Entities.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("AccountCreateDateTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("AccountType")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTimeOffset>("CreateDateTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<DateTimeOffset?>("UpdateDateTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Account", "credit");
                });

            modelBuilder.Entity("TestProject.ZipPay.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("CreateDateTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("EmailId")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)");

                    b.Property<decimal>("Expense")
                        .HasColumnType("money");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(12)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<decimal>("Salary")
                        .HasColumnType("money");

                    b.Property<DateTimeOffset?>("UpdateDateTime")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("EmailId")
                        .IsUnique()
                        .HasDatabaseName("NonClusterIndex_Email");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("EmailId"), false);

                    b.ToTable("User", "credit");
                });

            modelBuilder.Entity("TestProject.ZipPay.Domain.Entities.Account", b =>
                {
                    b.HasOne("TestProject.ZipPay.Domain.Entities.User", "User")
                        .WithOne("Account")
                        .HasForeignKey("TestProject.ZipPay.Domain.Entities.Account", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_User_Account_UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TestProject.ZipPay.Domain.Entities.User", b =>
                {
                    b.Navigation("Account")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
