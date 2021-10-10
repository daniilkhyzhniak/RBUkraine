using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RBUkraine.DAL.Migrations
{
    public partial class AddFieldsToTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateTime",
                table: "CharityEvents",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CharityEvents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Organizer",
                table: "CharityEvents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "CharityEvents",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "CharitableOrganizations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "CharitableOrganizations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LatinName",
                table: "Animals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Population",
                table: "Animals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$FM/mTdIEjMo0qejsh13sc.0hz6ClWwyksJsCQEdcjpzAxSTTnIeqO");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$jx0/q9Bj6nDHB6r/QPevlOx091PO7bX/8Dzw7tX3YiQFkiIOR4hCW");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "CharityEvents");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CharityEvents");

            migrationBuilder.DropColumn(
                name: "Organizer",
                table: "CharityEvents");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "CharityEvents");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "CharitableOrganizations");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "CharitableOrganizations");

            migrationBuilder.DropColumn(
                name: "LatinName",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "Population",
                table: "Animals");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$TTxhNeQ2eNlr5jquyM2XVu7FHQzUL5V4h9aHDNpzuj3N7D2R8llhm");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$E/bYh/gL2E08PEneAm3KD.kkbKZ0giM/Fp9CmmKqHW.tLHPcAOkWq");
        }
    }
}
