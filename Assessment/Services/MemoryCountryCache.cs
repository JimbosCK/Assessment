using Assessment.Dtos;
using Microsoft.Extensions.Caching.Memory;

namespace Assessment.Services {
    public class MemoryCountryCache : ICountryCache {
        private readonly IMemoryCache _cache;
        private const string CacheKey = "AllRestCountriesData";
        private readonly TimeSpan CacheDuration = TimeSpan.FromDays(30);

        public MemoryCountryCache(IMemoryCache cache) {
            _cache = cache;
        }

        public List<CountryResponseDto> GetCountries() {
            if (_cache.TryGetValue(CacheKey, out List<CountryResponseDto> countries)) {
                return countries;
            }

            return null;
        }

        public void SetCountries(List<CountryResponseDto> countries) {
            if (countries == null || !countries.Any()) {
                return;
            }

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(CacheDuration)
                .SetPriority(CacheItemPriority.Normal);

            _cache.Set(CacheKey, countries, cacheEntryOptions);
        }

        public void Clear() {
            _cache.Remove(CacheKey);
        }
    }

}
