using Microsoft.EntityFrameworkCore;

namespace EFCoreDemo.Database;

public class DemoDbContext : DbContext {
    protected override void OnConfiguring(DbContextOptionsBuilder builder){
        builder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=EFCoreDemo;Trusted_Connection=True");
    }

    protected override void OnModelCreating(ModelBuilder builder) {
        builder.Entity<Student>()
            .Ignore(s => s.FullName);

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
        // var entries = ChangeTracker
        //     .Entries()
        //     .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        // var currentDate = DateTime.Now;
        // foreach (var entityEntry in entries)
        // {
        //     if (entityEntry.Properties.Any(x => x.Metadata.GetColumnName().Equals("UpdatedDate"))) {

        //     }
            
        //     entityEntry.Property("UpdatedDate").CurrentValue = currentDate;

        //     if (entityEntry.State == EntityState.Added)
        //     {
        //         entityEntry.Property("CreatedDate").CurrentValue = currentDate;
        //     }
        // }

        return base.SaveChanges();
    }
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Grade> Grades { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
}