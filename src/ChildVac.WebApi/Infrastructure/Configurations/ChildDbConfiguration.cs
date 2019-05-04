using System;
using ChildVac.WebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChildVac.WebApi.Infrastructure.Configurations
{
    public class ChildDbConfiguration : IEntityTypeConfiguration<Child>
    {
        public void Configure(EntityTypeBuilder<Child> builder)
        {
            builder.Property(x => x.DateOfBirth)
                .IsRequired();

            builder.HasBaseType<User>();

            builder.HasData(
                new Child
                {
                    Id = 3,
                    Iin = "148814881488",
                    Password = "123456",
                    FirstName = "Чойбек",
                    LastName = "Чойбек",
                    Patronim = "Армановыч",
                    Gender = Gender.Female,
                    ParentId = 2,
                    DateOfBirth = DateTime.Now,
                    RoleId = 2
                }
            );
        }
    }
}