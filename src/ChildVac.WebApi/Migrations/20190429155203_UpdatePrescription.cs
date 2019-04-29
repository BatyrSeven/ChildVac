using Microsoft.EntityFrameworkCore.Migrations;

namespace ChildVac.WebApi.Migrations
{
    public partial class UpdatePrescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Medication",
                table: "Prescriptions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Prescriptions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Medication",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Prescriptions");
        }
    }
}
