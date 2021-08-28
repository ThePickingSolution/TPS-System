using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Picking.Migrations
{
    public partial class itemproc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Picking.PickingItemStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Picking.PickingItemStatus", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PickingItemProcessEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PickingItem_Id = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PickingItemId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status_Id = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: true),
                    Operator = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Barcode = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PickingItemProcessEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PickingItemProcessEntity_Picking.PickingItem_PickingItemId",
                        column: x => x.PickingItemId,
                        principalTable: "Picking.PickingItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PickingItemProcessEntity_Picking.PickingItemStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Picking.PickingItemStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Picking.PickingItemStatus",
                columns: new[] { "Id", "Status" },
                values: new object[,]
                {
                    { 1000, "PENDING" },
                    { 1500, "PENDING_READING" },
                    { 2000, "NO_READING" },
                    { 3000, "MISSING" },
                    { 4000, "PICKED" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PickingItemProcessEntity_PickingItemId",
                table: "PickingItemProcessEntity",
                column: "PickingItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PickingItemProcessEntity_StatusId",
                table: "PickingItemProcessEntity",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PickingItemProcessEntity");

            migrationBuilder.DropTable(
                name: "Picking.PickingItemStatus");
        }
    }
}
