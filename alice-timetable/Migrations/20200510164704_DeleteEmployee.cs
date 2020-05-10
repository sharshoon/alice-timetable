using Microsoft.EntityFrameworkCore.Migrations;

namespace Alice_Timetable.Migrations
{
    public partial class DeleteEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "employee",
                table: "Schedules");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "employee",
                table: "Schedules",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
