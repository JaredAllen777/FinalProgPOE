using System.ComponentModel.DataAnnotations;

//_______________________________________________START OF FILE___________________________________________________________\\

namespace ContractPoe.Models
{
    public class LecturerClaim
    {
        [Key]
        public int ClaimId { get; set; }

        [Range(0, 40, ErrorMessage = "Hours worked must be between 0 and 40.")]
        public double HoursWorked { get; set; }

        [Range(15, 50, ErrorMessage = "Hourly rate must be between $15 and $50.")]
        public double HourlyRate { get; set; }

        [MaxLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
        public string? AdditionalNotes { get; set; }

        public string? DocumentPath { get; set; }

        // Derived Property
        public double TotalAmount => HoursWorked * HourlyRate;

        public bool IsApproved { get; set; } // Non-nullable boolean

        // Add SubmissionDate to track when the claim is submitted
        public DateTime SubmissionDate { get; set; }

        // Foreign key to Lecturer
        public int LecturerId { get; set; }  // Foreign key to Lecturer

        // Navigation property for the Lecturer
        public Lecturer Lecturer { get; set; }  // Correct navigation property type
        public string LecturerName { get; set; }
    }
}

//_____________________________________________END OF FILE_______________________________________________________________\\