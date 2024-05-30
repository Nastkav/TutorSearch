using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infra.DatabaseAdapter._Migrations;

/// <inheritdoc />
public partial class searchUserFilter : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder) =>
        migrationBuilder.AddColumn<string>(
            "NormalizeName",
            "AspNetUsers",
            "longtext",
            nullable: false);

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder) =>
        migrationBuilder.DropColumn(
            "NormalizeName",
            "AspNetUsers");
}