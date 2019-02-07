﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Welsby.Surveys.DataLayer.Configurations;

namespace Welsby.Surveys.DataLayer.Migrations
{
    [DbContext(typeof(SurveyDbContext))]
    partial class SurveyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Welsby.Surveys.DataLayer.Models.CompletedQuestion", b =>
                {
                    b.Property<int>("CompletedQuestionId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Answer");

                    b.Property<int?>("CompletedSurveyId");

                    b.Property<int?>("QuestionId");

                    b.HasKey("CompletedQuestionId");

                    b.HasIndex("CompletedSurveyId");

                    b.HasIndex("QuestionId");

                    b.ToTable("CompletedQuestions");
                });

            modelBuilder.Entity("Welsby.Surveys.DataLayer.Models.CompletedSurvey", b =>
                {
                    b.Property<int>("CompletedSurveyId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CaseNumber");

                    b.Property<string>("Name");

                    b.HasKey("CompletedSurveyId");

                    b.ToTable("CompletedSurveys");
                });

            modelBuilder.Entity("Welsby.Surveys.DataLayer.Models.Question", b =>
                {
                    b.Property<int>("QuestionId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("QuestionGroupId");

                    b.Property<int?>("QuestionTypeId");

                    b.Property<string>("Text");

                    b.HasKey("QuestionId");

                    b.HasIndex("QuestionGroupId");

                    b.HasIndex("QuestionTypeId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Welsby.Surveys.DataLayer.Models.QuestionGroup", b =>
                {
                    b.Property<int>("QuestionGroupId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<bool>("SoftDelete");

                    b.Property<int?>("SurveyId");

                    b.HasKey("QuestionGroupId");

                    b.HasIndex("SurveyId");

                    b.ToTable("QuestionGroups");
                });

            modelBuilder.Entity("Welsby.Surveys.DataLayer.Models.QuestionType", b =>
                {
                    b.Property<int>("QuestionTypeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("QuestionTypeId");

                    b.ToTable("QuestionTypes");
                });

            modelBuilder.Entity("Welsby.Surveys.DataLayer.Models.Survey", b =>
                {
                    b.Property<int>("SurveyId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<bool>("SoftDelete");

                    b.HasKey("SurveyId");

                    b.ToTable("Surveys");
                });

            modelBuilder.Entity("Welsby.Surveys.DataLayer.Models.CompletedQuestion", b =>
                {
                    b.HasOne("Welsby.Surveys.DataLayer.Models.CompletedSurvey", "CompletedSurvey")
                        .WithMany("CompletedQuestions")
                        .HasForeignKey("CompletedSurveyId");

                    b.HasOne("Welsby.Surveys.DataLayer.Models.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId");
                });

            modelBuilder.Entity("Welsby.Surveys.DataLayer.Models.Question", b =>
                {
                    b.HasOne("Welsby.Surveys.DataLayer.Models.QuestionGroup", "QuestionGroup")
                        .WithMany("Questions")
                        .HasForeignKey("QuestionGroupId");

                    b.HasOne("Welsby.Surveys.DataLayer.Models.QuestionType", "QuestionType")
                        .WithMany()
                        .HasForeignKey("QuestionTypeId");
                });

            modelBuilder.Entity("Welsby.Surveys.DataLayer.Models.QuestionGroup", b =>
                {
                    b.HasOne("Welsby.Surveys.DataLayer.Models.Survey", "Survey")
                        .WithMany("QuestionGroups")
                        .HasForeignKey("SurveyId");
                });
#pragma warning restore 612, 618
        }
    }
}
