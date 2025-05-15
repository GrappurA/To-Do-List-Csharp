using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList_C_.Migrations
{
    /// <inheritdoc />
    public partial class AddMaxDaysInARow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "maxDaysInARow",
                table: "users",
                newName: "MaxDaysInARow");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaxDaysInARow",
                table: "users",
                newName: "maxDaysInARow");
        }
    }
}
