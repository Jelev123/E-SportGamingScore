using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_SportGamingScore.Infrastructure.Migrations
{
    public partial class createnewTable3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MatchId",
                table: "UpdateMessages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MatchName",
                table: "UpdateMessages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "MatchStartDate",
                table: "UpdateMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "MatchType",
                table: "UpdateMessages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MatchId",
                table: "UpdateMessages");

            migrationBuilder.DropColumn(
                name: "MatchName",
                table: "UpdateMessages");

            migrationBuilder.DropColumn(
                name: "MatchStartDate",
                table: "UpdateMessages");

            migrationBuilder.DropColumn(
                name: "MatchType",
                table: "UpdateMessages");
        }
    }
}
