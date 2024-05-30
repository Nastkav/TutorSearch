using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.DatabaseAdapter._Migrations;

/// <inheritdoc />
public partial class FavoriteName : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            "FK_FavoriteTutors_Tutors_ProfileId",
            "FavoriteTutors");

        migrationBuilder.DropIndex(
            "IX_FavoriteTutors_ProfileId",
            "FavoriteTutors");


        migrationBuilder.AddForeignKey(
            "FK_FavoriteTutors_Tutors_UserId",
            "FavoriteTutors",
            "UserId",
            "Tutors",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            "FK_FavoriteTutors_Tutors_UserId",
            "FavoriteTutors");

        migrationBuilder.CreateIndex(
            "IX_FavoriteTutors_ProfileId",
            "FavoriteTutors",
            "ProfileId");

        migrationBuilder.AddForeignKey(
            "FK_FavoriteTutors_Tutors_ProfileId",
            "FavoriteTutors",
            "ProfileId",
            "Tutors",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}