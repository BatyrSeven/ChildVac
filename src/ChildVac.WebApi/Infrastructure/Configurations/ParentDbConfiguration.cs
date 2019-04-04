using System.IO;
using ChildVac.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace ChildVac.WebApi.Infrastructure.Configurations
{
    public class ParentDbConfiguration : IEntityTypeConfiguration<Parent>
    {
        public void Configure(EntityTypeBuilder<Parent> builder)
        {
            builder.Property(x => x.Address)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasBaseType<User>();

            builder.HasData(new Parent
            {
                Id = 4,
                Login = "Parent",
                Password = "123456",
                FirstName = "Parent",
                LastName = "Test User",
                Address = "Test Address",
                RoleId = 4
            });
        }
    }
}