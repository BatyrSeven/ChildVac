using ChildVac.WebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChildVac.WebApi.Infrastructure.Configurations
{
    public class VaccineDbConfiguration : IEntityTypeConfiguration<Vaccine>
    {
        public void Configure(EntityTypeBuilder<Vaccine> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.RecieveMonth)
                .IsRequired();

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.HasData(
                new Vaccine
                {
                    Id = 1,
                    Name = "БЦЖ",
                    RecieveMonth = 0,
                    Description = "БЦЖ (Bacillus Calmette – Guérin - Бацилла Кальметта — Герена) – вакцина от туберкулеза. " +
                                  "Прививка делается дважды: в 1- 4 дни жизни еще в роддоме и ревакцинация в 6 лет."
                },
                new Vaccine
                {
                    Id = 2,
                    Name = "БЦЖ",
                    RecieveMonth = 72,
                    Description = "БЦЖ (Bacillus Calmette – Guérin - Бацилла Кальметта — Герена) – вакцина от туберкулеза. " +
                              "Прививка делается дважды: в 1- 4 дни жизни еще в роддоме и ревакцинация в 6 лет."
                },
                new Vaccine
                {
                    Id = 3,
                    Name = "ВГВ",
                    RecieveMonth = 0,
                    Description = "Вакцина против вирусного гепатита В."
                },
                new Vaccine
                {
                    Id = 4,
                    Name = "АбКДС + Хиб + ВГВ + ИПВ",
                    RecieveMonth = 2,
                    Description = "Против коклюша, дифтерии, столбняка, вирусного гепатита B, " +
                                  "гемофильной инфекции типа b и инактивированная полиовакцина."
                },
                new Vaccine
                {
                    Id = 5,
                    Name = "АбКДС + Хиб + ВГВ + ИПВ",
                    RecieveMonth = 4,
                    Description = "Против коклюша, дифтерии, столбняка, вирусного гепатита B, " +
                                  "гемофильной инфекции типа b и инактивированная полиовакцина."
                },
                new Vaccine
                {
                    Id = 6,
                    Name = "Пневмо",
                    RecieveMonth = 2,
                    Description = "Против пневмококковой инфекции"
                },
                new Vaccine
                {
                    Id = 7,
                    Name = "Пневмо",
                    RecieveMonth = 4,
                    Description = "Против пневмококковой инфекции"
                },
                new Vaccine
                {
                    Id = 8,
                    Name = "Пневмо",
                    RecieveMonth = 12,
                    Description = "Против пневмококковой инфекции"
                },
                new Vaccine
                {
                    Id = 9,
                    Name = "АбКДС + Хиб + ИПВ",
                    RecieveMonth = 3,
                    Description = "Против коклюша, дифтерии, столбняка, вирусного гепатита B, " +
                                  "гемофильной инфекции типа b и инактивированная полиовакцина."
                },
                new Vaccine
                {
                    Id = 10,
                    Name = "АбКДС + Хиб + ИПВ",
                    RecieveMonth = 18,
                    Description = "Против коклюша, дифтерии, столбняка, вирусного гепатита B, " +
                                  "гемофильной инфекции типа b и инактивированная полиовакцина."
                },
                new Vaccine
                {
                    Id = 11,
                    Name = "ОПВ",
                    RecieveMonth = 12,
                    Description = "Полио - против полиомиелита - оральная."
                },
                new Vaccine
                {
                    Id = 12,
                    Name = "ККП",
                    RecieveMonth = 12,
                    Description = "Против кори, краснухи и эпидемического паротита."
                },
                new Vaccine
                {
                    Id = 13,
                    Name = "ККП",
                    RecieveMonth = 72,
                    Description = "Против кори, краснухи и эпидемического паротита."
                },
                new Vaccine
                {
                    Id = 14,
                    Name = "АбКДС",
                    RecieveMonth = 72,
                    Description = "Против дифтерии, коклюша и столбняка."
                },
                new Vaccine
                {
                    Id = 15,
                    Name = "АДС-М",
                    RecieveMonth = 192,
                    Description = "Против дифтерии и столбняка."
                }
            );
        }
    }
}