using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.DatabaseAdapter._Migrations;

/// <inheritdoc />
public partial class FileServerName : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder) =>
        migrationBuilder.AddColumn<string>(
            "ServerName",
            "Files",
            "varchar(350)",
            maxLength: 350,
            nullable: false,
            defaultValue: "");

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder) =>
        migrationBuilder.DropColumn(
            "ServerName",
            "Files");
}