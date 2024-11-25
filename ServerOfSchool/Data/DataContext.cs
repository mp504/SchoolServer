
using Microsoft.EntityFrameworkCore;
using ServerOfSchool.Models;

namespace ServerOfSchool.Data
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
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {



            modelBuilder.Entity<Student>()
             .HasOne<StudentAddress>(s => s.Address)
             .WithOne(ad => ad.Student)
             .HasForeignKey<StudentAddress>(ad => ad.AddressOfStudentId);

           


            // Many-to-many between Student and Course
            modelBuilder.Entity<Student>()
                .HasMany(s => s.Courses)
                .WithMany(c => c.StudentCourses)
                .UsingEntity(j => j.ToTable("StudentCourses"));

            // Many-to-many between Teacher and Course
            modelBuilder.Entity<Teacher>()
                .HasMany(t => t.TeacherCourses)
                .WithMany(c => c.TeacherCourses)
                .UsingEntity(j => j.ToTable("TeacherCourses"));




        }

    }
}
