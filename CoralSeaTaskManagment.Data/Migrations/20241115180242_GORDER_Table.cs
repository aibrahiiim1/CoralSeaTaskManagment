using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoralSeaTaskManagment.Data.Migrations
{
    /// <inheritdoc />
    public partial class GORDER_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gorders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OtypeId = table.Column<int>(type: "int", nullable: false),
                    GroomsId = table.Column<int>(type: "int", nullable: false),
                    GlocationId = table.Column<int>(type: "int", nullable: false),
                    GitemId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    OstatusId = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GdepartmentFId = table.Column<int>(type: "int", nullable: false),
                    AssignFlag = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gorders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gorders_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Gorders_Departments_GdepartmentFId",
                        column: x => x.GdepartmentFId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Gorders_Gitems_GitemId",
                        column: x => x.GitemId,
                        principalTable: "Gitems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Gorders_Glocations_GlocationId",
                        column: x => x.GlocationId,
                        principalTable: "Glocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Gorders_Grooms_GroomsId",
                        column: x => x.GroomsId,
                        principalTable: "Grooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Gorders_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Gorders_Otypes_OtypeId",
                        column: x => x.OtypeId,
                        principalTable: "Otypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gorders_DepartmentId",
                table: "Gorders",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Gorders_GdepartmentFId",
                table: "Gorders",
                column: "GdepartmentFId");

            migrationBuilder.CreateIndex(
                name: "IX_Gorders_GitemId",
                table: "Gorders",
                column: "GitemId");

            migrationBuilder.CreateIndex(
                name: "IX_Gorders_GlocationId",
                table: "Gorders",
                column: "GlocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Gorders_GroomsId",
                table: "Gorders",
                column: "GroomsId");

            migrationBuilder.CreateIndex(
                name: "IX_Gorders_HotelId",
                table: "Gorders",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Gorders_OtypeId",
                table: "Gorders",
                column: "OtypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gorders");
        }
    }
}
