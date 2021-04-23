using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace e_me.Model.Migrations
{
    public partial class AddOneTimeAccessToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OneTimeAccessToken",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsValid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OneTimeAccessToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OneTimeAccessToken_UserDocument_UserDocumentId",
                        column: x => x.UserDocumentId,
                        principalTable: "UserDocument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OneTimeAccessToken_UserDocumentId",
                table: "OneTimeAccessToken",
                column: "UserDocumentId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OneTimeAccessToken");
        }
    }
}
