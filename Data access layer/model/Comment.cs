using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_access_layer.model
{
    public class Comment
    {
        [Key]
        public int ID { get; set; }

        public int LessonID { get; set; }
        public int? StudentID { get; set; }
        public int? InstructorID { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CommentDate { get; set; } = DateTime.UtcNow;

        public string Reply { get; set; }

        // Navigation properties
        public Lesson Lesson { get; set; }
        public Student Student { get; set; }
        public Instructor Instructor { get; set; }
    }
}
