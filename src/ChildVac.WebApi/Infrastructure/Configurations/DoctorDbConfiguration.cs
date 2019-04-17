using ChildVac.WebApi.Domain.Entities;
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
            builder.Property(x => x.PhoneNumber)
                .IsRequired()
                .HasMaxLength(10);

            builder.HasBaseType<User>();
        }
    }
}