using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyncPointBack.Migrations
{
    /// <inheritdoc />
    public partial class ProductionTimeChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Step 1: Add a temporary column with the new type
            migrationBuilder.AddColumn<double>(
                name: "TempProductionTime",
                table: "ExcelRecords",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            // Step 2: Copy data from the old column to the new column
            migrationBuilder.Sql(
                @"UPDATE ExcelRecords
          SET TempProductionTime = DATEDIFF(HOUR, '1970-01-01', ProductionTime)");

            // Step 3: Drop the old column
            migrationBuilder.DropColumn(
                name: "ProductionTime",
                table: "ExcelRecords");

            // Step 4: Rename the new column to the old column name
            migrationBuilder.RenameColumn(
                name: "TempProductionTime",
                table: "ExcelRecords",
                newName: "ProductionTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Step 1: Add a temporary column with the old type
            migrationBuilder.AddColumn<DateTime>(
                name: "TempProductionTime",
                table: "ExcelRecords",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            // Step 2: Copy data from the new column to the old column
            migrationBuilder.Sql(
                @"UPDATE ExcelRecords
          SET TempProductionTime = DATEADD(HOUR, ProductionTime, '1970-01-01')");

            // Step 3: Drop the new column
            migrationBuilder.DropColumn(
                name: "ProductionTime",
                table: "ExcelRecords");

            // Step 4: Rename the temporary column to the old column name
            migrationBuilder.RenameColumn(
                name: "TempProductionTime",
                table: "ExcelRecords",
                newName: "ProductionTime");
        }
    }
}