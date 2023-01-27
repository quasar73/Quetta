using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quetta.Data.Migrations
{
    public partial class AddedFiledsToDetectWhoReadAMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MessageUser",
                columns: table => new
                {
                    ReadMessagesId = table.Column<string>(type: "text", nullable: false),
                    WhoReadId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageUser", x => new { x.ReadMessagesId, x.WhoReadId });
                    table.ForeignKey(
                        name: "FK_MessageUser_AspNetUsers_WhoReadId",
                        column: x => x.WhoReadId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MessageUser_Messages_ReadMessagesId",
                        column: x => x.ReadMessagesId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MessageUser_WhoReadId",
                table: "MessageUser",
                column: "WhoReadId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageUser");
        }
    }
}
