using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Infra.DatabaseAdapter._Migrations;

/// <inheritdoc />
public partial class Reviews : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "TutorReviewModel");

        migrationBuilder.DropColumn(
            "CreatedId",
            "Cities");

        migrationBuilder.DropColumn(
            "UpdatedId",
            "Cities");

        migrationBuilder.CreateTable(
                "ReviewModel",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    TutorId = table.Column<int>("int", nullable: false),
                    Rating = table.Column<int>("int", nullable: false),
                    Description = table.Column<string>("varchar(300)", maxLength: 300, nullable: false),
                    CreatedId = table.Column<int>("int", nullable: false),
                    CreatedAt = table.Column<DateTime>("datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>("datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewModel", x => x.Id);
                    table.ForeignKey(
                        "FK_ReviewModel_AspNetUsers_CreatedId",
                        x => x.CreatedId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_ReviewModel_Tutor_TutorId",
                        x => x.TutorId,
                        "Tutor",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                })
            .Annotation("MySQL:Charset", "utf8mb4");

        migrationBuilder.CreateIndex(
            "IX_ReviewModel_CreatedId",
            "ReviewModel",
            "CreatedId");

        migrationBuilder.CreateIndex(
            "IX_ReviewModel_TutorId",
            "ReviewModel",
            "TutorId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "ReviewModel");

        migrationBuilder.AddColumn<int>(
            "CreatedId",
            "Cities",
            "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AddColumn<int>(
            "UpdatedId",
            "Cities",
            "int",
            nullable: true);

        migrationBuilder.CreateTable(
                "TutorReviewModel",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CreatedId = table.Column<int>("int", nullable: false),
                    TutorId = table.Column<int>("int", nullable: false),
                    CreatedAt = table.Column<DateTime>("datetime(6)", nullable: false),
                    Description = table.Column<string>("varchar(300)", maxLength: 300, nullable: false),
                    Rating = table.Column<int>("int", nullable: false),
                    UpdatedAt = table.Column<DateTime>("datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorReviewModel", x => x.Id);
                    table.ForeignKey(
                        "FK_TutorReviewModel_AspNetUsers_CreatedId",
                        x => x.CreatedId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_TutorReviewModel_Tutor_TutorId",
                        x => x.TutorId,
                        "Tutor",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                })
            .Annotation("MySQL:Charset", "utf8mb4");


        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            1,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            2,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            3,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            4,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            5,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            6,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            7,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            8,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            9,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            10,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            11,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            12,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            13,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            14,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            15,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            16,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            17,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            18,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            19,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            20,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            21,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            22,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            23,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            24,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            25,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            26,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            27,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            28,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            29,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            30,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            31,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            32,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            33,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            34,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            35,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            36,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            37,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            38,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            39,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            40,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            41,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            42,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            43,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            44,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            45,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            46,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            47,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            48,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            49,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            50,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            51,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            52,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            53,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            54,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            55,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            56,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            57,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            58,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            59,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            60,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            61,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            62,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            63,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            64,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            65,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            66,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            67,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            68,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            69,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            70,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            71,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            72,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            73,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            74,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            75,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            76,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            77,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            78,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            79,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            80,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            81,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            82,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            83,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            84,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            85,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            86,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            87,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            88,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            89,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            90,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            91,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            92,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            93,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            94,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            95,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            96,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            97,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            98,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            99,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            100,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            101,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            102,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            103,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            104,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            105,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            106,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            107,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            108,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            109,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            110,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            111,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            112,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            113,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            114,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            115,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            116,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            117,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            118,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            119,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            120,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            121,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            122,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            123,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            124,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            125,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            126,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            127,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            128,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            129,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            130,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            131,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            132,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            133,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            134,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            135,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            136,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            137,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            138,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            139,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            140,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            141,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            142,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            143,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            144,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            145,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            146,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            147,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            148,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            149,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            150,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            151,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            152,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            153,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            154,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            155,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            156,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            157,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            158,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            159,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            160,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            161,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            162,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            163,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            164,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            165,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            166,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            167,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            168,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            169,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            170,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            171,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            172,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            173,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            174,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            175,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            176,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            177,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            178,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            179,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            180,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            181,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            182,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            183,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            184,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            185,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            186,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            187,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            188,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            189,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            190,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            191,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            192,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            193,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            194,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            195,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            196,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            197,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            198,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            199,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            200,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            201,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            202,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            203,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            204,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            205,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            206,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            207,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            208,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            209,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            210,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            211,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            212,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            213,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            214,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            215,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            216,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            217,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            218,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            219,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            220,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            221,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            222,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            223,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            224,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            225,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            226,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            227,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            228,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            229,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            230,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            231,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            232,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            233,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            234,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            235,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            236,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            237,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            238,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            239,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            240,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            241,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            242,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            243,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            244,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            245,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            246,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            247,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            248,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            249,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            250,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            251,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            252,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            253,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            254,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            255,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            256,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            257,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            258,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            259,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            260,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            261,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            262,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            263,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            264,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            265,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            266,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            267,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            268,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            269,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            270,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            271,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            272,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            273,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            274,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            275,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            276,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            277,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            278,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            279,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            280,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            281,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            282,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            283,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            284,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            285,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            286,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            287,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            288,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            289,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            290,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            291,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            292,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            293,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            294,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            295,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            296,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            297,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            298,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            299,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            300,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            301,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            302,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            303,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            304,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            305,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            306,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            307,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            308,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            309,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            310,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            311,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            312,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            313,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            314,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            315,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            316,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            317,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            318,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            319,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            320,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            321,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            322,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            323,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            324,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            325,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            326,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            327,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            328,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            329,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            330,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            331,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            332,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            333,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            334,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            335,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            336,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            337,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            338,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            339,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            340,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            341,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            342,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            343,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            344,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            345,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            346,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            347,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            348,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            349,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            350,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            351,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            352,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            353,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            354,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            355,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            356,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            357,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            358,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            359,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            360,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            361,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            362,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            363,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            364,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            365,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            366,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            367,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            368,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            369,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            370,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            371,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            372,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            373,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            374,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            375,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            376,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            377,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            378,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            379,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            380,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            381,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            382,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            383,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            384,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            385,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            386,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            387,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            388,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            389,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            390,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            391,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            392,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            393,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            394,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            395,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            396,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            397,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            398,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            399,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            400,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            401,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            402,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            403,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            404,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            405,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            406,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            407,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            408,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            409,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            410,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            411,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            412,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            413,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            414,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            415,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            416,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            417,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            418,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            419,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            420,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            421,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            422,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            423,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            424,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            425,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            426,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            427,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            428,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            429,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            430,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            431,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            432,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            433,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            434,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            435,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            436,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            437,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            438,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            439,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            440,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            441,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            442,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            443,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            444,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            445,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            446,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            447,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            448,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            449,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            450,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            451,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            452,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            453,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            454,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            455,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            456,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            457,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            458,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            459,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            460,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.UpdateData(
            "Cities",
            "Id",
            461,
            new[] { "CreatedId", "UpdatedId" },
            new object[] { 0, null });

        migrationBuilder.CreateIndex(
            "IX_TutorReviewModel_CreatedId",
            "TutorReviewModel",
            "CreatedId");

        migrationBuilder.CreateIndex(
            "IX_TutorReviewModel_TutorId",
            "TutorReviewModel",
            "TutorId");
    }
}