using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace annual_events.Migrations
{
    /// <inheritdoc />
    public partial class uniqueUserName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Annual_Events_User",
                type: "NVARCHAR2(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Annual_Events_User_Username",
                table: "Annual_Events_User",
                column: "Username",
                unique: true,
                filter: "\"Username\" IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Annual_Events_User_Username",
                table: "Annual_Events_User");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Annual_Events_User",
                type: "NVARCHAR2(2000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(450)",
                oldNullable: true);
        }
    }
}
