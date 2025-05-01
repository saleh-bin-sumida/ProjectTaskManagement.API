using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectTaskManagement.API.Migrations
{
    /// <inheritdoc />
    public partial class RenameTaskStatses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_TaskStatus_StatusId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskStatusHistories_TaskStatus_StatusId",
                table: "TaskStatusHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskStatus",
                table: "TaskStatus");

            migrationBuilder.RenameTable(
                name: "TaskStatus",
                newName: "TaskStatuses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskStatuses",
                table: "TaskStatuses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_TaskStatuses_StatusId",
                table: "Tasks",
                column: "StatusId",
                principalTable: "TaskStatuses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskStatusHistories_TaskStatuses_StatusId",
                table: "TaskStatusHistories",
                column: "StatusId",
                principalTable: "TaskStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_TaskStatuses_StatusId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskStatusHistories_TaskStatuses_StatusId",
                table: "TaskStatusHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskStatuses",
                table: "TaskStatuses");

            migrationBuilder.RenameTable(
                name: "TaskStatuses",
                newName: "TaskStatus");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskStatus",
                table: "TaskStatus",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_TaskStatus_StatusId",
                table: "Tasks",
                column: "StatusId",
                principalTable: "TaskStatus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskStatusHistories_TaskStatus_StatusId",
                table: "TaskStatusHistories",
                column: "StatusId",
                principalTable: "TaskStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
