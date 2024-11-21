using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoralSeaTaskManagment.Data.Migrations
{
    /// <inheritdoc />
    public partial class items : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Namear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EcategoryId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstallDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Warranty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EfamilyId = table.Column<int>(type: "int", nullable: false),
                    statusFlag = table.Column<int>(type: "int", nullable: false),
                    EstatusId = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<int>(type: "int", nullable: false),
                    WarrantyYear = table.Column<int>(type: "int", nullable: false),
                    WarrantyStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EclassId = table.Column<int>(type: "int", nullable: false),
                    Agent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WarrantyFlag = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Items_Ecategories_EcategoryId",
                        column: x => x.EcategoryId,
                        principalTable: "Ecategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Items_Eclasses_EclassId",
                        column: x => x.EclassId,
                        principalTable: "Eclasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Items_Efamilies_EfamilyId",
                        column: x => x.EfamilyId,
                        principalTable: "Efamilies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Items_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Items_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_DepartmentId",
                table: "Items",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_EcategoryId",
                table: "Items",
                column: "EcategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_EclassId",
                table: "Items",
                column: "EclassId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_EfamilyId",
                table: "Items",
                column: "EfamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_HotelId",
                table: "Items",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_LocationId",
                table: "Items",
                column: "LocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");
        }
    }
}
