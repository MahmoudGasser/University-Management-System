using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ums.Migrations
{
    /// <inheritdoc />
    public partial class last : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdentityUserName",
                table: "Students",
                newName: "IdentityUserId");

            migrationBuilder.RenameColumn(
                name: "IdentityUserName",
                table: "Instructors",
                newName: "IdentityUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdentityUserId",
                table: "Students",
                newName: "IdentityUserName");

            migrationBuilder.RenameColumn(
                name: "IdentityUserId",
                table: "Instructors",
                newName: "IdentityUserName");
        }
    }
}
