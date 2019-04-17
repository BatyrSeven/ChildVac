using ChildVac.WebApi.Domain.Entities;
using ChildVac.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChildVac.WebApi.Infrastructure.Configurations
{
    public class HospitalDbConfiguration : IEntityTypeConfiguration<Hospital>
    {
        public void Configure(EntityTypeBuilder<Hospital> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired();

            builder.Property(x => x.Address)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}