using Microsoft.EntityFrameworkCore.Migrations;

namespace eVekilApplication.Migrations
{
    public partial class PhoneNumberColumnAddedToConnectTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Phone",
                table: "Connects",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Connects");
        }
    }
}
