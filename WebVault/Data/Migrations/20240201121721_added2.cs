using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebVault.Data.Migrations
{
    /// <inheritdoc />
    public partial class added2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EncryptionType",
                table: "EncryptedObjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Key1",
                table: "EncryptedObjects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Key2",
                table: "EncryptedObjects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EncryptionType",
                table: "EncryptedObjects");

            migrationBuilder.DropColumn(
                name: "Key1",
                table: "EncryptedObjects");

            migrationBuilder.DropColumn(
                name: "Key2",
                table: "EncryptedObjects");
        }
    }
}
