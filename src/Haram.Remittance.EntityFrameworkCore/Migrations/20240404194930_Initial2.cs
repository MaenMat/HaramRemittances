using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Haram.Remittance.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HaramRemittances_HaramRemittanceStatuses_CurrentStatusId",
                table: "HaramRemittances");

            migrationBuilder.DropForeignKey(
                name: "FK_HaramRemittanceStatuses_HaramRemittances_RemittanceClassId",
                table: "HaramRemittanceStatuses");

            migrationBuilder.DropIndex(
                name: "IX_HaramRemittanceStatuses_RemittanceClassId",
                table: "HaramRemittanceStatuses");

            migrationBuilder.DropIndex(
                name: "IX_HaramRemittances_CurrentStatusId",
                table: "HaramRemittances");

            migrationBuilder.DropColumn(
                name: "RemittanceClassId",
                table: "HaramRemittanceStatuses");

            migrationBuilder.DropColumn(
                name: "CurrentStatusId",
                table: "HaramRemittances");

            migrationBuilder.AddColumn<Guid>(
                name: "RemittanceId",
                table: "HaramRemittanceStatuses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_HaramRemittanceStatuses_RemittanceId",
                table: "HaramRemittanceStatuses",
                column: "RemittanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_HaramRemittanceStatuses_HaramRemittances_RemittanceId",
                table: "HaramRemittanceStatuses",
                column: "RemittanceId",
                principalTable: "HaramRemittances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HaramRemittanceStatuses_HaramRemittances_RemittanceId",
                table: "HaramRemittanceStatuses");

            migrationBuilder.DropIndex(
                name: "IX_HaramRemittanceStatuses_RemittanceId",
                table: "HaramRemittanceStatuses");

            migrationBuilder.DropColumn(
                name: "RemittanceId",
                table: "HaramRemittanceStatuses");

            migrationBuilder.AddColumn<Guid>(
                name: "RemittanceClassId",
                table: "HaramRemittanceStatuses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CurrentStatusId",
                table: "HaramRemittances",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_HaramRemittanceStatuses_RemittanceClassId",
                table: "HaramRemittanceStatuses",
                column: "RemittanceClassId");

            migrationBuilder.CreateIndex(
                name: "IX_HaramRemittances_CurrentStatusId",
                table: "HaramRemittances",
                column: "CurrentStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_HaramRemittances_HaramRemittanceStatuses_CurrentStatusId",
                table: "HaramRemittances",
                column: "CurrentStatusId",
                principalTable: "HaramRemittanceStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HaramRemittanceStatuses_HaramRemittances_RemittanceClassId",
                table: "HaramRemittanceStatuses",
                column: "RemittanceClassId",
                principalTable: "HaramRemittances",
                principalColumn: "Id");
        }
    }
}
