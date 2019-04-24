﻿// <auto-generated />
using System;
using ChildVac.WebApi.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ChildVac.WebApi.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20190424100026_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity("ChildVac.WebApi.Domain.Entities.Hospital", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Hospitals");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Address of test Hostpital",
                            Name = "Test Hostpital"
                        });
                });

            modelBuilder.Entity("ChildVac.WebApi.Domain.Entities.Prescription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateTime");

                    b.Property<string>("Description");

                    b.Property<string>("Diagnosis");

                    b.Property<int?>("TicketId");

                    b.HasKey("Id");

                    b.HasIndex("TicketId");

                    b.ToTable("Prescriptions");
                });

            modelBuilder.Entity("ChildVac.WebApi.Domain.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Child"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Doctor"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Parent"
                        });
                });

            modelBuilder.Entity("ChildVac.WebApi.Domain.Entities.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ChildId");

                    b.Property<int>("DoctorId");

                    b.Property<DateTime>("StartDateTime");

                    b.HasKey("Id");

                    b.HasIndex("ChildId");

                    b.HasIndex("DoctorId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("ChildVac.WebApi.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("Gender");

                    b.Property<string>("Iin")
                        .IsRequired()
                        .HasMaxLength(12);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Patronim")
                        .HasMaxLength(50);

                    b.Property<int?>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("Iin")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("ChildVac.WebApi.Domain.Entities.Vaccination", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateTime");

                    b.Property<int?>("TicketId");

                    b.Property<int?>("VaccineId");

                    b.HasKey("Id");

                    b.HasIndex("TicketId");

                    b.HasIndex("VaccineId");

                    b.ToTable("Vaccinations");
                });

            modelBuilder.Entity("ChildVac.WebApi.Domain.Entities.Vaccine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("RecieveMonth");

                    b.HasKey("Id");

                    b.ToTable("Vaccines");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "БЦЖ (Bacillus Calmette – Guérin - Бацилла Кальметта — Герена) – вакцина от туберкулеза. Прививка делается дважды: в 1- 4 дни жизни еще в роддоме и ревакцинация в 6 лет.",
                            Name = "БЦЖ",
                            RecieveMonth = 0
                        },
                        new
                        {
                            Id = 2,
                            Description = "БЦЖ (Bacillus Calmette – Guérin - Бацилла Кальметта — Герена) – вакцина от туберкулеза. Прививка делается дважды: в 1- 4 дни жизни еще в роддоме и ревакцинация в 6 лет.",
                            Name = "БЦЖ",
                            RecieveMonth = 72
                        },
                        new
                        {
                            Id = 3,
                            Description = "Вакцина против вирусного гепатита В.",
                            Name = "ВГВ",
                            RecieveMonth = 0
                        },
                        new
                        {
                            Id = 4,
                            Description = "Против коклюша, дифтерии, столбняка, вирусного гепатита B, гемофильной инфекции типа b и инактивированная полиовакцина.",
                            Name = "АбКДС + Хиб + ВГВ + ИПВ",
                            RecieveMonth = 2
                        },
                        new
                        {
                            Id = 5,
                            Description = "Против коклюша, дифтерии, столбняка, вирусного гепатита B, гемофильной инфекции типа b и инактивированная полиовакцина.",
                            Name = "АбКДС + Хиб + ВГВ + ИПВ",
                            RecieveMonth = 4
                        },
                        new
                        {
                            Id = 6,
                            Description = "Против пневмококковой инфекции",
                            Name = "Пневмо",
                            RecieveMonth = 2
                        },
                        new
                        {
                            Id = 7,
                            Description = "Против пневмококковой инфекции",
                            Name = "Пневмо",
                            RecieveMonth = 4
                        },
                        new
                        {
                            Id = 8,
                            Description = "Против пневмококковой инфекции",
                            Name = "Пневмо",
                            RecieveMonth = 12
                        },
                        new
                        {
                            Id = 9,
                            Description = "Против коклюша, дифтерии, столбняка, вирусного гепатита B, гемофильной инфекции типа b и инактивированная полиовакцина.",
                            Name = "АбКДС + Хиб + ИПВ",
                            RecieveMonth = 3
                        },
                        new
                        {
                            Id = 10,
                            Description = "Против коклюша, дифтерии, столбняка, вирусного гепатита B, гемофильной инфекции типа b и инактивированная полиовакцина.",
                            Name = "АбКДС + Хиб + ИПВ",
                            RecieveMonth = 18
                        },
                        new
                        {
                            Id = 11,
                            Description = "Полио - против полиомиелита - оральная.",
                            Name = "ОПВ",
                            RecieveMonth = 12
                        },
                        new
                        {
                            Id = 12,
                            Description = "Против кори, краснухи и эпидемического паротита.",
                            Name = "ККП",
                            RecieveMonth = 12
                        },
                        new
                        {
                            Id = 13,
                            Description = "Против кори, краснухи и эпидемического паротита.",
                            Name = "ККП",
                            RecieveMonth = 72
                        },
                        new
                        {
                            Id = 14,
                            Description = "Против дифтерии, коклюша и столбняка.",
                            Name = "АбКДС",
                            RecieveMonth = 72
                        },
                        new
                        {
                            Id = 15,
                            Description = "Против дифтерии и столбняка.",
                            Name = "АДС-М",
                            RecieveMonth = 192
                        });
                });

            modelBuilder.Entity("ChildVac.WebApi.Domain.Entities.Admin", b =>
                {
                    b.HasBaseType("ChildVac.WebApi.Domain.Entities.User");

                    b.HasDiscriminator().HasValue("Admin");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "Admin",
                            Gender = 0,
                            Iin = "123456789012",
                            LastName = "Superuser",
                            Password = "123456",
                            RoleId = 1
                        });
                });

            modelBuilder.Entity("ChildVac.WebApi.Domain.Entities.Child", b =>
                {
                    b.HasBaseType("ChildVac.WebApi.Domain.Entities.User");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<int>("ParentId");

                    b.HasIndex("ParentId");

                    b.HasDiscriminator().HasValue("Child");
                });

            modelBuilder.Entity("ChildVac.WebApi.Domain.Entities.Doctor", b =>
                {
                    b.HasBaseType("ChildVac.WebApi.Domain.Entities.User");

                    b.Property<int>("HospitalId");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.HasIndex("HospitalId");

                    b.HasDiscriminator().HasValue("Doctor");

                    b.HasData(
                        new
                        {
                            Id = 2,
                            FirstName = "Test",
                            Gender = 1,
                            Iin = "970812300739",
                            LastName = "Doctor",
                            Password = "test",
                            Patronim = "Testovich",
                            RoleId = 3,
                            HospitalId = 1,
                            PhoneNumber = "7087260265"
                        });
                });

            modelBuilder.Entity("ChildVac.WebApi.Domain.Entities.Parent", b =>
                {
                    b.HasBaseType("ChildVac.WebApi.Domain.Entities.User");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnName("Parent_PhoneNumber")
                        .HasMaxLength(10);

                    b.HasDiscriminator().HasValue("Parent");
                });

            modelBuilder.Entity("ChildVac.WebApi.Domain.Entities.Prescription", b =>
                {
                    b.HasOne("ChildVac.WebApi.Domain.Entities.Ticket", "Ticket")
                        .WithMany()
                        .HasForeignKey("TicketId");
                });

            modelBuilder.Entity("ChildVac.WebApi.Domain.Entities.Ticket", b =>
                {
                    b.HasOne("ChildVac.WebApi.Domain.Entities.Child", "Child")
                        .WithMany()
                        .HasForeignKey("ChildId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ChildVac.WebApi.Domain.Entities.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ChildVac.WebApi.Domain.Entities.User", b =>
                {
                    b.HasOne("ChildVac.WebApi.Domain.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("ChildVac.WebApi.Domain.Entities.Vaccination", b =>
                {
                    b.HasOne("ChildVac.WebApi.Domain.Entities.Ticket", "Ticket")
                        .WithMany()
                        .HasForeignKey("TicketId");

                    b.HasOne("ChildVac.WebApi.Domain.Entities.Vaccine", "Vaccine")
                        .WithMany()
                        .HasForeignKey("VaccineId");
                });

            modelBuilder.Entity("ChildVac.WebApi.Domain.Entities.Child", b =>
                {
                    b.HasOne("ChildVac.WebApi.Domain.Entities.Parent", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ChildVac.WebApi.Domain.Entities.Doctor", b =>
                {
                    b.HasOne("ChildVac.WebApi.Domain.Entities.Hospital", "Hospital")
                        .WithMany()
                        .HasForeignKey("HospitalId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
