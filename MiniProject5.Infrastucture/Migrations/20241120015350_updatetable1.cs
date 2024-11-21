using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniProject5.Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class updatetable1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_departments_location_location",
                table: "departments");

            migrationBuilder.DropIndex(
                name: "IX_departments_location",
                table: "departments");

            migrationBuilder.RenameColumn(
                name: "location",
                table: "departments",
                newName: "Location");

            migrationBuilder.AddColumn<int>(
                name: "LocationNavigationId",
                table: "departments",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_departments_LocationNavigationId",
                table: "departments",
                column: "LocationNavigationId");

            migrationBuilder.AddForeignKey(
                name: "FK_departments_location_LocationNavigationId",
                table: "departments",
                column: "LocationNavigationId",
                principalTable: "location",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_departments_location_LocationNavigationId",
                table: "departments");

            migrationBuilder.DropIndex(
                name: "IX_departments_LocationNavigationId",
                table: "departments");

            migrationBuilder.DropColumn(
                name: "LocationNavigationId",
                table: "departments");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "departments",
                newName: "location");

            migrationBuilder.CreateIndex(
                name: "IX_departments_location",
                table: "departments",
                column: "location");

            migrationBuilder.AddForeignKey(
                name: "FK_departments_location_location",
                table: "departments",
                column: "location",
                principalTable: "location",
                principalColumn: "Id");
        }
    }
}
