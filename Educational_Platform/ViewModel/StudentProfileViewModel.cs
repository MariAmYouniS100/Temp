using System.ComponentModel.DataAnnotations;

namespace Educational_Platform.ViewModel
{
    public class StudentProfileViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Father's Phone")]
        public string FatherPhone { get; set; }

        [Display(Name = "Grade Level")]
        public string GradeLevel { get; set; }

        [Display(Name = "Profile Picture")]
        public IFormFile ProfilePictureFile { get; set; }

        public string CurrentProfilePicture { get; set; }
    }
}