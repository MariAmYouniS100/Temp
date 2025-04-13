using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_access_layer.model
{
    public class Answers
    {
        public int ID { get; set; }
        [ForeignKey(nameof(Question))]
        public int QuestionID { get; set; }
        [ForeignKey(nameof(Student))]
        public int StudentID { get; set; }
        public string AnswerText { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        // Navigation properties
        public Questions Question { get; set; }
        public Student Student { get; set; }
    }
}
