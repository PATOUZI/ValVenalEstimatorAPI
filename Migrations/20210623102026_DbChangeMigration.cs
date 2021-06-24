using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace ValVenalEstimatorApi.Migrations
{
    public partial class DbChangeMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prefecture",
                table: "Places");

            migrationBuilder.AddColumn<long>(
                name: "PrefectureId",
                table: "Places",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Prefecture",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prefecture", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Places_PrefectureId",
                table: "Places",
                column: "PrefectureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Places_Prefecture_PrefectureId",
                table: "Places",
                column: "PrefectureId",
                principalTable: "Prefecture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Places_Prefecture_PrefectureId",
                table: "Places");

            migrationBuilder.DropTable(
                name: "Prefecture");

            migrationBuilder.DropIndex(
                name: "IX_Places_PrefectureId",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "PrefectureId",
                table: "Places");

            migrationBuilder.AddColumn<string>(
                name: "Prefecture",
                table: "Places",
                type: "text",
                nullable: true);
        }
    }
}
