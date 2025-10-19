using Assessment.Dtos;
using Assessment.Services;
using Microsoft.AspNetCore.Mvc;

namespace Assessment.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase {
        private readonly IExternalCountryService _countryService;

        public CountriesController(IExternalCountryService countryService) {
            _countryService = countryService;
        }

        /// <summary>
        /// Retrieves a list of all countries with their name, capital, and borders.
        /// </summary>
        [HttpGet("All")]
        [ProducesResponseType(typeof(IEnumerable<CountryResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCountries() {
            try {
                var countries = await _countryService.GetAllCountriesAsync();

                return Ok(countries);
            } catch (HttpRequestException) {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    "The third-party country API is currently unavailable.");
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected error occurred while processing the request.");
            }
        }
    }
}
