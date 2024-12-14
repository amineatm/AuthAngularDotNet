using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthECApi.Migrations
{
    /// <inheritdoc />
    public partial class RenameTablesAndColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(name: "AspNetUsers", newName: "Users");
            migrationBuilder.RenameTable(name: "AspNetRoles", newName: "Roles");
            migrationBuilder.RenameTable(name: "AspNetUserRoles", newName: "UserRoles");
            migrationBuilder.RenameTable(name: "AspNetUserClaims", newName: "UserClaims");
            migrationBuilder.RenameTable(name: "AspNetRoleClaims", newName: "RoleClaims");
            migrationBuilder.RenameTable(name: "AspNetUserLogins", newName: "UserLogins");
            migrationBuilder.RenameTable(name: "AspNetUserTokens", newName: "UserTokens");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // In case i wanna rollback the changes
            migrationBuilder.RenameTable(name: "Users", newName: "AspNetUsers");
            migrationBuilder.RenameTable(name: "Roles", newName: "AspNetRoles");
            migrationBuilder.RenameTable(name: "UserRoles", newName: "AspNetUserRoles");    
            migrationBuilder.RenameTable(name: "UserClaims", newName: "AspNetUserClaims");
            migrationBuilder.RenameTable(name: "RoleClaims", newName: "AspNetRoleClaims");
            migrationBuilder.RenameTable(name: "UserLogins", newName: "AspNetUserLogins");
            migrationBuilder.RenameTable(name: "UserTokens", newName: "AspNetUserTokens");
        }
    }
}
