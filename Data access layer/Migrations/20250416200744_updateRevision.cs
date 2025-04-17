using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_access_layer.Migrations
{
    /// <inheritdoc />
    public partial class updateRevision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VideoURL",
                table: "Revisions",
                newName: "Video");

            migrationBuilder.RenameColumn(
                name: "SupportingFiles",
                table: "Revisions",
                newName: "Files");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Video",
                table: "Revisions",
                newName: "VideoURL");

            migrationBuilder.RenameColumn(
                name: "Files",
                table: "Revisions",
                newName: "SupportingFiles");
        }
    }
}
