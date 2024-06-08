using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameOfLife.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Manipulate_Relationships_Type : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameStates_Games_GameRelationId",
                table: "GameStates");

            migrationBuilder.DropForeignKey(
                name: "GameState_FK",
                table: "GameStates");

            migrationBuilder.DropIndex(
                name: "IX_GameStates_GameRelationId",
                table: "GameStates");

            migrationBuilder.DropColumn(
                name: "GameRelationId",
                table: "GameStates");

            migrationBuilder.AddColumn<Guid>(
                name: "FinalGameStateId",
                table: "Games",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_FinalGameStateId",
                table: "Games",
                column: "FinalGameStateId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "GameStateFinal_FK",
                table: "Games",
                column: "FinalGameStateId",
                principalTable: "GameStates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "Game_FK",
                table: "GameStates",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "GameStateFinal_FK",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "Game_FK",
                table: "GameStates");

            migrationBuilder.DropIndex(
                name: "IX_Games_FinalGameStateId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "FinalGameStateId",
                table: "Games");

            migrationBuilder.AddColumn<Guid>(
                name: "GameRelationId",
                table: "GameStates",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameStates_GameRelationId",
                table: "GameStates",
                column: "GameRelationId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameStates_Games_GameRelationId",
                table: "GameStates",
                column: "GameRelationId",
                principalTable: "Games",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "GameState_FK",
                table: "GameStates",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
