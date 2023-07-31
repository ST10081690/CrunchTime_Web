using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrunchTime_Web.Migrations
{
    public partial class FixedDataBugs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SemesterStartDate",
                table: "ScheduleModel",
                newName: "DateOfStudy");

            migrationBuilder.AlterColumn<double>(
                name: "WeeksInSemester",
                table: "SemesterModel",
                type: "float",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "ModuleWeekDay",
                table: "ScheduleModel",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModuleWeekDay",
                table: "ScheduleModel");

            migrationBuilder.RenameColumn(
                name: "DateOfStudy",
                table: "ScheduleModel",
                newName: "SemesterStartDate");

            migrationBuilder.AlterColumn<string>(
                name: "WeeksInSemester",
                table: "SemesterModel",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
