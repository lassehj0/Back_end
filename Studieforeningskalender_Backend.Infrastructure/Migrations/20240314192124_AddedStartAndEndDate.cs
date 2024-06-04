using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Studieforeningskalender_Backend.Migrations
{
    public partial class AddedStartAndEndDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Event",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "Event",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Event");
        }
    }
}
