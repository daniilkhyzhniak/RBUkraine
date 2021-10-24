using Microsoft.EntityFrameworkCore.Migrations;

namespace RBUkraine.DAL.Migrations
{
    public partial class AddDescriptionForAnimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AnimalTranslates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Animals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$Kd9IGH6v.oTkrsnZYEPOaucnbbOOmYVC4nekSy5xTrtTweP9H5E52");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$20BfRqOHpxO2b83Nx4.lBuzhCjyp8xo/vL/cjCviu4wZnlXEQMghS");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "AnimalTranslates");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Animals");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$ElNpkHIeX1Ycbz1yFV0Y6.wpevV2mgUVMPt0SfZFtDa2NUYpUXvfC");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$tv77ehWrQIYiRlk1Lex.7O3o7JIyLPq2rX1lDP3oEE4YXz5KRVbO.");
        }
    }
}
