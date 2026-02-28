using System.Runtime.InteropServices.ComTypes;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace URLShortener.Infra.Migrations
{
    /// <inheritdoc />
    public partial class ChangeIdsBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "Urls");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "RefreshTokens");

            migrationBuilder.DropPrimaryKey("PK_Urls", "Urls");
            
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Urls"
            );
            migrationBuilder.AddColumn<int>(
                name: "Id",
                type: "int",
                table: "Urls",
                nullable: false
            );
            
            migrationBuilder.AddPrimaryKey(
                name: "PK_Urls",
                table: "Urls",
                column: "Id"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey("PK_Urls", "Urls");
            
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Urls"
            );
            
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Urls",
                type: "int",
                nullable: false
            ).Annotation("SqlServer:Identity", "1, 1");
            
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
    }
}
