using Microsoft.AspNetCore.Mvc;
using WageCalculatorBackend.Models;
using WageCalculatorBackend.Repositories;

namespace WageCalculatorBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculationsController : ControllerBase
    {
        private readonly ICalculationRepository _repository;

        public CalculationsController(ICalculationRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Calculation>>> GetCalculations()
        {
            var calculations = await _repository.GetAllCalculationsAsync();
            return Ok(calculations);
        }
    }
}
