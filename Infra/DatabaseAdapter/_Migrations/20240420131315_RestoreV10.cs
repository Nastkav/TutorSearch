using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infra.DatabaseAdapter._Migrations
{
    /// <inheritdoc />
    public partial class RestoreV10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Region = table.Column<string>(type: "varchar(140)", maxLength: 140, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedId = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Patronymic = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Avatar = table.Column<string>(type: "varchar(400)", maxLength: 400, nullable: false),
                    ProfileEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true),
                    SecurityStamp = table.Column<string>(type: "longtext", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tutor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    OnlineAccess = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TutorHomeAccess = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    StudentHomeAccess = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Descriptions = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    HourRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tutor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tutor_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AboutTutor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "varchar(5000)", maxLength: 5000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutTutor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AboutTutor_Tutor_Id",
                        column: x => x.Id,
                        principalTable: "Tutor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AvailableTimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    ProfileId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedId = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailableTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AvailableTimes_Tutor_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Tutor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FavoriteTutors",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProfileId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteTutors", x => new { x.UserId, x.ProfileId });
                    table.ForeignKey(
                        name: "FK_FavoriteTutors_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoriteTutors_Tutor_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Tutor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProfileSubjects",
                columns: table => new
                {
                    ProfilesId = table.Column<int>(type: "int", nullable: false),
                    SubjectsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileSubjects", x => new { x.ProfilesId, x.SubjectsId });
                    table.ForeignKey(
                        name: "FK_ProfileSubjects_Subjects_SubjectsId",
                        column: x => x.SubjectsId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfileSubjects_Tutor_ProfilesId",
                        column: x => x.ProfilesId,
                        principalTable: "Tutor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    TutorId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    From = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    To = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Comment = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    TutorComment = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    CreatedId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requests_AspNetUsers_CreatedId",
                        column: x => x.CreatedId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Requests_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Requests_Tutor_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Tutor",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    TutorId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    Deadline = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Tutor_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Tutor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TutorReviewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    TutorId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    CreatedId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorReviewModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TutorReviewModel_AspNetUsers_CreatedId",
                        column: x => x.CreatedId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TutorReviewModel_Tutor_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Tutor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    TutorId = table.Column<int>(type: "int", nullable: false),
                    TutorProfileId = table.Column<int>(type: "int", nullable: false),
                    From = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    To = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Title = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Comment = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lessons_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lessons_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lessons_Tutor_TutorProfileId",
                        column: x => x.TutorProfileId,
                        principalTable: "Tutor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Solutions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    Answer = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solutions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Solutions_AspNetUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Solutions_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StudentLessons",
                columns: table => new
                {
                    LessonsId = table.Column<int>(type: "int", nullable: false),
                    StudentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentLessons", x => new { x.LessonsId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_StudentLessons_AspNetUsers_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentLessons_Lessons_LessonsId",
                        column: x => x.LessonsId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    TaskId = table.Column<int>(type: "int", nullable: true),
                    SolutionId = table.Column<int>(type: "int", nullable: true),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Files_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Files_Solutions_SolutionId",
                        column: x => x.SolutionId,
                        principalTable: "Solutions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Files_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, null, "Administrator", "ADMINISTRATOR" },
                    { 2, null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Avatar", "BirthDate", "CityId", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "Patronymic", "PhoneNumber", "PhoneNumberConfirmed", "ProfileEnabled", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "", new DateTime(2004, 4, 20, 0, 0, 0, 0, DateTimeKind.Local), null, "185c8567-5c71-4272-bce5-5e61dbe36bf3", "admin@example.com", true, false, null, "Administrator", "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAIAAYagAAAAED/JcnGip5yrSrBHQH+LmlC7r6Pf1nzvsaAZgxa0Pc25cvBsvI1hAD7lJ+61BoGihQ==", "None", null, false, false, "482ea3c3-a1c3-4c83-aced-72e0e5b8808c", "None", false, "admin@example.com" },
                    { 2, 0, "", new DateTime(1999, 4, 20, 0, 0, 0, 0, DateTimeKind.Local), null, "1be26e7e-12b6-4733-9952-cac5105b144e", "tutor@example.com", true, false, null, "Ірина", "TUTOR@EXAMPLE.COM", "TUTOR@EXAMPLE.COM", "AQAAAAIAAYagAAAAEMfkHhl9MnIURCj0Kd8zbGKDK9t+NX29GB2ZJ7L2iwIYn7j1jbN2yDErYirY8PsRNA==", "Миколаївна", null, false, false, "9cbd20e0-3497-4bbb-95f7-da2f2710e420", "Мельник", false, "tutor@example.com" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CreatedAt", "CreatedId", "Name", "Region", "UpdatedAt", "UpdatedId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Авдіївка", "Донецька область", null, null },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Алмазна", "Луганська область", null, null },
                    { 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Алупка", "Автономна Республіка Крим", null, null },
                    { 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Алушта", "Автономна Республіка Крим", null, null },
                    { 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Алчевськ", "Луганська область", null, null },
                    { 6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Амвросіївка", "Донецька область", null, null },
                    { 7, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Ананьїв", "Одеська область", null, null },
                    { 8, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Андрушівка", "Житомирська область", null, null },
                    { 9, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Антрацит", "Луганська область", null, null },
                    { 10, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Апостолове", "Дніпропетровська область", null, null },
                    { 11, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Армянськ", "Автономна Республіка Крим", null, null },
                    { 12, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Арциз", "Одеська область", null, null },
                    { 13, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Балаклія", "Харківська область", null, null },
                    { 14, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Балта", "Одеська область", null, null },
                    { 15, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Бар", "Вінницька область", null, null },
                    { 16, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Баранівка", "Житомирська область", null, null },
                    { 17, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Барвінкове", "Харківська область", null, null },
                    { 18, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Батурин", "Чернігівська область", null, null },
                    { 19, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Бахмач", "Чернігівська область", null, null },
                    { 20, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Бахмут", "Донецька область", null, null },
                    { 21, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Бахчисарай", "Автономна Республіка Крим", null, null },
                    { 22, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Баштанка", "Миколаївська область", null, null },
                    { 23, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Белз", "Львівська область", null, null },
                    { 24, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Бердичів", "Житомирська область", null, null },
                    { 25, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Бердянськ", "Запорізька область", null, null },
                    { 26, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Берегове", "Закарпатська область", null, null },
                    { 27, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Бережани", "Тернопільська область", null, null },
                    { 28, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Березань", "Київська область", null, null },
                    { 29, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Березівка", "Одеська область", null, null },
                    { 30, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Березне", "Рівненська область", null, null },
                    { 31, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Берестечко", "Волинська область", null, null },
                    { 32, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Берислав", "Херсонська область", null, null },
                    { 33, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Бершадь", "Вінницька область", null, null },
                    { 34, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Бібрка", "Львівська область", null, null },
                    { 35, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Біла Церква", "Київська область", null, null },
                    { 36, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Білгород-Дністровський", "Одеська область", null, null },
                    { 37, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Білицьке", "Донецька область", null, null },
                    { 38, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Білогірськ", "Автономна Республіка Крим", null, null },
                    { 39, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Білозерське", "Донецька область", null, null },
                    { 40, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Білопілля", "Сумська область", null, null },
                    { 41, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Біляївка", "Одеська область", null, null },
                    { 42, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Благовіщенське", "Кіровоградська область", null, null },
                    { 43, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Бобринець", "Кіровоградська область", null, null },
                    { 44, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Бобровиця", "Чернігівська область", null, null },
                    { 45, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Богодухів", "Харківська область", null, null },
                    { 46, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Богуслав", "Київська область", null, null },
                    { 47, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Боково-Хрустальне", "Луганська область", null, null },
                    { 48, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Болград", "Одеська область", null, null },
                    { 49, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Болехів", "Івано-Франківська область", null, null },
                    { 50, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Борзна", "Чернігівська область", null, null },
                    { 51, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Борислав", "Львівська область", null, null },
                    { 52, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Бориспіль", "Київська область", null, null },
                    { 53, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Борщів", "Тернопільська область", null, null },
                    { 54, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Боярка", "Київська область", null, null },
                    { 55, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Бровари", "Київська область", null, null },
                    { 56, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Броди", "Львівська область", null, null },
                    { 57, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Брянка", "Луганська область", null, null },
                    { 58, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Бунге", "Донецька область", null, null },
                    { 59, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Буринь", "Сумська область", null, null },
                    { 60, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Бурштин", "Івано-Франківська область", null, null },
                    { 61, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Буськ", "Львівська область", null, null },
                    { 62, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Буча", "Київська область", null, null },
                    { 63, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Бучач", "Тернопільська область", null, null },
                    { 64, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Валки", "Харківська область", null, null },
                    { 65, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Вараш", "Рівненська область", null, null },
                    { 66, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Василівка", "Запорізька область", null, null },
                    { 67, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Васильків", "Київська область", null, null },
                    { 68, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Ватутіне", "Черкаська область", null, null },
                    { 69, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Вашківці", "Чернівецька область", null, null },
                    { 70, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Великі Мости", "Львівська область", null, null },
                    { 71, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Верхівцеве", "Дніпропетровська область", null, null },
                    { 72, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Верхньодніпровськ", "Дніпропетровська область", null, null },
                    { 73, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Вижниця", "Чернівецька область", null, null },
                    { 74, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Вилкове", "Одеська область", null, null },
                    { 75, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Винники", "Львівська область", null, null },
                    { 76, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Виноградів", "Закарпатська область", null, null },
                    { 77, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Вишгород", "Київська область", null, null },
                    { 78, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Вишневе", "Київська область", null, null },
                    { 79, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Вільногірськ", "Дніпропетровська область", null, null },
                    { 80, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Вільнянськ", "Запорізька область", null, null },
                    { 81, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Вінниця", "Вінницька область", null, null },
                    { 82, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Вовчанськ", "Харківська область", null, null },
                    { 83, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Вознесенівка", "Луганська область", null, null },
                    { 84, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Вознесенськ", "Миколаївська область", null, null },
                    { 85, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Волноваха", "Донецька область", null, null },
                    { 86, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Володимир", "Волинська область", null, null },
                    { 87, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Волочиськ", "Хмельницька область", null, null },
                    { 88, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Ворожба", "Сумська область", null, null },
                    { 89, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Вуглегірськ", "Донецька область", null, null },
                    { 90, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Вугледар", "Донецька область", null, null },
                    { 91, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Гадяч", "Полтавська область", null, null },
                    { 92, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Гайворон", "Кіровоградська область", null, null },
                    { 93, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Гайсин", "Вінницька область", null, null },
                    { 94, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Галич", "Івано-Франківська область", null, null },
                    { 95, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Генічеськ", "Херсонська область", null, null },
                    { 96, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Герца", "Чернівецька область", null, null },
                    { 97, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Гірник", "Донецька область", null, null },
                    { 98, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Гірське", "Луганська область", null, null },
                    { 99, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Глиняни", "Львівська область", null, null },
                    { 100, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Глобине", "Полтавська область", null, null },
                    { 101, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Глухів", "Сумська область", null, null },
                    { 102, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Гнівань", "Вінницька область", null, null },
                    { 103, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Гола Пристань", "Херсонська область", null, null },
                    { 104, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Голубівка", "Луганська область", null, null },
                    { 105, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Горішні Плавні", "Полтавська область", null, null },
                    { 106, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Горлівка", "Донецька область", null, null },
                    { 107, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Городенка", "Івано-Франківська область", null, null },
                    { 108, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Городище", "Черкаська область", null, null },
                    { 109, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Городня", "Чернігівська область", null, null },
                    { 110, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Городок", "Львівська область", null, null },
                    { 111, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Городок", "Хмельницька область", null, null },
                    { 112, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Горохів", "Волинська область", null, null },
                    { 113, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Гребінка", "Полтавська область", null, null },
                    { 114, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Гуляйполе", "Запорізька область", null, null },
                    { 115, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Дебальцеве", "Донецька область", null, null },
                    { 116, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Деражня", "Хмельницька область", null, null },
                    { 117, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Дергачі", "Харківська область", null, null },
                    { 118, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Джанкой", "Автономна Республіка Крим", null, null },
                    { 119, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Дніпро", "Дніпропетровська область", null, null },
                    { 120, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Дніпрорудне", "Запорізька область", null, null },
                    { 121, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Добромиль", "Львівська область", null, null },
                    { 122, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Добропілля", "Донецька область", null, null },
                    { 123, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Довжанськ", "Луганська область", null, null },
                    { 124, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Докучаєвськ", "Донецька область", null, null },
                    { 125, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Долина", "Івано-Франківська область", null, null },
                    { 126, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Долинська", "Кіровоградська область", null, null },
                    { 127, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Донецьк", "Донецька область", null, null },
                    { 128, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Дрогобич", "Львівська область", null, null },
                    { 129, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Дружба", "Сумська область", null, null },
                    { 130, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Дружківка", "Донецька область", null, null },
                    { 131, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Дубляни", "Львівська область", null, null },
                    { 132, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Дубно", "Рівненська область", null, null },
                    { 133, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Дубровиця", "Рівненська область", null, null },
                    { 134, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Дунаївці", "Хмельницька область", null, null },
                    { 135, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Енергодар", "Запорізька область", null, null },
                    { 136, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Євпаторія", "Автономна Республіка Крим", null, null },
                    { 137, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Єнакієве", "Донецька область", null, null },
                    { 138, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Жашків", "Черкаська область", null, null },
                    { 139, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Жданівка", "Донецька область", null, null },
                    { 140, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Жидачів", "Львівська область", null, null },
                    { 141, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Житомир", "Житомирська область", null, null },
                    { 142, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Жмеринка", "Вінницька область", null, null },
                    { 143, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Жовква", "Львівська область", null, null },
                    { 144, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Жовті Води", "Дніпропетровська область", null, null },
                    { 145, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Заводське", "Полтавська область", null, null },
                    { 146, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Залізне", "Донецька область", null, null },
                    { 147, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Заліщики", "Тернопільська область", null, null },
                    { 148, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Запоріжжя", "Запорізька область", null, null },
                    { 149, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Заставна", "Чернівецька область", null, null },
                    { 150, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Збараж", "Тернопільська область", null, null },
                    { 151, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Зборів", "Тернопільська область", null, null },
                    { 152, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Звенигородка", "Черкаська область", null, null },
                    { 153, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Звягель", "Житомирська область", null, null },
                    { 154, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Здолбунів", "Рівненська область", null, null },
                    { 155, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Зеленодольськ", "Дніпропетровська область", null, null },
                    { 156, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Зимогір'я", "Луганська область", null, null },
                    { 157, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Зіньків", "Полтавська область", null, null },
                    { 158, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Зміїв", "Харківська область", null, null },
                    { 159, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Знам'янка", "Кіровоградська область", null, null },
                    { 160, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Золоте", "Луганська область", null, null },
                    { 161, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Золотоноша", "Черкаська область", null, null },
                    { 162, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Золочів", "Львівська область", null, null },
                    { 163, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Зоринськ", "Луганська область", null, null },
                    { 164, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Зугрес", "Донецька область", null, null },
                    { 165, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Івано-Франківськ", "Івано-Франківська область", null, null },
                    { 166, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Ізмаїл", "Одеська область", null, null },
                    { 167, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Ізюм", "Харківська область", null, null },
                    { 168, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Ізяслав", "Хмельницька область", null, null },
                    { 169, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Іллінці", "Вінницька область", null, null },
                    { 170, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Іловайськ", "Донецька область", null, null },
                    { 171, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Інкерман", "Автономна Республіка Крим", null, null },
                    { 172, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Ірміно", "Луганська область", null, null },
                    { 173, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Ірпінь", "Київська область", null, null },
                    { 174, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Іршава", "Закарпатська область", null, null },
                    { 175, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Ічня", "Чернігівська область", null, null },
                    { 176, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Кагарлик", "Київська область", null, null },
                    { 177, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Кадіївка", "Луганська область", null, null },
                    { 178, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Калинівка", "Вінницька область", null, null },
                    { 179, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Калуш", "Івано-Франківська область", null, null },
                    { 180, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Кальміуське", "Донецька область", null, null },
                    { 181, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Камінь-Каширський", "Волинська область", null, null },
                    { 182, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Кам'янець-Подільський", "Хмельницька область", null, null },
                    { 183, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Кам'янка", "Черкаська область", null, null },
                    { 184, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Кам'янка-Бузька", "Львівська область", null, null },
                    { 185, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Кам'янка-Дніпровська", "Запорізька область", null, null },
                    { 186, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Кам'янське", "Дніпропетровська область", null, null },
                    { 187, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Канів", "Черкаська область", null, null },
                    { 188, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Карлівка", "Полтавська область", null, null },
                    { 189, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Каховка", "Херсонська область", null, null },
                    { 190, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Керч", "Автономна Республіка Крим", null, null },
                    { 191, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Київ", "Київ", null, null },
                    { 192, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Кипуче", "Луганська область", null, null },
                    { 193, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Ківерці", "Волинська область", null, null },
                    { 194, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Кілія", "Одеська область", null, null },
                    { 195, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Кіцмань", "Чернівецька область", null, null },
                    { 196, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Кобеляки", "Полтавська область", null, null },
                    { 197, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Ковель", "Волинська область", null, null },
                    { 198, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Кодима", "Одеська область", null, null },
                    { 199, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Козятин", "Вінницька область", null, null },
                    { 200, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Коломия", "Івано-Франківська область", null, null },
                    { 201, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Комарно", "Львівська область", null, null },
                    { 202, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Конотоп", "Сумська область", null, null },
                    { 203, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Копичинці", "Тернопільська область", null, null },
                    { 204, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Корець", "Рівненська область", null, null },
                    { 205, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Коростень", "Житомирська область", null, null },
                    { 206, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Коростишів", "Житомирська область", null, null },
                    { 207, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Корсунь-Шевченківський", "Черкаська область", null, null },
                    { 208, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Корюківка", "Чернігівська область", null, null },
                    { 209, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Косів", "Івано-Франківська область", null, null },
                    { 210, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Костопіль", "Рівненська область", null, null },
                    { 211, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Костянтинівка", "Донецька область", null, null },
                    { 212, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Краматорськ", "Донецька область", null, null },
                    { 213, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Красилів", "Хмельницька область", null, null },
                    { 214, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Красногорівка", "Донецька область", null, null },
                    { 215, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Красноград", "Харківська область", null, null },
                    { 216, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Кременець", "Тернопільська область", null, null },
                    { 217, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Кременчук", "Полтавська область", null, null },
                    { 218, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Кремінна", "Луганська область", null, null },
                    { 219, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Кривий Ріг", "Дніпропетровська область", null, null },
                    { 220, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Кролевець", "Сумська область", null, null },
                    { 221, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Кропивницький", "Кіровоградська область", null, null },
                    { 222, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Куп'янськ", "Харківська область", null, null },
                    { 223, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Курахове", "Донецька область", null, null },
                    { 224, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Ладижин", "Вінницька область", null, null },
                    { 225, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Ланівці", "Тернопільська область", null, null },
                    { 226, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Лебедин", "Сумська область", null, null },
                    { 227, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Лиман", "Донецька область", null, null },
                    { 228, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Липовець", "Вінницька область", null, null },
                    { 229, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Лисичанськ", "Луганська область", null, null },
                    { 230, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Лозова", "Харківська область", null, null },
                    { 231, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Лохвиця", "Полтавська область", null, null },
                    { 232, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Лубни", "Полтавська область", null, null },
                    { 233, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Луганськ", "Луганська область", null, null },
                    { 234, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Лутугине", "Луганська область", null, null },
                    { 235, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Луцьк", "Волинська область", null, null },
                    { 236, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Львів", "Львівська область", null, null },
                    { 237, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Любомль", "Волинська область", null, null },
                    { 238, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Люботин", "Харківська область", null, null },
                    { 239, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Макіївка", "Донецька область", null, null },
                    { 240, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Мала Виска", "Кіровоградська область", null, null },
                    { 241, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Малин", "Житомирська область", null, null },
                    { 242, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Марганець", "Дніпропетровська область", null, null },
                    { 243, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Маріуполь", "Донецька область", null, null },
                    { 244, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Мар'їнка", "Донецька область", null, null },
                    { 245, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Мелітополь", "Запорізька область", null, null },
                    { 246, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Мена", "Чернігівська область", null, null },
                    { 247, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Мерефа", "Харківська область", null, null },
                    { 248, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Миколаїв", "Львівська область", null, null },
                    { 249, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Миколаїв", "Миколаївська область", null, null },
                    { 250, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Миколаївка", "Донецька область", null, null },
                    { 251, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Миргород", "Полтавська область", null, null },
                    { 252, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Мирноград", "Донецька область", null, null },
                    { 253, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Миронівка", "Київська область", null, null },
                    { 254, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Міусинськ", "Луганська область", null, null },
                    { 255, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Могилів-Подільський", "Вінницька область", null, null },
                    { 256, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Молодогвардійськ", "Луганська область", null, null },
                    { 257, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Молочанськ", "Запорізька область", null, null },
                    { 258, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Монастириська", "Тернопільська область", null, null },
                    { 259, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Монастирище", "Черкаська область", null, null },
                    { 260, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Моршин", "Львівська область", null, null },
                    { 261, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Моспине", "Донецька область", null, null },
                    { 262, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Мостиська", "Львівська область", null, null },
                    { 263, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Мукачево", "Закарпатська область", null, null },
                    { 264, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Надвірна", "Івано-Франківська область", null, null },
                    { 265, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Немирів", "Вінницька область", null, null },
                    { 266, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Нетішин", "Хмельницька область", null, null },
                    { 267, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Ніжин", "Чернігівська область", null, null },
                    { 268, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Нікополь", "Дніпропетровська область", null, null },
                    { 269, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Нова Каховка", "Херсонська область", null, null },
                    { 270, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Нова Одеса", "Миколаївська область", null, null },
                    { 271, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Новгород-Сіверський", "Чернігівська область", null, null },
                    { 272, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Новий Буг", "Миколаївська область", null, null },
                    { 273, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Новий Калинів", "Львівська область", null, null },
                    { 274, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Новий Розділ", "Львівська область", null, null },
                    { 275, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Новоазовськ", "Донецька область", null, null },
                    { 276, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Нововолинськ", "Волинська область", null, null },
                    { 277, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Новогродівка", "Донецька область", null, null },
                    { 278, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Новодністровськ", "Чернівецька область", null, null },
                    { 279, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Новодружеськ", "Луганська область", null, null },
                    { 280, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Новомиргород", "Кіровоградська область", null, null },
                    { 281, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Новомосковськ", "Дніпропетровська область", null, null },
                    { 282, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Новоселиця", "Чернівецька область", null, null },
                    { 283, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Новоукраїнка", "Кіровоградська область", null, null },
                    { 284, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Новояворівськ", "Львівська область", null, null },
                    { 285, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Носівка", "Чернігівська область", null, null },
                    { 286, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Обухів", "Київська область", null, null },
                    { 287, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Овруч", "Житомирська область", null, null },
                    { 288, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Одеса", "Одеська область", null, null },
                    { 289, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Олевськ", "Житомирська область", null, null },
                    { 290, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Олександрівськ", "Луганська область", null, null },
                    { 291, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Олександрія", "Кіровоградська область", null, null },
                    { 292, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Олешки", "Херсонська область", null, null },
                    { 293, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Оріхів", "Запорізька область", null, null },
                    { 294, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Остер", "Чернігівська область", null, null },
                    { 295, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Острог", "Рівненська область", null, null },
                    { 296, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Охтирка", "Сумська область", null, null },
                    { 297, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Очаків", "Миколаївська область", null, null },
                    { 298, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Павлоград", "Дніпропетровська область", null, null },
                    { 299, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Первомайськ", "Луганська область", null, null },
                    { 300, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Первомайськ", "Миколаївська область", null, null },
                    { 301, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Первомайський", "Харківська область", null, null },
                    { 302, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Перевальськ", "Луганська область", null, null },
                    { 303, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Перемишляни", "Львівська область", null, null },
                    { 304, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Перечин", "Закарпатська область", null, null },
                    { 305, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Перещепине", "Дніпропетровська область", null, null },
                    { 306, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Переяслав", "Київська область", null, null },
                    { 307, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Першотравенськ", "Дніпропетровська область", null, null },
                    { 308, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Петрово-Красносілля", "Луганська область", null, null },
                    { 309, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Пирятин", "Полтавська область", null, null },
                    { 310, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Південне", "Харківська область", null, null },
                    { 311, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Підгайці", "Тернопільська область", null, null },
                    { 312, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Підгородне", "Дніпропетровська область", null, null },
                    { 313, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Погребище", "Вінницька область", null, null },
                    { 314, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Подільськ", "Одеська область", null, null },
                    { 315, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Покров", "Дніпропетровська область", null, null },
                    { 316, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Покровськ", "Донецька область", null, null },
                    { 317, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Пологи", "Запорізька область", null, null },
                    { 318, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Полонне", "Хмельницька область", null, null },
                    { 319, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Полтава", "Полтавська область", null, null },
                    { 320, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Помічна", "Кіровоградська область", null, null },
                    { 321, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Попасна", "Луганська область", null, null },
                    { 322, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Почаїв", "Тернопільська область", null, null },
                    { 323, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Привілля", "Луганська область", null, null },
                    { 324, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Прилуки", "Чернігівська область", null, null },
                    { 325, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Приморськ", "Запорізька область", null, null },
                    { 326, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Прип'ять", "Київська область", null, null },
                    { 327, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Пустомити", "Львівська область", null, null },
                    { 328, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Путивль", "Сумська область", null, null },
                    { 329, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "П'ятихатки", "Дніпропетровська область", null, null },
                    { 330, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Рава-Руська", "Львівська область", null, null },
                    { 331, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Радехів", "Львівська область", null, null },
                    { 332, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Радивилів", "Рівненська область", null, null },
                    { 333, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Радомишль", "Житомирська область", null, null },
                    { 334, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Рахів", "Закарпатська область", null, null },
                    { 335, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Рені", "Одеська область", null, null },
                    { 336, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Решетилівка", "Полтавська область", null, null },
                    { 337, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Ржищів", "Київська область", null, null },
                    { 338, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Рівне", "Рівненська область", null, null },
                    { 339, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Ровеньки", "Луганська область", null, null },
                    { 340, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Рогатин", "Івано-Франківська область", null, null },
                    { 341, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Родинське", "Донецька область", null, null },
                    { 342, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Рожище", "Волинська область", null, null },
                    { 343, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Роздільна", "Одеська область", null, null },
                    { 344, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Ромни", "Сумська область", null, null },
                    { 345, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Рубіжне", "Луганська область", null, null },
                    { 346, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Рудки", "Львівська область", null, null },
                    { 347, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Саки", "Автономна Республіка Крим", null, null },
                    { 348, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Самбір", "Львівська область", null, null },
                    { 349, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Сарни", "Рівненська область", null, null },
                    { 350, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Свалява", "Закарпатська область", null, null },
                    { 351, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Сватове", "Луганська область", null, null },
                    { 352, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Світловодськ", "Кіровоградська область", null, null },
                    { 353, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Світлодарськ", "Донецька область", null, null },
                    { 354, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Святогірськ", "Донецька область", null, null },
                    { 355, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Севастополь", "Севастополь", null, null },
                    { 356, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Селидове", "Донецька область", null, null },
                    { 357, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Семенівка", "Чернігівська область", null, null },
                    { 358, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Середина-Буда", "Сумська область", null, null },
                    { 359, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Сєвєродонецьк", "Луганська область", null, null },
                    { 360, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Синельникове", "Дніпропетровська область", null, null },
                    { 361, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Сіверськ", "Донецька область", null, null },
                    { 362, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Сімферополь", "Автономна Республіка Крим", null, null },
                    { 363, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Скадовськ", "Херсонська область", null, null },
                    { 364, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Скалат", "Тернопільська область", null, null },
                    { 365, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Сквира", "Київська область", null, null },
                    { 366, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Сколе", "Львівська область", null, null },
                    { 367, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Славута", "Хмельницька область", null, null },
                    { 368, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Славутич", "Київська область", null, null },
                    { 369, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Слов'янськ", "Донецька область", null, null },
                    { 370, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Сміла", "Черкаська область", null, null },
                    { 371, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Снігурівка", "Миколаївська область", null, null },
                    { 372, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Сніжне", "Донецька область", null, null },
                    { 373, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Сновськ", "Чернігівська область", null, null },
                    { 374, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Снятин", "Івано-Франківська область", null, null },
                    { 375, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Сокаль", "Львівська область", null, null },
                    { 376, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Сокиряни", "Чернівецька область", null, null },
                    { 377, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Соледар", "Донецька область", null, null },
                    { 378, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Сорокине", "Луганська область", null, null },
                    { 379, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Соснівка", "Львівська область", null, null },
                    { 380, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Старий Крим", "Автономна Республіка Крим", null, null },
                    { 381, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Старий Самбір", "Львівська область", null, null },
                    { 382, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Старобільськ", "Луганська область", null, null },
                    { 383, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Старокостянтинів", "Хмельницька область", null, null },
                    { 384, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Стебник", "Львівська область", null, null },
                    { 385, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Сторожинець", "Чернівецька область", null, null },
                    { 386, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Стрий", "Львівська область", null, null },
                    { 387, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Судак", "Автономна Республіка Крим", null, null },
                    { 388, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Судова Вишня", "Львівська область", null, null },
                    { 389, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Суми", "Сумська область", null, null },
                    { 390, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Суходільськ", "Луганська область", null, null },
                    { 391, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Таврійськ", "Херсонська область", null, null },
                    { 392, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Тальне", "Черкаська область", null, null },
                    { 393, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Тараща", "Київська область", null, null },
                    { 394, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Татарбунари", "Одеська область", null, null },
                    { 395, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Теплодар", "Одеська область", null, null },
                    { 396, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Теребовля", "Тернопільська область", null, null },
                    { 397, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Тернівка", "Дніпропетровська область", null, null },
                    { 398, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Тернопіль", "Тернопільська область", null, null },
                    { 399, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Тетіїв", "Київська область", null, null },
                    { 400, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Тисмениця", "Івано-Франківська область", null, null },
                    { 401, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Тлумач", "Івано-Франківська область", null, null },
                    { 402, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Токмак", "Запорізька область", null, null },
                    { 403, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Торецьк", "Донецька область", null, null },
                    { 404, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Тростянець", "Сумська область", null, null },
                    { 405, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Трускавець", "Львівська область", null, null },
                    { 406, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Тульчин", "Вінницька область", null, null },
                    { 407, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Турка", "Львівська область", null, null },
                    { 408, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Тячів", "Закарпатська область", null, null },
                    { 409, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Угнів", "Львівська область", null, null },
                    { 410, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Ужгород", "Закарпатська область", null, null },
                    { 411, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Узин", "Київська область", null, null },
                    { 412, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Українка", "Київська область", null, null },
                    { 413, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Українськ", "Донецька область", null, null },
                    { 414, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Умань", "Черкаська область", null, null },
                    { 415, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Устилуг", "Волинська область", null, null },
                    { 416, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Фастів", "Київська область", null, null },
                    { 417, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Феодосія", "Автономна Республіка Крим", null, null },
                    { 418, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Харків", "Харківська область", null, null },
                    { 419, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Харцизьк", "Донецька область", null, null },
                    { 420, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Херсон", "Херсонська область", null, null },
                    { 421, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Хирів", "Львівська область", null, null },
                    { 422, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Хмельницький", "Хмельницька область", null, null },
                    { 423, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Хмільник", "Вінницька область", null, null },
                    { 424, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Ходорів", "Львівська область", null, null },
                    { 425, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Хорол", "Полтавська область", null, null },
                    { 426, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Хоростків", "Тернопільська область", null, null },
                    { 427, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Хотин", "Чернівецька область", null, null },
                    { 428, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Хрестівка", "Донецька область", null, null },
                    { 429, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Христинівка", "Черкаська область", null, null },
                    { 430, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Хрустальний", "Луганська область", null, null },
                    { 431, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Хуст", "Закарпатська область", null, null },
                    { 432, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Часів Яр", "Донецька область", null, null },
                    { 433, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Червоноград", "Львівська область", null, null },
                    { 434, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Черкаси", "Черкаська область", null, null },
                    { 435, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Чернівці", "Чернівецька область", null, null },
                    { 436, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Чернігів", "Чернігівська область", null, null },
                    { 437, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Чигирин", "Черкаська область", null, null },
                    { 438, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Чистякове", "Донецька область", null, null },
                    { 439, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Чоп", "Закарпатська область", null, null },
                    { 440, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Чорнобиль", "Київська область", null, null },
                    { 441, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Чорноморськ", "Одеська область", null, null },
                    { 442, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Чортків", "Тернопільська область", null, null },
                    { 443, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Чугуїв", "Харківська область", null, null },
                    { 444, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Чуднів", "Житомирська область", null, null },
                    { 445, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Шаргород", "Вінницька область", null, null },
                    { 446, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Шахтарськ", "Донецька область", null, null },
                    { 447, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Шепетівка", "Хмельницька область", null, null },
                    { 448, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Шостка", "Сумська область", null, null },
                    { 449, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Шпола", "Черкаська область", null, null },
                    { 450, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Шумськ", "Тернопільська область", null, null },
                    { 451, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Щастя", "Луганська область", null, null },
                    { 452, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Щолкіне", "Автономна Республіка Крим", null, null },
                    { 453, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Южне", "Одеська область", null, null },
                    { 454, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Южноукраїнськ", "Миколаївська область", null, null },
                    { 455, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Яворів", "Львівська область", null, null },
                    { 456, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Яготин", "Київська область", null, null },
                    { 457, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Ялта", "Автономна Республіка Крим", null, null },
                    { 458, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Ямпіль", "Вінницька область", null, null },
                    { 459, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Яремче", "Івано-Франківська область", null, null },
                    { 460, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Яни Капу", "Автономна Республіка Крим", null, null },
                    { 461, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Ясинувата", "Донецька область", null, null }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Англійська" },
                    { 2, "Іспанська" },
                    { 3, "Французька" },
                    { 4, "Німецька" },
                    { 5, "Японська" },
                    { 6, "Італійська" },
                    { 7, "Корейська" },
                    { 8, "Арабська" },
                    { 9, "Китайська (мандарин)" },
                    { 10, "Інформатика" },
                    { 11, "Статистика" },
                    { 12, "Бухгалтерський облік" },
                    { 13, "Хімія" },
                    { 14, "Біологія" },
                    { 15, "Алгебра" },
                    { 16, "Фізика" },
                    { 17, "Історія" },
                    { 18, "Математика" },
                    { 19, "Португальська" },
                    { 20, "Економіка" },
                    { 21, "Польська" },
                    { 22, "Турецька" },
                    { 23, "Українська" },
                    { 24, "Голландська" },
                    { 25, "Фінська" },
                    { 26, "Шведська" },
                    { 27, "Угорська" },
                    { 28, "Хінді" },
                    { 29, "Чеська" },
                    { 30, "Норвезька" },
                    { 31, "Грецька" },
                    { 32, "Іврит" },
                    { 33, "Грузинська" },
                    { 34, "Вірменська" },
                    { 35, "Мистецтво" },
                    { 36, "Музика" },
                    { 37, "Акторська майстерність" },
                    { 38, "Уроки мистецтва" },
                    { 39, "Санскрит" },
                    { 40, "В'єтнамська" },
                    { 41, "Телугу" },
                    { 42, "Тамільська" },
                    { 43, "Жестовий" },
                    { 44, "Тагалог" },
                    { 45, "Румунська" },
                    { 46, "Ірландська" },
                    { 47, "Ісландська" },
                    { 48, "Перська (фарсі)" },
                    { 49, "Хорватська" },
                    { 50, "Каталонська" },
                    { 51, "Болгарська" },
                    { 52, "Бенгальська" },
                    { 53, "Датська" },
                    { 54, "Латинська" },
                    { 55, "Урду" },
                    { 56, "Пенджабі" },
                    { 57, "Тайська" },
                    { 58, "Білоруська" },
                    { 59, "Сербська" },
                    { 60, "Тибетська" },
                    { 61, "Кхмерська" },
                    { 62, "Литовська" },
                    { 63, "Словацький" },
                    { 64, "Електронний маркетинг" },
                    { 65, "PR" },
                    { 66, "Міжнародний бізнес" },
                    { 67, "Маркетингова стратегія" },
                    { 68, "Контент-маркетинг" },
                    { 69, "Бізнес та менеджмент" },
                    { 70, "Dota 2" },
                    { 71, "Concursos" },
                    { 72, "Objective C" },
                    { 73, "Data Science" },
                    { 74, "UX/UI" },
                    { 75, "Управління ІТ-проектами" },
                    { 76, "Штучний інтелект" },
                    { 77, "Веб-розробка" },
                    { 78, "Веб-аналітика" },
                    { 79, "Java" },
                    { 80, "C" },
                    { 81, "Swift" },
                    { 82, "Go language" },
                    { 83, "Rust" },
                    { 84, "Scala" },
                    { 85, "HTML" },
                    { 86, "XML" },
                    { 87, "CSS" },
                    { 88, "JavaScript" },
                    { 89, "NodeJS" },
                    { 90, "Python" },
                    { 91, "PHP" },
                    { 92, "Ruby" },
                    { 93, "Bash" },
                    { 94, "Розробка програм для iOS" },
                    { 95, "Розробка програм для Android" },
                    { 96, "Бази даних" },
                    { 97, "Алгоритми" },
                    { 98, "Маратхі" },
                    { 99, "йоруба" },
                    { 100, "амхарська" },
                    { 101, "маорі" },
                    { 102, "ігбо" },
                    { 103, "сингальська" },
                    { 104, "бірманська" },
                    { 105, "лаоська" },
                    { 106, "казахська" },
                    { 107, "С++" },
                    { 108, "тамазігхт" },
                    { 109, "С#" },
                    { 110, "валлійська" },
                    { 111, "каннада" },
                    { 112, "гуджараті" },
                    { 113, "давньогрецька" },
                    { 114, "мальтійська" },
                    { 115, "креольська" },
                    { 116, "їдиш" },
                    { 117, "боснійська" },
                    { 118, "естонська" },
                    { 119, "Луганда" },
                    { 120, "Себуано" },
                    { 121, "Баск" },
                    { 122, "Кічуа" },
                    { 123, "Корпоративні фінанси" },
                    { 124, "Географія" },
                    { 125, "Філософія" },
                    { 126, "Письмо" },
                    { 127, "Соціологія" },
                    { 128, "Психологія" },
                    { 129, "Соціальні та гуманітарні науки" },
                    { 130, "Графічний дизайн" },
                    { 131, "Дизайн руху" },
                    { 132, "Фотографія" },
                    { 133, "Малаялам" },
                    { 134, "R" },
                    { 135, "PPC" },
                    { 136, "Албанська" },
                    { 137, "Пушту" },
                    { 138, "Гавайська" },
                    { 139, "Есперанто" },
                    { 140, "Хоса" },
                    { 141, "Македонська" },
                    { 142, "Курдська" },
                    { 143, "Кіньяруанда" },
                    { 144, "Узбецький" },
                    { 145, "Геометрія" },
                    { 146, "Література" },
                    { 147, "3D-дизайн" },
                    { 148, "Відео-постпродакшн" },
                    { 149, "Тести" },
                    { 150, "Право" },
                    { 151, "Суахілі" },
                    { 152, "Африкаанс" },
                    { 153, "Малайська" },
                    { 154, "Сомалі" },
                    { 155, "Словенська" },
                    { 156, "Латиська" },
                    { 157, "Монгольська" },
                    { 158, "Люксембурзька" },
                    { 159, "Кечуа" },
                    { 160, "Азербайджанський" },
                    { 161, "Кантонський" },
                    { 162, "Індонезійський" },
                    { 163, "Продажі" },
                    { 164, "Бізнес-моделювання" },
                    { 165, "Управління продуктами" },
                    { 166, "Бізнес-стратегія" },
                    { 167, "Бізнес-аналітика" },
                    { 168, "Публічні виступи" },
                    { 169, "SEO" },
                    { 170, "SMM" },
                    { 171, "Копірайтинг " }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CityId",
                table: "AspNetUsers",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AvailableTimes_ProfileId",
                table: "AvailableTimes",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteTutors_ProfileId",
                table: "FavoriteTutors",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_OwnerId",
                table: "Files",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_SolutionId",
                table: "Files",
                column: "SolutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_TaskId",
                table: "Files",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_RequestId",
                table: "Lessons",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_SubjectId",
                table: "Lessons",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_TutorProfileId",
                table: "Lessons",
                column: "TutorProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileSubjects_SubjectsId",
                table: "ProfileSubjects",
                column: "SubjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_CreatedId",
                table: "Requests",
                column: "CreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_SubjectId",
                table: "Requests",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_TutorId",
                table: "Requests",
                column: "TutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Solutions_StudentId",
                table: "Solutions",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Solutions_TaskId",
                table: "Solutions",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentLessons_StudentsId",
                table: "StudentLessons",
                column: "StudentsId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TutorId",
                table: "Tasks",
                column: "TutorId");

            migrationBuilder.CreateIndex(
                name: "IX_TutorReviewModel_CreatedId",
                table: "TutorReviewModel",
                column: "CreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_TutorReviewModel_TutorId",
                table: "TutorReviewModel",
                column: "TutorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboutTutor");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AvailableTimes");

            migrationBuilder.DropTable(
                name: "FavoriteTutors");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "ProfileSubjects");

            migrationBuilder.DropTable(
                name: "StudentLessons");

            migrationBuilder.DropTable(
                name: "TutorReviewModel");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Solutions");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Tutor");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
