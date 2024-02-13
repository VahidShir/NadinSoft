﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NadinSoft.EntityFrameworkCore;

#nullable disable

namespace NadinSoft.EntityFrameworkCore.Migrations
{
    [DbContext(typeof(NadinSoftDbContext))]
    [Migration("20240213213213_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NadinSoft.Domain.Product", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<int>("IsAvailable")
                        .HasColumnType("int");

                    b.Property<string>("ManufactureEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ManufacturePhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly?>("ProduceDate")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("ManufactureEmail")
                        .IsUnique();

                    b.HasIndex("ProduceDate")
                        .IsUnique()
                        .HasFilter("[ProduceDate] IS NOT NULL");

                    b.ToTable("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
