using ChildVac.WebApi.Infrastructure.Configurations;
using ChildVac.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ChildVac.WebApi.Infrastructure
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {

        }

        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AdminDbConfiguration());
            modelBuilder.ApplyConfiguration(new ChildDbConfiguration());
            modelBuilder.ApplyConfiguration(new DoctorDbConfiguration());
            modelBuilder.ApplyConfiguration(new HospitalDbConfiguration());
            modelBuilder.ApplyConfiguration(new ParentDbConfiguration());
            modelBuilder.ApplyConfiguration(new PrescriptionDbConfiguration());
            modelBuilder.ApplyConfiguration(new TicketDbConfiguration());
            modelBuilder.ApplyConfiguration(new UserDbConfiguration());
            modelBuilder.ApplyConfiguration(new VaccinationDbConfiguration());
            modelBuilder.ApplyConfiguration(new VaccineDbConfiguration());
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Child> Children { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vaccination> Vaccinations { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }
    }
}
