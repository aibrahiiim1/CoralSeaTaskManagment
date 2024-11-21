using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoralSeaTaskManagment.Data.Migrations
{
    /// <inheritdoc />
    public partial class update_Location : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "Locations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "HotelId",
                table: "Locations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NameAr",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_HotelId",
                table: "Locations",
                column: "HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Hotels_HotelId",
                table: "Locations",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Hotels_HotelId",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_HotelId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "HotelId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "NameAr",
                table: "Locations");
        }
    }
}
