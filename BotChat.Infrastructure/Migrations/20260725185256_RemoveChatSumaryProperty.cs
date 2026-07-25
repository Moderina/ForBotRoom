using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BotChat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveChatSumaryProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Summary",
                table: "Chats");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "Chats",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
