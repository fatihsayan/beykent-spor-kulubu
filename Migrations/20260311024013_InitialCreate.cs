using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SporKulubu.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BransId",
                table: "Uyeler",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BransId",
                table: "Takimlar",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AntrenmanProgramlari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TakimId = table.Column<int>(type: "INTEGER", nullable: false),
                    GrupId = table.Column<int>(type: "INTEGER", nullable: true),
                    Gun = table.Column<string>(type: "TEXT", nullable: false),
                    Baslangic = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    Bitis = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    Salon = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntrenmanProgramlari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AntrenmanProgramlari_Gruplar_GrupId",
                        column: x => x.GrupId,
                        principalTable: "Gruplar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AntrenmanProgramlari_Takimlar_TakimId",
                        column: x => x.TakimId,
                        principalTable: "Takimlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Antrenorler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AdSoyad = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Telefon = table.Column<string>(type: "TEXT", nullable: false),
                    Uzmanlik = table.Column<string>(type: "TEXT", nullable: false),
                    Sifre = table.Column<string>(type: "TEXT", nullable: false),
                    ProfilResmi = table.Column<string>(type: "TEXT", nullable: true),
                    KayitTarihi = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Antrenorler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Branslar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Ad = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    TakimVarMi = table.Column<bool>(type: "INTEGER", nullable: false),
                    GrupVarMi = table.Column<bool>(type: "INTEGER", nullable: false),
                    SporSalonuId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branslar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Branslar_SporSalonlari_SporSalonuId",
                        column: x => x.SporSalonuId,
                        principalTable: "SporSalonlari",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AntrenorTakimlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AntrenorId = table.Column<int>(type: "INTEGER", nullable: false),
                    TakimId = table.Column<int>(type: "INTEGER", nullable: false),
                    GrupId = table.Column<int>(type: "INTEGER", nullable: true),
                    AtanmaTarihi = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntrenorTakimlar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AntrenorTakimlar_Antrenorler_AntrenorId",
                        column: x => x.AntrenorId,
                        principalTable: "Antrenorler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AntrenorTakimlar_Gruplar_GrupId",
                        column: x => x.GrupId,
                        principalTable: "Gruplar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AntrenorTakimlar_Takimlar_TakimId",
                        column: x => x.TakimId,
                        principalTable: "Takimlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Yoklamalar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AntrenorId = table.Column<int>(type: "INTEGER", nullable: false),
                    UyeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Tarih = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Durum = table.Column<string>(type: "TEXT", nullable: false),
                    Not = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Yoklamalar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Yoklamalar_Antrenorler_AntrenorId",
                        column: x => x.AntrenorId,
                        principalTable: "Antrenorler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Yoklamalar_Uyeler_UyeId",
                        column: x => x.UyeId,
                        principalTable: "Uyeler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Uyeler_BransId",
                table: "Uyeler",
                column: "BransId");

            migrationBuilder.CreateIndex(
                name: "IX_Takimlar_BransId",
                table: "Takimlar",
                column: "BransId");

            migrationBuilder.CreateIndex(
                name: "IX_AntrenmanProgramlari_GrupId",
                table: "AntrenmanProgramlari",
                column: "GrupId");

            migrationBuilder.CreateIndex(
                name: "IX_AntrenmanProgramlari_TakimId",
                table: "AntrenmanProgramlari",
                column: "TakimId");

            migrationBuilder.CreateIndex(
                name: "IX_AntrenorTakimlar_AntrenorId",
                table: "AntrenorTakimlar",
                column: "AntrenorId");

            migrationBuilder.CreateIndex(
                name: "IX_AntrenorTakimlar_GrupId",
                table: "AntrenorTakimlar",
                column: "GrupId");

            migrationBuilder.CreateIndex(
                name: "IX_AntrenorTakimlar_TakimId",
                table: "AntrenorTakimlar",
                column: "TakimId");

            migrationBuilder.CreateIndex(
                name: "IX_Branslar_SporSalonuId",
                table: "Branslar",
                column: "SporSalonuId");

            migrationBuilder.CreateIndex(
                name: "IX_Yoklamalar_AntrenorId",
                table: "Yoklamalar",
                column: "AntrenorId");

            migrationBuilder.CreateIndex(
                name: "IX_Yoklamalar_UyeId",
                table: "Yoklamalar",
                column: "UyeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Takimlar_Branslar_BransId",
                table: "Takimlar",
                column: "BransId",
                principalTable: "Branslar",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Uyeler_Branslar_BransId",
                table: "Uyeler",
                column: "BransId",
                principalTable: "Branslar",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Takimlar_Branslar_BransId",
                table: "Takimlar");

            migrationBuilder.DropForeignKey(
                name: "FK_Uyeler_Branslar_BransId",
                table: "Uyeler");

            migrationBuilder.DropTable(
                name: "AntrenmanProgramlari");

            migrationBuilder.DropTable(
                name: "AntrenorTakimlar");

            migrationBuilder.DropTable(
                name: "Branslar");

            migrationBuilder.DropTable(
                name: "Yoklamalar");

            migrationBuilder.DropTable(
                name: "Antrenorler");

            migrationBuilder.DropIndex(
                name: "IX_Uyeler_BransId",
                table: "Uyeler");

            migrationBuilder.DropIndex(
                name: "IX_Takimlar_BransId",
                table: "Takimlar");

            migrationBuilder.DropColumn(
                name: "BransId",
                table: "Uyeler");

            migrationBuilder.DropColumn(
                name: "BransId",
                table: "Takimlar");
        }
    }
}
