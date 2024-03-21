using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerLoan.API.Migrations
{
    /// <inheritdoc />
    public partial class LastMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConversionRate",
                table: "Loans");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ConversionRate",
                table: "Loans",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
