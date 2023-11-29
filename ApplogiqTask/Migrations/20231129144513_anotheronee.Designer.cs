﻿// <auto-generated />
using System;
using ApplogiqTask.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApplogiqTask.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20231129144513_anotheronee")]
    partial class anotheronee
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.24")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ApplogiqTask.Models.EtherTransactionResponseData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("message")
                        .HasColumnType("longtext");

                    b.Property<string>("serializedTransaction")
                        .HasColumnType("longtext");

                    b.Property<string>("status")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("EtherTransactionResponseData");
                });
#pragma warning restore 612, 618
        }
    }
}