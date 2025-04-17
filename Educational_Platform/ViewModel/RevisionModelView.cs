using Data_access_layer.model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Educational_Platform.ViewModel
{
    public class RevisionModelView
    {
        [Key]
        public int ID { get; set; }

        public int CourseID { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [StringLength(255)]
        public string Video { get; set; }

        public string Files { get; set; }

        public DateTime ScheduleDate { get; set; }
        public IFormFile File { get; set; }
        public IFormFile VideoFile { get; set; }


        // Navigation properties
        [ForeignKey(nameof(CourseID))]
        public virtual Course Course { get; set; }
    }
}
