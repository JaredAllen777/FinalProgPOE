using System.ComponentModel.DataAnnotations;

//_______________________________________________START OF FILE___________________________________________________________\\

namespace ContractPoe.Models
{
    public class LecturerClaim
    {
        [Key]
        public int ClaimId { get; set; }
        public double HoursWorked { get; set; }
        public double HourlyRate { get; set; }
        //non mapped property
        public double TotalAmount => HoursWorked * HourlyRate;
        public string? AdditionalNotes { get; set; }
        public string? DocumentPath { get; set; }
    }
}
//_____________________________________________END OF FILE_______________________________________________________________\\