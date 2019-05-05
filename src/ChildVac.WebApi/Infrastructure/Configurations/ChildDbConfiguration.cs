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
                    Id = 4,
                    Iin = "148814881488",
                    Password = "123456",
                    FirstName = "Чойбек",
                    LastName = "Киалбеков",
                    Patronim = "Арманович",
                    Gender = Gender.Male,
                    DateOfBirth = DateTime.Now.AddDays(-1),
                    ParentId = 3,
                    RoleId = 2,
                }
            );
        }
    }
}