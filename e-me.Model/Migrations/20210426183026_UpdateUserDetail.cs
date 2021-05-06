using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace e_me.Model.Migrations
{
    public partial class UpdateUserDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "UserDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "UserDetail",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "UserDetail");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "UserDetail");
        }
    }
}
