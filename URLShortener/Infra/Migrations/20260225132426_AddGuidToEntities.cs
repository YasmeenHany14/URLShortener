using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace URLShortener.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddGuidToEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OriginalUrl",
                table: "Urls",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "ExternalId",
                table: "Urls",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExternalId",
                table: "RefreshTokens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "Urls");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "RefreshTokens");

            migrationBuilder.AlterColumn<string>(
                name: "OriginalUrl",
                table: "Urls",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);
        }
    }
}
