using Microsoft.EntityFrameworkCore;
using TestApp.Domain.Models.DailyIndicator;
using TestApp.Domain.Models.PerformanceIndicator;

namespace TestApp.Adapters.EntityFramework
{
    public class TestAppContext : DbContext
    {
        public const string SCHEMA = "TestApp";

        public DbSet<PerformanceIndicator> PerformanceIndicators { get; set; }
        public DbSet<AverageIndicator> AverageIndicators { get; set; }
        public DbSet<SumIndicator> SumIndicators { get; set; }
        
        public TestAppContext(DbContextOptions options) : base(options) { } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(SCHEMA);
            base.OnModelCreating(modelBuilder);
        }

    }
}