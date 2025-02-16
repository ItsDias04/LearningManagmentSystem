using LearningManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystem.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Schools> School => Set<Schools>();
        public DbSet<Grades> Grades => Set<Grades>();
        public DbSet<Tutor> Tutor => Set<Tutor>();
        public DbSet<Group> Group => Set<Group>();
        public DbSet<Subjects> Subjects => Set<Subjects>();
        public DbSet<Student> Student => Set<Student>();

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<Grades>()
        //        .Property(e => e.Id)
        //        .HasDefaultValueSql("NEWID()");
        //}
    }
}
