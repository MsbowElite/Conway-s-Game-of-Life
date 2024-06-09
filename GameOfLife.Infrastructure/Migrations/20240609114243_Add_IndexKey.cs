using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameOfLife.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_IndexKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GameStates_GameId",
                table: "GameStates");

            migrationBuilder.CreateIndex(
                name: "IX_GameStates_GameId_GenerationNumber",
                table: "GameStates",
                columns: new[] { "GameId", "GenerationNumber" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GameStates_GameId_GenerationNumber",
                table: "GameStates");

            migrationBuilder.CreateIndex(
                name: "IX_GameStates_GameId",
                table: "GameStates",
                column: "GameId");
        }
    }
}
