using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ums.Models;

namespace Ums.Data
{
    public class UmsContext : IdentityDbContext<IdentityUser>
    {
        public UmsContext(DbContextOptions<UmsContext> options) : base(options)
        {
        }


        public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) 
            {
                
                optionsBuilder.UseSqlServer("Server=.;Database=UmsDb;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }
        protected override void OnModelCreating(ModelBuilder mb)
        {

            base.OnModelCreating(mb);

            mb.Entity<Student>().HasKey(s => s.Student_id);
            mb.Entity<Instructor>().HasKey(i => i.Instructor_id);
            mb.Entity<Department>().HasKey(d => d.Department_id);
            mb.Entity<Course>().HasKey(c => c.Course_id);
            mb.Entity<Enrollment>().HasKey(e => new { e.Student_id, e.Course_id });

            
            mb.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.Student_id)
                .OnDelete(DeleteBehavior.Cascade);

            
            mb.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.Course_id)
                .OnDelete(DeleteBehavior.Cascade);

           
            mb.Entity<Course>()
                .HasOne(c => c.Department)
                .WithMany(d => d.Courses)
                .HasForeignKey(c => c.Department_id)
                .OnDelete(DeleteBehavior.Restrict);

          
            mb.Entity<Instructor>()
                .HasOne(i => i.Department)
                .WithMany(d => d.Instructors)
                .HasForeignKey(i => i.Department_id)
                .OnDelete(DeleteBehavior.Restrict);

          
            mb.Entity<Course>()
                .HasOne(c => c.Instructor)
                .WithMany(i => i.Courses)
                .HasForeignKey(c => c.Instructor_id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}