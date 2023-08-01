﻿// <auto-generated />
using LoanAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LoanAPI.Migrations
{
    [DbContext(typeof(LoanAPIContext))]
    [Migration("20230729093622_sql")]
    partial class sql
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.HasSequence("AccountNumberSequence")
                .StartsAt(20089080L);

            modelBuilder.Entity("LoanAPI.Models.LoanApplied", b =>
                {
                    b.Property<long>("LoanAppliedID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValueSql("NEXT VALUE FOR AccountNumberSequence");

                    b.Property<double>("AmountToPay")
                        .HasColumnType("float");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Interest")
                        .HasColumnType("float");

                    b.Property<double>("LoanAmount")
                        .HasColumnType("float");

                    b.Property<string>("LoanStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LoanType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Tenure")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LoanAppliedID");

                    b.ToTable("LoansApplied");
                });

            modelBuilder.Entity("LoanAPI.Models.LoanOffered", b =>
                {
                    b.Property<int>("LoanOfferedID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LoanOfferedID"), 1L, 1);

                    b.Property<double>("Interest")
                        .HasColumnType("float");

                    b.Property<string>("LoanType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("image")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LoanOfferedID");

                    b.ToTable("LoansOffered");
                });
#pragma warning restore 612, 618
        }
    }
}