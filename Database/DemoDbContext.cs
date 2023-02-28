using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EFCoreDemo.Database;

public class DemoDbContext : DbContext {

    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Grade> Grades { get; set; }
    public DbSet<Teacher> Teachers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder){
        builder.UseSqlServer(@"Server=Jarvis;Database=EFCoreDemo;Trusted_Connection=True");
        builder.LogTo(Console.WriteLine, new [] { 
            DbLoggerCategory.Query.Name,
            DbLoggerCategory.Database.Command.Name
        }, LogLevel.Information);
        // show parameter values on logging
        builder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder builder) {
        builder.Entity<Student>()
            .Ignore(s => s.RUT);

        builder.Entity<Grade>()
            .Property(g => g.Value)
            .HasColumnType("decimal")
            .HasPrecision(2, 1);

        builder.Entity<Course>()
            .HasOne<Teacher>(c => c.Teacher)
            .WithMany(t => t.Courses)
            .HasForeignKey(c => c.TeacherOfCourseId)
            .IsRequired(false);
    }
    
    public override int SaveChanges() {
        UpdateTrackingDates();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
        UpdateTrackingDates();
        return base.SaveChangesAsync();
    }

    private void UpdateTrackingDates() {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        var currentDate = DateTime.Now;

        foreach (var entityEntry in entries)
        {
            if (entityEntry.Entity is IBaseEntity) {
                switch (entityEntry.State)
                {
                    case EntityState.Added: 
                        ((BaseEntity)entityEntry.Entity).CreatedDate = currentDate;
                        ((BaseEntity)entityEntry.Entity).UpdatedDate = currentDate;
                        break;
                    case EntityState.Modified: 
                        ((BaseEntity)entityEntry.Entity).UpdatedDate = currentDate;
                        break;
                }
            }
        }
    }
}