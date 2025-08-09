using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace employees_system.Migrations
{
    /// <inheritdoc />
    public partial class constrains : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PropertyDefinitions",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyDefinitions_Name",
                table: "PropertyDefinitions",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PropertyDefinitions_Name",
                table: "PropertyDefinitions");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PropertyDefinitions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
