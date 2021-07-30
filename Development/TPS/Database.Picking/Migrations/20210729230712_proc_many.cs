using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Picking.Migrations
{
    public partial class proc_many : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Picking.OrderPickingProcess_Picking.OrderPicking_OrderPicki~1",
                table: "Picking.OrderPickingProcess");

            migrationBuilder.DropForeignKey(
                name: "FK_Picking.OrderPickingProcess_Picking.OrderPicking_OrderPickin~",
                table: "Picking.OrderPickingProcess");

            migrationBuilder.DropIndex(
                name: "IX_Picking.OrderPickingProcess_OrderPickingEntityId",
                table: "Picking.OrderPickingProcess");

            migrationBuilder.DropColumn(
                name: "OrderPickingEntityId",
                table: "Picking.OrderPickingProcess");

            migrationBuilder.AddForeignKey(
                name: "FK_Picking.OrderPickingProcess_Picking.OrderPicking_OrderPickin~",
                table: "Picking.OrderPickingProcess",
                column: "OrderPicking_Id",
                principalTable: "Picking.OrderPicking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Picking.OrderPickingProcess_Picking.OrderPicking_OrderPickin~",
                table: "Picking.OrderPickingProcess");

            migrationBuilder.AddColumn<string>(
                name: "OrderPickingEntityId",
                table: "Picking.OrderPickingProcess",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Picking.OrderPickingProcess_OrderPickingEntityId",
                table: "Picking.OrderPickingProcess",
                column: "OrderPickingEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Picking.OrderPickingProcess_Picking.OrderPicking_OrderPicki~1",
                table: "Picking.OrderPickingProcess",
                column: "OrderPicking_Id",
                principalTable: "Picking.OrderPicking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Picking.OrderPickingProcess_Picking.OrderPicking_OrderPickin~",
                table: "Picking.OrderPickingProcess",
                column: "OrderPickingEntityId",
                principalTable: "Picking.OrderPicking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
