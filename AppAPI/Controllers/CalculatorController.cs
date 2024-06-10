using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        // Compound Interest Calculation API
        [HttpGet("calculate-compound-interest")]
        public ActionResult<double> CalculateCompoundInterest(double money, int n, double p)
        {
            if (money < 0 || n < 0 || p < 0)
            {
                return BadRequest("Input values must be non-negative.");
            }

            double interest = money * Math.Pow((1 + p / 100), n) - money;
            return Ok(interest);
        }

        [HttpPost("find-third-largest")]
        public ActionResult<string> FindThirdLargestNumber([FromBody] int[] arr)
        {
            if (arr.Length < 3)
            {
                return BadRequest("Array must contain at least three elements.");
            }

            int[] uniqueNumbers = arr.Distinct().ToArray();

            if (uniqueNumbers.Length < 3)
            {
                return BadRequest("Array must contain at least three unique elements.");
            }

            Array.Sort(uniqueNumbers);
            Array.Reverse(uniqueNumbers);

            int thirdLargest = uniqueNumbers[2];
            return Ok($"The third largest number in the array is: {thirdLargest}");
        }
    }
}
