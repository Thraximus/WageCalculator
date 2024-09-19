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
        private readonly ITimeRuleRepository _repository;

        public TimeRulesController(ITimeRuleRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TimeRule>>> GetTimeRules()
        {
            var calculations = await _repository.GetAllTimeRulesAsync();
            return Ok(calculations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetTimeRuleById(int id)
        {
            try
            {
                var calculation = await _repository.GetTimeRuleByIdAsync(id);

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
                var calculation = await _repository.GetDefaultTimeRuleAsync();

                return Ok(calculation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
