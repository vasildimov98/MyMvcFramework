using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWebServer.App.Migrations
{
    /// <inheritdoc />
    public partial class ChangeIdentityRoleColumnNameToRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdentityRole",
                table: "Users",
                newName: "Role");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Users",
                newName: "IdentityRole");
        }
    }
}
