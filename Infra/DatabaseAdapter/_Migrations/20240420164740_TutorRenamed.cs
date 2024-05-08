using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.DatabaseAdapter._Migrations
{
    /// <inheritdoc />
    public partial class TutorRenamed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Tutor_TutorProfileId",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Tutor_TutorId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_TutorProfileId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "TutorProfileId",
                table: "Lessons");

            migrationBuilder.AlterColumn<int>(
                name: "TutorId",
                table: "Requests",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "ea8e02b0-bd18-4e9b-ae65-e3acea4d4e88");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "5ba2625e-ff00-4456-a7d7-de02e53d6eb1");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_TutorId",
                table: "Lessons",
                column: "TutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Tutor_TutorId",
                table: "Lessons",
                column: "TutorId",
                principalTable: "Tutor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Tutor_TutorId",
                table: "Requests",
                column: "TutorId",
                principalTable: "Tutor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Tutor_TutorId",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Tutor_TutorId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_TutorId",
                table: "Lessons");

            migrationBuilder.AlterColumn<int>(
                name: "TutorId",
                table: "Requests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "TutorProfileId",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "185c8567-5c71-4272-bce5-5e61dbe36bf3");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "1be26e7e-12b6-4733-9952-cac5105b144e");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_TutorProfileId",
                table: "Lessons",
                column: "TutorProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Tutor_TutorProfileId",
                table: "Lessons",
                column: "TutorProfileId",
                principalTable: "Tutor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Tutor_TutorId",
                table: "Requests",
                column: "TutorId",
                principalTable: "Tutor",
                principalColumn: "Id");
        }
    }
}
