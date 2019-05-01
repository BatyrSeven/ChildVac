using Microsoft.EntityFrameworkCore.Migrations;

namespace ChildVac.WebApi.Migrations
{
    public partial class Advices : Migration
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
                values: new object[] { 4, "Да, но мы вам это не советуем. Отказываться от прививок небезопасно. Ребенок может заразиться опасным инфекционным заболеванием. Кроме того, такой ребенок создает для детского коллектива опасность серьезные заболеваний, которые можно было бы предотвратить. ", "А можно я все-таки не буду прививать ребенка?" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Advices");
        }
    }
}
