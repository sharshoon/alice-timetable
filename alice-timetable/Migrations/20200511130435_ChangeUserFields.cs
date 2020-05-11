using Microsoft.EntityFrameworkCore.Migrations;

namespace Alice_Timetable.Migrations
{
    public partial class ChangeUserFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DisplayAuditory",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DisplayEmployeeName",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DisplaySubjectTime",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DisplaySubjectType",
                table: "Users",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayAuditory",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DisplayEmployeeName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DisplaySubjectTime",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DisplaySubjectType",
                table: "Users");
        }
    }
}
