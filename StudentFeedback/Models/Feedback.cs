using System.ComponentModel.DataAnnotations;

namespace StudentFeedback.Models
{
    public class Feedback
    {
        [Required(ErrorMessage = "Enrollment number is mandatory.")]
        [Display(Name = "Enrollment Number")]
        public string EnrollmentNumber { get; set; }

        [Required(ErrorMessage = "Please enter the student's full name.")]
        [Display(Name = "Student Name")]
        [StringLength(80, ErrorMessage = "Name cannot exceed 80 characters.")]
        public string StudentName { get; set; }

        [Required(ErrorMessage = "Course/Subject title is required.")]
        [Display(Name = "Course / Subject")]
        public string SubjectName { get; set; }

        [Required(ErrorMessage = "Please provide an evaluation rating between 1 and 5.")]
        [Range(1, 5, ErrorMessage = "Evaluation score must be between 1 and 5.")]
        [Display(Name = "Satisfaction Rating")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "Feedback remarks cannot be left blank.")]
        [StringLength(500, ErrorMessage = "Remarks must be 500 characters or fewer.")]
        [Display(Name = "Feedback & Comments")]
        public string Comments { get; set; }
    }
}