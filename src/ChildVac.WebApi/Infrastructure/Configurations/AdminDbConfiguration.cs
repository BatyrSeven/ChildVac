using ChildVac.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChildVac.WebApi.Infrastructure.Configurations
{
    public class AdminDbConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasBaseType<User>();

            //if (_configuration.GetValue<bool>("AppConfiguration:UseTestData"))

            builder.HasData(new Admin
            {
                Id = 1,
                Login = "Admin",
                Password = "123456",
                FirstName = "Admin",
                LastName = "Superuser",
                RoleId = 1
            });

        }
    }
}