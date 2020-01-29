using Microsoft.EntityFrameworkCore.Migrations;

namespace RecruitMe.Web.Migrations
{
    public partial class ModifiedCandidateUserKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamTakers_Users_CandidateId",
                table: "ExamTakers");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Users_CandidateId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_ExamTakers_CandidateId",
                table: "ExamTakers");

            migrationBuilder.DropColumn(
                name: "CandidateId",
                table: "ExamTakers");

            migrationBuilder.AlterColumn<string>(
                name: "CandidateId",
                table: "Users",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ExamTakers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ExamTakers_UserId",
                table: "ExamTakers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamTakers_Users_UserId",
                table: "ExamTakers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamTakers_Users_UserId",
                table: "ExamTakers");

            migrationBuilder.DropIndex(
                name: "IX_ExamTakers_UserId",
                table: "ExamTakers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ExamTakers");

            migrationBuilder.AlterColumn<string>(
                name: "CandidateId",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CandidateId",
                table: "ExamTakers",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Users_CandidateId",
                table: "Users",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamTakers_CandidateId",
                table: "ExamTakers",
                column: "CandidateId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamTakers_Users_CandidateId",
                table: "ExamTakers",
                column: "CandidateId",
                principalTable: "Users",
                principalColumn: "CandidateId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
