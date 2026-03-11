using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SporKulubu.Migrations
{
    /// <inheritdoc />
    public partial class AntrenorTakim_SporSalonuId_Ekle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AntrenorTakimlar_Takimlar_TakimId",
                table: "AntrenorTakimlar");

            migrationBuilder.AlterColumn<int>(
                name: "TakimId",
                table: "AntrenorTakimlar",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "SporSalonuId",
                table: "AntrenorTakimlar",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AntrenorTakimlar_SporSalonuId",
                table: "AntrenorTakimlar",
                column: "SporSalonuId");

            migrationBuilder.AddForeignKey(
                name: "FK_AntrenorTakimlar_SporSalonlari_SporSalonuId",
                table: "AntrenorTakimlar",
                column: "SporSalonuId",
                principalTable: "SporSalonlari",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_AntrenorTakimlar_Takimlar_TakimId",
                table: "AntrenorTakimlar",
                column: "TakimId",
                principalTable: "Takimlar",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AntrenorTakimlar_SporSalonlari_SporSalonuId",
                table: "AntrenorTakimlar");

            migrationBuilder.DropForeignKey(
                name: "FK_AntrenorTakimlar_Takimlar_TakimId",
                table: "AntrenorTakimlar");

            migrationBuilder.DropIndex(
                name: "IX_AntrenorTakimlar_SporSalonuId",
                table: "AntrenorTakimlar");

            migrationBuilder.DropColumn(
                name: "SporSalonuId",
                table: "AntrenorTakimlar");

            migrationBuilder.AlterColumn<int>(
                name: "TakimId",
                table: "AntrenorTakimlar",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AntrenorTakimlar_Takimlar_TakimId",
                table: "AntrenorTakimlar",
                column: "TakimId",
                principalTable: "Takimlar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
