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
            modelBuilder.ApplyConfiguration(new ChildDbConfiguration());
            modelBuilder.ApplyConfiguration(new DoctorDbConfiguration());
            modelBuilder.ApplyConfiguration(new HospitalDbConfiguration());
            modelBuilder.ApplyConfiguration(new ParentDbConfiguration());
            modelBuilder.ApplyConfiguration(new PrescriptionDbConfiguration());
            modelBuilder.ApplyConfiguration(new TicketDbConfiguration());
            modelBuilder.ApplyConfiguration(new VaccinationDbConfiguration());
            modelBuilder.ApplyConfiguration(new VaccineDbConfiguration());
        }

        public DbSet<User> Users { get; set; }
    }
}
