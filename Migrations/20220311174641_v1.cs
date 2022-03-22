using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PROJEKAT_WEB.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Festivali",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adresa = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Grad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    opisFestivala = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumPocetka = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumKraja = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Festivali", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Rezervacije",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UkupnaCena = table.Column<float>(type: "real", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervacije", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DaniFesta",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CenaZaDan = table.Column<float>(type: "real", nullable: false),
                    datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FestivalID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaniFesta", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DaniFesta_Festivali_FestivalID",
                        column: x => x.FestivalID,
                        principalTable: "Festivali",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Karte",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ulaznica = table.Column<int>(type: "int", nullable: false),
                    DanID = table.Column<int>(type: "int", nullable: true),
                    RezervacijaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Karte", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Karte_DaniFesta_DanID",
                        column: x => x.DanID,
                        principalTable: "DaniFesta",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Karte_Rezervacije_RezervacijaID",
                        column: x => x.RezervacijaID,
                        principalTable: "Rezervacije",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Nastupi",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImeIzvodjaca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    vreme = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DanID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nastupi", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Nastupi_DaniFesta_DanID",
                        column: x => x.DanID,
                        principalTable: "DaniFesta",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DaniFesta_FestivalID",
                table: "DaniFesta",
                column: "FestivalID");

            migrationBuilder.CreateIndex(
                name: "IX_Karte_DanID",
                table: "Karte",
                column: "DanID");

            migrationBuilder.CreateIndex(
                name: "IX_Karte_RezervacijaID",
                table: "Karte",
                column: "RezervacijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Nastupi_DanID",
                table: "Nastupi",
                column: "DanID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Karte");

            migrationBuilder.DropTable(
                name: "Nastupi");

            migrationBuilder.DropTable(
                name: "Rezervacije");

            migrationBuilder.DropTable(
                name: "DaniFesta");

            migrationBuilder.DropTable(
                name: "Festivali");
        }
    }
}
