using Microsoft.AspNetCore.Mvc;
using WageCalculatorBackend.AppRepositories;
using WageCalculatorBackend.Models;
using WageCalculatorBackend.Repositories;

namespace WageCalculatorBackend.Controllers
{
    [Route("api/time-rules")]
    [ApiController]
    public class TimeRulesController : ControllerBase
    {
        private readonly ITimeRuleRepository _timeRuleRepository;

        public TimeRulesController(ITimeRuleRepository repository)
        {
            _timeRuleRepository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TimeRule>>> GetTimeRules()
        {
            var calculations = await _timeRuleRepository.GetAllTimeRulesAsync();
            return Ok(calculations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetTimeRuleById(int id)
        {
            try
            {
                var calculation = await _timeRuleRepository.GetTimeRuleByIdAsync(id);

                return Ok(calculation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("default")]
        public async Task<ActionResult> GetDefaultTimeRule()
        {
            try
            {
                var calculation = await _timeRuleRepository.GetDefaultTimeRuleAsync();

                return Ok(calculation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("create-rule")]
        public async Task<ActionResult<TimeRule>> AddTimeRule([FromBody] TimeRule requestTimeRule)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (requestTimeRule.Id != 0)
            {
                return BadRequest("The Id is auto incremented, and is not a needed parameter");
            }


            try
            {
                var createdTimeRule = await _timeRuleRepository.AddTimeRuleAsync(requestTimeRule);
                return CreatedAtAction(nameof(GetTimeRuleById), new { id = createdTimeRule.Id }, createdTimeRule);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("delete-rule/{id}")]
        public async Task<ActionResult> DeleteTimeRuleById(int id)
        {
            try
            {
                var calculation = await _timeRuleRepository.DeleteTimeRuleByIdAsync(id);

                var result = new
                {
                    message = "Rule deleted successfully"
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPatch("patch-rule")]
        public async Task<ActionResult> UpdateTimeRuleById([FromBody] TimeRule requestTimeRule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (requestTimeRule.Id == 0)
            {
                return BadRequest("The Id of the time rule is required");
            }


            try
            {
                var createdTimeRule = await _timeRuleRepository.UpdateTimeRuleAsync(requestTimeRule);

                var result = new
                {
                    message = "Rule patched successfully"
                };
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}
