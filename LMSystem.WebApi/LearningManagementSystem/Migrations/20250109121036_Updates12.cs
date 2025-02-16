using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class Updates12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_School_SchoolsId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Tutor_School_SchoolId",
                table: "Tutor");

            migrationBuilder.DropTable(
                name: "GroupTutor");

            migrationBuilder.DropTable(
                name: "StudentSubjects");

            migrationBuilder.DropTable(
                name: "SubjectsTutor");

            migrationBuilder.DropIndex(
                name: "IX_Tutor_SchoolId",
                table: "Tutor");

            migrationBuilder.DropIndex(
                name: "IX_Student_SchoolsId",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "SchoolId",
                table: "Tutor");

            migrationBuilder.DropColumn(
                name: "SchoolsId",
                table: "Student");

            migrationBuilder.AddColumn<int>(
                name: "TutorId",
                table: "Subjects",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "School",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_TutorId",
                table: "Subjects",
                column: "TutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Tutor_TutorId",
                table: "Subjects",
                column: "TutorId",
                principalTable: "Tutor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Tutor_TutorId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_TutorId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "TutorId",
                table: "Subjects");

            migrationBuilder.AddColumn<int>(
                name: "SchoolId",
                table: "Tutor",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SchoolsId",
                table: "Student",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "School",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "GroupTutor",
                columns: table => new
                {
                    GroupsId = table.Column<int>(type: "integer", nullable: false),
                    TutorsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTutor", x => new { x.GroupsId, x.TutorsId });
                    table.ForeignKey(
                        name: "FK_GroupTutor_Group_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupTutor_Tutor_TutorsId",
                        column: x => x.TutorsId,
                        principalTable: "Tutor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentSubjects",
                columns: table => new
                {
                    StudentsId = table.Column<int>(type: "integer", nullable: false),
                    SubjectsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSubjects", x => new { x.StudentsId, x.SubjectsId });
                    table.ForeignKey(
                        name: "FK_StudentSubjects_Student_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentSubjects_Subjects_SubjectsId",
                        column: x => x.SubjectsId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectsTutor",
                columns: table => new
                {
                    SubjectsId = table.Column<int>(type: "integer", nullable: false),
                    TutorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectsTutor", x => new { x.SubjectsId, x.TutorId });
                    table.ForeignKey(
                        name: "FK_SubjectsTutor_Subjects_SubjectsId",
                        column: x => x.SubjectsId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectsTutor_Tutor_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Tutor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tutor_SchoolId",
                table: "Tutor",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_SchoolsId",
                table: "Student",
                column: "SchoolsId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupTutor_TutorsId",
                table: "GroupTutor",
                column: "TutorsId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubjects_SubjectsId",
                table: "StudentSubjects",
                column: "SubjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectsTutor_TutorId",
                table: "SubjectsTutor",
                column: "TutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_School_SchoolsId",
                table: "Student",
                column: "SchoolsId",
                principalTable: "School",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tutor_School_SchoolId",
                table: "Tutor",
                column: "SchoolId",
                principalTable: "School",
                principalColumn: "Id");
        }
    }
}
