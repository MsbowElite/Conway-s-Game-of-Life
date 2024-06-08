using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameOfLife.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fix_GenerationNumber_Type : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "GenerationNumber",
                table: "GameStates",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "GenerationNumber",
                table: "GameStates",
                type: "integer",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");
        }
    }
}
