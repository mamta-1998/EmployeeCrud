using System.ComponentModel.DataAnnotations;

namespace FinalMVC.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        [Required]
        public string StudentName { get; set; }

        [Required]
        public string FatherName { get; set; }

        public string Gender { get; set; }

        public DateTime DOB { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public int PhysicsMarks { get; set; }

        public int ChemistryMarks { get; set; }

        public int MathMarks { get; set; }

        public int TotalMarks { get; set; }

        public decimal Percentage { get; set; }

        public string? AdmissionStatus { get; set; }
    }
}
