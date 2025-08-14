using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UptadeOptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Options_Questions_QuestionId1",
                table: "Options");

            migrationBuilder.DropIndex(
                name: "IX_Options_QuestionId1",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "QuestionId1",
                table: "Options");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuestionId1",
                table: "Options",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Options_QuestionId1",
                table: "Options",
                column: "QuestionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Questions_QuestionId1",
                table: "Options",
                column: "QuestionId1",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
