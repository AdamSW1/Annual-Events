using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace annual_events.Migrations
{
    /// <inheritdoc />
    public partial class fixingpf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "ProfilePicture",
                table: "Annual_Events_User",
                type: "RAW(2000)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "RAW(2000)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "ProfilePicture",
                table: "Annual_Events_User",
                type: "RAW(2000)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "RAW(2000)",
                oldNullable: true);
        }
    }
}
