﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pottencial.Payment.Api.Infraestrutura.Context;

#nullable disable

namespace Pottencial.Payment.Api.Infraestrutura.Migrations
{
    [DbContext(typeof(VendasContext))]
    partial class VendasContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.10");

            modelBuilder.Entity("Pottencial.Payment.Api.Infraestrutura.DTOs.ItemDTO", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("VendaId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("VendaId");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("Pottencial.Payment.Api.Infraestrutura.DTOs.VendaDTO", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataDaVenda")
                        .HasColumnType("TEXT");

                    b.Property<int>("StatusDaVenda")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("VendedorId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("VendedorId")
                        .IsUnique();

                    b.ToTable("Venda");
                });

            modelBuilder.Entity("Pottencial.Payment.Api.Infraestrutura.DTOs.VendedorDTO", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Vendedor");
                });

            modelBuilder.Entity("Pottencial.Payment.Api.Infraestrutura.DTOs.ItemDTO", b =>
                {
                    b.HasOne("Pottencial.Payment.Api.Infraestrutura.DTOs.VendaDTO", "Venda")
                        .WithMany("Itens")
                        .HasForeignKey("VendaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Venda");
                });

            modelBuilder.Entity("Pottencial.Payment.Api.Infraestrutura.DTOs.VendaDTO", b =>
                {
                    b.HasOne("Pottencial.Payment.Api.Infraestrutura.DTOs.VendedorDTO", "Vendedor")
                        .WithOne("Venda")
                        .HasForeignKey("Pottencial.Payment.Api.Infraestrutura.DTOs.VendaDTO", "VendedorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vendedor");
                });

            modelBuilder.Entity("Pottencial.Payment.Api.Infraestrutura.DTOs.VendaDTO", b =>
                {
                    b.Navigation("Itens");
                });

            modelBuilder.Entity("Pottencial.Payment.Api.Infraestrutura.DTOs.VendedorDTO", b =>
                {
                    b.Navigation("Venda");
                });
#pragma warning restore 612, 618
        }
    }
}
