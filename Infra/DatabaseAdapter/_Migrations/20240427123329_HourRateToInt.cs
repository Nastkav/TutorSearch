using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.DatabaseAdapter._Migrations
{
    /// <inheritdoc />
    public partial class HourRateToInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "HourRate",
                table: "Tutors",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BirthDate", "ConcurrencyStamp" },
                values: new object[] { new DateTime(2004, 4, 27, 0, 0, 0, 0, DateTimeKind.Local), "b0c67a3f-53e8-426e-bf5f-04ec252ed9db" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BirthDate", "ConcurrencyStamp" },
                values: new object[] { new DateTime(1999, 4, 27, 0, 0, 0, 0, DateTimeKind.Local), "11efaa4b-1f5a-45f6-852d-ade07e01fcfa" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "HourRate",
                table: "Tutors",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BirthDate", "ConcurrencyStamp" },
                values: new object[] { new DateTime(2004, 4, 24, 0, 0, 0, 0, DateTimeKind.Local), "bdac24fc-29aa-4740-8023-10c59e0e4e23" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BirthDate", "ConcurrencyStamp" },
                values: new object[] { new DateTime(1999, 4, 24, 0, 0, 0, 0, DateTimeKind.Local), "a9fc8738-b8a9-4213-83c0-5ec7a52e293b" });
        }
    }
}
