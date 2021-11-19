using Microsoft.EntityFrameworkCore.Migrations;

namespace RBUkraine.DAL.Migrations
{
    public partial class UpdateProductEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Animals_AnimalId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_AnimalId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "AnimalId",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "RatingItems",
                columns: table => new
                {
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$MnM01PV/dtR4LoNuKyBVWu/PRzbX38pmCuYo/L8KTAucXc/.aooJi");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$8dCNQQyhY49TC/U1KiB6GuMWLWU.paMduF0atv6QRLqdiQjsKOyLK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RatingItems");

            migrationBuilder.AddColumn<int>(
                name: "AnimalId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$HToZeRRR0/kxN45dSLi4qOMvBdZnb/KJBs4mlGJMnb.92PIv5sHPe");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$gxlwmEodvRQ1xehTA46tYO8smGlOu4QCQ5uUPmKJq0NujagrDHra6");

            migrationBuilder.CreateIndex(
                name: "IX_Products_AnimalId",
                table: "Products",
                column: "AnimalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Animals_AnimalId",
                table: "Products",
                column: "AnimalId",
                principalTable: "Animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
