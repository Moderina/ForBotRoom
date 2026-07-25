using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BotChat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChatMemory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "Chats",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ChatMemories",
                columns: table => new
                {
                    ChatId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Summary = table.Column<string>(type: "TEXT", nullable: true),
                    LastSummarizedMessageId = table.Column<Guid>(type: "TEXT", nullable: true),
                    LastSummarizedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMemories", x => x.ChatId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatMemories");

            migrationBuilder.DropColumn(
                name: "Summary",
                table: "Chats");
        }
    }
}
