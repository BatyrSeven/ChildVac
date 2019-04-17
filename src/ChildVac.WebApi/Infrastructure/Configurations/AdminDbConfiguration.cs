using ChildVac.WebApi.Domain.Entities;
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

            builder.HasData(new Admin
            {
                Id = 1,
                Iin = "123456789012",
                Password = "123456",
                FirstName = "Admin",
                LastName = "Superuser",
                RoleId = 1
            });

        }
    }
}