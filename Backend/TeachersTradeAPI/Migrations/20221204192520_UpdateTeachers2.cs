using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachersTradeAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTeachers2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShareCount",
                table: "Teachers",
                newName: "MaxShares");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaxShares",
                table: "Teachers",
                newName: "ShareCount");
        }
    }
}
