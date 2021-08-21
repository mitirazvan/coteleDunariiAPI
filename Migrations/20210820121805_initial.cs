using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoteleDunarii.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WaterEstimations",
                columns: table => new
                {
                    WaterEstimationsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Next24h = table.Column<int>(nullable: false),
                    Next48h = table.Column<int>(nullable: false),
                    Next72h = table.Column<int>(nullable: false),
                    Next96h = table.Column<int>(nullable: false),
                    Next120h = table.Column<int>(nullable: false),
                    ReadTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaterEstimations", x => x.WaterEstimationsId);
                });

            migrationBuilder.CreateTable(
                name: "WaterInfo",
                columns: table => new
                {
                    WaterInfoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Elevation = table.Column<string>(nullable: true),
                    Variation = table.Column<int>(nullable: false),
                    Temperature = table.Column<int>(nullable: false),
                    ReadTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaterInfo", x => x.WaterInfoId);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    CityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Km = table.Column<string>(nullable: true),
                    WaterInfoId = table.Column<int>(nullable: true),
                    WaterEstimationsId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.CityId);
                    table.ForeignKey(
                        name: "FK_City_WaterEstimations_WaterEstimationsId",
                        column: x => x.WaterEstimationsId,
                        principalTable: "WaterEstimations",
                        principalColumn: "WaterEstimationsId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_City_WaterInfo_WaterInfoId",
                        column: x => x.WaterInfoId,
                        principalTable: "WaterInfo",
                        principalColumn: "WaterInfoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_City_WaterEstimationsId",
                table: "City",
                column: "WaterEstimationsId");

            migrationBuilder.CreateIndex(
                name: "IX_City_WaterInfoId",
                table: "City",
                column: "WaterInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "WaterEstimations");

            migrationBuilder.DropTable(
                name: "WaterInfo");
        }
    }
}
