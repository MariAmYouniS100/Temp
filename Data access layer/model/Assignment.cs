using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_access_layer.model
{
    public class Assignment
    {
        [Key]
        public int ID { get; set; }

      

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        public string  Type { get; set; }

        public DateTime? Deadline { get; set; }
        public string Instructions { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal MaxGrade { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public int CourseID { get; set; }
        public int? LessonID { get; set; }
        public Course Course { get; set; }
        public Lesson Lesson { get; set; }
        public ICollection<Student_Assignment> Submissions { get; set; } = new List<Student_Assignment>();

    }
    
}
