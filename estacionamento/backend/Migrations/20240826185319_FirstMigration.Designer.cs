﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace backend.Migrations
{
    [DbContext(typeof(EstacionamentoContext))]
    [Migration("20240826185319_FirstMigration")]
    partial class FirstMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("backend.Models.Estacionamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DataEntrada")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DataSaida")
                        .HasColumnType("TEXT");

                    b.Property<int>("PrecoId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("ValorTotal")
                        .HasColumnType("REAL");

                    b.Property<int>("VeiculoId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PrecoId");

                    b.HasIndex("VeiculoId");

                    b.ToTable("Estacionamento");
                });

            modelBuilder.Entity("backend.Models.Preco", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("ValorHora")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Preco");
                });

            modelBuilder.Entity("backend.Models.Veiculo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Placa")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Veiculo");
                });

            modelBuilder.Entity("backend.Models.Estacionamento", b =>
                {
                    b.HasOne("backend.Models.Preco", "Preco")
                        .WithMany()
                        .HasForeignKey("PrecoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend.Models.Veiculo", "Veiculo")
                        .WithMany()
                        .HasForeignKey("VeiculoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Preco");

                    b.Navigation("Veiculo");
                });
#pragma warning restore 612, 618
        }
    }
}
