
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServerOfSchool.Models;

namespace ServerOfSchool.Data
{
    public class DataContext:IdentityDbContext<ApplicationUser>
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
            base.OnModelCreating(modelBuilder);


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



            // ApplicationUser to Student
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.StudentProfile)
                .WithOne(s => s.ApplicationUser)
                .HasForeignKey<Student>(s => s.ApplicationUserId);

            // ApplicationUser to Teacher
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.TeacherProfile)
                .WithOne(t => t.ApplicationUser)
                .HasForeignKey<Teacher>(t => t.ApplicationUserId);




        }

    }
}
