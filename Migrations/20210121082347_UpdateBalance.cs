using Microsoft.EntityFrameworkCore.Migrations;

namespace ParkingService.Migrations
{
    public partial class UpdateBalance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Balances_Entries_EntryId",
                table: "Balances");

            migrationBuilder.AlterColumn<int>(
                name: "EntryId",
                table: "Balances",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "Balances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Balances_CarId",
                table: "Balances",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Balances_Cars_CarId",
                table: "Balances",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Balances_Entries_EntryId",
                table: "Balances",
                column: "EntryId",
                principalTable: "Entries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Balances_Cars_CarId",
                table: "Balances");

            migrationBuilder.DropForeignKey(
                name: "FK_Balances_Entries_EntryId",
                table: "Balances");

            migrationBuilder.DropIndex(
                name: "IX_Balances_CarId",
                table: "Balances");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Balances");

            migrationBuilder.AlterColumn<int>(
                name: "EntryId",
                table: "Balances",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Balances_Entries_EntryId",
                table: "Balances",
                column: "EntryId",
                principalTable: "Entries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
