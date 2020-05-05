using Microsoft.EntityFrameworkCore.Migrations;

namespace Alice_Timetable.Migrations
{
    public partial class MoveToStringAndIntArrays : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntityInt");

            migrationBuilder.DropTable(
                name: "EntityString");

            migrationBuilder.AddColumn<string>(
                name: "auditory",
                table: "Schedule1",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "gradebookLessonlist",
                table: "Schedule1",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "studentGroup",
                table: "Schedule1",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "studentGroupInformation",
                table: "Schedule1",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "studentGroupModelList",
                table: "Schedule1",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "weekNumber",
                table: "Schedule1",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "academicDepartment",
                table: "Employee",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "auditory",
                table: "Schedule1");

            migrationBuilder.DropColumn(
                name: "gradebookLessonlist",
                table: "Schedule1");

            migrationBuilder.DropColumn(
                name: "studentGroup",
                table: "Schedule1");

            migrationBuilder.DropColumn(
                name: "studentGroupInformation",
                table: "Schedule1");

            migrationBuilder.DropColumn(
                name: "studentGroupModelList",
                table: "Schedule1");

            migrationBuilder.DropColumn(
                name: "weekNumber",
                table: "Schedule1");

            migrationBuilder.DropColumn(
                name: "academicDepartment",
                table: "Employee");

            migrationBuilder.CreateTable(
                name: "EntityInt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Item = table.Column<int>(type: "int", nullable: false),
                    ScheduleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityInt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntityInt_Schedule1_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedule1",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntityString",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Employeeid = table.Column<int>(type: "int", nullable: true),
                    Item = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScheduleId = table.Column<int>(type: "int", nullable: true),
                    ScheduleId1 = table.Column<int>(type: "int", nullable: true),
                    ScheduleId2 = table.Column<int>(type: "int", nullable: true),
                    ScheduleId3 = table.Column<int>(type: "int", nullable: true),
                    ScheduleId4 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityString", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntityString_Employee_Employeeid",
                        column: x => x.Employeeid,
                        principalTable: "Employee",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntityString_Schedule1_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedule1",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntityString_Schedule1_ScheduleId1",
                        column: x => x.ScheduleId1,
                        principalTable: "Schedule1",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntityString_Schedule1_ScheduleId2",
                        column: x => x.ScheduleId2,
                        principalTable: "Schedule1",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntityString_Schedule1_ScheduleId3",
                        column: x => x.ScheduleId3,
                        principalTable: "Schedule1",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntityString_Schedule1_ScheduleId4",
                        column: x => x.ScheduleId4,
                        principalTable: "Schedule1",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntityInt_ScheduleId",
                table: "EntityInt",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityString_Employeeid",
                table: "EntityString",
                column: "Employeeid");

            migrationBuilder.CreateIndex(
                name: "IX_EntityString_ScheduleId",
                table: "EntityString",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityString_ScheduleId1",
                table: "EntityString",
                column: "ScheduleId1");

            migrationBuilder.CreateIndex(
                name: "IX_EntityString_ScheduleId2",
                table: "EntityString",
                column: "ScheduleId2");

            migrationBuilder.CreateIndex(
                name: "IX_EntityString_ScheduleId3",
                table: "EntityString",
                column: "ScheduleId3");

            migrationBuilder.CreateIndex(
                name: "IX_EntityString_ScheduleId4",
                table: "EntityString",
                column: "ScheduleId4");
        }
    }
}
