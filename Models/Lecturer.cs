using System.ComponentModel.DataAnnotations;

namespace ContractPoe.Models
{
    public class Lecturer
    {
        [Key]
        public int LecturerId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        // Navigation property for claims
        public ICollection<LecturerClaim> LecturerClaims { get; set; }
    }
}
