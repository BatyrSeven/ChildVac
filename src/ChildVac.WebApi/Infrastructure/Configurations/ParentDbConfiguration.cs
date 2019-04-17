using ChildVac.WebApi.Domain.Entities;
using ChildVac.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChildVac.WebApi.Infrastructure.Configurations
{
    public class ParentDbConfiguration : IEntityTypeConfiguration<Parent>
    {
        public void Configure(EntityTypeBuilder<Parent> builder)
        {
            builder.Property(x => x.Address)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Iin)
                .IsRequired()
                .HasMaxLength(12);

            builder.Property(x => x.PhoneNumber)
                .IsRequired()
                .HasMaxLength(10);

            builder.HasBaseType<User>();
        }
    }
}