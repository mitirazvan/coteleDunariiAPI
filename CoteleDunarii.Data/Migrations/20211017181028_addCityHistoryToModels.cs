using Microsoft.EntityFrameworkCore.Migrations;

namespace CoteleDunarii.Data.Migrations
{
    public partial class addCityHistoryToModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_City_WaterEstimations_WaterEstimationsId",
                table: "City");

            migrationBuilder.DropForeignKey(
                name: "FK_City_WaterInfo_WaterInfoId",
                table: "City");

            migrationBuilder.DropIndex(
                name: "IX_City_WaterEstimationsId",
                table: "City");

            migrationBuilder.DropIndex(
                name: "IX_City_WaterInfoId",
                table: "City");

            migrationBuilder.DropColumn(
                name: "WaterEstimationsId",
                table: "City");

            migrationBuilder.DropColumn(
                name: "WaterInfoId",
                table: "City");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "WaterInfo",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "WaterEstimations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WaterInfo_CityId",
                table: "WaterInfo",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_WaterEstimations_CityId",
                table: "WaterEstimations",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_WaterEstimations_City_CityId",
                table: "WaterEstimations",
                column: "CityId",
                principalTable: "City",
                principalColumn: "CityId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WaterInfo_City_CityId",
                table: "WaterInfo",
                column: "CityId",
                principalTable: "City",
                principalColumn: "CityId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WaterEstimations_City_CityId",
                table: "WaterEstimations");

            migrationBuilder.DropForeignKey(
                name: "FK_WaterInfo_City_CityId",
                table: "WaterInfo");

            migrationBuilder.DropIndex(
                name: "IX_WaterInfo_CityId",
                table: "WaterInfo");

            migrationBuilder.DropIndex(
                name: "IX_WaterEstimations_CityId",
                table: "WaterEstimations");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "WaterInfo");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "WaterEstimations");

            migrationBuilder.AddColumn<int>(
                name: "WaterEstimationsId",
                table: "City",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WaterInfoId",
                table: "City",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_City_WaterEstimationsId",
                table: "City",
                column: "WaterEstimationsId");

            migrationBuilder.CreateIndex(
                name: "IX_City_WaterInfoId",
                table: "City",
                column: "WaterInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_City_WaterEstimations_WaterEstimationsId",
                table: "City",
                column: "WaterEstimationsId",
                principalTable: "WaterEstimations",
                principalColumn: "WaterEstimationsId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_City_WaterInfo_WaterInfoId",
                table: "City",
                column: "WaterInfoId",
                principalTable: "WaterInfo",
                principalColumn: "WaterInfoId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
