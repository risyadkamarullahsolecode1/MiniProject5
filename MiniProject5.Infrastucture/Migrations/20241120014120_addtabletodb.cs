using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MiniProject5.Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class addtabletodb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "location",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    location = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_location", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "departments",
                columns: table => new
                {
                    deptno = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    deptname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    mgrempno = table.Column<int>(type: "integer", nullable: true),
                    location = table.Column<int>(type: "integer", maxLength: 100, nullable: true),
                    spvempno = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departments", x => x.deptno);
                    table.ForeignKey(
                        name: "FK_departments_location_location",
                        column: x => x.location,
                        principalTable: "location",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    empno = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    lname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    address = table.Column<string>(type: "character varying", nullable: false),
                    dob = table.Column<DateOnly>(type: "date", nullable: false),
                    sex = table.Column<string>(type: "character varying", nullable: true),
                    phonenumber = table.Column<int>(type: "integer", nullable: true),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    position = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    deptno = table.Column<int>(type: "integer", nullable: true),
                    employeetype = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    level = table.Column<int>(type: "integer", nullable: true),
                    lastupdateddate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    nik = table.Column<int>(type: "integer", nullable: true),
                    status = table.Column<string>(type: "character varying", nullable: true),
                    statusreason = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    salary = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.empno);
                    table.ForeignKey(
                        name: "FK_employees_departments_deptno",
                        column: x => x.deptno,
                        principalTable: "departments",
                        principalColumn: "deptno");
                });

            migrationBuilder.CreateTable(
                name: "projects",
                columns: table => new
                {
                    projno = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    projname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    deptno = table.Column<int>(type: "integer", nullable: true),
                    Projectlocation = table.Column<int>(type: "integer", nullable: true),
                    LocationNavigationId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projects", x => x.projno);
                    table.ForeignKey(
                        name: "FK_projects_departments_deptno",
                        column: x => x.deptno,
                        principalTable: "departments",
                        principalColumn: "deptno");
                    table.ForeignKey(
                        name: "FK_projects_location_LocationNavigationId",
                        column: x => x.LocationNavigationId,
                        principalTable: "location",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "dependents",
                columns: table => new
                {
                    dependentno = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    sex = table.Column<string>(type: "character varying", nullable: true),
                    dob = table.Column<DateOnly>(type: "date", nullable: false),
                    relationship = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    empno = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dependents", x => x.dependentno);
                    table.ForeignKey(
                        name: "FK_dependents_employees_empno",
                        column: x => x.empno,
                        principalTable: "employees",
                        principalColumn: "empno");
                });

            migrationBuilder.CreateTable(
                name: "workson",
                columns: table => new
                {
                    empno = table.Column<int>(type: "integer", nullable: false),
                    projno = table.Column<int>(type: "integer", nullable: false),
                    dateworked = table.Column<DateOnly>(type: "date", nullable: false),
                    hoursworked = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("workson_pkey", x => new { x.empno, x.projno });
                    table.ForeignKey(
                        name: "workson_empno_fkey",
                        column: x => x.empno,
                        principalTable: "employees",
                        principalColumn: "empno",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "workson_projno_fkey",
                        column: x => x.projno,
                        principalTable: "projects",
                        principalColumn: "projno",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_departments_location",
                table: "departments",
                column: "location");

            migrationBuilder.CreateIndex(
                name: "IX_departments_mgrempno",
                table: "departments",
                column: "mgrempno");

            migrationBuilder.CreateIndex(
                name: "IX_departments_spvempno",
                table: "departments",
                column: "spvempno");

            migrationBuilder.CreateIndex(
                name: "IX_dependents_empno",
                table: "dependents",
                column: "empno");

            migrationBuilder.CreateIndex(
                name: "IX_employees_deptno",
                table: "employees",
                column: "deptno");

            migrationBuilder.CreateIndex(
                name: "IX_projects_deptno",
                table: "projects",
                column: "deptno");

            migrationBuilder.CreateIndex(
                name: "IX_projects_LocationNavigationId",
                table: "projects",
                column: "LocationNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_workson_projno",
                table: "workson",
                column: "projno");

            migrationBuilder.AddForeignKey(
                name: "FK_departments_employees_mgrempno",
                table: "departments",
                column: "mgrempno",
                principalTable: "employees",
                principalColumn: "empno");

            migrationBuilder.AddForeignKey(
                name: "FK_departments_employees_spvempno",
                table: "departments",
                column: "spvempno",
                principalTable: "employees",
                principalColumn: "empno");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_departments_employees_mgrempno",
                table: "departments");

            migrationBuilder.DropForeignKey(
                name: "FK_departments_employees_spvempno",
                table: "departments");

            migrationBuilder.DropTable(
                name: "dependents");

            migrationBuilder.DropTable(
                name: "workson");

            migrationBuilder.DropTable(
                name: "projects");

            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "departments");

            migrationBuilder.DropTable(
                name: "location");
        }
    }
}
