using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data_access_layer.model
{
    public class Questions
    {
        [Key]
        public int QuestionID { get; set; }

        [Required]
        [StringLength(20)]
        public string Type { get; set; }  

        [Required]
        public string QuestionText { get; set; }

        public string Answer { get; set; }

        // Navigation properties
        [ForeignKey(nameof(Assignment))]
        public int? AssignmentID { get; set; }
        public virtual Assignment Assignment { get; set; }

        public virtual MCQ MCQ { get; set; }
        public virtual Written Written { get; set; }

        public virtual ICollection<examQuestion> ExamQuestions { get; set; } = new HashSet<examQuestion>();
        public virtual ICollection<student_answers> Answers { get; set; } = new HashSet<student_answers>();
        public virtual ICollection<assignment_question> AssignmentQuestions { get; set; } = new HashSet<assignment_question>();
    }

    public class MCQ
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [ForeignKey(nameof(Questions))]
        public int QuestionID { get; set; }

        public string Content { get; set; }

        // Navigation properties
        public virtual Questions Question { get; set; }
        public virtual ICollection<Choice> Choices { get; set; } = new HashSet<Choice>();
    }

    public class Written
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [ForeignKey(nameof(Question))]
        public int QuestionID { get; set; }

        public string Content { get; set; }

        // Navigation properties
        public virtual Questions Question { get; set; }
    }

    public class Choice
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [ForeignKey(nameof(MCQ))]
        public int MCQQuestionID { get; set; }

        [Required]
        public string Content { get; set; }


        // Navigation properties
        public virtual MCQ MCQ { get; set; }
    }
}