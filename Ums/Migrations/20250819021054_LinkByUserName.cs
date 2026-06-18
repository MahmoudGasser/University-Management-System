using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ums.Migrations
{
    /// <inheritdoc />
    public partial class LinkByUserName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityUserName",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IdentityUserName",
                table: "Instructors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdentityUserName",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "IdentityUserName",
                table: "Instructors");
        }
    }
}
