using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RBUkraine.DAL.Migrations
{
    public partial class UpdateDatabase5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "PurchaseDate",
                table: "CharityEventPurchases",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$6DZmRewPKxObbOaxfTNsJO8VJtThKtSn3K4cp3mWhfDn6habW7eCa");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$XH85e2/qv6DvZG86yCBvwedrdegKnSytlUmh4o.RgGTFwZRt9NuW6");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchaseDate",
                table: "CharityEventPurchases");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$uZSXVvzvRGMFfOcon3PTeeaM1k0JH8IPzD9oOkDI7NJWMLZGgmHVm");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$pAuAszX1WJOyPUc5Bn7cy.VQBsZhPAVtejpqXW3c/AqjsWPgepQcK");
        }
    }
}
