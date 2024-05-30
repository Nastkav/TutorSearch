using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.DatabaseAdapter._Migrations;

/// <inheritdoc />
public partial class FavoriteTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "FavoriteTutors");

        migrationBuilder.CreateTable(
                "Favorites",
                table => new
                {
                    FavoriteTutorsId = table.Column<int>("int", nullable: false),
                    InFavoriteId = table.Column<int>("int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => new { x.FavoriteTutorsId, x.InFavoriteId });
                    table.ForeignKey(
                        "FK_Favorites_AspNetUsers_InFavoriteId",
                        x => x.InFavoriteId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Favorites_Tutors_FavoriteTutorsId",
                        x => x.FavoriteTutorsId,
                        "Tutors",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                })
            .Annotation("MySQL:Charset", "utf8mb4");


        migrationBuilder.CreateIndex(
            "IX_Favorites_InFavoriteId",
            "Favorites",
            "InFavoriteId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "Favorites");

        migrationBuilder.CreateTable(
                "FavoriteTutors",
                table => new
                {
                    UserId = table.Column<int>("int", nullable: false),
                    ProfileId = table.Column<int>("int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteTutors", x => new { x.UserId, x.ProfileId });
                    table.ForeignKey(
                        "FK_FavoriteTutors_AspNetUsers_UserId",
                        x => x.UserId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_FavoriteTutors_Tutors_UserId",
                        x => x.UserId,
                        "Tutors",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                })
            .Annotation("MySQL:Charset", "utf8mb4");
    }
}