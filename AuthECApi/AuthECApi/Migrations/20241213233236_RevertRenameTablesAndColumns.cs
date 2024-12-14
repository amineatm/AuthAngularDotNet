using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthECApi.Migrations
{
    /// <inheritdoc />
    public partial class RevertRenameTablesAndColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(name: "Users", newName: "AspNetUsers");
            migrationBuilder.RenameTable(name: "Roles", newName: "AspNetRoles");
            migrationBuilder.RenameTable(name: "UserRoles", newName: "AspNetUserRoles");
            migrationBuilder.RenameTable(name: "UserClaims", newName: "AspNetUserClaims");
            migrationBuilder.RenameTable(name: "RoleClaims", newName: "AspNetRoleClaims");
            migrationBuilder.RenameTable(name: "UserLogins", newName: "AspNetUserLogins");
            migrationBuilder.RenameTable(name: "UserTokens", newName: "AspNetUserTokens");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
