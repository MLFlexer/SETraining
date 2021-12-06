﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SETraining.Server.Contexts;

#nullable disable

namespace SETraining.Server.Migrations
{
    [DbContext(typeof(SETrainingContext))]
    partial class SETrainingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("AvgRating")
                        .HasColumnType("integer");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("CreatorId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Difficulty")
                        .HasColumnType("text");

                    b.Property<int?>("LearnerId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("LearnerId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("ArticleProgrammingLanguage", b =>
                {
                    b.Property<int>("ArticlesId")
                        .HasColumnType("integer");

                    b.Property<string>("ProgrammingLanguagesName")
                        .HasColumnType("text");

                    b.HasKey("ArticlesId", "ProgrammingLanguagesName");

                    b.HasIndex("ProgrammingLanguagesName");

                    b.ToTable("ArticleProgrammingLanguage");
                });

            modelBuilder.Entity("ProgrammingLanguageVideo", b =>
                {
                    b.Property<string>("ProgrammingLanguagesName")
                        .HasColumnType("text");

                    b.Property<int>("VideosId")
                        .HasColumnType("integer");

                    b.HasKey("ProgrammingLanguagesName", "VideosId");

                    b.HasIndex("VideosId");

                    b.ToTable("ProgrammingLanguageVideo");
                });

            modelBuilder.Entity("SETraining.Shared.Models.ArticleHistoryEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ArticleId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("LearnerId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("LearnerId");

                    b.ToTable("ArticleHistoryEntries");
                });

            modelBuilder.Entity("SETraining.Shared.Models.ArticleRating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ArticleId")
                        .HasColumnType("integer");

                    b.Property<int>("LearnerId")
                        .HasColumnType("integer");

                    b.Property<int>("Value")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("LearnerId");

                    b.ToTable("ArticleRatings");
                });

            modelBuilder.Entity("SETraining.Shared.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("SETraining.Shared.Models.Learner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("Level")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Learners");
                });

            modelBuilder.Entity("SETraining.Shared.Models.Moderator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Moderators");
                });

            modelBuilder.Entity("SETraining.Shared.Models.ProgrammingLanguage", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Name");

                    b.ToTable("ProgrammingLanguages");
                });

            modelBuilder.Entity("SETraining.Shared.Models.VideoHistoryEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("LearnerId")
                        .HasColumnType("integer");

                    b.Property<int>("VideoId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("LearnerId");

                    b.HasIndex("VideoId");

                    b.ToTable("VideoHistoryEntries");
                });

            modelBuilder.Entity("SETraining.Shared.Models.VideoRating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("LearnerId")
                        .HasColumnType("integer");

                    b.Property<int>("Value")
                        .HasColumnType("integer");

                    b.Property<int>("VideoId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("LearnerId");

                    b.HasIndex("VideoId");

                    b.ToTable("VideoRatings");
                });

            modelBuilder.Entity("Video", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("AvgRating")
                        .HasColumnType("integer");

                    b.Property<int?>("CreatorId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int?>("Difficulty")
                        .HasColumnType("integer");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("Article", b =>
                {
                    b.HasOne("SETraining.Shared.Models.Moderator", "Creator")
                        .WithMany("Contents")
                        .HasForeignKey("CreatorId");

                    b.HasOne("SETraining.Shared.Models.Learner", null)
                        .WithMany("Favorites")
                        .HasForeignKey("LearnerId");

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("ArticleProgrammingLanguage", b =>
                {
                    b.HasOne("Article", null)
                        .WithMany()
                        .HasForeignKey("ArticlesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SETraining.Shared.Models.ProgrammingLanguage", null)
                        .WithMany()
                        .HasForeignKey("ProgrammingLanguagesName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProgrammingLanguageVideo", b =>
                {
                    b.HasOne("SETraining.Shared.Models.ProgrammingLanguage", null)
                        .WithMany()
                        .HasForeignKey("ProgrammingLanguagesName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Video", null)
                        .WithMany()
                        .HasForeignKey("VideosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SETraining.Shared.Models.ArticleHistoryEntry", b =>
                {
                    b.HasOne("Article", "Article")
                        .WithMany()
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SETraining.Shared.Models.Learner", "Learner")
                        .WithMany("ArticleHistory")
                        .HasForeignKey("LearnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Article");

                    b.Navigation("Learner");
                });

            modelBuilder.Entity("SETraining.Shared.Models.ArticleRating", b =>
                {
                    b.HasOne("Article", "Article")
                        .WithMany("ArticleRatings")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SETraining.Shared.Models.Learner", "Learner")
                        .WithMany("Ratings")
                        .HasForeignKey("LearnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Article");

                    b.Navigation("Learner");
                });

            modelBuilder.Entity("SETraining.Shared.Models.VideoHistoryEntry", b =>
                {
                    b.HasOne("SETraining.Shared.Models.Learner", "Learner")
                        .WithMany("VideoHistory")
                        .HasForeignKey("LearnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Video", "Video")
                        .WithMany()
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Learner");

                    b.Navigation("Video");
                });

            modelBuilder.Entity("SETraining.Shared.Models.VideoRating", b =>
                {
                    b.HasOne("SETraining.Shared.Models.Learner", "Learner")
                        .WithMany()
                        .HasForeignKey("LearnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Video", "Video")
                        .WithMany("VideoRatings")
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Learner");

                    b.Navigation("Video");
                });

            modelBuilder.Entity("Video", b =>
                {
                    b.HasOne("SETraining.Shared.Models.Moderator", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId");

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("Article", b =>
                {
                    b.Navigation("ArticleRatings");
                });

            modelBuilder.Entity("SETraining.Shared.Models.Learner", b =>
                {
                    b.Navigation("ArticleHistory");

                    b.Navigation("Favorites");

                    b.Navigation("Ratings");

                    b.Navigation("VideoHistory");
                });

            modelBuilder.Entity("SETraining.Shared.Models.Moderator", b =>
                {
                    b.Navigation("Contents");
                });

            modelBuilder.Entity("Video", b =>
                {
                    b.Navigation("VideoRatings");
                });
#pragma warning restore 612, 618
        }
    }
}
