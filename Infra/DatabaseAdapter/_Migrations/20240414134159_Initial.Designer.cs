﻿// <auto-generated />
using System;
using Infra.DatabaseAdapter;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infra.DatabaseAdapter._Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240414134159_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Infra.DatabaseAdapter.Models.AboutTutor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(5000)
                        .HasColumnType("varchar(5000)");

                    b.Property<int>("ProfileId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId")
                        .IsUnique();

                    b.ToTable("AboutTutor");
                });

            modelBuilder.Entity("Infra.DatabaseAdapter.Models.Attendance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("LessonId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LessonId");

                    b.HasIndex("StudentId");

                    b.ToTable("Attendance");
                });

            modelBuilder.Entity("Infra.DatabaseAdapter.Models.AvailableTime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("ProfileId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId");

                    b.ToTable("AvailableTime");
                });

            modelBuilder.Entity("Infra.DatabaseAdapter.Models.CityModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Infra.DatabaseAdapter.Models.CourseModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("RequestId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.Property<int>("TutorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RequestId")
                        .IsUnique();

                    b.HasIndex("SubjectId");

                    b.HasIndex("TutorId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Infra.DatabaseAdapter.Models.CourseReview", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("CreatedBy");

                    b.ToTable("CourseReview");
                });

            modelBuilder.Entity("Infra.DatabaseAdapter.Models.FavoriteTutor", b =>
                {
                    b.Property<int>("UserModelId")
                        .HasColumnType("int");

                    b.Property<int>("TutorProfileId")
                        .HasColumnType("int");

                    b.HasKey("UserModelId", "TutorProfileId");

                    b.HasIndex("TutorProfileId");

                    b.ToTable("FavoriteTutor");
                });

            modelBuilder.Entity("Infra.DatabaseAdapter.Models.FileModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<int?>("SolutionId")
                        .HasColumnType("int");

                    b.Property<int?>("TaskId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("SolutionId");

                    b.HasIndex("TaskId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("Infra.DatabaseAdapter.Models.LessonModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<int>("TutorProfileId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("TutorProfileId");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("Infra.DatabaseAdapter.Models.RequestModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("TutorId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("TutorId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("Infra.DatabaseAdapter.Models.SolutionModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<int?>("TaskModelId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("TaskModelId");

                    b.ToTable("Solutions");
                });

            modelBuilder.Entity("Infra.DatabaseAdapter.Models.SubjectModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("Infra.DatabaseAdapter.Models.TaskModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("Infra.DatabaseAdapter.Models.TutorProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.ToTable("TutorProfiles");
                });

            modelBuilder.Entity("Infra.DatabaseAdapter.Models.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<Guid?>("CourseModelId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("Patronymic")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<string>("Surname")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("CourseModelId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("LessonModelUserModel", b =>
                {
                    b.Property<int>("LessonsId")
                        .HasColumnType("int");

                    b.Property<int>("StudentsId")
                        .HasColumnType("int");

                    b.HasKey("LessonsId", "StudentsId");

                    b.HasIndex("StudentsId");

                    b.ToTable("LessonModelUserModel");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("SubjectModelTutorProfile", b =>
                {
                    b.Property<int>("ProfilesId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectsId")
                        .HasColumnType("int");

                    b.HasKey("ProfilesId", "SubjectsId");

                    b.HasIndex("SubjectsId");

                    b.ToTable("SubjectModelTutorProfile");
                });

            modelBuilder.Entity("Infra.DatabaseAdapter.Models.AboutTutor", b =>
                {
                    b.HasOne("Infra.DatabaseAdapter.Models.TutorProfile", "Profile")
                        .WithOne("About")
                        .HasForeignKey("Infra.DatabaseAdapter.Models.AboutTutor", "ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("Infra.DatabaseAdapter.Models.Attendance", b =>
                {
                    b.HasOne("Infra.DatabaseAdapter.Models.LessonModel", "Lesson")
                        .WithMany("Attendances")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infra.DatabaseAdapter.Models.UserModel", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lesson");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Infra.DatabaseAdapter.Models.AvailableTime", b =>
                {
                    b.HasOne("Infra.DatabaseAdapter.Models.TutorProfile", "Profile")
                        .WithMany("AvailableTimes")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("Infra.DatabaseAdapter.Models.CourseModel", b =>
                {
                    b.HasOne("Infra.DatabaseAdapter.Models.RequestModel", "Request")
                        .WithOne("Course")
                        .HasForeignKey("Infra.DatabaseAdapter.Models.CourseModel", "RequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infra.DatabaseAdapter.Models.SubjectModel", "Subject")
                        .WithMany("Courses")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infra.DatabaseAdapter.Models.TutorProfile", "Tutor")
                        .WithMany("Courses")
                        .HasForeignKey("TutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Request");

                    b.Navigation("Subject");

                    b.Navigation("Tutor");
                });

            modelBuilder.Entity("Infra.DatabaseAdapter.Models.CourseReview", b =>
                {
                    b.HasOne("Infra.DatabaseAdapter.Models.CourseModel", "Course")
                        .WithMany("Reviews")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infra.DatabaseAdapter.Models.UserModel", "Owner")
                        .WithMany()
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Infra.DatabaseAdapter.Models.FavoriteTutor", b =>
                {
                    b.HasOne("Infra.DatabaseAdapter.Models.TutorProfile", "TutorProfile")
                        .WithMany()
                        .HasForeignKey("TutorProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infra.DatabaseAdapter.Models.UserModel", "UserModel")
                        .WithMany("FavoriteTutors")
                        .HasForeignKey("UserModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TutorProfile");

                    b.Navigation("UserModel");
                });

            modelBuilder.Entity("Infra.DatabaseAdapter.Models.FileModel", b =>
                {
                    b.HasOne("Infra.DatabaseAdapter.Models.UserModel", "Owner")
                        .WithMany()
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infra.DatabaseAdapter.Models.SolutionModel", "Solution")
                        .WithMany("Files")
                        .HasForeignKey("SolutionId");

                    b.HasOne("Infra.DatabaseAdapter.Models.TaskModel", "Task")
                        .WithMany("Files")
                        .HasForeignKey("TaskId");

                    b.Navigation("Owner");

                    b.Navigation("Solution");

                    b.Navigation("Task");
                });

            modelBuilder.Entity("Infra.DatabaseAdapter.Models.LessonModel", b =>
                {
                    b.HasOne("Infra.DatabaseAdapter.Models.CourseModel", "Course")
                        .WithMany("Lessons")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infra.DatabaseAdapter.Models.TutorProfile", "TutorProfile")
                        .WithMany("TeachingLessons")
                        .HasForeignKey("TutorProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("TutorProfile");
                });

            modelBuilder.Entity("Infra.DatabaseAdapter.Models.RequestModel", b =>
                {
                    b.HasOne("Infra.DatabaseAdapter.Models.UserModel", "Owner")
                        .WithMany()
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infra.DatabaseAdapter.Models.TutorProfile", "Tutor")
                        .WithMany()
                        .HasForeignKey("TutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");

                    b.Navigation("Tutor");
                });

            modelBuilder.Entity("Infra.DatabaseAdapter.Models.SolutionModel", b =>
                {
                    b.HasOne("Infra.DatabaseAdapter.Models.CourseModel", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infra.DatabaseAdapter.Models.UserModel", "Owner")
                        .WithMany()
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infra.DatabaseAdapter.Models.TaskModel", null)
                        .WithMany("Solutions")
                        .HasForeignKey("TaskModelId");

                    b.Navigation("Course");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Infra.DatabaseAdapter.Models.TaskModel", b =>
                {
                    b.HasOne("Infra.DatabaseAdapter.Models.CourseModel", "Course")
                        .WithMany("Tasks")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("Infra.DatabaseAdapter.Models.TutorProfile", b =>
                {
                    b.HasOne("Infra.DatabaseAdapter.Models.UserModel", "Owner")
                        .WithMany()
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Infra.DatabaseAdapter.Models.UserModel", b =>
                {
                    b.HasOne("Infra.DatabaseAdapter.Models.CityModel", "City")
                        .WithMany()
                        .HasForeignKey("CityId");

                    b.HasOne("Infra.DatabaseAdapter.Models.CourseModel", null)
                        .WithMany("Students")
                        .HasForeignKey("CourseModelId");

                    b.Navigation("City");
                });

            modelBuilder.Entity("LessonModelUserModel", b =>
                {
                    b.HasOne("Infra.DatabaseAdapter.Models.LessonModel", null)
                        .WithMany()
                        .HasForeignKey("LessonsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infra.DatabaseAdapter.Models.UserModel", null)
                        .WithMany()
                        .HasForeignKey("StudentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("Infra.DatabaseAdapter.Models.UserModel", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("Infra.DatabaseAdapter.Models.UserModel", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infra.DatabaseAdapter.Models.UserModel", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("Infra.DatabaseAdapter.Models.UserModel", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SubjectModelTutorProfile", b =>
                {
                    b.HasOne("Infra.DatabaseAdapter.Models.TutorProfile", null)
                        .WithMany()
                        .HasForeignKey("ProfilesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infra.DatabaseAdapter.Models.SubjectModel", null)
                        .WithMany()
                        .HasForeignKey("SubjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Infra.DatabaseAdapter.Models.CourseModel", b =>
                {
                    b.Navigation("Lessons");

                    b.Navigation("Reviews");

                    b.Navigation("Students");

                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("Infra.DatabaseAdapter.Models.LessonModel", b =>
                {
                    b.Navigation("Attendances");
                });

            modelBuilder.Entity("Infra.DatabaseAdapter.Models.RequestModel", b =>
                {
                    b.Navigation("Course")
                        .IsRequired();
                });

            modelBuilder.Entity("Infra.DatabaseAdapter.Models.SolutionModel", b =>
                {
                    b.Navigation("Files");
                });

            modelBuilder.Entity("Infra.DatabaseAdapter.Models.SubjectModel", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("Infra.DatabaseAdapter.Models.TaskModel", b =>
                {
                    b.Navigation("Files");

                    b.Navigation("Solutions");
                });

            modelBuilder.Entity("Infra.DatabaseAdapter.Models.TutorProfile", b =>
                {
                    b.Navigation("About")
                        .IsRequired();

                    b.Navigation("AvailableTimes");

                    b.Navigation("Courses");

                    b.Navigation("TeachingLessons");
                });

            modelBuilder.Entity("Infra.DatabaseAdapter.Models.UserModel", b =>
                {
                    b.Navigation("FavoriteTutors");
                });
#pragma warning restore 612, 618
        }
    }
}