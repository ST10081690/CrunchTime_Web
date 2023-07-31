using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrunchTime_Web.Migrations
{
    public partial class Academics1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SemesterModel",
                columns: table => new
                {
                    SemesterModelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SemesterName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeeksInSemester = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SemesterStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemesterModel", x => x.SemesterModelID);
                });

            migrationBuilder.CreateTable(
                name: "ModuleModel",
                columns: table => new
                {
                    ModuleModelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuleCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModuleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModuleCredits = table.Column<int>(type: "int", nullable: false),
                    ClassHours = table.Column<int>(type: "int", nullable: false),
                    SelfStudyHours = table.Column<double>(type: "float", nullable: true),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SemesterModelID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleModel", x => x.ModuleModelID);
                    table.ForeignKey(
                        name: "FK_ModuleModel_SemesterModel_SemesterModelID",
                        column: x => x.SemesterModelID,
                        principalTable: "SemesterModel",
                        principalColumn: "SemesterModelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleModel",
                columns: table => new
                {
                    ScheduleModelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoursStudied = table.Column<int>(type: "int", nullable: false),
                    SemesterStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RemainingStudyHours = table.Column<double>(type: "float", nullable: true),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModuleModelID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleModel", x => x.ScheduleModelID);
                    table.ForeignKey(
                        name: "FK_ScheduleModel_ModuleModel_ModuleModelID",
                        column: x => x.ModuleModelID,
                        principalTable: "ModuleModel",
                        principalColumn: "ModuleModelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModuleModel_SemesterModelID",
                table: "ModuleModel",
                column: "SemesterModelID");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleModel_ModuleModelID",
                table: "ScheduleModel",
                column: "ModuleModelID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduleModel");

            migrationBuilder.DropTable(
                name: "ModuleModel");

            migrationBuilder.DropTable(
                name: "SemesterModel");
        }
    }
}
