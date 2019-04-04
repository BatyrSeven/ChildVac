using ChildVac.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChildVac.WebApi.Infrastructure.Configurations
{
    public class RoleDbConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}