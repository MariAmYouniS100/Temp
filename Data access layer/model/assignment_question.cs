using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_access_layer.model
{
    public class assignment_question
    {
        [Key]
        public int ID { get; set; }

        public int AssignmentID { get; set; }
        public int QuestionID { get; set; }

        // Navigation properties
        [ForeignKey(nameof(AssignmentID))]
        public virtual Assignment Assignment { get; set; }

        [ForeignKey(nameof(QuestionID))]
        public virtual Questions Question { get; set; }
    }
}
