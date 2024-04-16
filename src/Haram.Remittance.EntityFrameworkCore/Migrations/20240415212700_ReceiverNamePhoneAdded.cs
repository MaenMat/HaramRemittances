using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Haram.Remittance.Migrations
{
    /// <inheritdoc />
    public partial class ReceiverNamePhoneAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReceiverName",
                table: "HaramRemittances",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReceiverPhone",
                table: "HaramRemittances",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiverName",
                table: "HaramRemittances");

            migrationBuilder.DropColumn(
                name: "ReceiverPhone",
                table: "HaramRemittances");
        }
    }
}
