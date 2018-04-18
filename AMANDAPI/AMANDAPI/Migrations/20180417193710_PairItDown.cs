using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AMANDAPI.Migrations
{
    public partial class PairItDown : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Source",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Tags",
                table: "Images");

            migrationBuilder.AlterColumn<float>(
                name: "Sentiment",
                table: "Images",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Sentiment",
                table: "Images",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Images",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Source",
                table: "Images",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "Images",
                nullable: true);
        }
    }
}
