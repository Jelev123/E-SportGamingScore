﻿// <auto-generated />
using System;
using E_SportGamingScore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace E_SportGamingScore.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("E_SportGamingScore.Infrastructure.Models.Bet", b =>
                {
                    b.Property<int>("BetId")
                        .HasColumnType("int");

                    b.Property<string>("BetName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsLive")
                        .HasColumnType("bit");

                    b.Property<int>("MatchId")
                        .HasColumnType("int");

                    b.HasKey("BetId");

                    b.HasIndex("MatchId");

                    b.ToTable("Bets");
                });

            modelBuilder.Entity("E_SportGamingScore.Infrastructure.Models.Event", b =>
                {
                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("EventName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsLive")
                        .HasColumnType("bit");

                    b.Property<int>("SportId")
                        .HasColumnType("int");

                    b.HasKey("EventId");

                    b.HasIndex("SportId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("E_SportGamingScore.Infrastructure.Models.Match", b =>
                {
                    b.Property<int>("MatchId")
                        .HasColumnType("int");

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<string>("MatchName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MatchType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("MatchId");

                    b.HasIndex("EventId");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("E_SportGamingScore.Infrastructure.Models.Odd", b =>
                {
                    b.Property<int>("OddId")
                        .HasColumnType("int");

                    b.Property<int>("BetId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("OddValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("SpecialBetValue")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("OddId");

                    b.HasIndex("BetId");

                    b.ToTable("Odds");
                });

            modelBuilder.Entity("E_SportGamingScore.Infrastructure.Models.Sport", b =>
                {
                    b.Property<int>("SportId")
                        .HasColumnType("int");

                    b.Property<string>("SportName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SportId");

                    b.ToTable("Sports");
                });

            modelBuilder.Entity("E_SportGamingScore.Infrastructure.Models.UpdateBetMessages", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ActionType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BetId")
                        .HasColumnType("int");

                    b.Property<string>("BetName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsBetLive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("UpdateBetMessages");
                });

            modelBuilder.Entity("E_SportGamingScore.Infrastructure.Models.UpdateMatchMessages", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ActionType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MatchId")
                        .HasColumnType("int");

                    b.Property<string>("MatchName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("MatchStartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("MatchType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("UpdateMatchMessages");
                });

            modelBuilder.Entity("E_SportGamingScore.Infrastructure.Models.UpdateOddMessages", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ActionType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OddId")
                        .HasColumnType("int");

                    b.Property<string>("OddName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("OddValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("SpecialBetValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("UpdateOddMessages");
                });

            modelBuilder.Entity("E_SportGamingScore.Infrastructure.Models.Bet", b =>
                {
                    b.HasOne("E_SportGamingScore.Infrastructure.Models.Match", "Match")
                        .WithMany("Bets")
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Match");
                });

            modelBuilder.Entity("E_SportGamingScore.Infrastructure.Models.Event", b =>
                {
                    b.HasOne("E_SportGamingScore.Infrastructure.Models.Sport", "Sport")
                        .WithMany("Events")
                        .HasForeignKey("SportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sport");
                });

            modelBuilder.Entity("E_SportGamingScore.Infrastructure.Models.Match", b =>
                {
                    b.HasOne("E_SportGamingScore.Infrastructure.Models.Event", "Event")
                        .WithMany("Matches")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("E_SportGamingScore.Infrastructure.Models.Odd", b =>
                {
                    b.HasOne("E_SportGamingScore.Infrastructure.Models.Bet", "Bet")
                        .WithMany("Odds")
                        .HasForeignKey("BetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bet");
                });

            modelBuilder.Entity("E_SportGamingScore.Infrastructure.Models.Bet", b =>
                {
                    b.Navigation("Odds");
                });

            modelBuilder.Entity("E_SportGamingScore.Infrastructure.Models.Event", b =>
                {
                    b.Navigation("Matches");
                });

            modelBuilder.Entity("E_SportGamingScore.Infrastructure.Models.Match", b =>
                {
                    b.Navigation("Bets");
                });

            modelBuilder.Entity("E_SportGamingScore.Infrastructure.Models.Sport", b =>
                {
                    b.Navigation("Events");
                });
#pragma warning restore 612, 618
        }
    }
}
