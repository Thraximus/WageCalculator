using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using WageCalculatorBackend.Models;
using WageCalculatorBackend.Repositories;
using WageCalculatorBackend.SwaggerExamples;

namespace WageCalculatorBackend.Controllers
{
    [Route("api/calculations")]
    [ApiController]
    public class CalculationController: ControllerBase
    {

        private readonly ITimeRuleRepository _timeRuleRepository;
        public CalculationController(ITimeRuleRepository repository) 
        {
            _timeRuleRepository = repository;
        }

        private Dictionary<string, int> CalculateHoursInBlocks(int workStart, int workEnd, int regularTimeStartTime, int nightTimeStartTime, int midnightTimeStartTIme)
        {
            var hoursInBlocks = new Dictionary<string, int>
            {
                { "RegularRate", 0 },
                { "NightTimeRate", 0 },
                { "MidnightRate", 0 }
            };

            for (int hour = workStart; hour < workEnd; hour++)
            {
                if (IsHourBetween(hour, regularTimeStartTime, nightTimeStartTime))
                {
                    hoursInBlocks["RegularRate"]++;
                }
                else if (IsHourBetween(hour, nightTimeStartTime, midnightTimeStartTIme))
                {
                    hoursInBlocks["NightTimeRate"]++;
                }
                else if (IsHourBetween(hour, midnightTimeStartTIme, regularTimeStartTime))
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


        [HttpPost("calculate-custom")]
        [SwaggerRequestExample(typeof(CalculateRequest),typeof(CalculateExamples))]
        public ActionResult Calculate([FromBody] CalculateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach (var day in request.Days.Select((value,i) => (value, i)))
            {
                if (day.value.Start >= day.value.End)
                {
                    var message = new
                    {
                        message = "Shift start time of day" + day.i+1 +" cannot be greater or equal to shift end time"
                    };
                    return BadRequest(message);
                }
            }

            if (request.TimeRule.RegularStartTime >= request.TimeRule.NightTimeStartTime || 
                    request.TimeRule.NightTimeStartTime >= request.TimeRule.MidnightStartTime || 
                        request.TimeRule.RegularStartTime >= request.TimeRule.MidnightStartTime){
                var response = new
                {
                    message = "Time Rule invalid"
                };
                return BadRequest(response);
            }

            int totalWage = 0;

            for (int i = 0; i < request.NumberOfDays; i++)
            {
                var hoursInBlocks = CalculateHoursInBlocks(request.Days[i].Start, request.Days[i].End, request.TimeRule.RegularStartTime, request.TimeRule.NightTimeStartTime, request.TimeRule.MidnightStartTime);
                foreach (var block in hoursInBlocks)
                {
                    if (block.Key.Equals("RegularRate"))
                    {
                        totalWage += block.Value * request.RegularRate;
                    }
                    else if (block.Key.Equals("NightTimeRate"))
                    {
                        totalWage += block.Value * request.NightTimeRate;
                    }
                    else if (block.Key.Equals("MidnightRate"))
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

        [HttpPost("calculate-standard")]
        [SwaggerRequestExample(typeof(CalculateRequestStandard), typeof(CalculateStandardExample))]
        public async Task<ActionResult> CalculateStandard([FromBody] CalculateRequestStandard request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (var day in request.Days.Select((value, i) => (value, i)))
            {
                if (day.value.Start > day.value.End)
                {
                    var message = new
                    {
                        message = "Shift start time of day" + day.i + 1 + " cannot be greater than shift end time"
                    };
                    return BadRequest(message);
                }
            }

            int totalWage = 0;

            TimeRule defaultTimeRule = await _timeRuleRepository.GetDefaultTimeRuleAsync();

            for (int i = 0; i < request.NumberOfDays; i++)
            {
                var hoursInBlocks = CalculateHoursInBlocks(request.Days[i].Start, request.Days[i].End, defaultTimeRule.RegularStartTime, defaultTimeRule.NightTimeStartTime, defaultTimeRule.MidnightStartTime);
                foreach (var block in hoursInBlocks)
                {
                    if (block.Key.Equals("RegularRate"))
                    {
                        totalWage += block.Value * request.RegularRate;
                    }
                    else if (block.Key.Equals("NightTimeRate"))
                    {
                        totalWage += block.Value * request.NightTimeRate;
                    }
                    else if (block.Key.Equals("MidnightRate"))
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
