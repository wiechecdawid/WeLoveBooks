using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeLoveBooks.DataAccess.Migrations
{
    public partial class BookRateEntityRedefined : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookRates_AspNetUsers_AppUserId",
                table: "BookRates");

            migrationBuilder.DropForeignKey(
                name: "FK_BookRates_Books_BookId",
                table: "BookRates");

            migrationBuilder.DropIndex(
                name: "IX_BookRates_AppUserId",
                table: "BookRates");

            migrationBuilder.DropIndex(
                name: "IX_BookRates_BookId",
                table: "BookRates");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "BookRates");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "BookRates",
                newName: "ReviewId");

            migrationBuilder.AddColumn<Guid>(
                name: "BookRateId",
                table: "Reviews",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BookRateId",
                table: "BookRates",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_BookRates_BookRateId",
                table: "BookRates",
                column: "BookRateId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookRates_Reviews_BookRateId",
                table: "BookRates",
                column: "BookRateId",
                principalTable: "Reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookRates_Reviews_BookRateId",
                table: "BookRates");

            migrationBuilder.DropIndex(
                name: "IX_BookRates_BookRateId",
                table: "BookRates");

            migrationBuilder.DropColumn(
                name: "BookRateId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "BookRateId",
                table: "BookRates");

            migrationBuilder.RenameColumn(
                name: "ReviewId",
                table: "BookRates",
                newName: "BookId");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "BookRates",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_BookRates_AppUserId",
                table: "BookRates",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BookRates_BookId",
                table: "BookRates",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookRates_AspNetUsers_AppUserId",
                table: "BookRates",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookRates_Books_BookId",
                table: "BookRates",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
