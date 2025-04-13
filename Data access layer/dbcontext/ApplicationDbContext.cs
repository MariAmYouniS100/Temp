// ApplicationDbContext.cs
using Data_access_layer.model;
using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // DbSets for all your entities
    public DbSet<Answers> Answers { get; set; }
    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<assignment_question> AssignmentQuestions { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<examQuestion> ExamQuestions { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Questions> Questions { get; set; }
    public DbSet<Revision> Revisions { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Student_Assignment> StudentAssignments { get; set; }
    public DbSet<Student_Exam> StudentExams { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure composite keys for join tables
        modelBuilder.Entity<assignment_question>()
            .HasKey(aq => new { aq.AssignmentID, aq.QuestionID });

        modelBuilder.Entity<examQuestion>()
            .HasKey(eq => new { eq.ExamID, eq.QuestionID });

        // Configure relationships
        modelBuilder.Entity<Answers>()
            .HasOne(a => a.Question)
            .WithMany()
            .HasForeignKey(a => a.QuestionID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Answers>()
            .HasOne(a => a.Student)
            .WithMany(s => s.Answers)
            .HasForeignKey(a => a.StudentID)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure the rest of your relationships similarly...
        // Add other specific configurations as needed

        // Example configuration for the Enrollment entity
        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Student)
            .WithMany(s => s.Enrollments)
            .HasForeignKey(e => e.StudentID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Course)
            .WithMany(c => c.Enrollments)
            .HasForeignKey(e => e.CourseID)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure enum conversion for ExamType
        modelBuilder.Entity<Exam>()
            .Property(e => e.Type)
            .HasConversion<string>();

        // Configure decimal precision where needed
        modelBuilder.Entity<Assignment>()
            .Property(a => a.MaxGrade)
            .HasColumnType("decimal(5,2)");

        modelBuilder.Entity<Course>()
            .Property(c => c.Price)
            .HasColumnType("decimal(10,2)");

        modelBuilder.Entity<Student_Assignment>()
            .Property(sa => sa.Grade)
            .HasColumnType("decimal(5,2)");

        modelBuilder.Entity<Student_Exam>()
            .Property(se => se.Score)
            .HasColumnType("decimal(5,2)");

        // Configure default values
        modelBuilder.Entity<Assignment>()
            .Property(a => a.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        modelBuilder.Entity<Comment>()
            .Property(c => c.CommentDate)
            .HasDefaultValueSql("GETUTCDATE()");

        modelBuilder.Entity<Course>()
            .Property(c => c.Price)
            .HasDefaultValue(0.00m);

        modelBuilder.Entity<Enrollment>()
            .Property(e => e.EnrollmentDate)
            .HasDefaultValueSql("GETUTCDATE()");

        modelBuilder.Entity<Exam>()
            .Property(e => e.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        modelBuilder.Entity<Lesson>()
            .Property(l => l.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        modelBuilder.Entity<Revision>()
            .Property(r => r.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        // Configure table names if different from DbSet names
        modelBuilder.Entity<assignment_question>().ToTable("AssignmentQuestions");
        modelBuilder.Entity<examQuestion>().ToTable("ExamQuestions");
        modelBuilder.Entity<Student_Assignment>().ToTable("StudentAssignments");
        modelBuilder.Entity<Student_Exam>().ToTable("StudentExams");
    }
}