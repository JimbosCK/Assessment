using Assessment.EF.Context;
using Assessment.Model;
using Microsoft.EntityFrameworkCore;

namespace Assessment.EF.Repositories {
    public class CountryRepo {
        private readonly AssessmentDbContext _context;

        public CountryRepo(AssessmentDbContext context) {
            _context = context;
        }

        public async Task<IEnumerable<Country>> GetAllAsync() {
            return await _context.Countries.ToListAsync();
        }

        public async Task SaveCountriesAsync(IEnumerable<Country> countries) {
            if (countries == null) {
                return;
            }

            await _context.Countries.AddRangeAsync(countries);
            await _context.SaveChangesAsync();
        }
    }
}
