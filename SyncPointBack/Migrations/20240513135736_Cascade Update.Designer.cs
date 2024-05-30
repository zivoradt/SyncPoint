﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SyncPointBack.Persistance;

#nullable disable

namespace SyncPointBack.Migrations
{
    [DbContext(typeof(SyncPointDb))]
    [Migration("20240513135736_Cascade Update")]
    partial class CascadeUpdate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SyncPointBack.Model.Excel.ExcelRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("NumOfChanges")
                        .HasColumnType("int");

                    b.Property<int?>("NumOfPages")
                        .HasColumnType("int");

                    b.Property<string>("Other")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ProductionTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TicketId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ExcelRecords");
                });

            modelBuilder.Entity("SyncPointBack.Model.Excel.GNB", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ExcelRecordId")
                        .HasColumnType("int");

                    b.Property<string>("gnb")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ExcelRecordId")
                        .IsUnique();

                    b.ToTable("GNB");
                });

            modelBuilder.Entity("SyncPointBack.Model.Excel.PDModification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ExcelRecordId")
                        .HasColumnType("int");

                    b.Property<string>("pdMOdification")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ExcelRecordId")
                        .IsUnique();

                    b.ToTable("PDModification");
                });

            modelBuilder.Entity("SyncPointBack.Model.Excel.PDRegistration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ExcelRecordId")
                        .HasColumnType("int");

                    b.Property<string>("pdRegistration")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ExcelRecordId")
                        .IsUnique();

                    b.ToTable("PDRegistration");
                });

            modelBuilder.Entity("SyncPointBack.Model.Excel.PIM", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ExcelRecordId")
                        .HasColumnType("int");

                    b.Property<string>("pim")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ExcelRecordId")
                        .IsUnique();

                    b.ToTable("PIM");
                });

            modelBuilder.Entity("SyncPointBack.Model.Excel.StaticPageCreation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ExcelRecordId")
                        .HasColumnType("int");

                    b.Property<string>("staticPageCreation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ExcelRecordId")
                        .IsUnique();

                    b.ToTable("StaticPageCreation");
                });

            modelBuilder.Entity("SyncPointBack.Model.Excel.StaticPageModification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ExcelRecordId")
                        .HasColumnType("int");

                    b.Property<string>("staticPageModification")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ExcelRecordId")
                        .IsUnique();

                    b.ToTable("StaticPageModification");
                });

            modelBuilder.Entity("SyncPointBack.Model.Excel.GNB", b =>
                {
                    b.HasOne("SyncPointBack.Model.Excel.ExcelRecord", "ExcelRecord")
                        .WithOne("GNB")
                        .HasForeignKey("SyncPointBack.Model.Excel.GNB", "ExcelRecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExcelRecord");
                });

            modelBuilder.Entity("SyncPointBack.Model.Excel.PDModification", b =>
                {
                    b.HasOne("SyncPointBack.Model.Excel.ExcelRecord", "ExcelRecord")
                        .WithOne("PDModification")
                        .HasForeignKey("SyncPointBack.Model.Excel.PDModification", "ExcelRecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExcelRecord");
                });

            modelBuilder.Entity("SyncPointBack.Model.Excel.PDRegistration", b =>
                {
                    b.HasOne("SyncPointBack.Model.Excel.ExcelRecord", "ExcelRecord")
                        .WithOne("PDRegistration")
                        .HasForeignKey("SyncPointBack.Model.Excel.PDRegistration", "ExcelRecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExcelRecord");
                });

            modelBuilder.Entity("SyncPointBack.Model.Excel.PIM", b =>
                {
                    b.HasOne("SyncPointBack.Model.Excel.ExcelRecord", "ExcelRecord")
                        .WithOne("PIM")
                        .HasForeignKey("SyncPointBack.Model.Excel.PIM", "ExcelRecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExcelRecord");
                });

            modelBuilder.Entity("SyncPointBack.Model.Excel.StaticPageCreation", b =>
                {
                    b.HasOne("SyncPointBack.Model.Excel.ExcelRecord", "ExcelRecord")
                        .WithOne("StaticPageCreation")
                        .HasForeignKey("SyncPointBack.Model.Excel.StaticPageCreation", "ExcelRecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExcelRecord");
                });

            modelBuilder.Entity("SyncPointBack.Model.Excel.StaticPageModification", b =>
                {
                    b.HasOne("SyncPointBack.Model.Excel.ExcelRecord", "ExcelRecord")
                        .WithOne("StaticPageModification")
                        .HasForeignKey("SyncPointBack.Model.Excel.StaticPageModification", "ExcelRecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExcelRecord");
                });

            modelBuilder.Entity("SyncPointBack.Model.Excel.ExcelRecord", b =>
                {
                    b.Navigation("GNB");

                    b.Navigation("PDModification");

                    b.Navigation("PDRegistration");

                    b.Navigation("PIM");

                    b.Navigation("StaticPageCreation");

                    b.Navigation("StaticPageModification");
                });
#pragma warning restore 612, 618
        }
    }
}
