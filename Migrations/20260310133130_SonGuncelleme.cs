using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SporKulubu.Migrations
{
    /// <inheritdoc />
    public partial class SonGuncelleme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "KiyafetVerildiMi",
                table: "Uyeler",
                type: "INTEGER",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<DateTime>(
                name: "KayitTarihi",
                table: "Uyeler",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Branş",
                table: "Uyeler",
                type: "TEXT",
                nullable: false,
                defaultValue: "Voleybol",
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<decimal>(
                name: "AylikAidat",
                table: "Uyeler",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<decimal>(
                name: "Tutar",
                table: "Aidatlar",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<bool>(
                name: "OdendiMi",
                table: "Aidatlar",
                type: "INTEGER",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_Uyeler_AdSoyad",
                table: "Uyeler",
                column: "AdSoyad");

            migrationBuilder.CreateIndex(
                name: "IX_Uyeler_Telefon",
                table: "Uyeler",
                column: "Telefon");

            migrationBuilder.CreateIndex(
                name: "IX_Aidatlar_OdemeTarihi",
                table: "Aidatlar",
                column: "OdemeTarihi");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Uyeler_AdSoyad",
                table: "Uyeler");

            migrationBuilder.DropIndex(
                name: "IX_Uyeler_Telefon",
                table: "Uyeler");

            migrationBuilder.DropIndex(
                name: "IX_Aidatlar_OdemeTarihi",
                table: "Aidatlar");

            migrationBuilder.AlterColumn<bool>(
                name: "KiyafetVerildiMi",
                table: "Uyeler",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "KayitTarihi",
                table: "Uyeler",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<string>(
                name: "Branş",
                table: "Uyeler",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldDefaultValue: "Voleybol");

            migrationBuilder.AlterColumn<decimal>(
                name: "AylikAidat",
                table: "Uyeler",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Tutar",
                table: "Aidatlar",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<bool>(
                name: "OdendiMi",
                table: "Aidatlar",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER",
                oldDefaultValue: false);
        }
    }
}
