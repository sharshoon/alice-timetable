using Microsoft.EntityFrameworkCore.Migrations;

namespace Alice_Timetable.Migrations
{
    public partial class AddGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_StudentGroup_studentGroupid",
                table: "Schedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentGroup",
                table: "StudentGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employee",
                table: "Employee");

            migrationBuilder.RenameColumn(
                name: "studentGroupid",
                table: "Schedules",
                newName: "studentGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_studentGroupid",
                table: "Schedules",
                newName: "IX_Schedules_studentGroupId");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "StudentGroup",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "studentGroupId",
                table: "StudentGroup",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "Employee",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "employeeId",
                table: "Employee",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentGroup",
                table: "StudentGroup",
                column: "studentGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employee",
                table: "Employee",
                column: "employeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_StudentGroup_studentGroupId",
                table: "Schedules",
                column: "studentGroupId",
                principalTable: "StudentGroup",
                principalColumn: "studentGroupId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_StudentGroup_studentGroupId",
                table: "Schedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentGroup",
                table: "StudentGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employee",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "studentGroupId",
                table: "StudentGroup");

            migrationBuilder.DropColumn(
                name: "employeeId",
                table: "Employee");

            migrationBuilder.RenameColumn(
                name: "studentGroupId",
                table: "Schedules",
                newName: "studentGroupid");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_studentGroupId",
                table: "Schedules",
                newName: "IX_Schedules_studentGroupid");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "StudentGroup",
                type: "int",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "Employee",
                type: "int",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentGroup",
                table: "StudentGroup",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employee",
                table: "Employee",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_StudentGroup_studentGroupid",
                table: "Schedules",
                column: "studentGroupid",
                principalTable: "StudentGroup",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
