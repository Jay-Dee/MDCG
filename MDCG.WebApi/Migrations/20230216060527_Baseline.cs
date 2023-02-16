using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MDCG.WebApi.Migrations
{
    public partial class Baseline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EquitySpotMarketDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinesssDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ticker = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    LongName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Bid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Ask = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Mid = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquitySpotMarketDatas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FxSpotMarketDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinesssDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseCurrency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CounterCurrency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Ask = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Mid = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FxSpotMarketDatas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CanRead = table.Column<bool>(type: "bit", nullable: false),
                    CanWrite = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquitySpotMarketDatas");

            migrationBuilder.DropTable(
                name: "FxSpotMarketDatas");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
