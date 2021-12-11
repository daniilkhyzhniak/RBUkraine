using Microsoft.EntityFrameworkCore.Migrations;

namespace RBUkraine.DAL.Migrations
{
    public partial class AddBonusReceivedField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBonusReceived",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$SFHzy5MyGvDlpIhKn6xloeofBFxS7GrNPL3CjDfrrBWJPnrPOKQEO");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$XYlJAibWNR5HiH7WKbMIDuvaYzbozObcWUCbgPn9Ev8ZMw7RK12uu");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBonusReceived",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$5OgMNHdLwgDQBM6OoifhHeLmXpp.RTeVqV6LhAaiFZwo2OE7.hXoK");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$qmz3ykHIrzphuOpWcNjUfeiKoGYOqEsSke.9URtgizlKMfDxJ2r9K");
        }
    }
}
