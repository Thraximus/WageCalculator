using Microsoft.AspNetCore.Mvc;
using WageCalculatorBackend.Models;
using WageCalculatorBackend.Repositories;

namespace WageCalculatorBackend.Controllers
{
    [Route("api/calculations")]
    [ApiController]
    public class CalculationController: ControllerBase
    {

        public CalculationController() { }

        private Dictionary<string, int> CalculateHoursInBlocks(int workStart, int workEnd, int regularTimeStartTime, int nightTimeStartTime, int midnightTimeStartTIme)
        {
            var hoursInBlocks = new Dictionary<string, int>
            {
                { "RegularRate", 0 },
                { "NightTimeRate", 0 },
                { "MidnightRate", 0 }
            };

            if (workEnd < workStart)
            {
                workEnd += 24;
            }

            for (int hour = workStart; hour < workEnd; hour++)
            {
                int normalizedHour = hour % 24;
                if (IsHourBetween(normalizedHour, regularTimeStartTime, nightTimeStartTime))
                {
                    hoursInBlocks["RegularRate"]++;
                }
                else if (IsHourBetween(normalizedHour, nightTimeStartTime, midnightTimeStartTIme))
                {
                    hoursInBlocks["NightTimeRate"]++;
                }
                else if (IsHourBetween(normalizedHour, midnightTimeStartTIme, regularTimeStartTime))
                {
                    hoursInBlocks["MidnightRate"]++;
                }
            }

            return hoursInBlocks;
        }
        private bool IsHourBetween(int timeToCheck, int startTime, int endTime)
        {
            if (startTime < endTime)
            {
                return timeToCheck >= startTime && timeToCheck < endTime;
            }
            else
            {
                return timeToCheck >= startTime || timeToCheck < endTime;
            }
        }


        [HttpPost("calculate-standard")]
        public ActionResult Calculate([FromBody] CalculateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int totalWage = 0;


            
            for (int i = 0; i < request.NumberOfDays;i++)
            {
                var hoursInBlocks = CalculateHoursInBlocks(request.Days[i].Start, request.Days[i].End, request.TimeRule.RegularStartTime, request.TimeRule.NightTimeStartTime, request.TimeRule.MidnightStartTime);
                foreach (var block in hoursInBlocks)
                {
                    if (block.Key.Equals("RegularRate"))
                    {
                        totalWage += block.Value* request.RegularRate;
                    }
                    else if (block.Key.Equals("NightTimeRate"))
                    {
                        totalWage += block.Value * request.NightTimeRate;
                    } else if (block.Key.Equals("MidnightRate"))
                    {
                        totalWage += block.Value * request.MidnightRate;
                    }
                }


            }


            var result = new
            {
                GrandTotal = totalWage
            };

            return Ok(result);
        }
    }
}
