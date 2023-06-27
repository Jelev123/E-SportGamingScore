using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_SportGamingScore.Infrastructure.Migrations
{
    public partial class createnewTable4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UpdateMessages",
                table: "UpdateMessages");

            migrationBuilder.RenameTable(
                name: "UpdateMessages",
                newName: "UpdateMatchMessages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UpdateMatchMessages",
                table: "UpdateMatchMessages",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "UpdateBetMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BetId = table.Column<int>(type: "int", nullable: false),
                    BetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsBetLive = table.Column<bool>(type: "bit", nullable: false),
                    MessageType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    ActionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpdateBetMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UpdateOddMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OddId = table.Column<int>(type: "int", nullable: false),
                    OddName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OddValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SpecialBetValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MessageType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    ActionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpdateOddMessages", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UpdateBetMessages");

            migrationBuilder.DropTable(
                name: "UpdateOddMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UpdateMatchMessages",
                table: "UpdateMatchMessages");

            migrationBuilder.RenameTable(
                name: "UpdateMatchMessages",
                newName: "UpdateMessages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UpdateMessages",
                table: "UpdateMessages",
                column: "Id");
        }
    }
}
