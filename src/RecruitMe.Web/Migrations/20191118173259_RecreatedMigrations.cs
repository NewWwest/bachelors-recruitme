using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RecruitMe.Web.Migrations
{
    public partial class RecreatedMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Pesel = table.Column<string>(nullable: true),
                    CandidateId = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    EmailVerified = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConfirmationEmails",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    Used = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfirmationEmails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfirmationEmails_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PasswordResets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    InsertDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordResets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PasswordResets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    FileUrl = table.Column<string>(nullable: true),
                    ContentType = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalDocuments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalData",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    Adress = table.Column<string>(nullable: true),
                    MotherName = table.Column<string>(nullable: true),
                    FatherName = table.Column<string>(nullable: true),
                    PrimarySchool = table.Column<string>(nullable: true),
                    ProfilePictureFileId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalData", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_PersonalData_PersonalDocuments_ProfilePictureFileId",
                        column: x => x.ProfilePictureFileId,
                        principalTable: "PersonalDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonalData_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConfirmationEmails_UserId",
                table: "ConfirmationEmails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PasswordResets_UserId",
                table: "PasswordResets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalData_ProfilePictureFileId",
                table: "PersonalData",
                column: "ProfilePictureFileId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDocuments_UserId",
                table: "PersonalDocuments",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfirmationEmails");

            migrationBuilder.DropTable(
                name: "PasswordResets");

            migrationBuilder.DropTable(
                name: "PersonalData");

            migrationBuilder.DropTable(
                name: "PersonalDocuments");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
