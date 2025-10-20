using Assessment.Dtos;
using Assessment.EF.Repositories;
using Assessment.Services;
using Microsoft.AspNetCore.Mvc;

namespace Assessment.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase {
        private readonly IExternalCountryService _countryService;
        private readonly CountryRepo _countryRepo;

        public CountriesController(IExternalCountryService countryService, CountryRepo countryRepo) {
            _countryService = countryService;
            _countryRepo = countryRepo;
        }

        /// <summary>
        /// Retrieves a list of all countries with their name, capital, and borders.
        /// </summary>
        [HttpGet("All")]
        [ProducesResponseType(typeof(IEnumerable<CountryResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCountries() {
            try {
                var dbCountries = await _countryRepo.GetAllAsync();
                IEnumerable<CountryResponseDto> countriesToReturn;

                if (dbCountries == null || !dbCountries.Any()) {

                    var apiDtos = await _countryService.GetAllCountriesAsync();
                    var countryEntities = apiDtos.ToCountryEntities();

                    await _countryRepo.SaveCountriesAsync(countryEntities);

                    countriesToReturn = apiDtos;
                } else {

                    countriesToReturn = dbCountries.ToCountryResponseDtos();
                }

                return Ok(countriesToReturn);
            }
    // ... (Exception handling remains the same) ...
    catch (HttpRequestException) {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    "The third-party country API is currently unavailable.");
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected error occurred while processing the request.");
            }
        }
    }
}
