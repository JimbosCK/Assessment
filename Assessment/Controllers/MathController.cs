using Assessment.Models;
using Assessment.Services;
using Microsoft.AspNetCore.Mvc;

namespace Assessment.Controllers {


    [Route("api/[controller]")]
    [ApiController]
    public class MathController : ControllerBase {
        private readonly IMathService _mathService;

        public MathController(IMathService mathService) {
            _mathService = mathService;
        }


        /// <summary>
        /// Finds and returns the second largest distinct integer from the input array.
        /// </summary>
        [HttpPost("SecondLargest")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SecondLargest([FromBody] RequestObj request) {
            IEnumerable<int> rawIntegers;
            try {
                rawIntegers = request.RequestArrayObj.Cast<int>();
            } catch (InvalidCastException) {
                return BadRequest("The array contains elements that are not valid integers.");
            }

            try {
                int secondLargest = _mathService.FindSecondLargest(rawIntegers);
                return Ok(secondLargest);

            } catch (InsufficientDataException ex) {
                return BadRequest(ex.Message);
            }
        }

    }
}
