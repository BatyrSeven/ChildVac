using ChildVac.WebApi.Domain.Entities;
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

            builder.HasData(new Parent
            {
                Id = 3,
                Iin = "970625350560",
                Password = "123456",
                FirstName = "Арман",
                LastName = "Киалбеков",
                Patronim = "Жылдабылович",
                Gender = Gender.Male,
                Address = "ул. Сейфулина, 134А, 33",
                PhoneNumber = "+77089134584",
                Email = "arman.kialbekov@gmail.com",
                RoleId = 4
            });
        }
    }
}