using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Studieforeningskalender_Backend.Migrations
{
    public partial class AddedAddressFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddressLine",
                table: "Event",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Event",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Event",
                type: "character varying(4)",
                maxLength: 4,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressLine",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Event");
        }
    }
}
