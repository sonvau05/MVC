using Microsoft.EntityFrameworkCore;
using StudentMarksMvc.Models;

namespace StudentMarksMvc.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<StudentMark> StudentMarks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StudentMark>()
                .HasOne(sm => sm.Student)
                .WithMany(s => s.Marks)
                .HasForeignKey(sm => sm.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StudentMark>()
                .HasOne(sm => sm.Subject)
                .WithMany()
                .HasForeignKey(sm => sm.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
