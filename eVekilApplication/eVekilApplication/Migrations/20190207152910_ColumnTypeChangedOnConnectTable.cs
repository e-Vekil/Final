using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eVekilApplication.Migrations
{
    public partial class ColumnTypeChangedOnConnectTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ReplyDate",
                table: "Connects",
                nullable: true,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ReplyDate",
                table: "Connects",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
