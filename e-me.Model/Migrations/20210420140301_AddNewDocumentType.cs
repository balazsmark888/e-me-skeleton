using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace e_me.Model.Migrations
{
    public partial class AddNewDocumentType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DocumentType",
                keyColumn: "Id",
                keyValue: new Guid("eb3dea44-5676-407f-8678-6d5191c626fc"));

            migrationBuilder.DeleteData(
                table: "SecurityRole",
                keyColumn: "Id",
                keyValue: new Guid("2581b6fa-5b0f-4a30-86d9-9334471e2bf5"));

            migrationBuilder.DeleteData(
                table: "SecurityRole",
                keyColumn: "Id",
                keyValue: new Guid("dd6243f1-2e78-4d06-a1b9-c17b8e7c7105"));

            migrationBuilder.InsertData(
                table: "DocumentType",
                columns: new[] { "Id", "DisplayName", "Name" },
                values: new object[,]
                {
                    { new Guid("9cfd64bd-3f8c-4ce9-b8da-552267897ac0"), "Test Document Type", "TestDocumentType" },
                    { new Guid("11f61509-08fd-4591-8ce8-5d009e69964d"), "Self Declaration", "SelfDeclaration" }
                });

            migrationBuilder.InsertData(
                table: "SecurityRole",
                columns: new[] { "Id", "Name", "SecurityType" },
                values: new object[,]
                {
                    { new Guid("cd640a09-4bff-4174-b298-083be91775bd"), "Administrator", 0 },
                    { new Guid("7d7c04b2-fc0a-4195-8037-b31f35213958"), "Regular User", 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DocumentType",
                keyColumn: "Id",
                keyValue: new Guid("11f61509-08fd-4591-8ce8-5d009e69964d"));

            migrationBuilder.DeleteData(
                table: "DocumentType",
                keyColumn: "Id",
                keyValue: new Guid("9cfd64bd-3f8c-4ce9-b8da-552267897ac0"));

            migrationBuilder.DeleteData(
                table: "SecurityRole",
                keyColumn: "Id",
                keyValue: new Guid("7d7c04b2-fc0a-4195-8037-b31f35213958"));

            migrationBuilder.DeleteData(
                table: "SecurityRole",
                keyColumn: "Id",
                keyValue: new Guid("cd640a09-4bff-4174-b298-083be91775bd"));

            migrationBuilder.InsertData(
                table: "DocumentType",
                columns: new[] { "Id", "DisplayName", "Name" },
                values: new object[] { new Guid("eb3dea44-5676-407f-8678-6d5191c626fc"), "Test Document Type", "TestDocumentType" });

            migrationBuilder.InsertData(
                table: "SecurityRole",
                columns: new[] { "Id", "Name", "SecurityType" },
                values: new object[] { new Guid("dd6243f1-2e78-4d06-a1b9-c17b8e7c7105"), "Administrator", 0 });

            migrationBuilder.InsertData(
                table: "SecurityRole",
                columns: new[] { "Id", "Name", "SecurityType" },
                values: new object[] { new Guid("2581b6fa-5b0f-4a30-86d9-9334471e2bf5"), "Regular User", 1 });
        }
    }
}
