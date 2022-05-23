using Microsoft.EntityFrameworkCore.Migrations;

namespace SciPaperService.Migrations
{
    public partial class AddedFluentApi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Section_Papers_PaperId",
                table: "Section");

            migrationBuilder.AlterColumn<int>(
                name: "PaperId",
                table: "Section",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Section_Papers_PaperId",
                table: "Section",
                column: "PaperId",
                principalTable: "Papers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Section_Papers_PaperId",
                table: "Section");

            migrationBuilder.AlterColumn<int>(
                name: "PaperId",
                table: "Section",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Section_Papers_PaperId",
                table: "Section",
                column: "PaperId",
                principalTable: "Papers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
