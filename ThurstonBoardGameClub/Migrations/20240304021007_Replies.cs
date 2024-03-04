using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThurstonBoardGameClub.Migrations
{
    /// <inheritdoc />
    public partial class Replies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Messages",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "MessageId1",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReplyId",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "idOriginalMessage",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_MessageId1",
                table: "Messages",
                column: "MessageId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Messages_MessageId1",
                table: "Messages",
                column: "MessageId1",
                principalTable: "Messages",
                principalColumn: "MessageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Messages_MessageId1",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_MessageId1",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "MessageId1",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ReplyId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "idOriginalMessage",
                table: "Messages");
        }
    }
}
