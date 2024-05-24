using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReiCalcLib;

namespace ReiCalcApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MathController : ControllerBase
    {
        private readonly ReiCalc calculator;

        public MathController(ReiCalc calculator)
        {
            this.calculator = calculator;
        }

        [HttpGet]
        public IActionResult Calculate(string expression)
        {
            double result = calculator.Calculate(expression);

            return Ok(new
            {
                Result = result
            });
        }
    }
}
