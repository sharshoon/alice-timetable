using Microsoft.EntityFrameworkCore.Migrations;

namespace Alice_Timetable.Migrations
{
    public partial class RemoveSchedules : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentGroup",
                columns: table => new
                {
                    studentGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    calendarId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    course = table.Column<int>(type: "int", nullable: false),
                    facultyId = table.Column<int>(type: "int", nullable: false),
                    facultyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    specialityDepartmentEducationFormId = table.Column<int>(type: "int", nullable: false),
                    specialityName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentGroup", x => x.studentGroupId);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    currentWeekNumber = table.Column<int>(type: "int", nullable: false),
                    dateEnd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dateStart = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sessionEnd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sessionStart = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    studentGroupId = table.Column<int>(type: "int", nullable: true),
                    todayDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tomorrowDate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_StudentGroup_studentGroupId",
                        column: x => x.studentGroupId,
                        principalTable: "StudentGroup",
                        principalColumn: "studentGroupId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExamSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BsuirScheduleResponseId = table.Column<int>(type: "int", nullable: true),
                    weekDay = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamSchedule_Schedules_BsuirScheduleResponseId",
                        column: x => x.BsuirScheduleResponseId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BsuirScheduleResponseId = table.Column<int>(type: "int", nullable: true),
                    weekDay = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedule_Schedules_BsuirScheduleResponseId",
                        column: x => x.BsuirScheduleResponseId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Schedule1",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BsuirScheduleResponseId = table.Column<int>(type: "int", nullable: true),
                    BsuirScheduleResponseId1 = table.Column<int>(type: "int", nullable: true),
                    ExamScheduleId = table.Column<int>(type: "int", nullable: true),
                    ScheduleId = table.Column<int>(type: "int", nullable: true),
                    auditory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    endLessonTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    gradebookLesson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    gradebookLessonlist = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lessonTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lessonType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    numSubgroup = table.Column<int>(type: "int", nullable: false),
                    startLessonTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    studentGroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    studentGroupInformation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    studentGroupModelList = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    weekNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    zaoch = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule1", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedule1_Schedules_BsuirScheduleResponseId",
                        column: x => x.BsuirScheduleResponseId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schedule1_Schedules_BsuirScheduleResponseId1",
                        column: x => x.BsuirScheduleResponseId1,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    employeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduleId = table.Column<int>(type: "int", nullable: true),
                    academicDepartment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    calendarId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    id = table.Column<int>(type: "int", nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    middleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    photoLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rank = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.employeeId);
                    table.ForeignKey(
                        name: "FK_Employee_Schedule1_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedule1",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_ScheduleId",
                table: "Employee",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamSchedule_BsuirScheduleResponseId",
                table: "ExamSchedule",
                column: "BsuirScheduleResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_BsuirScheduleResponseId",
                table: "Schedule",
                column: "BsuirScheduleResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule1_BsuirScheduleResponseId",
                table: "Schedule1",
                column: "BsuirScheduleResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule1_BsuirScheduleResponseId1",
                table: "Schedule1",
                column: "BsuirScheduleResponseId1");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule1_ExamScheduleId",
                table: "Schedule1",
                column: "ExamScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule1_ScheduleId",
                table: "Schedule1",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_studentGroupId",
                table: "Schedules",
                column: "studentGroupId");
        }
    }
}
