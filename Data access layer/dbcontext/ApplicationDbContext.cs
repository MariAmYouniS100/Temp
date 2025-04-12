// ApplicationDbContext.cs
using Data_access_layer.model;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<Student_Assignment> Submissions { get; set; }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<Student_Exam> ExamResults { get; set; }
    public DbSet<Revision> Revisions { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure composite key for Enrollment to prevent duplicates
        modelBuilder.Entity<Enrollment>()
            .HasIndex(e => new { e.StudentID, e.CourseID })
            .IsUnique();

        // Configure relationships and cascading behavior
        modelBuilder.Entity<Course>()
            .HasMany(c => c.Lessons)
            .WithOne(l => l.Course)
            .HasForeignKey(l => l.CourseID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Course>()
            .HasMany(c => c.Assignments)
            .WithOne(a => a.Course)
            .HasForeignKey(a => a.CourseID)
            .OnDelete(DeleteBehavior.Cascade);

        // Add other relationship configurations as needed
        // ...

        base.OnModelCreating(modelBuilder);
    }
}