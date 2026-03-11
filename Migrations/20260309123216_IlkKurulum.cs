using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SporKulubu.Migrations
{
    /// <inheritdoc />
    public partial class IlkKurulum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SporSalonlari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Ad = table.Column<string>(type: "TEXT", nullable: false),
                    Adres = table.Column<string>(type: "TEXT", nullable: true),
                    Telefon = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SporSalonlari", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Takimlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Ad = table.Column<string>(type: "TEXT", nullable: false),
                    SporSalonuId = table.Column<int>(type: "INTEGER", nullable: false),
                    AntrenorAdi = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Takimlar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Takimlar_SporSalonlari_SporSalonuId",
                        column: x => x.SporSalonuId,
                        principalTable: "SporSalonlari",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gruplar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Ad = table.Column<string>(type: "TEXT", nullable: false),
                    TakimId = table.Column<int>(type: "INTEGER", nullable: false),
                    Kontenjan = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gruplar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gruplar_Takimlar_TakimId",
                        column: x => x.TakimId,
                        principalTable: "Takimlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Uyeler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AdSoyad = table.Column<string>(type: "TEXT", nullable: false),
                    Telefon = table.Column<string>(type: "TEXT", nullable: false),
                    DogumTarihi = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Branş = table.Column<string>(type: "TEXT", nullable: false),
                    KayitTarihi = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AylikAidat = table.Column<decimal>(type: "TEXT", nullable: false),
                    VeliAdSoyad = table.Column<string>(type: "TEXT", nullable: false),
                    VeliTelefon = table.Column<string>(type: "TEXT", nullable: false),
                    SporSalonuId = table.Column<int>(type: "INTEGER", nullable: true),
                    TakimId = table.Column<int>(type: "INTEGER", nullable: true),
                    GrupId = table.Column<int>(type: "INTEGER", nullable: true),
                    KiyafetVerildiMi = table.Column<bool>(type: "INTEGER", nullable: false),
                    KiyafetVerilisTarihi = table.Column<DateTime>(type: "TEXT", nullable: true),
                    KiyafetNotlari = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uyeler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Uyeler_Gruplar_GrupId",
                        column: x => x.GrupId,
                        principalTable: "Gruplar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Uyeler_SporSalonlari_SporSalonuId",
                        column: x => x.SporSalonuId,
                        principalTable: "SporSalonlari",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Uyeler_Takimlar_TakimId",
                        column: x => x.TakimId,
                        principalTable: "Takimlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Aidatlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UyeId = table.Column<int>(type: "INTEGER", nullable: false),
                    OdemeTarihi = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Tutar = table.Column<decimal>(type: "TEXT", nullable: false),
                    Ay = table.Column<string>(type: "TEXT", nullable: true),
                    OdendiMi = table.Column<bool>(type: "INTEGER", nullable: false),
                    Aciklama = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aidatlar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aidatlar_Uyeler_UyeId",
                        column: x => x.UyeId,
                        principalTable: "Uyeler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aidatlar_UyeId",
                table: "Aidatlar",
                column: "UyeId");

            migrationBuilder.CreateIndex(
                name: "IX_Gruplar_TakimId",
                table: "Gruplar",
                column: "TakimId");

            migrationBuilder.CreateIndex(
                name: "IX_Takimlar_SporSalonuId",
                table: "Takimlar",
                column: "SporSalonuId");

            migrationBuilder.CreateIndex(
                name: "IX_Uyeler_GrupId",
                table: "Uyeler",
                column: "GrupId");

            migrationBuilder.CreateIndex(
                name: "IX_Uyeler_SporSalonuId",
                table: "Uyeler",
                column: "SporSalonuId");

            migrationBuilder.CreateIndex(
                name: "IX_Uyeler_TakimId",
                table: "Uyeler",
                column: "TakimId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aidatlar");

            migrationBuilder.DropTable(
                name: "Uyeler");

            migrationBuilder.DropTable(
                name: "Gruplar");

            migrationBuilder.DropTable(
                name: "Takimlar");

            migrationBuilder.DropTable(
                name: "SporSalonlari");
        }
    }
}
