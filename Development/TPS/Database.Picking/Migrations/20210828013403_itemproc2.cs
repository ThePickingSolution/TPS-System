using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Picking.Migrations
{
    public partial class itemproc2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PickingItemProcessEntity_Picking.PickingItem_PickingItemId",
                table: "PickingItemProcessEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_PickingItemProcessEntity_Picking.PickingItemStatus_StatusId",
                table: "PickingItemProcessEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PickingItemProcessEntity",
                table: "PickingItemProcessEntity");

            migrationBuilder.DropIndex(
                name: "IX_PickingItemProcessEntity_PickingItemId",
                table: "PickingItemProcessEntity");

            migrationBuilder.DropIndex(
                name: "IX_PickingItemProcessEntity_StatusId",
                table: "PickingItemProcessEntity");

            migrationBuilder.DropColumn(
                name: "PickingItemId",
                table: "PickingItemProcessEntity");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "PickingItemProcessEntity");

            migrationBuilder.RenameTable(
                name: "PickingItemProcessEntity",
                newName: "Picking.PickingItemProcess");

            migrationBuilder.AlterColumn<string>(
                name: "PickingItem_Id",
                table: "Picking.PickingItemProcess",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Operator",
                table: "Picking.PickingItemProcess",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Picking.PickingItemProcess",
                table: "Picking.PickingItemProcess",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Picking.PickingItemProcess_PickingItem_Id",
                table: "Picking.PickingItemProcess",
                column: "PickingItem_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Picking.PickingItemProcess_Status_Id",
                table: "Picking.PickingItemProcess",
                column: "Status_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Picking.PickingItemProcess_Picking.PickingItem_PickingItem_Id",
                table: "Picking.PickingItemProcess",
                column: "PickingItem_Id",
                principalTable: "Picking.PickingItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Picking.PickingItemProcess_Picking.PickingItemStatus_Status_~",
                table: "Picking.PickingItemProcess",
                column: "Status_Id",
                principalTable: "Picking.PickingItemStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Picking.PickingItemProcess_Picking.PickingItem_PickingItem_Id",
                table: "Picking.PickingItemProcess");

            migrationBuilder.DropForeignKey(
                name: "FK_Picking.PickingItemProcess_Picking.PickingItemStatus_Status_~",
                table: "Picking.PickingItemProcess");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Picking.PickingItemProcess",
                table: "Picking.PickingItemProcess");

            migrationBuilder.DropIndex(
                name: "IX_Picking.PickingItemProcess_PickingItem_Id",
                table: "Picking.PickingItemProcess");

            migrationBuilder.DropIndex(
                name: "IX_Picking.PickingItemProcess_Status_Id",
                table: "Picking.PickingItemProcess");

            migrationBuilder.RenameTable(
                name: "Picking.PickingItemProcess",
                newName: "PickingItemProcessEntity");

            migrationBuilder.AlterColumn<string>(
                name: "PickingItem_Id",
                table: "PickingItemProcessEntity",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Operator",
                table: "PickingItemProcessEntity",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PickingItemId",
                table: "PickingItemProcessEntity",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "PickingItemProcessEntity",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PickingItemProcessEntity",
                table: "PickingItemProcessEntity",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PickingItemProcessEntity_PickingItemId",
                table: "PickingItemProcessEntity",
                column: "PickingItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PickingItemProcessEntity_StatusId",
                table: "PickingItemProcessEntity",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_PickingItemProcessEntity_Picking.PickingItem_PickingItemId",
                table: "PickingItemProcessEntity",
                column: "PickingItemId",
                principalTable: "Picking.PickingItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PickingItemProcessEntity_Picking.PickingItemStatus_StatusId",
                table: "PickingItemProcessEntity",
                column: "StatusId",
                principalTable: "Picking.PickingItemStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
