using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChildVac.WebApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hospitals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: false),
                    Address = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospitals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vaccines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    RecieveMonth = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaccines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Iin = table.Column<string>(maxLength: 12, nullable: false),
                    Password = table.Column<string>(maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    Patronim = table.Column<string>(maxLength: 50, nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: true),
                    ParentId = table.Column<int>(nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 10, nullable: true),
                    HospitalId = table.Column<int>(nullable: true),
                    Address = table.Column<string>(maxLength: 100, nullable: true),
                    Parent_PhoneNumber = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Users_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Hospitals_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "Hospitals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StartDateTime = table.Column<DateTime>(nullable: false),
                    ChildId = table.Column<int>(nullable: false),
                    DoctorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Users_ChildId",
                        column: x => x.ChildId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_Users_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TicketId = table.Column<int>(nullable: true),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Diagnosis = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vaccinations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VaccineId = table.Column<int>(nullable: true),
                    TicketId = table.Column<int>(nullable: true),
                    DateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaccinations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vaccinations_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vaccinations_Vaccines_VaccineId",
                        column: x => x.VaccineId,
                        principalTable: "Vaccines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Hospitals",
                columns: new[] { "Id", "Address", "Name" },
                values: new object[] { 1, "Address of test Hostpital", "Test Hostpital" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Admin" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Child" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Doctor" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { 4, "Parent" });

            migrationBuilder.InsertData(
                table: "Vaccines",
                columns: new[] { "Id", "Description", "Name", "RecieveMonth" },
                values: new object[] { 13, "Против кори, краснухи и эпидемического паротита.", "ККП", 72 });

            migrationBuilder.InsertData(
                table: "Vaccines",
                columns: new[] { "Id", "Description", "Name", "RecieveMonth" },
                values: new object[] { 12, "Против кори, краснухи и эпидемического паротита.", "ККП", 12 });

            migrationBuilder.InsertData(
                table: "Vaccines",
                columns: new[] { "Id", "Description", "Name", "RecieveMonth" },
                values: new object[] { 11, "Полио - против полиомиелита - оральная.", "ОПВ", 12 });

            migrationBuilder.InsertData(
                table: "Vaccines",
                columns: new[] { "Id", "Description", "Name", "RecieveMonth" },
                values: new object[] { 10, "Против коклюша, дифтерии, столбняка, вирусного гепатита B, гемофильной инфекции типа b и инактивированная полиовакцина.", "АбКДС + Хиб + ИПВ", 18 });

            migrationBuilder.InsertData(
                table: "Vaccines",
                columns: new[] { "Id", "Description", "Name", "RecieveMonth" },
                values: new object[] { 9, "Против коклюша, дифтерии, столбняка, вирусного гепатита B, гемофильной инфекции типа b и инактивированная полиовакцина.", "АбКДС + Хиб + ИПВ", 3 });

            migrationBuilder.InsertData(
                table: "Vaccines",
                columns: new[] { "Id", "Description", "Name", "RecieveMonth" },
                values: new object[] { 8, "Против пневмококковой инфекции", "Пневмо", 12 });

            migrationBuilder.InsertData(
                table: "Vaccines",
                columns: new[] { "Id", "Description", "Name", "RecieveMonth" },
                values: new object[] { 5, "Против коклюша, дифтерии, столбняка, вирусного гепатита B, гемофильной инфекции типа b и инактивированная полиовакцина.", "АбКДС + Хиб + ВГВ + ИПВ", 4 });

            migrationBuilder.InsertData(
                table: "Vaccines",
                columns: new[] { "Id", "Description", "Name", "RecieveMonth" },
                values: new object[] { 6, "Против пневмококковой инфекции", "Пневмо", 2 });

            migrationBuilder.InsertData(
                table: "Vaccines",
                columns: new[] { "Id", "Description", "Name", "RecieveMonth" },
                values: new object[] { 14, "Против дифтерии, коклюша и столбняка.", "АбКДС", 72 });

            migrationBuilder.InsertData(
                table: "Vaccines",
                columns: new[] { "Id", "Description", "Name", "RecieveMonth" },
                values: new object[] { 4, "Против коклюша, дифтерии, столбняка, вирусного гепатита B, гемофильной инфекции типа b и инактивированная полиовакцина.", "АбКДС + Хиб + ВГВ + ИПВ", 2 });

            migrationBuilder.InsertData(
                table: "Vaccines",
                columns: new[] { "Id", "Description", "Name", "RecieveMonth" },
                values: new object[] { 3, "Вакцина против вирусного гепатита В.", "ВГВ", 0 });

            migrationBuilder.InsertData(
                table: "Vaccines",
                columns: new[] { "Id", "Description", "Name", "RecieveMonth" },
                values: new object[] { 2, "БЦЖ (Bacillus Calmette – Guérin - Бацилла Кальметта — Герена) – вакцина от туберкулеза. Прививка делается дважды: в 1- 4 дни жизни еще в роддоме и ревакцинация в 6 лет.", "БЦЖ", 72 });

            migrationBuilder.InsertData(
                table: "Vaccines",
                columns: new[] { "Id", "Description", "Name", "RecieveMonth" },
                values: new object[] { 1, "БЦЖ (Bacillus Calmette – Guérin - Бацилла Кальметта — Герена) – вакцина от туберкулеза. Прививка делается дважды: в 1- 4 дни жизни еще в роддоме и ревакцинация в 6 лет.", "БЦЖ", 0 });

            migrationBuilder.InsertData(
                table: "Vaccines",
                columns: new[] { "Id", "Description", "Name", "RecieveMonth" },
                values: new object[] { 7, "Против пневмококковой инфекции", "Пневмо", 4 });

            migrationBuilder.InsertData(
                table: "Vaccines",
                columns: new[] { "Id", "Description", "Name", "RecieveMonth" },
                values: new object[] { 15, "Против дифтерии и столбняка.", "АДС-М", 192 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Discriminator", "FirstName", "Gender", "Iin", "LastName", "Password", "Patronim", "RoleId" },
                values: new object[] { 1, "Admin", "Admin", 0, "123456789012", "Superuser", "123456", null, 1 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Discriminator", "FirstName", "Gender", "Iin", "LastName", "Password", "Patronim", "RoleId", "HospitalId", "PhoneNumber" },
                values: new object[] { 2, "Doctor", "Test", 1, "970812300739", "Doctor", "test", "Testovich", 3, 1, "7087260265" });

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_TicketId",
                table: "Prescriptions",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ChildId",
                table: "Tickets",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_DoctorId",
                table: "Tickets",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ParentId",
                table: "Users",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_HospitalId",
                table: "Users",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Iin",
                table: "Users",
                column: "Iin",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Vaccinations_TicketId",
                table: "Vaccinations",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Vaccinations_VaccineId",
                table: "Vaccinations",
                column: "VaccineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropTable(
                name: "Vaccinations");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Vaccines");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Hospitals");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
