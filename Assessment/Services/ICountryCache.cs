using Assessment.Dtos;

namespace Assessment.Services {
    public interface ICountryCache {
        List<CountryResponseDto> GetCountries();
        void SetCountries(List<CountryResponseDto> countries);
        void Clear();
    }
}
