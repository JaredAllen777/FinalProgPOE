namespace ContractPoe.Models
{
    public class LecturerClaim
    {
        public int Id { get; set; } // Primary Key
        public double HoursWorked { get; set; }
        public double HourlyRate { get; set; }
        public string AdditionalNotes { get; set; }
        public string DocumentPath { get; set; } // Path to the uploaded document
    }
}