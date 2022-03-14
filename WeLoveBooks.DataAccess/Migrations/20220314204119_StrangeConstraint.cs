using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeLoveBooks.DataAccess.Migrations
{
    public partial class StrangeConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Photos_PhotoId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Photos_PhotoId",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_BookRates_Reviews_BookRateId",
                table: "BookRates");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Photos_PhotoId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_BookRates_BookRateId",
                table: "BookRates");

            migrationBuilder.DropColumn(
                name: "BookRateId",
                table: "BookRates");

            migrationBuilder.AlterColumn<string>(
                name: "PhotoId",
                table: "Books",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PhotoId",
                table: "Authors",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PhotoId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BookRateId",
                table: "Reviews",
                column: "BookRateId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Photos_PhotoId",
                table: "AspNetUsers",
                column: "PhotoId",
                principalTable: "Photos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Photos_PhotoId",
                table: "Authors",
                column: "PhotoId",
                principalTable: "Photos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Photos_PhotoId",
                table: "Books",
                column: "PhotoId",
                principalTable: "Photos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_BookRates_BookRateId",
                table: "Reviews",
                column: "BookRateId",
                principalTable: "BookRates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Photos_PhotoId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Photos_PhotoId",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Photos_PhotoId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_BookRates_BookRateId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_BookRateId",
                table: "Reviews");

            migrationBuilder.AlterColumn<string>(
                name: "PhotoId",
                table: "Books",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BookRateId",
                table: "BookRates",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "PhotoId",
                table: "Authors",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhotoId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookRates_BookRateId",
                table: "BookRates",
                column: "BookRateId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Photos_PhotoId",
                table: "AspNetUsers",
                column: "PhotoId",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Photos_PhotoId",
                table: "Authors",
                column: "PhotoId",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookRates_Reviews_BookRateId",
                table: "BookRates",
                column: "BookRateId",
                principalTable: "Reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Photos_PhotoId",
                table: "Books",
                column: "PhotoId",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
