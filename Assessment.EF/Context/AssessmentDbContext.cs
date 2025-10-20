using Assessment.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Assessment.EF.Context {
    public class AssessmentDbContext : DbContext {
        public AssessmentDbContext() : base() { }

        public AssessmentDbContext(DbContextOptions<AssessmentDbContext> options)
            : base(options) {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Border> Borders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Country>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Border>()
                .Property(b => b.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Border>()
                .HasOne(b => b.Country)
                .WithMany(c => c.Borders)
                .HasForeignKey(b => b.CountryId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
