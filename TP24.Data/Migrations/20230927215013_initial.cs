using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TP24.Data.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Receivables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Reference = table.Column<string>(type: "text", nullable: false),
                    CurrencyCode = table.Column<string>(type: "text", nullable: false),
                    IssueDate = table.Column<string>(type: "text", nullable: false),
                    OpeningValue = table.Column<decimal>(type: "numeric", nullable: false),
                    PaidValue = table.Column<decimal>(type: "numeric", nullable: false),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ClosedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Cancelled = table.Column<bool>(type: "boolean", nullable: false),
                    DebtorName = table.Column<string>(type: "text", nullable: false),
                    DebtorReference = table.Column<string>(type: "text", nullable: false),
                    DebtorAddress1 = table.Column<string>(type: "text", nullable: false),
                    DebtorAddress2 = table.Column<string>(type: "text", nullable: false),
                    DebtorTown = table.Column<string>(type: "text", nullable: false),
                    DebtorState = table.Column<string>(type: "text", nullable: false),
                    DebtorZip = table.Column<string>(type: "text", nullable: false),
                    DebtorCountryCode = table.Column<string>(type: "text", nullable: false),
                    DebtorRegistrationNumber = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receivables", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Receivables");
        }
    }
}
