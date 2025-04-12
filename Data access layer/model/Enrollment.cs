using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_access_layer.model
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }

        public int StudentID { get; set; }
        public int CourseID { get; set; }

        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;

        [Required]
        public string PaymentStatus { get; set; } 

        // Navigation properties
        public Student Student { get; set; } = new Student();
        public Course Course { get; set; } = new Course();
    }

  
}
