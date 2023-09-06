using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRepairShop.web.Migrations
{
    public partial class AddVehicleImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Model",
                table: "Vehicles",
                newName: "ImageUrl");

            migrationBuilder.AddColumn<string>(
                name: "CarModel",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarModel",
                table: "Vehicles");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Vehicles",
                newName: "Model");
        }
    }
}
