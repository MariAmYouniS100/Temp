using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_access_layer.model
{
    // Instructor.cs
    public class Instructor
    {
        [Key]
        public int InstructorID { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Photo { get; set; }

        public string Subjects { get; set; }
        public string Qualifications { get; set; }
        public string Bio { get; set; }

        
        [Required]
        [StringLength(255)]
        public string Password { get; set; }



        // Navigation properties
        public ICollection<Course> Courses { get; set; } = new List<Course>();
        public ICollection<Comment> Comments { get; set; }= new List<Comment>();
    }



}