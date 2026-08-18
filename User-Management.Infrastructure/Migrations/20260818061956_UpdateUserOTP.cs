using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User_Management.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserOTP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSent",
                table: "UserOTPs",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSent",
                table: "UserOTPs");
        }
    }
}
