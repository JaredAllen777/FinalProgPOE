﻿// <auto-generated />
using ContractPoe.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ContractPoe.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ContractPoe.Models.LecturerClaim", b =>
                {
                    b.Property<int>("ClaimId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClaimId"));

                    b.Property<string>("AdditionalNotes")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("DocumentPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("HourlyRate")
                        .HasColumnType("float");

                    b.Property<double>("HoursWorked")
                        .HasColumnType("float");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit");

                    b.HasKey("ClaimId");

                    b.ToTable("LecturerClaims");
                });
#pragma warning restore 612, 618
        }
    }
}
