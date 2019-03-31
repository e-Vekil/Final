using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eVekilApplication.Migrations
{
    public partial class CardTypeTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CartType",
                table: "Payments");

            migrationBuilder.AddColumn<int>(
                name: "CartTypeId",
                table: "Payments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CardTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_CartTypeId",
                table: "Payments",
                column: "CartTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_CardTypes_CartTypeId",
                table: "Payments",
                column: "CartTypeId",
                principalTable: "CardTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_CardTypes_CartTypeId",
                table: "Payments");

            migrationBuilder.DropTable(
                name: "CardTypes");

            migrationBuilder.DropIndex(
                name: "IX_Payments_CartTypeId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "CartTypeId",
                table: "Payments");

            migrationBuilder.AddColumn<string>(
                name: "CartType",
                table: "Payments",
                nullable: true);
        }
    }
}
