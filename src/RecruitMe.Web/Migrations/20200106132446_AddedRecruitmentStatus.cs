using Microsoft.EntityFrameworkCore.Migrations;

namespace RecruitMe.Web.Migrations
{
    public partial class AddedRecruitmentStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "PersonalData",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "PersonalData");
        }
    }
}
