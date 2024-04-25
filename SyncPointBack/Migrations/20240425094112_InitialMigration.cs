using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyncPointBack.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExcelRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TicketId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Other = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumOfPages = table.Column<int>(type: "int", nullable: true),
                    NumOfChanges = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductionTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcelRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GNB",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExcelRecordId = table.Column<int>(type: "int", nullable: false),
                    gnb = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GNB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GNB_ExcelRecords_ExcelRecordId",
                        column: x => x.ExcelRecordId,
                        principalTable: "ExcelRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PDModification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExcelRecordId = table.Column<int>(type: "int", nullable: false),
                    pdMOdification = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PDModification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PDModification_ExcelRecords_ExcelRecordId",
                        column: x => x.ExcelRecordId,
                        principalTable: "ExcelRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PDRegistration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExcelRecordId = table.Column<int>(type: "int", nullable: false),
                    pdRegistration = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PDRegistration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PDRegistration_ExcelRecords_ExcelRecordId",
                        column: x => x.ExcelRecordId,
                        principalTable: "ExcelRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PIM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExcelRecordId = table.Column<int>(type: "int", nullable: false),
                    pim = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PIM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PIM_ExcelRecords_ExcelRecordId",
                        column: x => x.ExcelRecordId,
                        principalTable: "ExcelRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StaticPageCreation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExcelRecordId = table.Column<int>(type: "int", nullable: false),
                    staticPageCreation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaticPageCreation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaticPageCreation_ExcelRecords_ExcelRecordId",
                        column: x => x.ExcelRecordId,
                        principalTable: "ExcelRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StaticPageModification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExcelRecordId = table.Column<int>(type: "int", nullable: false),
                    staticPageModification = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaticPageModification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaticPageModification_ExcelRecords_ExcelRecordId",
                        column: x => x.ExcelRecordId,
                        principalTable: "ExcelRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GNB_ExcelRecordId",
                table: "GNB",
                column: "ExcelRecordId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PDModification_ExcelRecordId",
                table: "PDModification",
                column: "ExcelRecordId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PDRegistration_ExcelRecordId",
                table: "PDRegistration",
                column: "ExcelRecordId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PIM_ExcelRecordId",
                table: "PIM",
                column: "ExcelRecordId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StaticPageCreation_ExcelRecordId",
                table: "StaticPageCreation",
                column: "ExcelRecordId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StaticPageModification_ExcelRecordId",
                table: "StaticPageModification",
                column: "ExcelRecordId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GNB");

            migrationBuilder.DropTable(
                name: "PDModification");

            migrationBuilder.DropTable(
                name: "PDRegistration");

            migrationBuilder.DropTable(
                name: "PIM");

            migrationBuilder.DropTable(
                name: "StaticPageCreation");

            migrationBuilder.DropTable(
                name: "StaticPageModification");

            migrationBuilder.DropTable(
                name: "ExcelRecords");
        }
    }
}
