using Microsoft.EntityFrameworkCore.Migrations;

namespace Hotline.Migrations
{
    public partial class fixing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reclamations_Clients_ClientId",
                table: "Reclamations");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Reclamations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reclamations_Clients_ClientId",
                table: "Reclamations",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reclamations_Clients_ClientId",
                table: "Reclamations");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Reclamations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Reclamations_Clients_ClientId",
                table: "Reclamations",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
