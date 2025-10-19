using Assessment.Dtos;

namespace Assessment.Services {
    public interface IExternalCountryService {
        Task<IEnumerable<CountryResponseDto>> GetAllCountriesAsync();
    }
}
