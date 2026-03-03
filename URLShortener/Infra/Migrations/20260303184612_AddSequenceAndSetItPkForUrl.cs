using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace URLShortener.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddSequenceSndSetItPKForUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "UrlNumber",
                startValue: 100L,
                incrementBy: 10);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Urls",
                type: "int",
                nullable: false,
                defaultValueSql: "NEXT VALUE FOR UrlNumber",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "60628d11-b8b2-40d0-bcf3-fb9e60f76f30",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "User", "USER" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AspNetUsers");

            migrationBuilder.DropSequence(
                name: "UrlNumber");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Urls",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValueSql: "NEXT VALUE FOR UrlNumber");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "60628d11-b8b2-40d0-bcf3-fb9e60f76f30",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "Student", "STUDENT" });
        }
    }
}
