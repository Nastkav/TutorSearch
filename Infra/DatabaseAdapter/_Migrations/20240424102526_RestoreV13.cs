using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infra.DatabaseAdapter._Migrations;

/// <inheritdoc />
public partial class RestoreV13 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterDatabase()
            .Annotation("MySQL:Charset", "utf8mb4");

        migrationBuilder.CreateTable(
                "AspNetRoles",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>("varchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>("varchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>("longtext", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_AspNetRoles", x => x.Id); })
            .Annotation("MySQL:Charset", "utf8mb4");

        migrationBuilder.CreateTable(
                "Cities",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>("varchar(100)", maxLength: 100, nullable: false),
                    Region = table.Column<string>("varchar(140)", maxLength: 140, nullable: false),
                    CreatedAt = table.Column<DateTime>("datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>("datetime(6)", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Cities", x => x.Id); })
            .Annotation("MySQL:Charset", "utf8mb4");

        migrationBuilder.CreateTable(
                "Subjects",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>("varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Subjects", x => x.Id); })
            .Annotation("MySQL:Charset", "utf8mb4");

        migrationBuilder.CreateTable(
                "AspNetRoleClaims",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>("int", nullable: false),
                    ClaimType = table.Column<string>("longtext", nullable: true),
                    ClaimValue = table.Column<string>("longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        x => x.RoleId,
                        "AspNetRoles",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                })
            .Annotation("MySQL:Charset", "utf8mb4");

        migrationBuilder.CreateTable(
                "AspNetUsers",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>("varchar(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>("varchar(50)", maxLength: 50, nullable: false),
                    Patronymic = table.Column<string>("varchar(50)", maxLength: 50, nullable: false),
                    ProfileEnabled = table.Column<bool>("tinyint(1)", nullable: false),
                    CityId = table.Column<int>("int", nullable: true),
                    BirthDate = table.Column<DateTime>("datetime(6)", nullable: true),
                    UserName = table.Column<string>("varchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>("varchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>("varchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>("varchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>("tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>("longtext", nullable: true),
                    SecurityStamp = table.Column<string>("longtext", nullable: true),
                    ConcurrencyStamp = table.Column<string>("longtext", nullable: true),
                    PhoneNumber = table.Column<string>("longtext", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>("tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>("tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>("datetime", nullable: true),
                    LockoutEnabled = table.Column<bool>("tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>("int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        "FK_AspNetUsers_Cities_CityId",
                        x => x.CityId,
                        "Cities",
                        "Id");
                })
            .Annotation("MySQL:Charset", "utf8mb4");

        migrationBuilder.CreateTable(
                "AspNetUserClaims",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>("int", nullable: false),
                    ClaimType = table.Column<string>("longtext", nullable: true),
                    ClaimValue = table.Column<string>("longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        "FK_AspNetUserClaims_AspNetUsers_UserId",
                        x => x.UserId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                })
            .Annotation("MySQL:Charset", "utf8mb4");

        migrationBuilder.CreateTable(
                "AspNetUserLogins",
                table => new
                {
                    LoginProvider = table.Column<string>("varchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>("varchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>("longtext", nullable: true),
                    UserId = table.Column<int>("int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        "FK_AspNetUserLogins_AspNetUsers_UserId",
                        x => x.UserId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                })
            .Annotation("MySQL:Charset", "utf8mb4");

        migrationBuilder.CreateTable(
                "AspNetUserRoles",
                table => new
                {
                    UserId = table.Column<int>("int", nullable: false),
                    RoleId = table.Column<int>("int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        x => x.RoleId,
                        "AspNetRoles",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_AspNetUserRoles_AspNetUsers_UserId",
                        x => x.UserId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                })
            .Annotation("MySQL:Charset", "utf8mb4");

        migrationBuilder.CreateTable(
                "AspNetUserTokens",
                table => new
                {
                    UserId = table.Column<int>("int", nullable: false),
                    LoginProvider = table.Column<string>("varchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>("varchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>("longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        "FK_AspNetUserTokens_AspNetUsers_UserId",
                        x => x.UserId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                })
            .Annotation("MySQL:Charset", "utf8mb4");

        migrationBuilder.CreateTable(
                "Files",
                table => new
                {
                    Id = table.Column<Guid>("char(36)", nullable: false),
                    FileName = table.Column<string>("varchar(100)", maxLength: 100, nullable: false),
                    ServerName = table.Column<string>("varchar(350)", maxLength: 350, nullable: false),
                    OwnerId = table.Column<int>("int", nullable: false),
                    CreatedAt = table.Column<DateTime>("datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>("datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                    table.ForeignKey(
                        "FK_Files_AspNetUsers_OwnerId",
                        x => x.OwnerId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                })
            .Annotation("MySQL:Charset", "utf8mb4");

        migrationBuilder.CreateTable(
                "Tutors",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false),
                    Address = table.Column<string>("varchar(300)", maxLength: 300, nullable: false),
                    OnlineAccess = table.Column<bool>("tinyint(1)", nullable: false),
                    TutorHomeAccess = table.Column<bool>("tinyint(1)", nullable: false),
                    StudentHomeAccess = table.Column<bool>("tinyint(1)", nullable: false),
                    Descriptions = table.Column<string>("varchar(300)", maxLength: 300, nullable: false),
                    HourRate = table.Column<decimal>("decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>("datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>("datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tutors", x => x.Id);
                    table.ForeignKey(
                        "FK_Tutors_AspNetUsers_Id",
                        x => x.Id,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                })
            .Annotation("MySQL:Charset", "utf8mb4");

        migrationBuilder.CreateTable(
                "AboutTutor",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false),
                    Content = table.Column<string>("varchar(5000)", maxLength: 5000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutTutor", x => x.Id);
                    table.ForeignKey(
                        "FK_AboutTutor_Tutors_Id",
                        x => x.Id,
                        "Tutors",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                })
            .Annotation("MySQL:Charset", "utf8mb4");

        migrationBuilder.CreateTable(
                "Assignments",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    TutorId = table.Column<int>("int", nullable: false),
                    SubjectId = table.Column<int>("int", nullable: false),
                    Title = table.Column<string>("varchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>("varchar(500)", maxLength: 500, nullable: false),
                    Deadline = table.Column<DateTime>("datetime(6)", nullable: false),
                    CreatedAt = table.Column<DateTime>("datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>("datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignments", x => x.Id);
                    table.ForeignKey(
                        "FK_Assignments_Subjects_SubjectId",
                        x => x.SubjectId,
                        "Subjects",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Assignments_Tutors_TutorId",
                        x => x.TutorId,
                        "Tutors",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                })
            .Annotation("MySQL:Charset", "utf8mb4");

        migrationBuilder.CreateTable(
                "AvailableTimes",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    DayOfWeek = table.Column<int>("int", nullable: false),
                    StartTime = table.Column<TimeSpan>("time(6)", nullable: false),
                    EndTime = table.Column<TimeSpan>("time(6)", nullable: false),
                    ProfileId = table.Column<int>("int", nullable: false),
                    CreatedAt = table.Column<DateTime>("datetime(6)", nullable: false),
                    CreatedId = table.Column<int>("int", nullable: false),
                    UpdatedAt = table.Column<DateTime>("datetime(6)", nullable: true),
                    UpdatedId = table.Column<int>("int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailableTimes", x => x.Id);
                    table.ForeignKey(
                        "FK_AvailableTimes_Tutors_ProfileId",
                        x => x.ProfileId,
                        "Tutors",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                })
            .Annotation("MySQL:Charset", "utf8mb4");

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
                        "FK_FavoriteTutors_Tutors_ProfileId",
                        x => x.ProfileId,
                        "Tutors",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                })
            .Annotation("MySQL:Charset", "utf8mb4");

        migrationBuilder.CreateTable(
                "ProfileSubjects",
                table => new
                {
                    ProfilesId = table.Column<int>("int", nullable: false),
                    SubjectsId = table.Column<int>("int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileSubjects", x => new { x.ProfilesId, x.SubjectsId });
                    table.ForeignKey(
                        "FK_ProfileSubjects_Subjects_SubjectsId",
                        x => x.SubjectsId,
                        "Subjects",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_ProfileSubjects_Tutors_ProfilesId",
                        x => x.ProfilesId,
                        "Tutors",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                })
            .Annotation("MySQL:Charset", "utf8mb4");

        migrationBuilder.CreateTable(
                "Requests",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    TutorId = table.Column<int>("int", nullable: false),
                    Status = table.Column<int>("int", nullable: false),
                    From = table.Column<DateTime>("datetime(6)", nullable: false),
                    To = table.Column<DateTime>("datetime(6)", nullable: false),
                    Comment = table.Column<string>("varchar(300)", maxLength: 300, nullable: false),
                    TutorComment = table.Column<string>("varchar(300)", maxLength: 300, nullable: false),
                    SubjectId = table.Column<int>("int", nullable: false),
                    CreatedId = table.Column<int>("int", nullable: false),
                    CreatedAt = table.Column<DateTime>("datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>("datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        "FK_Requests_AspNetUsers_CreatedId",
                        x => x.CreatedId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Requests_Subjects_SubjectId",
                        x => x.SubjectId,
                        "Subjects",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Requests_Tutors_TutorId",
                        x => x.TutorId,
                        "Tutors",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                })
            .Annotation("MySQL:Charset", "utf8mb4");

        migrationBuilder.CreateTable(
                "Reviews",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    TutorId = table.Column<int>("int", nullable: false),
                    Rating = table.Column<int>("int", nullable: false),
                    Description = table.Column<string>("varchar(300)", maxLength: 300, nullable: false),
                    AuthorId = table.Column<int>("int", nullable: false),
                    CreatedAt = table.Column<DateTime>("datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>("datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        "FK_Reviews_AspNetUsers_AuthorId",
                        x => x.AuthorId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Reviews_Tutors_TutorId",
                        x => x.TutorId,
                        "Tutors",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                })
            .Annotation("MySQL:Charset", "utf8mb4");

        migrationBuilder.CreateTable(
                "AssignmentFiles",
                table => new
                {
                    AssignmentsId = table.Column<int>("int", nullable: false),
                    FilesId = table.Column<Guid>("char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentFiles", x => new { x.AssignmentsId, x.FilesId });
                    table.ForeignKey(
                        "FK_AssignmentFiles_Assignments_AssignmentsId",
                        x => x.AssignmentsId,
                        "Assignments",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_AssignmentFiles_Files_FilesId",
                        x => x.FilesId,
                        "Files",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                })
            .Annotation("MySQL:Charset", "utf8mb4");

        migrationBuilder.CreateTable(
                "Solutions",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    AssignmentId = table.Column<int>("int", nullable: false),
                    StudentId = table.Column<int>("int", nullable: false),
                    Status = table.Column<int>("int", nullable: false),
                    Answer = table.Column<string>("varchar(500)", maxLength: 500, nullable: false),
                    TutorComment = table.Column<string>("varchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>("datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>("datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solutions", x => x.Id);
                    table.ForeignKey(
                        "FK_Solutions_AspNetUsers_StudentId",
                        x => x.StudentId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Solutions_Assignments_AssignmentId",
                        x => x.AssignmentId,
                        "Assignments",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                })
            .Annotation("MySQL:Charset", "utf8mb4");

        migrationBuilder.CreateTable(
                "Lessons",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    TutorId = table.Column<int>("int", nullable: false),
                    From = table.Column<DateTime>("datetime(6)", nullable: false),
                    To = table.Column<DateTime>("datetime(6)", nullable: false),
                    Title = table.Column<string>("varchar(50)", maxLength: 50, nullable: false),
                    Comment = table.Column<string>("varchar(200)", maxLength: 200, nullable: false),
                    RequestId = table.Column<int>("int", nullable: true),
                    SubjectId = table.Column<int>("int", nullable: false),
                    CreatedAt = table.Column<DateTime>("datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>("datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.Id);
                    table.ForeignKey(
                        "FK_Lessons_Requests_RequestId",
                        x => x.RequestId,
                        "Requests",
                        "Id");
                    table.ForeignKey(
                        "FK_Lessons_Subjects_SubjectId",
                        x => x.SubjectId,
                        "Subjects",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Lessons_Tutors_TutorId",
                        x => x.TutorId,
                        "Tutors",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                })
            .Annotation("MySQL:Charset", "utf8mb4");

        migrationBuilder.CreateTable(
                "SolutiontFiles",
                table => new
                {
                    FilesId = table.Column<Guid>("char(36)", nullable: false),
                    SolutionsId = table.Column<int>("int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolutiontFiles", x => new { x.FilesId, x.SolutionsId });
                    table.ForeignKey(
                        "FK_SolutiontFiles_Files_FilesId",
                        x => x.FilesId,
                        "Files",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_SolutiontFiles_Solutions_SolutionsId",
                        x => x.SolutionsId,
                        "Solutions",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                })
            .Annotation("MySQL:Charset", "utf8mb4");

        migrationBuilder.CreateTable(
                "StudentLessons",
                table => new
                {
                    LessonsId = table.Column<int>("int", nullable: false),
                    StudentsId = table.Column<int>("int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentLessons", x => new { x.LessonsId, x.StudentsId });
                    table.ForeignKey(
                        "FK_StudentLessons_AspNetUsers_StudentsId",
                        x => x.StudentsId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_StudentLessons_Lessons_LessonsId",
                        x => x.LessonsId,
                        "Lessons",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                })
            .Annotation("MySQL:Charset", "utf8mb4");

        migrationBuilder.InsertData(
            "AspNetRoles",
            new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
            new object[,]
            {
                { 1, null, "Administrator", "ADMINISTRATOR" },
                { 2, null, "User", "USER" }
            });

        migrationBuilder.InsertData(
            "Cities",
            new[] { "Id", "CreatedAt", "Name", "Region", "UpdatedAt" },
            new object[,]
            {
                {
                    1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Авдіївка", "Донецька область",
                    null
                },
                {
                    2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Алмазна", "Луганська область",
                    null
                },
                {
                    3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Алупка",
                    "Автономна Республіка Крим", null
                },
                {
                    4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Алушта",
                    "Автономна Республіка Крим", null
                },
                {
                    5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Алчевськ", "Луганська область",
                    null
                },
                {
                    6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Амвросіївка",
                    "Донецька область", null
                },
                {
                    7, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ананьїв", "Одеська область",
                    null
                },
                {
                    8, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Андрушівка",
                    "Житомирська область", null
                },
                {
                    9, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Антрацит", "Луганська область",
                    null
                },
                {
                    10, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Апостолове",
                    "Дніпропетровська область", null
                },
                {
                    11, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Армянськ",
                    "Автономна Республіка Крим", null
                },
                {
                    12, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Арциз", "Одеська область", null
                },
                {
                    13, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Балаклія",
                    "Харківська область", null
                },
                {
                    14, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Балта", "Одеська область", null
                },
                {
                    15, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Бар", "Вінницька область", null
                },
                {
                    16, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Баранівка",
                    "Житомирська область", null
                },
                {
                    17, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Барвінкове",
                    "Харківська область", null
                },
                {
                    18, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Батурин",
                    "Чернігівська область", null
                },
                {
                    19, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Бахмач",
                    "Чернігівська область", null
                },
                {
                    20, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Бахмут", "Донецька область",
                    null
                },
                {
                    21, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Бахчисарай",
                    "Автономна Республіка Крим", null
                },
                {
                    22, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Баштанка",
                    "Миколаївська область", null
                },
                {
                    23, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Белз", "Львівська область",
                    null
                },
                {
                    24, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Бердичів",
                    "Житомирська область", null
                },
                {
                    25, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Бердянськ",
                    "Запорізька область", null
                },
                {
                    26, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Берегове",
                    "Закарпатська область", null
                },
                {
                    27, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Бережани",
                    "Тернопільська область", null
                },
                {
                    28, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Березань", "Київська область",
                    null
                },
                {
                    29, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Березівка", "Одеська область",
                    null
                },
                {
                    30, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Березне", "Рівненська область",
                    null
                },
                {
                    31, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Берестечко",
                    "Волинська область", null
                },
                {
                    32, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Берислав",
                    "Херсонська область", null
                },
                {
                    33, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Бершадь", "Вінницька область",
                    null
                },
                {
                    34, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Бібрка", "Львівська область",
                    null
                },
                {
                    35, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Біла Церква",
                    "Київська область", null
                },
                {
                    36, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Білгород-Дністровський",
                    "Одеська область", null
                },
                {
                    37, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Білицьке", "Донецька область",
                    null
                },
                {
                    38, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Білогірськ",
                    "Автономна Республіка Крим", null
                },
                {
                    39, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Білозерське",
                    "Донецька область", null
                },
                {
                    40, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Білопілля", "Сумська область",
                    null
                },
                {
                    41, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Біляївка", "Одеська область",
                    null
                },
                {
                    42, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Благовіщенське",
                    "Кіровоградська область", null
                },
                {
                    43, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Бобринець",
                    "Кіровоградська область", null
                },
                {
                    44, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Бобровиця",
                    "Чернігівська область", null
                },
                {
                    45, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Богодухів",
                    "Харківська область", null
                },
                {
                    46, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Богуслав", "Київська область",
                    null
                },
                {
                    47, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Боково-Хрустальне",
                    "Луганська область", null
                },
                {
                    48, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Болград", "Одеська область",
                    null
                },
                {
                    49, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Болехів",
                    "Івано-Франківська область", null
                },
                {
                    50, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Борзна",
                    "Чернігівська область", null
                },
                {
                    51, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Борислав", "Львівська область",
                    null
                },
                {
                    52, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Бориспіль", "Київська область",
                    null
                },
                {
                    53, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Борщів",
                    "Тернопільська область", null
                },
                {
                    54, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Боярка", "Київська область",
                    null
                },
                {
                    55, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Бровари", "Київська область",
                    null
                },
                {
                    56, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Броди", "Львівська область",
                    null
                },
                {
                    57, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Брянка", "Луганська область",
                    null
                },
                {
                    58, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Бунге", "Донецька область",
                    null
                },
                {
                    59, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Буринь", "Сумська область",
                    null
                },
                {
                    60, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Бурштин",
                    "Івано-Франківська область", null
                },
                {
                    61, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Буськ", "Львівська область",
                    null
                },
                {
                    62, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Буча", "Київська область", null
                },
                {
                    63, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Бучач",
                    "Тернопільська область", null
                },
                {
                    64, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Валки", "Харківська область",
                    null
                },
                {
                    65, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Вараш", "Рівненська область",
                    null
                },
                {
                    66, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Василівка",
                    "Запорізька область", null
                },
                {
                    67, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Васильків", "Київська область",
                    null
                },
                {
                    68, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ватутіне", "Черкаська область",
                    null
                },
                {
                    69, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Вашківці",
                    "Чернівецька область", null
                },
                {
                    70, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Великі Мости",
                    "Львівська область", null
                },
                {
                    71, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Верхівцеве",
                    "Дніпропетровська область", null
                },
                {
                    72, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Верхньодніпровськ",
                    "Дніпропетровська область", null
                },
                {
                    73, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Вижниця",
                    "Чернівецька область", null
                },
                {
                    74, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Вилкове", "Одеська область",
                    null
                },
                {
                    75, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Винники", "Львівська область",
                    null
                },
                {
                    76, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Виноградів",
                    "Закарпатська область", null
                },
                {
                    77, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Вишгород", "Київська область",
                    null
                },
                {
                    78, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Вишневе", "Київська область",
                    null
                },
                {
                    79, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Вільногірськ",
                    "Дніпропетровська область", null
                },
                {
                    80, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Вільнянськ",
                    "Запорізька область", null
                },
                {
                    81, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Вінниця", "Вінницька область",
                    null
                },
                {
                    82, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Вовчанськ",
                    "Харківська область", null
                },
                {
                    83, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Вознесенівка",
                    "Луганська область", null
                },
                {
                    84, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Вознесенськ",
                    "Миколаївська область", null
                },
                {
                    85, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Волноваха", "Донецька область",
                    null
                },
                {
                    86, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Володимир",
                    "Волинська область", null
                },
                {
                    87, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Волочиськ",
                    "Хмельницька область", null
                },
                {
                    88, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ворожба", "Сумська область",
                    null
                },
                {
                    89, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Вуглегірськ",
                    "Донецька область", null
                },
                {
                    90, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Вугледар", "Донецька область",
                    null
                },
                {
                    91, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Гадяч", "Полтавська область",
                    null
                },
                {
                    92, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Гайворон",
                    "Кіровоградська область", null
                },
                {
                    93, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Гайсин", "Вінницька область",
                    null
                },
                {
                    94, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Галич",
                    "Івано-Франківська область", null
                },
                {
                    95, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Генічеськ",
                    "Херсонська область", null
                },
                {
                    96, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Герца", "Чернівецька область",
                    null
                },
                {
                    97, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Гірник", "Донецька область",
                    null
                },
                {
                    98, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Гірське", "Луганська область",
                    null
                },
                {
                    99, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Глиняни", "Львівська область",
                    null
                },
                {
                    100, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Глобине",
                    "Полтавська область", null
                },
                {
                    101, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Глухів", "Сумська область",
                    null
                },
                {
                    102, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Гнівань", "Вінницька область",
                    null
                },
                {
                    103, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Гола Пристань",
                    "Херсонська область", null
                },
                {
                    104, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Голубівка",
                    "Луганська область", null
                },
                {
                    105, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Горішні Плавні",
                    "Полтавська область", null
                },
                {
                    106, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Горлівка", "Донецька область",
                    null
                },
                {
                    107, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Городенка",
                    "Івано-Франківська область", null
                },
                {
                    108, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Городище",
                    "Черкаська область", null
                },
                {
                    109, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Городня",
                    "Чернігівська область", null
                },
                {
                    110, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Городок", "Львівська область",
                    null
                },
                {
                    111, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Городок",
                    "Хмельницька область", null
                },
                {
                    112, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Горохів", "Волинська область",
                    null
                },
                {
                    113, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Гребінка",
                    "Полтавська область", null
                },
                {
                    114, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Гуляйполе",
                    "Запорізька область", null
                },
                {
                    115, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Дебальцеве",
                    "Донецька область", null
                },
                {
                    116, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Деражня",
                    "Хмельницька область", null
                },
                {
                    117, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Дергачі",
                    "Харківська область", null
                },
                {
                    118, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Джанкой",
                    "Автономна Республіка Крим", null
                },
                {
                    119, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Дніпро",
                    "Дніпропетровська область", null
                },
                {
                    120, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Дніпрорудне",
                    "Запорізька область", null
                },
                {
                    121, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Добромиль",
                    "Львівська область", null
                },
                {
                    122, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Добропілля",
                    "Донецька область", null
                },
                {
                    123, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Довжанськ",
                    "Луганська область", null
                },
                {
                    124, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Докучаєвськ",
                    "Донецька область", null
                },
                {
                    125, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Долина",
                    "Івано-Франківська область", null
                },
                {
                    126, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Долинська",
                    "Кіровоградська область", null
                },
                {
                    127, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Донецьк", "Донецька область",
                    null
                },
                {
                    128, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Дрогобич",
                    "Львівська область", null
                },
                {
                    129, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Дружба", "Сумська область",
                    null
                },
                {
                    130, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Дружківка",
                    "Донецька область", null
                },
                {
                    131, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Дубляни", "Львівська область",
                    null
                },
                {
                    132, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Дубно", "Рівненська область",
                    null
                },
                {
                    133, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Дубровиця",
                    "Рівненська область", null
                },
                {
                    134, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Дунаївці",
                    "Хмельницька область", null
                },
                {
                    135, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Енергодар",
                    "Запорізька область", null
                },
                {
                    136, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Євпаторія",
                    "Автономна Республіка Крим", null
                },
                {
                    137, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Єнакієве", "Донецька область",
                    null
                },
                {
                    138, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Жашків", "Черкаська область",
                    null
                },
                {
                    139, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Жданівка", "Донецька область",
                    null
                },
                {
                    140, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Жидачів", "Львівська область",
                    null
                },
                {
                    141, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Житомир",
                    "Житомирська область", null
                },
                {
                    142, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Жмеринка",
                    "Вінницька область", null
                },
                {
                    143, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Жовква", "Львівська область",
                    null
                },
                {
                    144, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Жовті Води",
                    "Дніпропетровська область", null
                },
                {
                    145, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Заводське",
                    "Полтавська область", null
                },
                {
                    146, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Залізне", "Донецька область",
                    null
                },
                {
                    147, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Заліщики",
                    "Тернопільська область", null
                },
                {
                    148, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Запоріжжя",
                    "Запорізька область", null
                },
                {
                    149, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Заставна",
                    "Чернівецька область", null
                },
                {
                    150, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Збараж",
                    "Тернопільська область", null
                },
                {
                    151, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Зборів",
                    "Тернопільська область", null
                },
                {
                    152, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Звенигородка",
                    "Черкаська область", null
                },
                {
                    153, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Звягель",
                    "Житомирська область", null
                },
                {
                    154, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Здолбунів",
                    "Рівненська область", null
                },
                {
                    155, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Зеленодольськ",
                    "Дніпропетровська область", null
                },
                {
                    156, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Зимогір'я",
                    "Луганська область", null
                },
                {
                    157, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Зіньків",
                    "Полтавська область", null
                },
                {
                    158, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Зміїв", "Харківська область",
                    null
                },
                {
                    159, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Знам'янка",
                    "Кіровоградська область", null
                },
                {
                    160, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Золоте", "Луганська область",
                    null
                },
                {
                    161, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Золотоноша",
                    "Черкаська область", null
                },
                {
                    162, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Золочів", "Львівська область",
                    null
                },
                {
                    163, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Зоринськ",
                    "Луганська область", null
                },
                {
                    164, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Зугрес", "Донецька область",
                    null
                },
                {
                    165, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Івано-Франківськ",
                    "Івано-Франківська область", null
                },
                {
                    166, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ізмаїл", "Одеська область",
                    null
                },
                {
                    167, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ізюм", "Харківська область",
                    null
                },
                {
                    168, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ізяслав",
                    "Хмельницька область", null
                },
                {
                    169, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Іллінці", "Вінницька область",
                    null
                },
                {
                    170, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Іловайськ",
                    "Донецька область", null
                },
                {
                    171, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Інкерман",
                    "Автономна Республіка Крим", null
                },
                {
                    172, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ірміно", "Луганська область",
                    null
                },
                {
                    173, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ірпінь", "Київська область",
                    null
                },
                {
                    174, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Іршава",
                    "Закарпатська область", null
                },
                {
                    175, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ічня", "Чернігівська область",
                    null
                },
                {
                    176, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Кагарлик", "Київська область",
                    null
                },
                {
                    177, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Кадіївка",
                    "Луганська область", null
                },
                {
                    178, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Калинівка",
                    "Вінницька область", null
                },
                {
                    179, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Калуш",
                    "Івано-Франківська область", null
                },
                {
                    180, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Кальміуське",
                    "Донецька область", null
                },
                {
                    181, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Камінь-Каширський",
                    "Волинська область", null
                },
                {
                    182, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Кам'янець-Подільський",
                    "Хмельницька область", null
                },
                {
                    183, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Кам'янка",
                    "Черкаська область", null
                },
                {
                    184, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Кам'янка-Бузька",
                    "Львівська область", null
                },
                {
                    185, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Кам'янка-Дніпровська",
                    "Запорізька область", null
                },
                {
                    186, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Кам'янське",
                    "Дніпропетровська область", null
                },
                {
                    187, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Канів", "Черкаська область",
                    null
                },
                {
                    188, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Карлівка",
                    "Полтавська область", null
                },
                {
                    189, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Каховка",
                    "Херсонська область", null
                },
                {
                    190, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Керч",
                    "Автономна Республіка Крим", null
                },
                { 191, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Київ", "Київ", null },
                {
                    192, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Кипуче", "Луганська область",
                    null
                },
                {
                    193, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ківерці", "Волинська область",
                    null
                },
                {
                    194, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Кілія", "Одеська область",
                    null
                },
                {
                    195, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Кіцмань",
                    "Чернівецька область", null
                },
                {
                    196, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Кобеляки",
                    "Полтавська область", null
                },
                {
                    197, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ковель", "Волинська область",
                    null
                },
                {
                    198, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Кодима", "Одеська область",
                    null
                },
                {
                    199, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Козятин", "Вінницька область",
                    null
                },
                {
                    200, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Коломия",
                    "Івано-Франківська область", null
                },
                {
                    201, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Комарно", "Львівська область",
                    null
                },
                {
                    202, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Конотоп", "Сумська область",
                    null
                },
                {
                    203, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Копичинці",
                    "Тернопільська область", null
                },
                {
                    204, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Корець", "Рівненська область",
                    null
                },
                {
                    205, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Коростень",
                    "Житомирська область", null
                },
                {
                    206, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Коростишів",
                    "Житомирська область", null
                },
                {
                    207, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Корсунь-Шевченківський",
                    "Черкаська область", null
                },
                {
                    208, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Корюківка",
                    "Чернігівська область", null
                },
                {
                    209, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Косів",
                    "Івано-Франківська область", null
                },
                {
                    210, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Костопіль",
                    "Рівненська область", null
                },
                {
                    211, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Костянтинівка",
                    "Донецька область", null
                },
                {
                    212, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Краматорськ",
                    "Донецька область", null
                },
                {
                    213, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Красилів",
                    "Хмельницька область", null
                },
                {
                    214, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Красногорівка",
                    "Донецька область", null
                },
                {
                    215, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Красноград",
                    "Харківська область", null
                },
                {
                    216, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Кременець",
                    "Тернопільська область", null
                },
                {
                    217, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Кременчук",
                    "Полтавська область", null
                },
                {
                    218, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Кремінна",
                    "Луганська область", null
                },
                {
                    219, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Кривий Ріг",
                    "Дніпропетровська область", null
                },
                {
                    220, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Кролевець", "Сумська область",
                    null
                },
                {
                    221, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Кропивницький",
                    "Кіровоградська область", null
                },
                {
                    222, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Куп'янськ",
                    "Харківська область", null
                },
                {
                    223, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Курахове", "Донецька область",
                    null
                },
                {
                    224, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ладижин", "Вінницька область",
                    null
                },
                {
                    225, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ланівці",
                    "Тернопільська область", null
                },
                {
                    226, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Лебедин", "Сумська область",
                    null
                },
                {
                    227, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Лиман", "Донецька область",
                    null
                },
                {
                    228, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Липовець",
                    "Вінницька область", null
                },
                {
                    229, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Лисичанськ",
                    "Луганська область", null
                },
                {
                    230, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Лозова", "Харківська область",
                    null
                },
                {
                    231, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Лохвиця",
                    "Полтавська область", null
                },
                {
                    232, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Лубни", "Полтавська область",
                    null
                },
                {
                    233, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Луганськ",
                    "Луганська область", null
                },
                {
                    234, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Лутугине",
                    "Луганська область", null
                },
                {
                    235, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Луцьк", "Волинська область",
                    null
                },
                {
                    236, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Львів", "Львівська область",
                    null
                },
                {
                    237, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Любомль", "Волинська область",
                    null
                },
                {
                    238, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Люботин",
                    "Харківська область", null
                },
                {
                    239, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Макіївка", "Донецька область",
                    null
                },
                {
                    240, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Мала Виска",
                    "Кіровоградська область", null
                },
                {
                    241, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Малин", "Житомирська область",
                    null
                },
                {
                    242, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Марганець",
                    "Дніпропетровська область", null
                },
                {
                    243, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Маріуполь",
                    "Донецька область", null
                },
                {
                    244, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Мар'їнка", "Донецька область",
                    null
                },
                {
                    245, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Мелітополь",
                    "Запорізька область", null
                },
                {
                    246, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Мена", "Чернігівська область",
                    null
                },
                {
                    247, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Мерефа", "Харківська область",
                    null
                },
                {
                    248, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Миколаїв",
                    "Львівська область", null
                },
                {
                    249, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Миколаїв",
                    "Миколаївська область", null
                },
                {
                    250, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Миколаївка",
                    "Донецька область", null
                },
                {
                    251, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Миргород",
                    "Полтавська область", null
                },
                {
                    252, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Мирноград",
                    "Донецька область", null
                },
                {
                    253, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Миронівка",
                    "Київська область", null
                },
                {
                    254, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Міусинськ",
                    "Луганська область", null
                },
                {
                    255, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Могилів-Подільський",
                    "Вінницька область", null
                },
                {
                    256, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Молодогвардійськ",
                    "Луганська область", null
                },
                {
                    257, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Молочанськ",
                    "Запорізька область", null
                },
                {
                    258, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Монастириська",
                    "Тернопільська область", null
                },
                {
                    259, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Монастирище",
                    "Черкаська область", null
                },
                {
                    260, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Моршин", "Львівська область",
                    null
                },
                {
                    261, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Моспине", "Донецька область",
                    null
                },
                {
                    262, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Мостиська",
                    "Львівська область", null
                },
                {
                    263, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Мукачево",
                    "Закарпатська область", null
                },
                {
                    264, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Надвірна",
                    "Івано-Франківська область", null
                },
                {
                    265, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Немирів", "Вінницька область",
                    null
                },
                {
                    266, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Нетішин",
                    "Хмельницька область", null
                },
                {
                    267, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ніжин",
                    "Чернігівська область", null
                },
                {
                    268, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Нікополь",
                    "Дніпропетровська область", null
                },
                {
                    269, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Нова Каховка",
                    "Херсонська область", null
                },
                {
                    270, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Нова Одеса",
                    "Миколаївська область", null
                },
                {
                    271, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Новгород-Сіверський",
                    "Чернігівська область", null
                },
                {
                    272, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Новий Буг",
                    "Миколаївська область", null
                },
                {
                    273, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Новий Калинів",
                    "Львівська область", null
                },
                {
                    274, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Новий Розділ",
                    "Львівська область", null
                },
                {
                    275, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Новоазовськ",
                    "Донецька область", null
                },
                {
                    276, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Нововолинськ",
                    "Волинська область", null
                },
                {
                    277, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Новогродівка",
                    "Донецька область", null
                },
                {
                    278, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Новодністровськ",
                    "Чернівецька область", null
                },
                {
                    279, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Новодружеськ",
                    "Луганська область", null
                },
                {
                    280, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Новомиргород",
                    "Кіровоградська область", null
                },
                {
                    281, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Новомосковськ",
                    "Дніпропетровська область", null
                },
                {
                    282, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Новоселиця",
                    "Чернівецька область", null
                },
                {
                    283, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Новоукраїнка",
                    "Кіровоградська область", null
                },
                {
                    284, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Новояворівськ",
                    "Львівська область", null
                },
                {
                    285, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Носівка",
                    "Чернігівська область", null
                },
                {
                    286, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Обухів", "Київська область",
                    null
                },
                {
                    287, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Овруч", "Житомирська область",
                    null
                },
                {
                    288, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Одеса", "Одеська область",
                    null
                },
                {
                    289, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Олевськ",
                    "Житомирська область", null
                },
                {
                    290, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Олександрівськ",
                    "Луганська область", null
                },
                {
                    291, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Олександрія",
                    "Кіровоградська область", null
                },
                {
                    292, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Олешки", "Херсонська область",
                    null
                },
                {
                    293, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Оріхів", "Запорізька область",
                    null
                },
                {
                    294, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Остер",
                    "Чернігівська область", null
                },
                {
                    295, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Острог", "Рівненська область",
                    null
                },
                {
                    296, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Охтирка", "Сумська область",
                    null
                },
                {
                    297, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Очаків",
                    "Миколаївська область", null
                },
                {
                    298, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Павлоград",
                    "Дніпропетровська область", null
                },
                {
                    299, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Первомайськ",
                    "Луганська область", null
                },
                {
                    300, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Первомайськ",
                    "Миколаївська область", null
                },
                {
                    301, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Первомайський",
                    "Харківська область", null
                },
                {
                    302, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Перевальськ",
                    "Луганська область", null
                },
                {
                    303, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Перемишляни",
                    "Львівська область", null
                },
                {
                    304, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Перечин",
                    "Закарпатська область", null
                },
                {
                    305, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Перещепине",
                    "Дніпропетровська область", null
                },
                {
                    306, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Переяслав",
                    "Київська область", null
                },
                {
                    307, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Першотравенськ",
                    "Дніпропетровська область", null
                },
                {
                    308, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Петрово-Красносілля",
                    "Луганська область", null
                },
                {
                    309, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Пирятин",
                    "Полтавська область", null
                },
                {
                    310, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Південне",
                    "Харківська область", null
                },
                {
                    311, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Підгайці",
                    "Тернопільська область", null
                },
                {
                    312, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Підгородне",
                    "Дніпропетровська область", null
                },
                {
                    313, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Погребище",
                    "Вінницька область", null
                },
                {
                    314, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Подільськ", "Одеська область",
                    null
                },
                {
                    315, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Покров",
                    "Дніпропетровська область", null
                },
                {
                    316, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Покровськ",
                    "Донецька область", null
                },
                {
                    317, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Пологи", "Запорізька область",
                    null
                },
                {
                    318, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Полонне",
                    "Хмельницька область", null
                },
                {
                    319, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Полтава",
                    "Полтавська область", null
                },
                {
                    320, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Помічна",
                    "Кіровоградська область", null
                },
                {
                    321, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Попасна", "Луганська область",
                    null
                },
                {
                    322, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Почаїв",
                    "Тернопільська область", null
                },
                {
                    323, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Привілля",
                    "Луганська область", null
                },
                {
                    324, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Прилуки",
                    "Чернігівська область", null
                },
                {
                    325, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Приморськ",
                    "Запорізька область", null
                },
                {
                    326, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Прип'ять", "Київська область",
                    null
                },
                {
                    327, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Пустомити",
                    "Львівська область", null
                },
                {
                    328, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Путивль", "Сумська область",
                    null
                },
                {
                    329, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "П'ятихатки",
                    "Дніпропетровська область", null
                },
                {
                    330, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Рава-Руська",
                    "Львівська область", null
                },
                {
                    331, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Радехів", "Львівська область",
                    null
                },
                {
                    332, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Радивилів",
                    "Рівненська область", null
                },
                {
                    333, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Радомишль",
                    "Житомирська область", null
                },
                {
                    334, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Рахів",
                    "Закарпатська область", null
                },
                {
                    335, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Рені", "Одеська область", null
                },
                {
                    336, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Решетилівка",
                    "Полтавська область", null
                },
                {
                    337, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ржищів", "Київська область",
                    null
                },
                {
                    338, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Рівне", "Рівненська область",
                    null
                },
                {
                    339, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ровеньки",
                    "Луганська область", null
                },
                {
                    340, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Рогатин",
                    "Івано-Франківська область", null
                },
                {
                    341, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Родинське",
                    "Донецька область", null
                },
                {
                    342, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Рожище", "Волинська область",
                    null
                },
                {
                    343, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Роздільна", "Одеська область",
                    null
                },
                {
                    344, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ромни", "Сумська область",
                    null
                },
                {
                    345, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Рубіжне", "Луганська область",
                    null
                },
                {
                    346, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Рудки", "Львівська область",
                    null
                },
                {
                    347, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Саки",
                    "Автономна Республіка Крим", null
                },
                {
                    348, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Самбір", "Львівська область",
                    null
                },
                {
                    349, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Сарни", "Рівненська область",
                    null
                },
                {
                    350, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Свалява",
                    "Закарпатська область", null
                },
                {
                    351, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Сватове", "Луганська область",
                    null
                },
                {
                    352, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Світловодськ",
                    "Кіровоградська область", null
                },
                {
                    353, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Світлодарськ",
                    "Донецька область", null
                },
                {
                    354, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Святогірськ",
                    "Донецька область", null
                },
                {
                    355, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Севастополь", "Севастополь",
                    null
                },
                {
                    356, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Селидове", "Донецька область",
                    null
                },
                {
                    357, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Семенівка",
                    "Чернігівська область", null
                },
                {
                    358, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Середина-Буда",
                    "Сумська область", null
                },
                {
                    359, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Сєвєродонецьк",
                    "Луганська область", null
                },
                {
                    360, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Синельникове",
                    "Дніпропетровська область", null
                },
                {
                    361, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Сіверськ", "Донецька область",
                    null
                },
                {
                    362, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Сімферополь",
                    "Автономна Республіка Крим", null
                },
                {
                    363, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Скадовськ",
                    "Херсонська область", null
                },
                {
                    364, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Скалат",
                    "Тернопільська область", null
                },
                {
                    365, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Сквира", "Київська область",
                    null
                },
                {
                    366, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Сколе", "Львівська область",
                    null
                },
                {
                    367, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Славута",
                    "Хмельницька область", null
                },
                {
                    368, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Славутич", "Київська область",
                    null
                },
                {
                    369, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Слов'янськ",
                    "Донецька область", null
                },
                {
                    370, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Сміла", "Черкаська область",
                    null
                },
                {
                    371, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Снігурівка",
                    "Миколаївська область", null
                },
                {
                    372, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Сніжне", "Донецька область",
                    null
                },
                {
                    373, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Сновськ",
                    "Чернігівська область", null
                },
                {
                    374, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Снятин",
                    "Івано-Франківська область", null
                },
                {
                    375, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Сокаль", "Львівська область",
                    null
                },
                {
                    376, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Сокиряни",
                    "Чернівецька область", null
                },
                {
                    377, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Соледар", "Донецька область",
                    null
                },
                {
                    378, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Сорокине",
                    "Луганська область", null
                },
                {
                    379, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Соснівка",
                    "Львівська область", null
                },
                {
                    380, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Старий Крим",
                    "Автономна Республіка Крим", null
                },
                {
                    381, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Старий Самбір",
                    "Львівська область", null
                },
                {
                    382, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Старобільськ",
                    "Луганська область", null
                },
                {
                    383, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Старокостянтинів",
                    "Хмельницька область", null
                },
                {
                    384, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Стебник", "Львівська область",
                    null
                },
                {
                    385, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Сторожинець",
                    "Чернівецька область", null
                },
                {
                    386, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Стрий", "Львівська область",
                    null
                },
                {
                    387, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Судак",
                    "Автономна Республіка Крим", null
                },
                {
                    388, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Судова Вишня",
                    "Львівська область", null
                },
                {
                    389, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Суми", "Сумська область", null
                },
                {
                    390, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Суходільськ",
                    "Луганська область", null
                },
                {
                    391, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Таврійськ",
                    "Херсонська область", null
                },
                {
                    392, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Тальне", "Черкаська область",
                    null
                },
                {
                    393, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Тараща", "Київська область",
                    null
                },
                {
                    394, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Татарбунари",
                    "Одеська область", null
                },
                {
                    395, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Теплодар", "Одеська область",
                    null
                },
                {
                    396, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Теребовля",
                    "Тернопільська область", null
                },
                {
                    397, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Тернівка",
                    "Дніпропетровська область", null
                },
                {
                    398, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Тернопіль",
                    "Тернопільська область", null
                },
                {
                    399, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Тетіїв", "Київська область",
                    null
                },
                {
                    400, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Тисмениця",
                    "Івано-Франківська область", null
                },
                {
                    401, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Тлумач",
                    "Івано-Франківська область", null
                },
                {
                    402, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Токмак", "Запорізька область",
                    null
                },
                {
                    403, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Торецьк", "Донецька область",
                    null
                },
                {
                    404, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Тростянець",
                    "Сумська область", null
                },
                {
                    405, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Трускавець",
                    "Львівська область", null
                },
                {
                    406, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Тульчин", "Вінницька область",
                    null
                },
                {
                    407, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Турка", "Львівська область",
                    null
                },
                {
                    408, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Тячів",
                    "Закарпатська область", null
                },
                {
                    409, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Угнів", "Львівська область",
                    null
                },
                {
                    410, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ужгород",
                    "Закарпатська область", null
                },
                {
                    411, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Узин", "Київська область",
                    null
                },
                {
                    412, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Українка", "Київська область",
                    null
                },
                {
                    413, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Українськ",
                    "Донецька область", null
                },
                {
                    414, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Умань", "Черкаська область",
                    null
                },
                {
                    415, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Устилуг", "Волинська область",
                    null
                },
                {
                    416, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Фастів", "Київська область",
                    null
                },
                {
                    417, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Феодосія",
                    "Автономна Республіка Крим", null
                },
                {
                    418, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Харків", "Харківська область",
                    null
                },
                {
                    419, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Харцизьк", "Донецька область",
                    null
                },
                {
                    420, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Херсон", "Херсонська область",
                    null
                },
                {
                    421, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Хирів", "Львівська область",
                    null
                },
                {
                    422, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Хмельницький",
                    "Хмельницька область", null
                },
                {
                    423, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Хмільник",
                    "Вінницька область", null
                },
                {
                    424, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ходорів", "Львівська область",
                    null
                },
                {
                    425, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Хорол", "Полтавська область",
                    null
                },
                {
                    426, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Хоростків",
                    "Тернопільська область", null
                },
                {
                    427, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Хотин", "Чернівецька область",
                    null
                },
                {
                    428, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Хрестівка",
                    "Донецька область", null
                },
                {
                    429, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Христинівка",
                    "Черкаська область", null
                },
                {
                    430, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Хрустальний",
                    "Луганська область", null
                },
                {
                    431, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Хуст", "Закарпатська область",
                    null
                },
                {
                    432, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Часів Яр", "Донецька область",
                    null
                },
                {
                    433, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Червоноград",
                    "Львівська область", null
                },
                {
                    434, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Черкаси", "Черкаська область",
                    null
                },
                {
                    435, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Чернівці",
                    "Чернівецька область", null
                },
                {
                    436, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Чернігів",
                    "Чернігівська область", null
                },
                {
                    437, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Чигирин", "Черкаська область",
                    null
                },
                {
                    438, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Чистякове",
                    "Донецька область", null
                },
                {
                    439, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Чоп", "Закарпатська область",
                    null
                },
                {
                    440, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Чорнобиль",
                    "Київська область", null
                },
                {
                    441, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Чорноморськ",
                    "Одеська область", null
                },
                {
                    442, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Чортків",
                    "Тернопільська область", null
                },
                {
                    443, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Чугуїв", "Харківська область",
                    null
                },
                {
                    444, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Чуднів",
                    "Житомирська область", null
                },
                {
                    445, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Шаргород",
                    "Вінницька область", null
                },
                {
                    446, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Шахтарськ",
                    "Донецька область", null
                },
                {
                    447, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Шепетівка",
                    "Хмельницька область", null
                },
                {
                    448, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Шостка", "Сумська область",
                    null
                },
                {
                    449, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Шпола", "Черкаська область",
                    null
                },
                {
                    450, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Шумськ",
                    "Тернопільська область", null
                },
                {
                    451, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Щастя", "Луганська область",
                    null
                },
                {
                    452, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Щолкіне",
                    "Автономна Республіка Крим", null
                },
                {
                    453, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Южне", "Одеська область", null
                },
                {
                    454, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Южноукраїнськ",
                    "Миколаївська область", null
                },
                {
                    455, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Яворів", "Львівська область",
                    null
                },
                {
                    456, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Яготин", "Київська область",
                    null
                },
                {
                    457, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ялта",
                    "Автономна Республіка Крим", null
                },
                {
                    458, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ямпіль", "Вінницька область",
                    null
                },
                {
                    459, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Яремче",
                    "Івано-Франківська область", null
                },
                {
                    460, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Яни Капу",
                    "Автономна Республіка Крим", null
                },
                {
                    461, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ясинувата",
                    "Донецька область", null
                }
            });

        migrationBuilder.InsertData(
            "Subjects",
            new[] { "Id", "Name" },
            new object[,]
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

        migrationBuilder.CreateIndex(
            "IX_AspNetRoleClaims_RoleId",
            "AspNetRoleClaims",
            "RoleId");

        migrationBuilder.CreateIndex(
            "RoleNameIndex",
            "AspNetRoles",
            "NormalizedName",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_AspNetUserClaims_UserId",
            "AspNetUserClaims",
            "UserId");

        migrationBuilder.CreateIndex(
            "IX_AspNetUserLogins_UserId",
            "AspNetUserLogins",
            "UserId");

        migrationBuilder.CreateIndex(
            "IX_AspNetUserRoles_RoleId",
            "AspNetUserRoles",
            "RoleId");

        migrationBuilder.CreateIndex(
            "EmailIndex",
            "AspNetUsers",
            "NormalizedEmail");

        migrationBuilder.CreateIndex(
            "IX_AspNetUsers_CityId",
            "AspNetUsers",
            "CityId");

        migrationBuilder.CreateIndex(
            "UserNameIndex",
            "AspNetUsers",
            "NormalizedUserName",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_AssignmentFiles_FilesId",
            "AssignmentFiles",
            "FilesId");

        migrationBuilder.CreateIndex(
            "IX_Assignments_SubjectId",
            "Assignments",
            "SubjectId");

        migrationBuilder.CreateIndex(
            "IX_Assignments_TutorId",
            "Assignments",
            "TutorId");

        migrationBuilder.CreateIndex(
            "IX_AvailableTimes_ProfileId",
            "AvailableTimes",
            "ProfileId");

        migrationBuilder.CreateIndex(
            "IX_FavoriteTutors_ProfileId",
            "FavoriteTutors",
            "ProfileId");

        migrationBuilder.CreateIndex(
            "IX_Files_OwnerId",
            "Files",
            "OwnerId");

        migrationBuilder.CreateIndex(
            "IX_Lessons_RequestId",
            "Lessons",
            "RequestId");

        migrationBuilder.CreateIndex(
            "IX_Lessons_SubjectId",
            "Lessons",
            "SubjectId");

        migrationBuilder.CreateIndex(
            "IX_Lessons_TutorId",
            "Lessons",
            "TutorId");

        migrationBuilder.CreateIndex(
            "IX_ProfileSubjects_SubjectsId",
            "ProfileSubjects",
            "SubjectsId");

        migrationBuilder.CreateIndex(
            "IX_Requests_CreatedId",
            "Requests",
            "CreatedId");

        migrationBuilder.CreateIndex(
            "IX_Requests_SubjectId",
            "Requests",
            "SubjectId");

        migrationBuilder.CreateIndex(
            "IX_Requests_TutorId",
            "Requests",
            "TutorId");

        migrationBuilder.CreateIndex(
            "IX_Reviews_AuthorId",
            "Reviews",
            "AuthorId");

        migrationBuilder.CreateIndex(
            "IX_Reviews_TutorId",
            "Reviews",
            "TutorId");

        migrationBuilder.CreateIndex(
            "IX_Solutions_AssignmentId",
            "Solutions",
            "AssignmentId");

        migrationBuilder.CreateIndex(
            "IX_Solutions_StudentId",
            "Solutions",
            "StudentId");

        migrationBuilder.CreateIndex(
            "IX_SolutiontFiles_SolutionsId",
            "SolutiontFiles",
            "SolutionsId");

        migrationBuilder.CreateIndex(
            "IX_StudentLessons_StudentsId",
            "StudentLessons",
            "StudentsId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "AboutTutor");

        migrationBuilder.DropTable(
            "AspNetRoleClaims");

        migrationBuilder.DropTable(
            "AspNetUserClaims");

        migrationBuilder.DropTable(
            "AspNetUserLogins");

        migrationBuilder.DropTable(
            "AspNetUserRoles");

        migrationBuilder.DropTable(
            "AspNetUserTokens");

        migrationBuilder.DropTable(
            "AssignmentFiles");

        migrationBuilder.DropTable(
            "AvailableTimes");

        migrationBuilder.DropTable(
            "FavoriteTutors");

        migrationBuilder.DropTable(
            "ProfileSubjects");

        migrationBuilder.DropTable(
            "Reviews");

        migrationBuilder.DropTable(
            "SolutiontFiles");

        migrationBuilder.DropTable(
            "StudentLessons");

        migrationBuilder.DropTable(
            "AspNetRoles");

        migrationBuilder.DropTable(
            "Files");

        migrationBuilder.DropTable(
            "Solutions");

        migrationBuilder.DropTable(
            "Lessons");

        migrationBuilder.DropTable(
            "Assignments");

        migrationBuilder.DropTable(
            "Requests");

        migrationBuilder.DropTable(
            "Subjects");

        migrationBuilder.DropTable(
            "Tutors");

        migrationBuilder.DropTable(
            "AspNetUsers");

        migrationBuilder.DropTable(
            "Cities");
    }
}