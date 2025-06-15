using System.ComponentModel.DataAnnotations;

namespace VoterEligChecker.Models
{
    public class UserDetails
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Surname { get; set; }
        [Required]
        public string IdNum { get; set; } 
    }
}
