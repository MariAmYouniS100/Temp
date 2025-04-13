using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_access_layer.model
{
    public class student_answers
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey(nameof(Question))]
        public int QuestionID { get; set; }

        [ForeignKey(nameof(Student))]
        public int StudentID { get; set; }

        public string AnswerText { get; set; }

        // Navigation properties
        public virtual Questions Question { get; set; }
        public virtual Student Student { get; set; }
    }


}
