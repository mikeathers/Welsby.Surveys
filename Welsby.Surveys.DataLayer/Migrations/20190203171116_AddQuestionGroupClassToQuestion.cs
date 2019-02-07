using Microsoft.EntityFrameworkCore.Migrations;

namespace Welsby.Surveys.DataLayer.Migrations
{
    public partial class AddQuestionGroupClassToQuestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_QuestionGroups_QuestionGroupId",
                table: "Question");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionGroupId",
                table: "Question",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Question_QuestionGroups_QuestionGroupId",
                table: "Question",
                column: "QuestionGroupId",
                principalTable: "QuestionGroups",
                principalColumn: "QuestionGroupId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_QuestionGroups_QuestionGroupId",
                table: "Question");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionGroupId",
                table: "Question",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_QuestionGroups_QuestionGroupId",
                table: "Question",
                column: "QuestionGroupId",
                principalTable: "QuestionGroups",
                principalColumn: "QuestionGroupId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
