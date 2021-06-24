﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ValVenalEstimatorApi.Data;

namespace ValVenalEstimatorApi.Migrations
{
    [DbContext(typeof(ValVenalEstimatorDbContext))]
    [Migration("20210623102026_DbChangeMigration")]
    partial class DbChangeMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.4");

            modelBuilder.Entity("ValVenalEstimatorApi.Models.Place", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("District")
                        .HasColumnType("text");

                    b.Property<long>("PrefectureId")
                        .HasColumnType("bigint");

                    b.Property<double>("PricePerMeterSquare")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("PrefectureId");

                    b.ToTable("Places");
                });

            modelBuilder.Entity("ValVenalEstimatorApi.Models.Prefecture", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Prefecture");
                });

            modelBuilder.Entity("ValVenalEstimatorApi.Models.Place", b =>
                {
                    b.HasOne("ValVenalEstimatorApi.Models.Prefecture", "Prefecture")
                        .WithMany("Places")
                        .HasForeignKey("PrefectureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Prefecture");
                });

            modelBuilder.Entity("ValVenalEstimatorApi.Models.Prefecture", b =>
                {
                    b.Navigation("Places");
                });
#pragma warning restore 612, 618
        }
    }
}