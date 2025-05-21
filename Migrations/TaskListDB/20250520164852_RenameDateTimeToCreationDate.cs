using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList_C_.Migrations.TaskListDB
{
    /// <inheritdoc />
    public partial class RenameDateTimeToCreationDate : Migration
    {
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.RenameColumn(
				name: "DateTime",              // old name
				table: "lists",                // table name
				newName: "CreationDate");     // new name
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.RenameColumn(
				name: "CreationDate",
				table: "lists",
				newName: "DateTime");
		}
	}
}
