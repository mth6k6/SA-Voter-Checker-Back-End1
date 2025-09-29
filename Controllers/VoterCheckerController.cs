
using Microsoft.AspNetCore.Mvc;
using VoterEligChecker.Models;

namespace VoterEligChecker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoterCheckerController : ControllerBase
    {
        [HttpPost]
        public ActionResult<VerifiedResults> CheckEligibility(UserDetails user)
        {
            // ID Number validation
            if (string.IsNullOrWhiteSpace(user.IdNum) || user.IdNum.Length <=13)
            {
                return BadRequest("Invalid ID number.");
            }

            // Extract date of birth
            string birthYear = user.IdNum.Substring(0, 2);
            string birthMonth = user.IdNum.Substring(2, 2);
            string birthDay = user.IdNum.Substring(4, 2);

            int year = int.Parse(birthYear);
            year += (year >= 0 && year <= 25) ? 2000 : 1900;

            DateTime birthDate;
            try
            {
                birthDate = new DateTime(year, int.Parse(birthMonth), int.Parse(birthDay));
            }
            catch
            {
                return BadRequest("Invalid birth date in ID number.");
            }

            int age = DateTime.Now.Year - birthDate.Year;
            if (DateTime.Now < birthDate.AddYears(age)) age--;


            // Gender (7th to 10th digit: < 5000 is female, >= 5000 is male)
            int genderDigits = int.Parse(user.IdNum.Substring(6, 4));
            string gender = genderDigits < 5000 ? "Female" : "Male";


            // Citizenship (11th digit: 0 = SA citizen, 1 = permanent resident)
            bool isCitizen = user.IdNum[10] == '0';


            // Voting eligibility
            bool eligibleToVote = isCitizen && age >= 18;


            return Ok(new VerifiedResults
            {
                Age = age,
                Gender = gender,
                IsCitizen = isCitizen,
                IsEligibleToVote = eligibleToVote
            }
             );
           [HttpGet("Ping")]
           public IActionResult Ping()
           {
                  Return Ok("API is active")
           }

        }
    }
}

