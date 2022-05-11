using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wordle.api.Migrations
{
    public partial class AverageSeconds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AverageSeconds",
                table: "Players",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageSeconds",
                table: "Players");
        }
    }
}
