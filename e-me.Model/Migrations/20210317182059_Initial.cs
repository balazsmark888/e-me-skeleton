using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace e_me.Model.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "ApplicationSetting",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationSetting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientEcdhKeyPair",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PublicKey = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DerivedKey = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SessionId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientEcdhKeyPair", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SecurityRole",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SecurityType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ChangePasswordNextLogon = table.Column<bool>(type: "bit", nullable: false),
                    PersonalNumericCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JwtToken",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cancelled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JwtToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JwtToken_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResetPasswordToken",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TokenString = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expired = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResetPasswordToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResetPasswordToken_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAvatar",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Avatar = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAvatar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAvatar_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSecurityRole",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SecurityRoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSecurityRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSecurityRole_SecurityRole_SecurityRoleId",
                        column: x => x.SecurityRoleId,
                        principalTable: "SecurityRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSecurityRole_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "SecurityRole",
                columns: new[] { "Id", "Name", "SecurityType" },
                values: new object[] { new Guid("094f8987-8aa5-4297-bd74-7e315a3e5548"), "Administrator", 0 });

            migrationBuilder.InsertData(
                table: "SecurityRole",
                columns: new[] { "Id", "Name", "SecurityType" },
                values: new object[] { new Guid("3a3a1eee-6f5e-444e-915c-36db71ec947c"), "Regular User", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_JwtToken_UserId",
                table: "JwtToken",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResetPasswordToken_UserId",
                table: "ResetPasswordToken",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_LoginName",
                table: "User",
                column: "LoginName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_PersonalNumericCode",
                table: "User",
                column: "PersonalNumericCode",
                unique: true,
                filter: "[PersonalNumericCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserAvatar_UserId",
                table: "UserAvatar",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSecurityRole_SecurityRoleId",
                table: "UserSecurityRole",
                column: "SecurityRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSecurityRole_UserId",
                table: "UserSecurityRole",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationSetting");

            migrationBuilder.DropTable(
                name: "ClientEcdhKeyPair",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "JwtToken");

            migrationBuilder.DropTable(
                name: "ResetPasswordToken");

            migrationBuilder.DropTable(
                name: "UserAvatar");

            migrationBuilder.DropTable(
                name: "UserSecurityRole");

            migrationBuilder.DropTable(
                name: "SecurityRole");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
