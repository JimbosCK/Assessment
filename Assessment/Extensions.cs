using Assessment.Dtos;
using Assessment.Model;

namespace Assessment {
    public static class CountryMapper {
        public static IEnumerable<Country> ToCountryEntities(this IEnumerable<CountryResponseDto> dtos) {
            if (dtos == null) {
                return Enumerable.Empty<Country>();
            }

            return dtos.Select(dto => new Country {
                Name = dto.Name,
                Capital = dto.Capital,
                Borders = dto.Borders.Select(code => new Border {
                    BorderCode = code,
                }).ToList()
            }).ToList();
        }

        public static IEnumerable<CountryResponseDto> ToCountryResponseDtos(this IEnumerable<Country> entities) {
            if (entities == null) {
                return Enumerable.Empty<CountryResponseDto>();
            }

            return entities.Select(entity => new CountryResponseDto {
                Name = entity.Name,
                Capital = entity.Capital,
                Borders = entity.Borders
                    .Select(b => b.BorderCode)
                    .ToList()
                    ?? Enumerable.Empty<string>()
            }).ToList();
        }
    }
}
