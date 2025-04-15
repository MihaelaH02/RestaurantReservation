using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantReservation.Migrations
{
    public partial class UpdateMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ARG_RESTAURANT_SEARCH_FILTERS",
                columns: table => new
                {
                    UUID = table.Column<Guid>(name: "UUID",type: "uniqueidentifier", nullable: false),
                    FilterType = table.Column<short>(name: "FILER_TYPE", type: "smallint", nullable: false),
                    FilterValue = table.Column<string>(name: "FILTER_VALUE", type: "nvarchar(32)", nullable: false)
                }
            );

            migrationBuilder.CreateTable(
                name: "ARG_PASSWORD_RESER_TOKENS",
                columns: table => new
                {
                    Id = table.Column<Guid>(name: "UUID", type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(name: "EMAIL", type: "nvarchar(32)", nullable: false),
                    Token = table.Column<string>(name: "TOKEN", type: "nvarchar(16)", nullable: false),
                    ExpiresAt = table.Column<DateTime>(name: "EXPIRE_AT", type: "datetime", nullable: false)
                }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ARG_RESTAURANT_SEARCH_FILTERS");

            migrationBuilder.DropTable(
                name: "ARG_PASSWORD_RESER_TOKENS");
        }
    }
}
