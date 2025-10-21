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
        private readonly ICountryCache _countryCache;

        public CountriesController(IExternalCountryService countryService, CountryRepo countryRepo, ICountryCache countryCache) {
            _countryService = countryService;
            _countryRepo = countryRepo;
            _countryCache = countryCache;
        }

        /// <summary>
        /// Retrieves a list of all countries with their name, capital, and borders.
        /// </summary>
        [HttpGet("All")]
        [ProducesResponseType(typeof(IEnumerable<CountryResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCountries() {
            try {
                var cachedCountries = _countryCache.GetCountries();
                //  Cache Hit
                if (cachedCountries != null && cachedCountries.Any()) {
                    return Ok(cachedCountries);
                }

                //  Cache Miss
                var dbEntities = await _countryRepo.GetAllAsync();
                if (dbEntities != null && dbEntities.Any()) {
                    var cacheDtos = dbEntities.ToCountryResponseDtos().ToList();
                    _countryCache.SetCountries(cacheDtos);

                    var responseFromDb = dbEntities.ToCountryResponseDtos();
                    return Ok(responseFromDb);
                }

                //  Both Cache and DB Miss
                var apiDtos = await _countryService.GetAllCountriesAsync();

                if (apiDtos == null || !apiDtos.Any()) {
                    return NotFound("No country data available from the external source.");
                }

                //  Save to DB
                var countryEntities = apiDtos.ToCountryEntities();
                await _countryRepo.SaveCountriesAsync(countryEntities);

                //  Save to Cache
                _countryCache.SetCountries(apiDtos.ToList());

                return Ok(apiDtos);
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
