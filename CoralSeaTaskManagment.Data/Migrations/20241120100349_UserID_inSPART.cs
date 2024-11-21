using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoralSeaTaskManagment.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserID_inSPART : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Sparts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sparts_OrderId",
                table: "Sparts",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sparts_Orders_OrderId",
                table: "Sparts",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sparts_Orders_OrderId",
                table: "Sparts");

            migrationBuilder.DropIndex(
                name: "IX_Sparts_OrderId",
                table: "Sparts");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Sparts");
        }
    }
}
