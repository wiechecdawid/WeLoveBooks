using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeLoveBooks.DataAccess.Migrations
{
    public partial class PhotoEntityAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoId",
                table: "Books",
                type: "nvarchar(450)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhotoId",
                table: "Authors",
                type: "nvarchar(450)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhotoId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_PhotoId",
                table: "Books",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_PhotoId",
                table: "Authors",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PhotoId",
                table: "AspNetUsers",
                column: "PhotoId");

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

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Books_PhotoId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Authors_PhotoId",
                table: "Authors");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PhotoId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "AspNetUsers");
        }
    }
}
