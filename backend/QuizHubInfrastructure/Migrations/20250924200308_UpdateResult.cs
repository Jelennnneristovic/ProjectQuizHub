using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizHubInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Results",
                keyColumn: "Id",
                keyValue: 1,
                column: "QuizTitle",
                value: "Kviz programiranja");

            migrationBuilder.UpdateData(
                table: "Results",
                keyColumn: "Id",
                keyValue: 2,
                column: "QuizTitle",
                value: "Kviz programiranja");

            migrationBuilder.UpdateData(
                table: "Results",
                keyColumn: "Id",
                keyValue: 3,
                column: "QuizTitle",
                value: "Kviz o arhitekturi Novog Sada");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Results",
                keyColumn: "Id",
                keyValue: 1,
                column: "QuizTitle",
                value: "");

            migrationBuilder.UpdateData(
                table: "Results",
                keyColumn: "Id",
                keyValue: 2,
                column: "QuizTitle",
                value: "");

            migrationBuilder.UpdateData(
                table: "Results",
                keyColumn: "Id",
                keyValue: 3,
                column: "QuizTitle",
                value: "");
        }
    }
}
