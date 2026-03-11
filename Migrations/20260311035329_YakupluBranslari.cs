using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SporKulubu.Migrations
{
    /// <inheritdoc />
    public partial class YakupluBranslari : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AntrenmanProgramlari_Gruplar_GrupId",
                table: "AntrenmanProgramlari");

            migrationBuilder.DropForeignKey(
                name: "FK_AntrenorTakimlar_Gruplar_GrupId",
                table: "AntrenorTakimlar");

            migrationBuilder.AlterColumn<string>(
                name: "Branş",
                table: "Uyeler",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldDefaultValue: "Voleybol");

            migrationBuilder.CreateIndex(
                name: "IX_Antrenorler_Email",
                table: "Antrenorler",
                column: "Email",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AntrenmanProgramlari_Gruplar_GrupId",
                table: "AntrenmanProgramlari",
                column: "GrupId",
                principalTable: "Gruplar",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_AntrenorTakimlar_Gruplar_GrupId",
                table: "AntrenorTakimlar",
                column: "GrupId",
                principalTable: "Gruplar",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AntrenmanProgramlari_Gruplar_GrupId",
                table: "AntrenmanProgramlari");

            migrationBuilder.DropForeignKey(
                name: "FK_AntrenorTakimlar_Gruplar_GrupId",
                table: "AntrenorTakimlar");

            migrationBuilder.DropIndex(
                name: "IX_Antrenorler_Email",
                table: "Antrenorler");

            migrationBuilder.AlterColumn<string>(
                name: "Branş",
                table: "Uyeler",
                type: "TEXT",
                nullable: false,
                defaultValue: "Voleybol",
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_AntrenmanProgramlari_Gruplar_GrupId",
                table: "AntrenmanProgramlari",
                column: "GrupId",
                principalTable: "Gruplar",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AntrenorTakimlar_Gruplar_GrupId",
                table: "AntrenorTakimlar",
                column: "GrupId",
                principalTable: "Gruplar",
                principalColumn: "Id");
        }
    }
}
