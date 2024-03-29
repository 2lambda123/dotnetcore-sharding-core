﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sample.Migrations.EFCores;

#nullable disable

namespace Sample.Migrations.Migrations
{
    [DbContext(typeof(DefaultShardingTableDbContext))]
    [Migration("20211222075250_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Sample.Migrations.EFCores.NoShardingTable", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(128)
                        .IsUnicode(false)
                        .HasColumnType("varchar(128)");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.ToTable("NoShardingTable", (string)null);
                });

            modelBuilder.Entity("Sample.Migrations.EFCores.ShardingWithDateTime", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(128)
                        .IsUnicode(false)
                        .HasColumnType("varchar(128)");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasComment("用户姓名");

                    b.HasKey("Id");

                    b.ToTable("ShardingWithDateTime", (string)null);
                });

            modelBuilder.Entity("Sample.Migrations.EFCores.ShardingWithMod", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(128)
                        .IsUnicode(false)
                        .HasColumnType("varchar(128)");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasComment("用户姓名");

                    b.Property<string>("TextStr")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasDefaultValue("")
                        .HasComment("值123");

                    b.Property<string>("TextStr1")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasDefaultValue("123");

                    b.Property<string>("TextStr2")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasDefaultValue("123");

                    b.HasKey("Id");

                    b.ToTable("ShardingWithMod", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
