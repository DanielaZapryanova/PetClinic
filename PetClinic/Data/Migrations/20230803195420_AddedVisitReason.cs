using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetClinic.Data.Migrations
{
    public partial class AddedVisitReason : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReasonForVisit",
                table: "Visits",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReasonForVisit",
                table: "Visits");
        }
    }
}
