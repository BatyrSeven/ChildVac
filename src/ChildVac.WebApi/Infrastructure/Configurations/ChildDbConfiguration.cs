using System;
using ChildVac.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;

namespace ChildVac.WebApi.Infrastructure.Configurations
{
    public class ChildDbConfiguration : IEntityTypeConfiguration<Child>
    {
        public void Configure(EntityTypeBuilder<Child> builder)
        {
            builder.Property(x => x.Iin)
                .IsRequired()
                .HasMaxLength(12);

            builder.Property(x => x.DateOfBirth)
                .IsRequired();

            builder.HasBaseType<User>();
        }
    }
}