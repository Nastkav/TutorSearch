using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.DatabaseAdapter._Migrations
{
    /// <inheritdoc />
    public partial class FixSolutiontFiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentFiles_Solutions_SolutionsId",
                table: "AssignmentFiles");

            migrationBuilder.DropIndex(
                name: "IX_AssignmentFiles_SolutionsId",
                table: "AssignmentFiles");

            migrationBuilder.DropColumn(
                name: "SolutionsId",
                table: "AssignmentFiles");

            migrationBuilder.CreateTable(
                name: "SolutiontFiles",
                columns: table => new
                {
                    FilesId = table.Column<int>(type: "int", nullable: false),
                    SolutionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolutiontFiles", x => new { x.FilesId, x.SolutionsId });
                    table.ForeignKey(
                        name: "FK_SolutiontFiles_Files_FilesId",
                        column: x => x.FilesId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolutiontFiles_Solutions_SolutionsId",
                        column: x => x.SolutionsId,
                        principalTable: "Solutions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "9e62203e-797d-4015-bd07-c6c1c114b55b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "95e48fef-d0ae-49a8-b2e3-b7ba3db348a4");

            migrationBuilder.CreateIndex(
                name: "IX_SolutiontFiles_SolutionsId",
                table: "SolutiontFiles",
                column: "SolutionsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SolutiontFiles");

            migrationBuilder.AddColumn<int>(
                name: "SolutionsId",
                table: "AssignmentFiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "04b6e721-52bf-437b-bdfe-99ef9906bb7d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "fd20a7fe-1481-4781-9647-0349196975f3");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentFiles_SolutionsId",
                table: "AssignmentFiles",
                column: "SolutionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentFiles_Solutions_SolutionsId",
                table: "AssignmentFiles",
                column: "SolutionsId",
                principalTable: "Solutions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
