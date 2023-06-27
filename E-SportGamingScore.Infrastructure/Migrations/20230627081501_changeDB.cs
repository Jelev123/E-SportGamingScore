using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_SportGamingScore.Infrastructure.Migrations
{
    public partial class changeDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntityId",
                table: "UpdateOddMessages");

            migrationBuilder.DropColumn(
                name: "MessageType",
                table: "UpdateOddMessages");

            migrationBuilder.DropColumn(
                name: "EntityId",
                table: "UpdateMatchMessages");

            migrationBuilder.DropColumn(
                name: "MessageType",
                table: "UpdateMatchMessages");

            migrationBuilder.DropColumn(
                name: "EntityId",
                table: "UpdateBetMessages");

            migrationBuilder.DropColumn(
                name: "MessageType",
                table: "UpdateBetMessages");

            migrationBuilder.AlterColumn<decimal>(
                name: "SpecialBetValue",
                table: "UpdateOddMessages",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "SpecialBetValue",
                table: "UpdateOddMessages",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EntityId",
                table: "UpdateOddMessages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MessageType",
                table: "UpdateOddMessages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EntityId",
                table: "UpdateMatchMessages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MessageType",
                table: "UpdateMatchMessages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EntityId",
                table: "UpdateBetMessages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MessageType",
                table: "UpdateBetMessages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
