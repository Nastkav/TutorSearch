using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.DatabaseAdapter._Migrations;

/// <inheritdoc />
public partial class RequestMustNull : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            "FK_Lessons_Requests_RequestId",
            "Lessons");

        migrationBuilder.AlterColumn<int>(
            "RequestId",
            "Lessons",
            "int",
            nullable: true,
            oldClrType: typeof(int),
            oldType: "int");


        migrationBuilder.AddForeignKey(
            "FK_Lessons_Requests_RequestId",
            "Lessons",
            "RequestId",
            "Requests",
            principalColumn: "Id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            "FK_Lessons_Requests_RequestId",
            "Lessons");

        migrationBuilder.AlterColumn<int>(
            "RequestId",
            "Lessons",
            "int",
            nullable: false,
            defaultValue: 0,
            oldClrType: typeof(int),
            oldType: "int",
            oldNullable: true);


        migrationBuilder.AddForeignKey(
            "FK_Lessons_Requests_RequestId",
            "Lessons",
            "RequestId",
            "Requests",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}