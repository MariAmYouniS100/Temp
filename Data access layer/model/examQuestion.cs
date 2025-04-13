using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_access_layer.model
{
    public class examQuestion
    {
        public int ID { get; set; }
        public int ExamID { get; set; }
        public int QuestionID { get; set; }
        public Exam Exam { get; set; }
        public Questions Question { get; set; }
    }
}
