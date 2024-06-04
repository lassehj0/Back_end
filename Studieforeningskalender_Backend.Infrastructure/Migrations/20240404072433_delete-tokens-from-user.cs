using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Studieforeningskalender_Backend.Migrations
{
    public partial class deletetokensfromuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "User");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiration",
                table: "User");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "User",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiration",
                table: "User",
                type: "timestamp with time zone",
                nullable: true);
        }
    }
}
