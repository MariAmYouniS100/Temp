using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_access_layer.model
{
    public class Exam
    {
        [Key]
        public int ID { get; set; }

        public int CourseID { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        public ExamType Type { get; set; }

        public int Duration { get; set; } // in minutes

        [Column(TypeName = "decimal(5, 2)")]
        public decimal MaxGrade { get; set; }

        public bool IsTimed { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Course Course { get; set; }
        public ICollection<examQuestion> examQuestions = new HashSet<examQuestion>();


        public ICollection<Student_Exam> ExamResults { get; set; }
    }
    public enum ExamType
    {
        MCQ,
        [Display(Name = "True/False")]
        TrueFalse,
        Essay
    }
}
