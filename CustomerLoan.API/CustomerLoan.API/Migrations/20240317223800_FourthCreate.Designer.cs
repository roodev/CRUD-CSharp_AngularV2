﻿// <auto-generated />
using System;
using CustomerLoan.API.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CustomerLoan.API.Migrations
{
    [DbContext(typeof(CustomerLoanContext))]
    [Migration("20240317223800_FourthCreate")]
    partial class FourthCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.17")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CustomerLoan.API.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("CustomerLoan.API.Models.Loan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<decimal>("ConversionRate")
                        .HasColumnType("numeric");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("LoanDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Loans");
                });

            modelBuilder.Entity("CustomerLoan.API.Models.Loan", b =>
                {
                    b.HasOne("CustomerLoan.API.Models.Customer", "Customer")
                        .WithMany("Loans")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("CustomerLoan.API.Models.Customer", b =>
                {
                    b.Navigation("Loans");
                });
#pragma warning restore 612, 618
        }
    }
}
