using Microsoft.EntityFrameworkCore;
using ValVenalEstimatorApi.Models;

namespace ValVenalEstimatorApi.Data
{
    public class ValVenalEstimatorDbContext : DbContext
    {
        public ValVenalEstimatorDbContext(DbContextOptions<ValVenalEstimatorDbContext> options) : base(options)
        {
            
        }
        public DbSet<Place> Places { get; set; }
        public DbSet<Prefecture> Prefectures { get; set; }
        
    }
}