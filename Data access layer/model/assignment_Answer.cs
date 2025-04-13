using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_access_layer.model
{
    public class assignment_Answer
    {
        [Key]
        public int ID;
        [ForeignKey(nameof(Assignment))]
        public int AssignmentID { get; set; }
        [ForeignKey(nameof(Student))]
        public int StudentID { get; set; }
        public string Answer { get; set; }
        public virtual Assignment Assignment { get; set; }
        public virtual Student Student { get; set; }
    }
}
