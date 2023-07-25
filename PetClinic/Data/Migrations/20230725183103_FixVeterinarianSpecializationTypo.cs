using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetClinic.Data.Migrations
{
    public partial class FixVeterinarianSpecializationTypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Specalization",
                table: "Vets",
                newName: "Specialization");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Specialization",
                table: "Vets",
                newName: "Specalization");
        }
    }
}
