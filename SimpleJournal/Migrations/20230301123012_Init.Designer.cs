﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SimpleJournal;

#nullable disable

namespace SimpleJournal.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230301123012_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.8");

            modelBuilder.Entity("SimpleJournal.Entities.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("FinishedAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("GroupId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("StartedAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("SubjectId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("SubjectId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("SimpleJournal.Entities.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("OnlyFullTimeEducation")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("SimpleJournal.Entities.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("Birthday")
                        .HasColumnType("TEXT");

                    b.Property<int>("EducationCost")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("GroupId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsFullTimeEducation")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsStateFunded")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("SimpleJournal.Entities.Subject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("SimpleJournal.Entities.Visit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SubjectId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.HasIndex("SubjectId");

                    b.ToTable("Visits");
                });

            modelBuilder.Entity("SimpleJournal.Entities.Course", b =>
                {
                    b.HasOne("SimpleJournal.Entities.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId");

                    b.HasOne("SimpleJournal.Entities.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId");

                    b.Navigation("Group");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("SimpleJournal.Entities.Student", b =>
                {
                    b.HasOne("SimpleJournal.Entities.Group", "Group")
                        .WithMany("Students")
                        .HasForeignKey("GroupId");

                    b.OwnsOne("SimpleJournal.ValueObjects.Phone", "Phone", b1 =>
                        {
                            b1.Property<Guid>("StudentId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.HasKey("StudentId");

                            b1.ToTable("Students");

                            b1.WithOwner()
                                .HasForeignKey("StudentId");
                        });

                    b.Navigation("Group");

                    b.Navigation("Phone");
                });

            modelBuilder.Entity("SimpleJournal.Entities.Visit", b =>
                {
                    b.HasOne("SimpleJournal.Entities.Student", "Student")
                        .WithMany("Visits")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SimpleJournal.Entities.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("SimpleJournal.Entities.Group", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("SimpleJournal.Entities.Student", b =>
                {
                    b.Navigation("Visits");
                });
#pragma warning restore 612, 618
        }
    }
}