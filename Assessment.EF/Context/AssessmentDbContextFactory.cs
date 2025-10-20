using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Assessment.EF.Context {
    public class AssessmentDbContextFactory : IDesignTimeDbContextFactory<AssessmentDbContext> {
        public AssessmentDbContext CreateDbContext(string[] args) {
            var configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var builder = new DbContextOptionsBuilder<AssessmentDbContext>();

            builder.UseSqlServer(connectionString, sqlServerOptions => {
                sqlServerOptions.MigrationsAssembly("Assessment.EF");
            });

            return new AssessmentDbContext(builder.Options);
        }
    }
}
