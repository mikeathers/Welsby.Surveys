using Microsoft.EntityFrameworkCore.Migrations;

namespace Welsby.Surveys.DataLayer.Migrations
{
    public partial class AddedSurveyIdToQuestionGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionGroups_Surveys_SurveyId",
                table: "QuestionGroups");

            migrationBuilder.AlterColumn<int>(
                name: "SurveyId",
                table: "QuestionGroups",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionGroups_Surveys_SurveyId",
                table: "QuestionGroups",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "SurveyId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionGroups_Surveys_SurveyId",
                table: "QuestionGroups");

            migrationBuilder.AlterColumn<int>(
                name: "SurveyId",
                table: "QuestionGroups",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionGroups_Surveys_SurveyId",
                table: "QuestionGroups",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "SurveyId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
