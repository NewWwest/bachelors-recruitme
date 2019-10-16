using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RecruitMe.Web.Migrations
{
    public partial class AddResetEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "PersonalData",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "FatherName",
                table: "PersonalData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotherName",
                table: "PersonalData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrimarySchool",
                table: "PersonalData",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_PasswordResets_UserId",
                table: "PasswordResets",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalData_Users_UserId",
                table: "PersonalData",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalData_Users_UserId",
                table: "PersonalData");

            migrationBuilder.DropTable(
                name: "PasswordResets");

            migrationBuilder.DropColumn(
                name: "FatherName",
                table: "PersonalData");

            migrationBuilder.DropColumn(
                name: "MotherName",
                table: "PersonalData");

            migrationBuilder.DropColumn(
                name: "PrimarySchool",
                table: "PersonalData");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "PersonalData",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);
        }
    }
}
