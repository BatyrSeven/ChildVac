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
            modelBuilder.ApplyConfiguration(new RoleDbConfiguration());
            modelBuilder.ApplyConfiguration(new TicketDbConfiguration());
            modelBuilder.ApplyConfiguration(new UserDbConfiguration());
            modelBuilder.ApplyConfiguration(new VaccinationDbConfiguration());
            modelBuilder.ApplyConfiguration(new VaccineDbConfiguration());

            // adding roles
            //var adminRole = new Role { Id = 1, Name = "admin" };
            //var childRole = new Role { Id = 2, Name = "child" };
            //var doctorRole = new Role { Id = 3, Name = "doctor" };
            //var parentRole = new Role { Id = 4, Name = "parent" };

            //modelBuilder.Entity<Role>().HasData(adminRole, childRole, doctorRole, parentRole);

            //var adminUser = new Admin
            //{
            //    Id = 1,
            //    Login = "admin",
            //    Password = "123456",
            //    FirstName = "Admin Name",
            //    LastName = "Admin Surname",
            //    RoleId = adminRole.Id
            //};
            //modelBuilder.Entity<Admin>().HasData(adminUser);
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Child> Children { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vaccination> Vaccinations { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }
    }
}
