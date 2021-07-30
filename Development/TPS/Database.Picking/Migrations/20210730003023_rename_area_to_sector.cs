using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Picking.Migrations
{
    public partial class rename_area_to_sector : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Area",
                table: "Picking.OrderPickingProcess",
                newName: "Sector");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sector",
                table: "Picking.OrderPickingProcess",
                newName: "Area");
        }
    }
}
