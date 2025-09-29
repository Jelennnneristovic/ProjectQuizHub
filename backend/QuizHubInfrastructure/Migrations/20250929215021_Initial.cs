using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuizHubInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvatarUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Quizzes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeLimit = table.Column<int>(type: "int", nullable: false),
                    DifficultyLevel = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizzes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quizzes_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuizId = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    QuestionType = table.Column<int>(type: "int", nullable: false),
                    CorrectFillInAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuizAttempts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    QuizId = table.Column<int>(type: "int", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeTakenMin = table.Column<int>(type: "int", nullable: true),
                    FinishedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Score = table.Column<int>(type: "int", nullable: false),
                    CurrentQuizScore = table.Column<int>(type: "int", nullable: false),
                    CurrentQuestionCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizAttempts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizAttempts_Quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuizAttempts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnswerOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswerOptions_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AttemptAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FillInAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    AwardedPoints = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    QuizAttemptId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttemptAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttemptAnswers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttemptAnswers_QuizAttempts_QuizAttemptId",
                        column: x => x.QuizAttemptId,
                        principalTable: "QuizAttempts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuizAttemptId = table.Column<int>(type: "int", nullable: false),
                    QuizTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalQuestions = table.Column<int>(type: "int", nullable: false),
                    MaximumScore = table.Column<int>(type: "int", nullable: false),
                    CorrectAnswers = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Percentage = table.Column<double>(type: "float", nullable: false),
                    TimeTakenMin = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Results_QuizAttempts_QuizAttemptId",
                        column: x => x.QuizAttemptId,
                        principalTable: "QuizAttempts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttemptAnswerOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnswerOptionId = table.Column<int>(type: "int", nullable: false),
                    AttemptAnswerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttemptAnswerOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttemptAnswerOptions_AnswerOptions_AnswerOptionId",
                        column: x => x.AnswerOptionId,
                        principalTable: "AnswerOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttemptAnswerOptions_AttemptAnswers_AttemptAnswerId",
                        column: x => x.AttemptAnswerId,
                        principalTable: "AttemptAnswers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, "Generalno sve o programiranju", true, "Programiranje" },
                    { 2, "Rimska i anticka arhitektura", true, "Arhitektura" },
                    { 3, "Opste znanje", true, "Istorija" },
                    { 4, "Klasicna muzika", true, "Muzika" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AvatarUrl", "Email", "PasswordHash", "Role", "UserName" },
                values: new object[,]
                {
                    { 1, null, "admin@admin.com", "AQAAAAIAAYagAAAAEFg+InxaoNsss+/H2jitDRm8G652TpCi7RMkdRoeNeVai+H3/7foRQ0XtTmgpkQ+WQ==", 0, "admin" },
                    { 2, null, "marko@cake.com", "AQAAAAIAAYagAAAAEFg+InxaoNsss+/H2jitDRm8G652TpCi7RMkdRoeNeVai+H3/7foRQ0XtTmgpkQ+WQ==", 1, "marko" },
                    { 3, null, "ana@cake.com", "AQAAAAIAAYagAAAAEFg+InxaoNsss+/H2jitDRm8G652TpCi7RMkdRoeNeVai+H3/7foRQ0XtTmgpkQ+WQ==", 1, "ana" },
                    { 4, null, "stefan@cake.com", "AQAAAAIAAYagAAAAEFg+InxaoNsss+/H2jitDRm8G652TpCi7RMkdRoeNeVai+H3/7foRQ0XtTmgpkQ+WQ==", 1, "stefan" }
                });

            migrationBuilder.InsertData(
                table: "Quizzes",
                columns: new[] { "Id", "CategoryId", "Description", "DifficultyLevel", "IsActive", "TimeLimit", "Title" },
                values: new object[,]
                {
                    { 1, 1, null, 0, true, 3, "Kviz programiranja" },
                    { 2, 2, "Istorija arhitektura grada Novog Sada", 1, true, 3, "Kviz o arhitekturi Novog Sada" }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "CorrectFillInAnswer", "IsActive", "Points", "QuestionType", "QuizId", "Text" },
                values: new object[,]
                {
                    { 1, null, true, 3, 1, 1, "Koji od navedenih programa podrzava klase" },
                    { 2, null, true, 2, 2, 1, "Metoda Console.WriteLine() vraca vrednost string?" },
                    { 3, "Svetozar Miletic", true, 2, 3, 2, "Ciji spomenik se nalazi na trgu u centru grada?" }
                });

            migrationBuilder.InsertData(
                table: "QuizAttempts",
                columns: new[] { "Id", "CurrentQuestionCount", "CurrentQuizScore", "FinishedAt", "QuizId", "Score", "StartedAt", "TimeTakenMin", "UserId" },
                values: new object[,]
                {
                    { 1, 2, 5, new DateTime(2025, 9, 13, 15, 32, 0, 0, DateTimeKind.Unspecified), 1, 5, new DateTime(2025, 9, 13, 15, 30, 0, 0, DateTimeKind.Unspecified), 2, 2 },
                    { 2, 1, 2, new DateTime(2025, 9, 28, 18, 32, 0, 0, DateTimeKind.Unspecified), 2, 2, new DateTime(2025, 9, 28, 18, 30, 0, 0, DateTimeKind.Unspecified), 2, 3 }
                });

            migrationBuilder.InsertData(
                table: "AnswerOptions",
                columns: new[] { "Id", "IsActive", "IsCorrect", "QuestionId", "Text" },
                values: new object[,]
                {
                    { 1, true, true, 1, "Java" },
                    { 2, true, true, 1, "C#" },
                    { 3, true, true, 1, "C++" },
                    { 4, true, true, 2, "True" },
                    { 5, true, false, 2, "False" }
                });

            migrationBuilder.InsertData(
                table: "AttemptAnswers",
                columns: new[] { "Id", "AwardedPoints", "FillInAnswer", "IsCorrect", "QuestionId", "QuizAttemptId" },
                values: new object[,]
                {
                    { 1, 3, null, true, 1, 1 },
                    { 2, 2, null, true, 2, 1 },
                    { 3, 2, "Svetozara Miletica", true, 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "Results",
                columns: new[] { "Id", "CorrectAnswers", "CreatedAt", "MaximumScore", "Percentage", "QuizAttemptId", "QuizTitle", "Score", "TimeTakenMin", "TotalQuestions" },
                values: new object[,]
                {
                    { 1, 2, new DateTime(2025, 9, 13, 15, 32, 0, 0, DateTimeKind.Unspecified), 5, 100.0, 1, "Kviz programiranja", 5, 2, 2 },
                    { 2, 1, new DateTime(2025, 9, 28, 18, 32, 0, 0, DateTimeKind.Unspecified), 2, 100.0, 2, "Kviz o arhitekturi Novog Sada", 2, 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "AttemptAnswerOptions",
                columns: new[] { "Id", "AnswerOptionId", "AttemptAnswerId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 3, 1 },
                    { 4, 4, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnswerOptions_QuestionId",
                table: "AnswerOptions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AttemptAnswerOptions_AnswerOptionId",
                table: "AttemptAnswerOptions",
                column: "AnswerOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_AttemptAnswerOptions_AttemptAnswerId",
                table: "AttemptAnswerOptions",
                column: "AttemptAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_AttemptAnswers_QuestionId",
                table: "AttemptAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AttemptAnswers_QuizAttemptId",
                table: "AttemptAnswers",
                column: "QuizAttemptId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuizId",
                table: "Questions",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizAttempts_QuizId",
                table: "QuizAttempts",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizAttempts_UserId",
                table: "QuizAttempts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_CategoryId",
                table: "Quizzes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_QuizAttemptId",
                table: "Results",
                column: "QuizAttemptId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttemptAnswerOptions");

            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "AnswerOptions");

            migrationBuilder.DropTable(
                name: "AttemptAnswers");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "QuizAttempts");

            migrationBuilder.DropTable(
                name: "Quizzes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
