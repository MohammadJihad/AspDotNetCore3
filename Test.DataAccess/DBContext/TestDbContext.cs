using Microsoft.EntityFrameworkCore;
using Test.DataAccess.Mappings;
using Test.DataAccess.Migrations.Seed;
using Test.Entities.Entities;

namespace Test.DataAccess.DBContext
{
    public class TestDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Country> Countries { get; set; }

        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new UserMapping());
            //modelBuilder.ApplyConfiguration(new CountryMapping());
            base.OnModelCreating(modelBuilder);
        }
    }
}
