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


        [HttpPost("calculate-standard")]
        public ActionResult Calculate([FromBody] CalculateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var result = new
            {
                RegularRate = request.RegularRate,
                NightTimerate = request.NightTimeRate,
                MidnightRate = request.MidnightRate,
                GrandTotal = 0
            };

            return Ok(result);
        }
    }
}
