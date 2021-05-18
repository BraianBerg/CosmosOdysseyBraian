using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CosmosOdyssey.Data.Migrations
{
    public partial class proov46 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PriceListDomains",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ValidUntil = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceListDomains", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdAi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PriceListDomainId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdAi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdAi_PriceListDomains_PriceListDomainId",
                        column: x => x.PriceListDomainId,
                        principalTable: "PriceListDomains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProviderAllDomains",
                columns: table => new
                {
                    ProviderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    FlightStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FlightEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    To = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    From = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FromId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RouteInfoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Distance = table.Column<long>(type: "bigint", nullable: false),
                    LegId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriceListDomainId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProviderAllDomains", x => x.ProviderId);
                    table.ForeignKey(
                        name: "FK_ProviderAllDomains_PriceListDomains_PriceListDomainId",
                        column: x => x.PriceListDomainId,
                        principalTable: "PriceListDomains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IdAi_PriceListDomainId",
                table: "IdAi",
                column: "PriceListDomainId");

            migrationBuilder.CreateIndex(
                name: "IX_ProviderAllDomains_PriceListDomainId",
                table: "ProviderAllDomains",
                column: "PriceListDomainId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdAi");

            migrationBuilder.DropTable(
                name: "ProviderAllDomains");

            migrationBuilder.DropTable(
                name: "PriceListDomains");
        }
    }
}
