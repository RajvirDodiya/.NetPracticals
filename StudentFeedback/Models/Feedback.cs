using System.ComponentModel.DataAnnotations;

namespace StudentFeedback.Models
{
    public class Feedback
    {
        [Required(ErrorMessage = "Enrollment Number is required")]
        [Display(Name = "Enrollment Number")]
        public string EnrollmentNumber { get; set; }

        [Required(ErrorMessage = "Student Name is required")]
        [Display(Name = "Student Name")]
        public string StudentName { get; set; }

        [Required(ErrorMessage = "Subject Name is required")]
        [Display(Name = "Subject Name")]
        public string SubjectName { get; set; }

        [Required(ErrorMessage = "Please select a rating")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "Comments are required")]
        [StringLength(300, ErrorMessage = "Comments cannot exceed 300 characters")]
        public string Comments { get; set; }
    }
}