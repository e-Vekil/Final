using Microsoft.EntityFrameworkCore.Migrations;

namespace eVekilApplication.Migrations
{
    public partial class CardTypeChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "CardTypes",
                newName: "Value");

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "CardTypes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Key",
                table: "CardTypes");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "CardTypes",
                newName: "Name");
        }
    }
}
