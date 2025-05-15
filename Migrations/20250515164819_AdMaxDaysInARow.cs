using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList_C_.Migrations
{
    /// <inheritdoc />
    public partial class AdMaxDaysInARow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.AddColumn<int>(
		  name: "MaxDaysInARow",
		  table: "Users",
		  type: "INTEGER",
		  nullable: false,
		  defaultValue: 0);
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.DropColumn(
			name: "MaxDaysInARow",
			table: "Users");
		}
    }
}
