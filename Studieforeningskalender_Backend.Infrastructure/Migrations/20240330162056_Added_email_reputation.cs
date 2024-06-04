using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Studieforeningskalender_Backend.Migrations
{
    public partial class Added_email_reputation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailReputation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmailAddress = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    HardBounces = table.Column<int>(type: "integer", nullable: false),
                    SoftBounces = table.Column<int>(type: "integer", nullable: false),
                    Complaints = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailReputation", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailReputation");
        }
    }
}
