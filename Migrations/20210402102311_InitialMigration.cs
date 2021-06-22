using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace ValVenalEstimatorApi.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Prefecture = table.Column<string>(type: "text", nullable: true),
                    District = table.Column<string>(type: "text", nullable: true),
                    PricePerMeterSquare = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Places");
        }
    }
}
