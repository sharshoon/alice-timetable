using Microsoft.EntityFrameworkCore.Migrations;

namespace Alice_Timetable.Migrations
{
    public partial class AddTeachers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(nullable: true),
                    lastName = table.Column<string>(nullable: true),
                    middleName = table.Column<string>(nullable: true),
                    rank = table.Column<string>(nullable: true),
                    photoLink = table.Column<string>(nullable: true),
                    calendarId = table.Column<string>(nullable: true),
                    academicDepartment = table.Column<string>(nullable: true),
                    fio = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Teachers");
        }
    }
}
