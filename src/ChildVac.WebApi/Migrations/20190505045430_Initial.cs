using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChildVac.WebApi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Advices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(maxLength: 1000, nullable: false),
                    Text = table.Column<string>(maxLength: 5000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advices", x => x.Id);
                });

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
                    Parent_PhoneNumber = table.Column<string>(maxLength: 10, nullable: true),
                    Email = table.Column<string>(nullable: true)
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
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    DoctorName = table.Column<string>(nullable: true),
                    Text = table.Column<string>(maxLength: 10000, nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Rate = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    Room = table.Column<string>(nullable: true),
                    StartDateTime = table.Column<DateTime>(nullable: false),
                    TicketType = table.Column<int>(nullable: false),
                    VaccineId = table.Column<int>(nullable: true),
                    ChildId = table.Column<int>(nullable: false),
                    DoctorId = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false)
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
                    table.ForeignKey(
                        name: "FK_Tickets_Vaccines_VaccineId",
                        column: x => x.VaccineId,
                        principalTable: "Vaccines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Diagnosis = table.Column<string>(nullable: true),
                    Medication = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    TicketId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Advices",
                columns: new[] { "Id", "Text", "Title" },
                values: new object[] { 1, "Вакцина — это лекарство, которое создает у человека устойчивость (иммунитет) к болезни. Слово «вакцинация» происходит от «вакциния» (название вируса коровьей оспы). Этот вирус использовался в первой в истории вакцине(от оспы). Современная медицина создала множество вакцин. Вакцины ПРЕДУПРЕЖДАЮТ вирусные и бактериальные инфекции, которые когда - то приводили к тяжелым болезням и смерти.", "Что такое вакцина?" });

            migrationBuilder.InsertData(
                table: "Advices",
                columns: new[] { "Id", "Text", "Title" },
                values: new object[] { 2, "У маленьких детей иммунитет лучше, чем у взрослых людей и детей постарше.Когда маленькому ребенку одновременно делают несколько прививок, у него формируется хороший иммунитет к нескольким болезням. Даже если ребенку сделать 11 прививок одновременно, его иммунная система потратит на них только 0, 1 % своих возможностей.", "Не слишком ли часто ребенку делают прививки?" });

            migrationBuilder.InsertData(
                table: "Advices",
                columns: new[] { "Id", "Text", "Title" },
                values: new object[] { 3, "Нет! Это распространенное заблуждение. Прививки можно делать, даже если ребенок немножко болен.Переоценить важность своевременных прививок невозможно. Не переносите прививку из-за того, что ребенок немножко сопливый. Прививки можно делать, даже когда ребенка лечат антибиотиками.", "Мой ребенок простужен. Не отложить ли прививки?" });

            migrationBuilder.InsertData(
                table: "Advices",
                columns: new[] { "Id", "Text", "Title" },
                values: new object[] { 4, "Да, но мы вам это не советуем. Отказываться от прививок небезопасно. Ребенок может заразиться опасным инфекционным заболеванием. Кроме того, такой ребенок создает для детского коллектива опасность заражения серьезными заболеваниями, которые можно было бы предотвратить.", "А можно я все-таки не буду прививать ребенка?" });

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
                values: new object[] { 3, "Вакцина против вирусного гепатита В.", "ВГВ", 0 });

            migrationBuilder.InsertData(
                table: "Vaccines",
                columns: new[] { "Id", "Description", "Name", "RecieveMonth" },
                values: new object[] { 6, "Против пневмококковой инфекции", "Пневмо", 2 });

            migrationBuilder.InsertData(
                table: "Vaccines",
                columns: new[] { "Id", "Description", "Name", "RecieveMonth" },
                values: new object[] { 5, "Против коклюша, дифтерии, столбняка, вирусного гепатита B, гемофильной инфекции типа b и инактивированная полиовакцина.", "АбКДС + Хиб + ВГВ + ИПВ", 4 });

            migrationBuilder.InsertData(
                table: "Vaccines",
                columns: new[] { "Id", "Description", "Name", "RecieveMonth" },
                values: new object[] { 4, "Против коклюша, дифтерии, столбняка, вирусного гепатита B, гемофильной инфекции типа b и инактивированная полиовакцина.", "АбКДС + Хиб + ВГВ + ИПВ", 2 });

            migrationBuilder.InsertData(
                table: "Vaccines",
                columns: new[] { "Id", "Description", "Name", "RecieveMonth" },
                values: new object[] { 14, "Против дифтерии, коклюша и столбняка.", "АбКДС", 72 });

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
                values: new object[] { 2, "Doctor", "Батыржан", 1, "970812300739", "Жетписбаев", "test", "Дулатович", 3, 1, "+77087260265" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Discriminator", "FirstName", "Gender", "Iin", "LastName", "Password", "Patronim", "RoleId", "Address", "Email", "Parent_PhoneNumber" },
                values: new object[] { 3, "Parent", "Арман", 1, "970625350560", "Киалбеков", "123456", "Жылдабылович", 4, "ул. Сейфулина, 134А, 33", "arman.kialbekov@gmail.com", "+77089134584" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Discriminator", "FirstName", "Gender", "Iin", "LastName", "Password", "Patronim", "RoleId", "DateOfBirth", "ParentId" },
                values: new object[] { 4, "Child", "Чойбек", 1, "148814881488", "Киалбеков", "123456", "Арманович", 2, new DateTime(2019, 5, 4, 10, 54, 29, 750, DateTimeKind.Local).AddTicks(9233), 3 });

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_UserId",
                table: "Feedbacks",
                column: "UserId");

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
                name: "IX_Tickets_VaccineId",
                table: "Tickets",
                column: "VaccineId");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Advices");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Vaccines");

            migrationBuilder.DropTable(
                name: "Hospitals");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
