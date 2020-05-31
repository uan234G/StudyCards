﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudyCards.Models;

namespace StudyCards.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20200509214958_initialmig")]
    partial class initialmig
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("StudyCards.Models.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author");

                    b.Property<string>("ImageUrl");

                    b.Property<string>("Title");

                    b.HasKey("BookId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("StudyCards.Models.Chapter", b =>
                {
                    b.Property<int>("ChapterId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BookId");

                    b.Property<string>("ChName");

                    b.Property<int>("ChNumber");

                    b.HasKey("ChapterId");

                    b.HasIndex("BookId");

                    b.ToTable("Chapters");
                });

            modelBuilder.Entity("StudyCards.Models.FlashCard", b =>
                {
                    b.Property<int>("FlashCardId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ChapterId");

                    b.Property<string>("Definition")
                        .IsRequired();

                    b.Property<bool>("IsComplete");

                    b.Property<string>("Word")
                        .IsRequired();

                    b.HasKey("FlashCardId");

                    b.HasIndex("ChapterId");

                    b.ToTable("FlashCards");
                });

            modelBuilder.Entity("StudyCards.Models.Chapter", b =>
                {
                    b.HasOne("StudyCards.Models.Book", "Book")
                        .WithMany("Chapters")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StudyCards.Models.FlashCard", b =>
                {
                    b.HasOne("StudyCards.Models.Chapter", "Chapter")
                        .WithMany("FlashCards")
                        .HasForeignKey("ChapterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
