using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hotline.Migrations
{
    public partial class FixingDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reclamations_Produits_ProduitId",
                table: "Reclamations");

            migrationBuilder.DropTable(
                name: "Produits");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Reclamations",
                newName: "Statut");

            migrationBuilder.RenameColumn(
                name: "ProduitId",
                table: "Reclamations",
                newName: "ResponsableId");

            migrationBuilder.RenameColumn(
                name: "Etat",
                table: "Reclamations",
                newName: "Solution");

            migrationBuilder.RenameColumn(
                name: "DateCreaction",
                table: "Reclamations",
                newName: "DateSoumission");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Reclamations",
                newName: "Numero");

            migrationBuilder.RenameIndex(
                name: "IX_Reclamations_ProduitId",
                table: "Reclamations",
                newName: "IX_Reclamations_ResponsableId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAffectation",
                table: "Reclamations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateResolution",
                table: "Reclamations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DomaineId",
                table: "Reclamations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjetId",
                table: "Reclamations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Domaines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Domaines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reclamations_DomaineId",
                table: "Reclamations",
                column: "DomaineId");

            migrationBuilder.CreateIndex(
                name: "IX_Reclamations_ProjetId",
                table: "Reclamations",
                column: "ProjetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reclamations_Domaines_DomaineId",
                table: "Reclamations",
                column: "DomaineId",
                principalTable: "Domaines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reclamations_Projets_ProjetId",
                table: "Reclamations",
                column: "ProjetId",
                principalTable: "Projets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reclamations_User_ResponsableId",
                table: "Reclamations",
                column: "ResponsableId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reclamations_Domaines_DomaineId",
                table: "Reclamations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reclamations_Projets_ProjetId",
                table: "Reclamations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reclamations_User_ResponsableId",
                table: "Reclamations");

            migrationBuilder.DropTable(
                name: "Domaines");

            migrationBuilder.DropTable(
                name: "Projets");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropIndex(
                name: "IX_Reclamations_DomaineId",
                table: "Reclamations");

            migrationBuilder.DropIndex(
                name: "IX_Reclamations_ProjetId",
                table: "Reclamations");

            migrationBuilder.DropColumn(
                name: "DateAffectation",
                table: "Reclamations");

            migrationBuilder.DropColumn(
                name: "DateResolution",
                table: "Reclamations");

            migrationBuilder.DropColumn(
                name: "DomaineId",
                table: "Reclamations");

            migrationBuilder.DropColumn(
                name: "ProjetId",
                table: "Reclamations");

            migrationBuilder.RenameColumn(
                name: "Statut",
                table: "Reclamations",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "Solution",
                table: "Reclamations",
                newName: "Etat");

            migrationBuilder.RenameColumn(
                name: "ResponsableId",
                table: "Reclamations",
                newName: "ProduitId");

            migrationBuilder.RenameColumn(
                name: "DateSoumission",
                table: "Reclamations",
                newName: "DateCreaction");

            migrationBuilder.RenameColumn(
                name: "Numero",
                table: "Reclamations",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Reclamations_ResponsableId",
                table: "Reclamations",
                newName: "IX_Reclamations_ProduitId");

            migrationBuilder.CreateTable(
                name: "Produits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produits", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Reclamations_Produits_ProduitId",
                table: "Reclamations",
                column: "ProduitId",
                principalTable: "Produits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
