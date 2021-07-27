﻿// <auto-generated />
using System;
using Hotline.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Hotline.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210726113515_FixingIssues3")]
    partial class FixingIssues3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Hotline.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Hotline.Models.Domaine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nom")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProjetId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProjetId");

                    b.ToTable("Domaines");
                });

            modelBuilder.Entity("Hotline.Models.Projet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("Nom")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Projets");
                });

            modelBuilder.Entity("Hotline.Models.Reclamation", b =>
                {
                    b.Property<int>("Numero")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateAffectation")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateResolution")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateSoumission")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DomaineId")
                        .HasColumnType("int");

                    b.Property<int?>("ProjetId")
                        .HasColumnType("int");

                    b.Property<int?>("ResponsableId")
                        .HasColumnType("int");

                    b.Property<string>("Solution")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Statut")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Numero");

                    b.HasIndex("ClientId");

                    b.HasIndex("DomaineId");

                    b.HasIndex("ProjetId");

                    b.HasIndex("ResponsableId");

                    b.ToTable("Reclamations");
                });

            modelBuilder.Entity("Hotline.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Hotline.Models.Domaine", b =>
                {
                    b.HasOne("Hotline.Models.Projet", "Projet")
                        .WithMany()
                        .HasForeignKey("ProjetId");

                    b.Navigation("Projet");
                });

            modelBuilder.Entity("Hotline.Models.Projet", b =>
                {
                    b.HasOne("Hotline.Models.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("Hotline.Models.Reclamation", b =>
                {
                    b.HasOne("Hotline.Models.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId");

                    b.HasOne("Hotline.Models.Domaine", "Domaine")
                        .WithMany()
                        .HasForeignKey("DomaineId");

                    b.HasOne("Hotline.Models.Projet", "Projet")
                        .WithMany()
                        .HasForeignKey("ProjetId");

                    b.HasOne("Hotline.Models.User", "Responsable")
                        .WithMany()
                        .HasForeignKey("ResponsableId");

                    b.Navigation("Client");

                    b.Navigation("Domaine");

                    b.Navigation("Projet");

                    b.Navigation("Responsable");
                });
#pragma warning restore 612, 618
        }
    }
}
