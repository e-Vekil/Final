using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eVekilApplication.Migrations
{
    public partial class eag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchasedDocuments_AspNetUsers_UserId1",
                table: "PurchasedDocuments");

            migrationBuilder.DropIndex(
                name: "IX_PurchasedDocuments_UserId1",
                table: "PurchasedDocuments");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "PurchasedDocuments");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "PurchasedDocuments",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "PurchasedDocuments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "CreatedDocuments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Comments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_PurchasedDocuments_UserId",
                table: "PurchasedDocuments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasedDocuments_AspNetUsers_UserId",
                table: "PurchasedDocuments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchasedDocuments_AspNetUsers_UserId",
                table: "PurchasedDocuments");

            migrationBuilder.DropIndex(
                name: "IX_PurchasedDocuments_UserId",
                table: "PurchasedDocuments");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "PurchasedDocuments");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "CreatedDocuments");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "PurchasedDocuments",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "PurchasedDocuments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchasedDocuments_UserId1",
                table: "PurchasedDocuments",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasedDocuments_AspNetUsers_UserId1",
                table: "PurchasedDocuments",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
