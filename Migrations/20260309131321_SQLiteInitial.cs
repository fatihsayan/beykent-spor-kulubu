using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SporKulubu.Migrations
{
    /// <inheritdoc />
    public partial class SQLiteInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SonAidatOdemeTarihi",
                table: "Uyeler",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SonAidatOdemeTarihi",
                table: "Uyeler");
        }
    }
}
