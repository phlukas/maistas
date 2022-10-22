using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Maistas.Migrations
{
    public partial class AddedDishes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Calories",
                table: "Dishes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Dishes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Dishes",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Dishes",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Remainder",
                table: "Dishes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Vegan",
                table: "Dishes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "WeightInGrams",
                table: "Dishes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Calories",
                table: "Dishes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Dishes");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Dishes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Dishes");

            migrationBuilder.DropColumn(
                name: "Remainder",
                table: "Dishes");

            migrationBuilder.DropColumn(
                name: "Vegan",
                table: "Dishes");

            migrationBuilder.DropColumn(
                name: "WeightInGrams",
                table: "Dishes");
        }
    }
}
