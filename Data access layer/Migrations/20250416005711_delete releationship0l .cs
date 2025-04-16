using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_access_layer.Migrations
{
    /// <inheritdoc />
    public partial class deletereleationship0l : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Subject",
                table: "Courses");
        }
    }
}
