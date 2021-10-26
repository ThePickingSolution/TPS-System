using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Picking.Migrations
{
    public partial class itemproc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Picking.OrderPicking",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreationDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Picking.OrderPicking", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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
                name: "Picking.OrderPickingDetail",
                columns: table => new
                {
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OrderPicking_Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "varchar(511)", maxLength: 511, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Picking.OrderPickingDetail", x => new { x.OrderPicking_Id, x.Name });
                    table.ForeignKey(
                        name: "FK_Picking.OrderPickingDetail_Picking.OrderPicking_OrderPicking~",
                        column: x => x.OrderPicking_Id,
                        principalTable: "Picking.OrderPicking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Picking.PickingItem",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SKU = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OrderPicking_Id = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Picking.PickingItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Picking.PickingItem_Picking.OrderPicking_OrderPicking_Id",
                        column: x => x.OrderPicking_Id,
                        principalTable: "Picking.OrderPicking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    Sector = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Container = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Picking.OrderPickingProcess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Picking.OrderPickingProcess_Picking.OrderPicking_OrderPickin~",
                        column: x => x.OrderPicking_Id,
                        principalTable: "Picking.OrderPicking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Picking.OrderPickingProcess_Picking.OrderPickingStatus_Statu~",
                        column: x => x.Status_Id,
                        principalTable: "Picking.OrderPickingStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Picking.PickingItemDetail",
                columns: table => new
                {
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PickingItem_Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "varchar(511)", maxLength: 511, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Picking.PickingItemDetail", x => new { x.PickingItem_Id, x.Name });
                    table.ForeignKey(
                        name: "FK_Picking.PickingItemDetail_Picking.PickingItem_PickingItem_Id",
                        column: x => x.PickingItem_Id,
                        principalTable: "Picking.PickingItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Picking.PickingItemProcess",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PickingItem_Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status_Id = table.Column<int>(type: "int", nullable: false),
                    Operator = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Barcode = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Picking.PickingItemProcess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Picking.PickingItemProcess_Picking.PickingItem_PickingItem_Id",
                        column: x => x.PickingItem_Id,
                        principalTable: "Picking.PickingItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Picking.PickingItemProcess_Picking.PickingItemStatus_Status_~",
                        column: x => x.Status_Id,
                        principalTable: "Picking.PickingItemStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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
                name: "IX_Picking.OrderPickingProcess_OrderPicking_Id",
                table: "Picking.OrderPickingProcess",
                column: "OrderPicking_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Picking.OrderPickingProcess_Status_Id",
                table: "Picking.OrderPickingProcess",
                column: "Status_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Picking.PickingItem_OrderPicking_Id",
                table: "Picking.PickingItem",
                column: "OrderPicking_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Picking.PickingItemProcess_PickingItem_Id",
                table: "Picking.PickingItemProcess",
                column: "PickingItem_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Picking.PickingItemProcess_Status_Id",
                table: "Picking.PickingItemProcess",
                column: "Status_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Picking.OrderPickingDetail");

            migrationBuilder.DropTable(
                name: "Picking.OrderPickingProcess");

            migrationBuilder.DropTable(
                name: "Picking.PickingItemDetail");

            migrationBuilder.DropTable(
                name: "Picking.PickingItemProcess");

            migrationBuilder.DropTable(
                name: "Picking.OrderPickingStatus");

            migrationBuilder.DropTable(
                name: "Picking.PickingItem");

            migrationBuilder.DropTable(
                name: "Picking.PickingItemStatus");

            migrationBuilder.DropTable(
                name: "Picking.OrderPicking");
        }
    }
}
