using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_access_layer.model
{
    public class Questions
    {
        [Key]
        public int QuestionID { get; set; }

        public string Type { get; set; }  // MCQ or Written
        public string QuestionText { get; set; }
        public string Answer { get; set; }

        public int AssignmentID { get; set; }
        [ForeignKey(nameof(AssignmentID))]
        public Assignment Assignment { get; set; }

        public ICollection<examQuestion>examQuestions= new HashSet<examQuestion>();
        public MCQ MCQ { get; set; }
        public Written Written { get; set; }
        public ICollection<Answers> Answers { get; set; } = new HashSet<Answers>();

        public ICollection<assignment_question> Choices { get; set; } = new HashSet<assignment_question>();

    }
    public class MCQ
    {
        [Key]
        public int ID { get; set; }
        [ ForeignKey(nameof(Question))]
        public int QuestionID { get; set; }

        public string Content { get; set; }

        public Questions Question { get; set; }
        public ICollection<Choice> Choices { get; set; }
    }

    public class Written
    {
        [Key]
        public int ID { get; set; }
        [ ForeignKey(nameof(Question))]
        public int QuestionID { get; set; }

        public string Content { get; set; }

        public Questions Question { get; set; }
    }

    public class Choice
    {
        [Key]
        public int ID { get; set; }

        public int MCQQuestionID { get; set; }
        [ForeignKey(nameof(MCQQuestionID))]
        public MCQ MCQ { get; set; }

        public string Content { get; set; }
    }
}
