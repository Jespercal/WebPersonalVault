using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineVault.Server.Migrations
{
    /// <inheritdoc />
    public partial class Initz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EncryptedUserInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PrivateKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublicKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserPublicKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SessionToken = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EncryptedUserInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EncryptedUserInfos_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EncryptedUserInfos_UserId",
                table: "EncryptedUserInfos",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EncryptedUserInfos");
        }
    }
}
