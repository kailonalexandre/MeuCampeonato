﻿// <auto-generated />
using System;
using MeuCampeonato.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Migrations
{
    [DbContext(typeof(MyContext))]
    partial class MyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Api.Domain.Entities.GamesEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("FirstTeam")
                        .IsRequired()
                        .HasColumnType("varchar(60) CHARACTER SET utf8mb4")
                        .HasMaxLength(60);

                    b.Property<int>("GoalsFistTeam")
                        .HasColumnType("int");

                    b.Property<int>("GoalsSecondTeam")
                        .HasColumnType("int");

                    b.Property<string>("SecondTeam")
                        .IsRequired()
                        .HasColumnType("varchar(60) CHARACTER SET utf8mb4")
                        .HasMaxLength(60);

                    b.Property<int>("TeamKey")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Api.Domain.Entities.TeamsEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("TeamName")
                        .IsRequired()
                        .HasColumnType("varchar(60) CHARACTER SET utf8mb4")
                        .HasMaxLength(60);

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });
#pragma warning restore 612, 618
        }
    }
}
