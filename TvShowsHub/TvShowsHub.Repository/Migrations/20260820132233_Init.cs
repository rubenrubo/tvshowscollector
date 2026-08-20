using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TvShowsHub.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TvShows",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Premiered = table.Column<DateOnly>(type: "date", nullable: true),
                    Genres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TvShows", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TvShows");
        }
    }
}
