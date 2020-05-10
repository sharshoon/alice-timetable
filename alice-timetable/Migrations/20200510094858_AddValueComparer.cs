using Microsoft.EntityFrameworkCore.Migrations;

namespace Alice_Timetable.Migrations
{
    public partial class AddValueComparer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamSchedule_Schedules_SimplifiedScheduleId",
                table: "ExamSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_Schedules_SimplifiedScheduleId",
                table: "Schedule");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedule1_Schedules_SimplifiedScheduleId",
                table: "Schedule1");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedule1_Schedules_SimplifiedScheduleId1",
                table: "Schedule1");

            migrationBuilder.DropIndex(
                name: "IX_Schedule1_SimplifiedScheduleId",
                table: "Schedule1");

            migrationBuilder.DropIndex(
                name: "IX_Schedule1_SimplifiedScheduleId1",
                table: "Schedule1");

            migrationBuilder.DropIndex(
                name: "IX_Schedule_SimplifiedScheduleId",
                table: "Schedule");

            migrationBuilder.DropIndex(
                name: "IX_ExamSchedule_SimplifiedScheduleId",
                table: "ExamSchedule");

            migrationBuilder.DropColumn(
                name: "SimplifiedScheduleId",
                table: "Schedule1");

            migrationBuilder.DropColumn(
                name: "SimplifiedScheduleId1",
                table: "Schedule1");

            migrationBuilder.DropColumn(
                name: "SimplifiedScheduleId",
                table: "Schedule");

            migrationBuilder.DropColumn(
                name: "SimplifiedScheduleId",
                table: "ExamSchedule");

            migrationBuilder.AddColumn<string>(
                name: "employee",
                table: "Schedules",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BsuirScheduleResponseId",
                table: "Schedule1",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BsuirScheduleResponseId1",
                table: "Schedule1",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BsuirScheduleResponseId",
                table: "Schedule",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BsuirScheduleResponseId",
                table: "ExamSchedule",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schedule1_BsuirScheduleResponseId",
                table: "Schedule1",
                column: "BsuirScheduleResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule1_BsuirScheduleResponseId1",
                table: "Schedule1",
                column: "BsuirScheduleResponseId1");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_BsuirScheduleResponseId",
                table: "Schedule",
                column: "BsuirScheduleResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamSchedule_BsuirScheduleResponseId",
                table: "ExamSchedule",
                column: "BsuirScheduleResponseId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamSchedule_Schedules_BsuirScheduleResponseId",
                table: "ExamSchedule",
                column: "BsuirScheduleResponseId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_Schedules_BsuirScheduleResponseId",
                table: "Schedule",
                column: "BsuirScheduleResponseId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule1_Schedules_BsuirScheduleResponseId",
                table: "Schedule1",
                column: "BsuirScheduleResponseId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule1_Schedules_BsuirScheduleResponseId1",
                table: "Schedule1",
                column: "BsuirScheduleResponseId1",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamSchedule_Schedules_BsuirScheduleResponseId",
                table: "ExamSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_Schedules_BsuirScheduleResponseId",
                table: "Schedule");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedule1_Schedules_BsuirScheduleResponseId",
                table: "Schedule1");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedule1_Schedules_BsuirScheduleResponseId1",
                table: "Schedule1");

            migrationBuilder.DropIndex(
                name: "IX_Schedule1_BsuirScheduleResponseId",
                table: "Schedule1");

            migrationBuilder.DropIndex(
                name: "IX_Schedule1_BsuirScheduleResponseId1",
                table: "Schedule1");

            migrationBuilder.DropIndex(
                name: "IX_Schedule_BsuirScheduleResponseId",
                table: "Schedule");

            migrationBuilder.DropIndex(
                name: "IX_ExamSchedule_BsuirScheduleResponseId",
                table: "ExamSchedule");

            migrationBuilder.DropColumn(
                name: "employee",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "BsuirScheduleResponseId",
                table: "Schedule1");

            migrationBuilder.DropColumn(
                name: "BsuirScheduleResponseId1",
                table: "Schedule1");

            migrationBuilder.DropColumn(
                name: "BsuirScheduleResponseId",
                table: "Schedule");

            migrationBuilder.DropColumn(
                name: "BsuirScheduleResponseId",
                table: "ExamSchedule");

            migrationBuilder.AddColumn<int>(
                name: "SimplifiedScheduleId",
                table: "Schedule1",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SimplifiedScheduleId1",
                table: "Schedule1",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SimplifiedScheduleId",
                table: "Schedule",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SimplifiedScheduleId",
                table: "ExamSchedule",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schedule1_SimplifiedScheduleId",
                table: "Schedule1",
                column: "SimplifiedScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule1_SimplifiedScheduleId1",
                table: "Schedule1",
                column: "SimplifiedScheduleId1");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_SimplifiedScheduleId",
                table: "Schedule",
                column: "SimplifiedScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamSchedule_SimplifiedScheduleId",
                table: "ExamSchedule",
                column: "SimplifiedScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamSchedule_Schedules_SimplifiedScheduleId",
                table: "ExamSchedule",
                column: "SimplifiedScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_Schedules_SimplifiedScheduleId",
                table: "Schedule",
                column: "SimplifiedScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule1_Schedules_SimplifiedScheduleId",
                table: "Schedule1",
                column: "SimplifiedScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule1_Schedules_SimplifiedScheduleId1",
                table: "Schedule1",
                column: "SimplifiedScheduleId1",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
