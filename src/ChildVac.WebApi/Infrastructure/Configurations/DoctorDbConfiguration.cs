using ChildVac.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;

namespace ChildVac.WebApi.Infrastructure.Configurations
{
    public class DoctorDbConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasBaseType<User>();

            builder.HasData(new Doctor
            {
                Id = 3,
                Login = "Doctor",
                Password = "123456",
                FirstName = "Doctor",
                LastName = "Test User",
                HospitalId = 1,
                RoleId = 3
            });
        }
    }
}