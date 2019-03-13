using ChildVac.WebApi.Infrastructure.Configurations;
using ChildVac.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ChildVac.WebApi.Infrastructure
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserDbConfiguration());
        }

        public DbSet<User> Users { get; set; }
    }
}
