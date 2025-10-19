using Assessment.Dtos;

namespace Assessment.Services {
    public class ExternalCountryService : IExternalCountryService {
        private readonly HttpClient _httpClient;

        public ExternalCountryService(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CountryResponseDto>> GetAllCountriesAsync() {
            // fields are required
            const string apiUrl = "all?fields=name,capital,borders";

            var restCountries = await _httpClient.GetFromJsonAsync<IEnumerable<RestCountryDto>>(apiUrl);

            if (restCountries == null) {
                return Enumerable.Empty<CountryResponseDto>();
            }

            var responseDtos = restCountries.Select(c => new CountryResponseDto {
                Name = c.Name?.Common ?? "N/A",
                Capital = c.Capital?.FirstOrDefault() ?? "N/A",
                Borders = c.Borders ?? Enumerable.Empty<string>()
            }).ToList();

            return responseDtos;
        }
    }
}
