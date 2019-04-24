using ChildVac.WebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChildVac.WebApi.Infrastructure.Configurations
{
    public class DoctorDbConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.Property(x => x.PhoneNumber)
                .IsRequired()
                .HasMaxLength(10);

            builder.HasBaseType<User>();

            builder.HasData(new Doctor
            {
                Id = 2,
                FirstName = "Test",
                LastName = "Doctor",
                Patronim = "Testovich",
                Gender = Gender.Male,
                Iin = "970812300739",
                Password = "test",
                PhoneNumber = "7087260265",
                RoleId = 3,
                HospitalId = 1
            });
        }
    }
}