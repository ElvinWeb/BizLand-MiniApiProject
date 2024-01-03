using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProjectDateTypeChangedToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProjectDate",
                table: "Portfolios",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<bool>(
                name: "IsPoster",
                table: "PortfolioImages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPoster",
                table: "PortfolioImages");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ProjectDate",
                table: "Portfolios",
                type: "datetime2",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);
        }
    }
}
