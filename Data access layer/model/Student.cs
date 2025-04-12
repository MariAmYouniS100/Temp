using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_access_layer.model
{
    // Student.cs
    public class Student
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(50)]
        public string GradeLevel { get; set; }

        [StringLength(255)]
        public string ProfilePicture { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

       
        // Navigation properties
        public ICollection<Student_Assignment> Student_Assignment { get; set; }
        public ICollection<Student_Exam> Student_Exam { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
