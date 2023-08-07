using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetClinic.Data.Migrations
{
    public partial class AddIsActiveToVeterinarian : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Vets",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Vets");
        }
    }
}
