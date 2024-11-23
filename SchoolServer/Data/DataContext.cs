using Microsoft.EntityFrameworkCore;
using SchoolServer.Models;

namespace SchoolServer.Data
{
    public class DataContext:DbContext
    {

        public DataContext(DbContextOptions<DataContext> options): base(options)
        {

        }

        public DbSet<StudentAddress> Addresses { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<StudentCourses> StudentCourses { get; set; }
        public DbSet<TeacherCourses> TeacherCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {



            modelBuilder.Entity<Student>()
             .HasOne<StudentAddress>(s => s.Address)
             .WithOne(ad => ad.Student)
             .HasForeignKey<StudentAddress>(ad => ad.AddressOfStudentId);




            // Many-to-many relationship between Student and Course
            modelBuilder.Entity<StudentCourses>().HasKey(sc => new { sc.StudentId, sc.CourseId });
            modelBuilder.Entity<StudentCourses>()
            .HasOne(sc => sc.Student)
            .WithMany(s => s.StudentCourses)
            .HasForeignKey(sc => sc.StudentId);

            modelBuilder.Entity<StudentCourses>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.Courses)
                .HasForeignKey(sc => sc.CourseId);

            // Many-to-many relationship between Teacher and Course
            modelBuilder.Entity<TeacherCourses>().HasKey(sc => new { sc.TeacherId, sc.CourseId });
            modelBuilder.Entity<TeacherCourses>()
                .HasOne(tc => tc.Teacher)
                .WithMany(t => t.TeacherCourses)
                .HasForeignKey(tc => tc.TeacherId);

            modelBuilder.Entity<TeacherCourses>()
                .HasOne(tc => tc.Course)
                .WithMany(c => c.TeacherCourses)
                .HasForeignKey(tc => tc.CourseId);

            modelBuilder.Entity<Course>().ToTable("Courses");




            //// Many-to-many between Student and Course
            //modelBuilder.Entity<Student>()
            //    .HasMany(s => s.Courses)
            //    .WithMany(c => c.StudentCourses)
            //    .UsingEntity(j => j.ToTable("StudentCourses"));

            //// Many-to-many between Teacher and Course
            //modelBuilder.Entity<Teacher>()
            //    .HasMany(t => t.TeacherCourses)
            //    .WithMany(c => c.TeacherCourses)
            //    .UsingEntity(j => j.ToTable("TeacherCourses"));





            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, CourseName = "Math" },
                new Course { Id = 2, CourseName = "Science" },
                new Course { Id = 3, CourseName = "English" }
            );

            // Seed Students
            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, FirstName = "John", LastName = "Doe", DateOfBirth = new DateTime(2005, 5, 15) },
                new Student { Id = 2, FirstName = "Jane", LastName = "Doe", DateOfBirth = new DateTime(2006, 7, 20) }
            );

            // Seed Teachers
            modelBuilder.Entity<Teacher>().HasData(
                new Teacher { Id = 1, FirstName = "Mr.", LastName = "Smith", DateOfBirth = new DateTime(1980, 3, 10) },
                new Teacher { Id = 2, FirstName = "Ms.", LastName = "Johnson", DateOfBirth = new DateTime(1985, 8, 25) }
            );

            // Seed StudentAddresses
            modelBuilder.Entity<StudentAddress>().HasData(
                new StudentAddress { Id = 1, Street = "123 Main St", City = "Hometown", Country = "CountryA", AddressOfStudentId = 1 },
                new StudentAddress { Id = 2, Street = "456 Oak St", City = "Hometown", Country = "CountryA", AddressOfStudentId = 2 }
            );

            // Seed StudentCourses (Many-to-Many relation)
            modelBuilder.Entity<StudentCourses>().HasData(
                new StudentCourses { StudentId = 1, CourseId = 1 }, // John enrolled in Math
                new StudentCourses { StudentId = 1, CourseId = 2 }, // John enrolled in Science
                new StudentCourses { StudentId = 2, CourseId = 1 }, // Jane enrolled in Math
                new StudentCourses { StudentId = 2, CourseId = 3 }  // Jane enrolled in English
            );

            // Seed TeacherCourses (Many-to-Many relation)
            modelBuilder.Entity<TeacherCourses>().HasData(
                new TeacherCourses { TeacherId = 1, CourseId = 1 }, // Mr. Smith teaches Math
                new TeacherCourses { TeacherId = 1, CourseId = 2 }, // Mr. Smith teaches Science
                new TeacherCourses { TeacherId = 2, CourseId = 3 }  // Ms. Johnson teaches English
            );

        }

    }
}
