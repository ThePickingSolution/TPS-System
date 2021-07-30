using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Picking.Migrations
{
    public partial class pikingstatusseed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Picking.OrderPickingStatus",
                columns: new[] { "Id", "Status" },
                values: new object[,]
                {
                    { 1000, "PENDING" },
                    { 2000, "WIP" },
                    { 3000, "READY" },
                    { 4000, "PICKED" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Picking.OrderPickingStatus",
                keyColumn: "Id",
                keyValue: 1000);

            migrationBuilder.DeleteData(
                table: "Picking.OrderPickingStatus",
                keyColumn: "Id",
                keyValue: 2000);

            migrationBuilder.DeleteData(
                table: "Picking.OrderPickingStatus",
                keyColumn: "Id",
                keyValue: 3000);

            migrationBuilder.DeleteData(
                table: "Picking.OrderPickingStatus",
                keyColumn: "Id",
                keyValue: 4000);
        }
    }
}
