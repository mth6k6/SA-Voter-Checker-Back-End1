namespace VoterEligChecker.Models
{
    public class VerifiedResults
    {

        public int Age { get; set; }
        public string? Gender { get; set; }
        public bool IsCitizen { get; set; }
        public bool IsEligibleToVote { get; set; }
    }
}
