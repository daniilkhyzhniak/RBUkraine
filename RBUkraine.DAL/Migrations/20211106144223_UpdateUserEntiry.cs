using Microsoft.EntityFrameworkCore.Migrations;

namespace RBUkraine.DAL.Migrations
{
    public partial class UpdateUserEntiry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IncludeInRating",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$4nxos/Ucl5eIu8xARFRggeKCkDIU3QOcenQGNv5b2DXhNw6XrmzAi");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$BaTiY0R2XhfAVr4LpHOa7.a2fvFeypwkuQTwGooNQXXwKj36IlDqO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IncludeInRating",
                table: "Users");

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
    }
}
