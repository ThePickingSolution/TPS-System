using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Picking.Migrations
{
    public partial class process : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Picking.OrderPickingStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Picking.OrderPickingStatus", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Picking.OrderPickingProcess",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrderPicking_Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status_Id = table.Column<int>(type: "int", nullable: false),
                    Operator = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Area = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Container = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    OrderPickingEntityId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Picking.OrderPickingProcess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Picking.OrderPickingProcess_Picking.OrderPicking_OrderPicki~1",
                        column: x => x.OrderPicking_Id,
                        principalTable: "Picking.OrderPicking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Picking.OrderPickingProcess_Picking.OrderPicking_OrderPickin~",
                        column: x => x.OrderPickingEntityId,
                        principalTable: "Picking.OrderPicking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Picking.OrderPickingProcess_Picking.OrderPickingStatus_Statu~",
                        column: x => x.Status_Id,
                        principalTable: "Picking.OrderPickingStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Picking.OrderPickingProcess_OrderPicking_Id",
                table: "Picking.OrderPickingProcess",
                column: "OrderPicking_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Picking.OrderPickingProcess_OrderPickingEntityId",
                table: "Picking.OrderPickingProcess",
                column: "OrderPickingEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Picking.OrderPickingProcess_Status_Id",
                table: "Picking.OrderPickingProcess",
                column: "Status_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Picking.OrderPickingProcess");

            migrationBuilder.DropTable(
                name: "Picking.OrderPickingStatus");
        }
    }
}
