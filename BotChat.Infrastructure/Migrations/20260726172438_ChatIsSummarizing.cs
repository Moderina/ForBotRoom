using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BotChat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChatIsSummarizing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSummarizing",
                table: "ChatMemories",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSummarizing",
                table: "ChatMemories");
        }
    }
}
