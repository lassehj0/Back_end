using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Studieforeningskalender_Backend.Migrations
{
    public partial class SmallFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserEvent_Event_EventId",
                table: "UserEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_UserEvent_User_UserId",
                table: "UserEvent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserEvent",
                table: "UserEvent");

            migrationBuilder.RenameTable(
                name: "UserEvent",
                newName: "EventUser");

            migrationBuilder.RenameIndex(
                name: "IX_UserEvent_UserId",
                table: "EventUser",
                newName: "IX_EventUser_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventUser",
                table: "EventUser",
                columns: new[] { "EventId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_EventUser_Event_EventId",
                table: "EventUser",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventUser_User_UserId",
                table: "EventUser",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventUser_Event_EventId",
                table: "EventUser");

            migrationBuilder.DropForeignKey(
                name: "FK_EventUser_User_UserId",
                table: "EventUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventUser",
                table: "EventUser");

            migrationBuilder.RenameTable(
                name: "EventUser",
                newName: "UserEvent");

            migrationBuilder.RenameIndex(
                name: "IX_EventUser_UserId",
                table: "UserEvent",
                newName: "IX_UserEvent_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserEvent",
                table: "UserEvent",
                columns: new[] { "EventId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserEvent_Event_EventId",
                table: "UserEvent",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserEvent_User_UserId",
                table: "UserEvent",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
