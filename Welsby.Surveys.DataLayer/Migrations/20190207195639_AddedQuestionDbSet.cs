using Microsoft.EntityFrameworkCore.Migrations;

namespace Welsby.Surveys.DataLayer.Migrations
{
    public partial class AddedQuestionDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompletedQuestions_CompletedSurveys_CompletedSurveyId",
                table: "CompletedQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_CompletedQuestions_Question_QuestionId",
                table: "CompletedQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_QuestionGroups_QuestionGroupId",
                table: "Question");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_QuestionTypes_QuestionTypeId",
                table: "Question");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Question",
                table: "Question");

            migrationBuilder.RenameTable(
                name: "Question",
                newName: "Questions");

            migrationBuilder.RenameIndex(
                name: "IX_Question_QuestionTypeId",
                table: "Questions",
                newName: "IX_Questions_QuestionTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Question_QuestionGroupId",
                table: "Questions",
                newName: "IX_Questions_QuestionGroupId");

            migrationBuilder.AlterColumn<int>(
                name: "CompletedSurveyId",
                table: "CompletedQuestions",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Questions",
                table: "Questions",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedQuestions_CompletedSurveys_CompletedSurveyId",
                table: "CompletedQuestions",
                column: "CompletedSurveyId",
                principalTable: "CompletedSurveys",
                principalColumn: "CompletedSurveyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedQuestions_Questions_QuestionId",
                table: "CompletedQuestions",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_QuestionGroups_QuestionGroupId",
                table: "Questions",
                column: "QuestionGroupId",
                principalTable: "QuestionGroups",
                principalColumn: "QuestionGroupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_QuestionTypes_QuestionTypeId",
                table: "Questions",
                column: "QuestionTypeId",
                principalTable: "QuestionTypes",
                principalColumn: "QuestionTypeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompletedQuestions_CompletedSurveys_CompletedSurveyId",
                table: "CompletedQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_CompletedQuestions_Questions_QuestionId",
                table: "CompletedQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_QuestionGroups_QuestionGroupId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_QuestionTypes_QuestionTypeId",
                table: "Questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Questions",
                table: "Questions");

            migrationBuilder.RenameTable(
                name: "Questions",
                newName: "Question");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_QuestionTypeId",
                table: "Question",
                newName: "IX_Question_QuestionTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_QuestionGroupId",
                table: "Question",
                newName: "IX_Question_QuestionGroupId");

            migrationBuilder.AlterColumn<int>(
                name: "CompletedSurveyId",
                table: "CompletedQuestions",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Question",
                table: "Question",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedQuestions_CompletedSurveys_CompletedSurveyId",
                table: "CompletedQuestions",
                column: "CompletedSurveyId",
                principalTable: "CompletedSurveys",
                principalColumn: "CompletedSurveyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedQuestions_Question_QuestionId",
                table: "CompletedQuestions",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_QuestionGroups_QuestionGroupId",
                table: "Question",
                column: "QuestionGroupId",
                principalTable: "QuestionGroups",
                principalColumn: "QuestionGroupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_QuestionTypes_QuestionTypeId",
                table: "Question",
                column: "QuestionTypeId",
                principalTable: "QuestionTypes",
                principalColumn: "QuestionTypeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
