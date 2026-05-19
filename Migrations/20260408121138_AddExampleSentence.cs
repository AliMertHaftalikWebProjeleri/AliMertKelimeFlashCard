using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AliMertKelimeEzberleme.Migrations
{
    /// <inheritdoc />
    public partial class AddExampleSentence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExampleSentence",
                table: "Flashcards",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExampleSentence",
                table: "Flashcards");
        }
    }
}
