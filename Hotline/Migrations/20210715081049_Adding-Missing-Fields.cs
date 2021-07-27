using Microsoft.EntityFrameworkCore.Migrations;

namespace Hotline.Migrations
{
    public partial class AddingMissingFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Reclamations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Projets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjetId",
                table: "Domaines",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reclamations_ClientId",
                table: "Reclamations",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Projets_ClientId",
                table: "Projets",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Domaines_ProjetId",
                table: "Domaines",
                column: "ProjetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Domaines_Projets_ProjetId",
                table: "Domaines",
                column: "ProjetId",
                principalTable: "Projets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projets_Clients_ClientId",
                table: "Projets",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reclamations_Clients_ClientId",
                table: "Reclamations",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Domaines_Projets_ProjetId",
                table: "Domaines");

            migrationBuilder.DropForeignKey(
                name: "FK_Projets_Clients_ClientId",
                table: "Projets");

            migrationBuilder.DropForeignKey(
                name: "FK_Reclamations_Clients_ClientId",
                table: "Reclamations");

            migrationBuilder.DropIndex(
                name: "IX_Reclamations_ClientId",
                table: "Reclamations");

            migrationBuilder.DropIndex(
                name: "IX_Projets_ClientId",
                table: "Projets");

            migrationBuilder.DropIndex(
                name: "IX_Domaines_ProjetId",
                table: "Domaines");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Reclamations");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Projets");

            migrationBuilder.DropColumn(
                name: "ProjetId",
                table: "Domaines");
        }
    }
}
