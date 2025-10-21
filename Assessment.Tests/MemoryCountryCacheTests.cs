using Assessment.Dtos;
using Assessment.Services;
using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace Assessment.Tests {
    public class MemoryCountryCacheTests {
        private readonly Mock<IMemoryCache> _mockCache;
        private readonly MemoryCountryCache _cacheService;
        private const string CacheKey = "AllRestCountriesData";

        public MemoryCountryCacheTests() {
            _mockCache = new Mock<IMemoryCache>();
            _cacheService = new MemoryCountryCache(_mockCache.Object);
        }

        private List<CountryResponseDto> GetTestCountries() {
            return new List<CountryResponseDto>{
                new CountryResponseDto { Name = "Greece", Capital = "Athens", Borders = new List<string> { "ALB", "BGR", "TUR", "MKD"} },
                new CountryResponseDto { Name = "Mexico", Capital = "Mexico City", Borders = new List<string> { "USA", "Guatemala" } }
            };
        }

        [Fact]
        public void GetCountries_CacheHit_ReturnsCountries() {
            var expectedCountries = GetTestCountries();
            object cachedValue = expectedCountries;

            _mockCache.Setup(m => m.TryGetValue(CacheKey, out cachedValue))
                      .Returns(true);


            var result = _cacheService.GetCountries();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Greece", result.First().Name);

            _mockCache.Verify(m => m.TryGetValue(CacheKey, out It.Ref<object>.IsAny), Times.Once);
        }

        [Fact]
        public void GetCountries_CacheMiss_ReturnsNull() {
            object cachedValue = null;

            _mockCache.Setup(m => m.TryGetValue(CacheKey, out cachedValue))
                      .Returns(false);

            var result = _cacheService.GetCountries();

            Assert.Null(result);
        }

        [Fact]
        public void SetCountries_ValidData_CallsSetWithCorrectKey() {
            var countriesToCache = GetTestCountries();
            var mockCacheEntry = new Mock<ICacheEntry>();

            _mockCache.Setup(m => m.CreateEntry(CacheKey)).Returns(mockCacheEntry.Object);
            _cacheService.SetCountries(countriesToCache);

            _mockCache.Verify(m => m.CreateEntry(CacheKey), Times.Once);
            mockCacheEntry.VerifySet(
                m => m.Value = countriesToCache,
                Times.Once
            );
            mockCacheEntry.VerifySet(
                m => m.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(30),
                Times.Once
            );
            mockCacheEntry.Verify(m => m.Dispose(), Times.Once);
        }

        [Fact]
        public void SetCountries_NullOrEmptyData_DoesNotCallSet() {
            List<CountryResponseDto> nullCountries = null;

            // Act (Null)
            _cacheService.SetCountries(nullCountries);

            var emptyCountries = new List<CountryResponseDto>();

            // Act (Empty)
            _cacheService.SetCountries(emptyCountries);

            _mockCache.Verify(m => m.CreateEntry(It.IsAny<object>()), Times.Never);
        }

        [Fact]
        public void Clear_CallsRemoveWithCorrectKey() {
            _cacheService.Clear();

            _mockCache.Verify(m => m.Remove(CacheKey), Times.Once);
        }
    }
}
