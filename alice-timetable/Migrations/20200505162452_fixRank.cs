using Microsoft.EntityFrameworkCore.Migrations;

namespace Alice_Timetable.Migrations
{
    public partial class fixRank : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentGroup",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(nullable: true),
                    facultyId = table.Column<int>(nullable: false),
                    facultyName = table.Column<string>(nullable: true),
                    specialityDepartmentEducationFormId = table.Column<int>(nullable: false),
                    specialityName = table.Column<string>(nullable: true),
                    course = table.Column<int>(nullable: false),
                    calendarId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentGroup", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    studentGroupid = table.Column<int>(nullable: true),
                    todayDate = table.Column<string>(nullable: true),
                    tomorrowDate = table.Column<string>(nullable: true),
                    currentWeekNumber = table.Column<int>(nullable: false),
                    dateStart = table.Column<string>(nullable: true),
                    dateEnd = table.Column<string>(nullable: true),
                    sessionStart = table.Column<string>(nullable: true),
                    sessionEnd = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_StudentGroup_studentGroupid",
                        column: x => x.studentGroupid,
                        principalTable: "StudentGroup",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExamSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    weekDay = table.Column<string>(nullable: true),
                    SimplifiedScheduleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamSchedule_Schedules_SimplifiedScheduleId",
                        column: x => x.SimplifiedScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    weekDay = table.Column<string>(nullable: true),
                    SimplifiedScheduleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedule_Schedules_SimplifiedScheduleId",
                        column: x => x.SimplifiedScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Schedule1",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    numSubgroup = table.Column<int>(nullable: false),
                    lessonTime = table.Column<string>(nullable: true),
                    startLessonTime = table.Column<string>(nullable: true),
                    endLessonTime = table.Column<string>(nullable: true),
                    gradebookLesson = table.Column<string>(nullable: true),
                    subject = table.Column<string>(nullable: true),
                    note = table.Column<string>(nullable: true),
                    lessonType = table.Column<string>(nullable: true),
                    zaoch = table.Column<bool>(nullable: false),
                    ExamScheduleId = table.Column<int>(nullable: true),
                    ScheduleId = table.Column<int>(nullable: true),
                    SimplifiedScheduleId = table.Column<int>(nullable: true),
                    SimplifiedScheduleId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule1", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedule1_ExamSchedule_ExamScheduleId",
                        column: x => x.ExamScheduleId,
                        principalTable: "ExamSchedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schedule1_Schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schedule1_Schedules_SimplifiedScheduleId",
                        column: x => x.SimplifiedScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schedule1_Schedules_SimplifiedScheduleId1",
                        column: x => x.SimplifiedScheduleId1,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
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
                    fio = table.Column<string>(nullable: true),
                    ScheduleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.id);
                    table.ForeignKey(
                        name: "FK_Employee_Schedule1_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedule1",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntityInt",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Item = table.Column<int>(nullable: false),
                    ScheduleId = table.Column<int>(nullable: true)
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Item = table.Column<string>(nullable: true),
                    Employeeid = table.Column<int>(nullable: true),
                    ScheduleId = table.Column<int>(nullable: true),
                    ScheduleId1 = table.Column<int>(nullable: true),
                    ScheduleId2 = table.Column<int>(nullable: true),
                    ScheduleId3 = table.Column<int>(nullable: true),
                    ScheduleId4 = table.Column<int>(nullable: true)
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
                name: "IX_Employee_ScheduleId",
                table: "Employee",
                column: "ScheduleId");

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

            migrationBuilder.CreateIndex(
                name: "IX_ExamSchedule_SimplifiedScheduleId",
                table: "ExamSchedule",
                column: "SimplifiedScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_SimplifiedScheduleId",
                table: "Schedule",
                column: "SimplifiedScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule1_ExamScheduleId",
                table: "Schedule1",
                column: "ExamScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule1_ScheduleId",
                table: "Schedule1",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule1_SimplifiedScheduleId",
                table: "Schedule1",
                column: "SimplifiedScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule1_SimplifiedScheduleId1",
                table: "Schedule1",
                column: "SimplifiedScheduleId1");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_studentGroupid",
                table: "Schedules",
                column: "studentGroupid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntityInt");

            migrationBuilder.DropTable(
                name: "EntityString");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Schedule1");

            migrationBuilder.DropTable(
                name: "ExamSchedule");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "StudentGroup");
        }
    }
}
