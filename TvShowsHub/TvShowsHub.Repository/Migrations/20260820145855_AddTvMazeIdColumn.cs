using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TvShowsHub.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddTvMazeIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TvMazeId",
                table: "TvShows",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TvShows_TvMazeId",
                table: "TvShows",
                column: "TvMazeId",
                unique: true,
                filter: "[TvMazeId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TvShows_TvMazeId",
                table: "TvShows");

            migrationBuilder.DropColumn(
                name: "TvMazeId",
                table: "TvShows");
        }
    }
}
