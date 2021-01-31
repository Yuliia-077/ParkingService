using Microsoft.EntityFrameworkCore.Migrations;

namespace ParkingService.Migrations
{
    public partial class UpdateBalanceEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Balances_Entries_EntryId",
                table: "Balances");

            migrationBuilder.DropIndex(
                name: "IX_Balances_EntryId",
                table: "Balances");

            migrationBuilder.DropColumn(
                name: "EntryId",
                table: "Balances");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "EntryId",
                table: "Balances",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Balances_EntryId",
                table: "Balances",
                column: "EntryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Balances_Entries_EntryId",
                table: "Balances",
                column: "EntryId",
                principalTable: "Entries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
