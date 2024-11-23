﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SchoolServer.Data;

#nullable disable

namespace SchoolServer.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20241121074305_SeedData")]
    partial class SeedData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SchoolServer.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Courses", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CourseName = "Math"
                        },
                        new
                        {
                            Id = 2,
                            CourseName = "Science"
                        },
                        new
                        {
                            Id = 3,
                            CourseName = "English"
                        });
                });

            modelBuilder.Entity("SchoolServer.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Students");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateOfBirth = new DateTime(2005, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "John",
                            LastName = "Doe"
                        },
                        new
                        {
                            Id = 2,
                            DateOfBirth = new DateTime(2006, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Jane",
                            LastName = "Doe"
                        });
                });

            modelBuilder.Entity("SchoolServer.Models.StudentAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AddressOfStudentId")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("AddressOfStudentId")
                        .IsUnique();

                    b.ToTable("Addresses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AddressOfStudentId = 1,
                            City = "Hometown",
                            Country = "CountryA",
                            Street = "123 Main St"
                        },
                        new
                        {
                            Id = 2,
                            AddressOfStudentId = 2,
                            City = "Hometown",
                            Country = "CountryA",
                            Street = "456 Oak St"
                        });
                });

            modelBuilder.Entity("SchoolServer.Models.StudentCourses", b =>
                {
                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.HasKey("StudentId", "CourseId");

                    b.HasIndex("CourseId");

                    b.ToTable("StudentCourses");

                    b.HasData(
                        new
                        {
                            StudentId = 1,
                            CourseId = 1
                        },
                        new
                        {
                            StudentId = 1,
                            CourseId = 2
                        },
                        new
                        {
                            StudentId = 2,
                            CourseId = 1
                        },
                        new
                        {
                            StudentId = 2,
                            CourseId = 3
                        });
                });

            modelBuilder.Entity("SchoolServer.Models.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Teachers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateOfBirth = new DateTime(1980, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Mr.",
                            LastName = "Smith"
                        },
                        new
                        {
                            Id = 2,
                            DateOfBirth = new DateTime(1985, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Ms.",
                            LastName = "Johnson"
                        });
                });

            modelBuilder.Entity("SchoolServer.Models.TeacherCourses", b =>
                {
                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.HasKey("TeacherId", "CourseId");

                    b.HasIndex("CourseId");

                    b.ToTable("TeacherCourses");

                    b.HasData(
                        new
                        {
                            TeacherId = 1,
                            CourseId = 1
                        },
                        new
                        {
                            TeacherId = 1,
                            CourseId = 2
                        },
                        new
                        {
                            TeacherId = 2,
                            CourseId = 3
                        });
                });

            modelBuilder.Entity("SchoolServer.Models.StudentAddress", b =>
                {
                    b.HasOne("SchoolServer.Models.Student", "Student")
                        .WithOne("Address")
                        .HasForeignKey("SchoolServer.Models.StudentAddress", "AddressOfStudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("SchoolServer.Models.StudentCourses", b =>
                {
                    b.HasOne("SchoolServer.Models.Course", "Course")
                        .WithMany("StudentCourses")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SchoolServer.Models.Student", "Student")
                        .WithMany("StudentCourses")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("SchoolServer.Models.TeacherCourses", b =>
                {
                    b.HasOne("SchoolServer.Models.Course", "Course")
                        .WithMany("TeacherCourses")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SchoolServer.Models.Teacher", "Teacher")
                        .WithMany("TeacherCourses")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("SchoolServer.Models.Course", b =>
                {
                    b.Navigation("StudentCourses");

                    b.Navigation("TeacherCourses");
                });

            modelBuilder.Entity("SchoolServer.Models.Student", b =>
                {
                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("StudentCourses");
                });

            modelBuilder.Entity("SchoolServer.Models.Teacher", b =>
                {
                    b.Navigation("TeacherCourses");
                });
#pragma warning restore 612, 618
        }
    }
}