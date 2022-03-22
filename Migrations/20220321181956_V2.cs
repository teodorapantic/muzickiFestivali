using Microsoft.EntityFrameworkCore.Migrations;

namespace PROJEKAT_WEB.Migrations
{
    public partial class V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FestivalID",
                table: "Rezervacije",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacije_FestivalID",
                table: "Rezervacije",
                column: "FestivalID");

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervacije_Festivali_FestivalID",
                table: "Rezervacije",
                column: "FestivalID",
                principalTable: "Festivali",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacije_Festivali_FestivalID",
                table: "Rezervacije");

            migrationBuilder.DropIndex(
                name: "IX_Rezervacije_FestivalID",
                table: "Rezervacije");

            migrationBuilder.DropColumn(
                name: "FestivalID",
                table: "Rezervacije");
        }
    }
}
