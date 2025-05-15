using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList_C_.Migrations
{
    /// <inheritdoc />
    public partial class AddMaxDaysInARowUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DonePercentage = table.Column<int>(type: "INTEGER", nullable: false),
                    dateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    GotStar = table.Column<bool>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ToDoTask",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Position = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<bool>(type: "INTEGER", nullable: false),
                    DueDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TaskListId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ToDoTask_TaskList_TaskListId",
                        column: x => x.TaskListId,
                        principalTable: "TaskList",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    starCount = table.Column<int>(type: "INTEGER", nullable: false),
                    averageTasksDone = table.Column<double>(type: "REAL", nullable: false),
                    daysInARow = table.Column<int>(type: "INTEGER", nullable: false),
                    maxDaysInARow = table.Column<int>(type: "INTEGER", nullable: false),
                    CurrentListId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_users_TaskList_CurrentListId",
                        column: x => x.CurrentListId,
                        principalTable: "TaskList",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Star",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Size = table.Column<int>(type: "INTEGER", nullable: false),
                    earnDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ListName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Star", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Star_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Star_UserId",
                table: "Star",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ToDoTask_TaskListId",
                table: "ToDoTask",
                column: "TaskListId");

            migrationBuilder.CreateIndex(
                name: "IX_users_CurrentListId",
                table: "users",
                column: "CurrentListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Star");

            migrationBuilder.DropTable(
                name: "ToDoTask");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "TaskList");
        }
    }
}
