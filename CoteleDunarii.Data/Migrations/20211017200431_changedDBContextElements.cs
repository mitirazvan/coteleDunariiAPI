using Microsoft.EntityFrameworkCore.Migrations;

namespace CoteleDunarii.Data.Migrations
{
    public partial class changedDBContextElements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WaterEstimations_City_CityId",
                table: "WaterEstimations");

            migrationBuilder.DropForeignKey(
                name: "FK_WaterInfo_City_CityId",
                table: "WaterInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WaterInfo",
                table: "WaterInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_City",
                table: "City");

            migrationBuilder.RenameTable(
                name: "WaterInfo",
                newName: "WaterInfos");

            migrationBuilder.RenameTable(
                name: "City",
                newName: "Cities");

            migrationBuilder.RenameIndex(
                name: "IX_WaterInfo_CityId",
                table: "WaterInfos",
                newName: "IX_WaterInfos_CityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WaterInfos",
                table: "WaterInfos",
                column: "WaterInfoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cities",
                table: "Cities",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_WaterEstimations_Cities_CityId",
                table: "WaterEstimations",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "CityId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WaterInfos_Cities_CityId",
                table: "WaterInfos",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "CityId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WaterEstimations_Cities_CityId",
                table: "WaterEstimations");

            migrationBuilder.DropForeignKey(
                name: "FK_WaterInfos_Cities_CityId",
                table: "WaterInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WaterInfos",
                table: "WaterInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cities",
                table: "Cities");

            migrationBuilder.RenameTable(
                name: "WaterInfos",
                newName: "WaterInfo");

            migrationBuilder.RenameTable(
                name: "Cities",
                newName: "City");

            migrationBuilder.RenameIndex(
                name: "IX_WaterInfos_CityId",
                table: "WaterInfo",
                newName: "IX_WaterInfo_CityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WaterInfo",
                table: "WaterInfo",
                column: "WaterInfoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_City",
                table: "City",
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
    }
}
