// ApplicationDbContext.cs
using Data_access_layer.model;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    // DbSets for all your entities
    public DbSet<student_answers> Answers { get; set; }
    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<assignment_question> AssignmentQuestions { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<student_Course> Enrollments { get; set; }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<examQuestion> ExamQuestions { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Questions> Questions { get; set; }
    public DbSet<Revision> Revisions { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Student_Assignment> StudentAssignments { get; set; }
    public DbSet<Student_Exam> StudentExams { get; set; }
    public DbSet<assignment_Answer> AssignmentAnswers { get; set; }

}