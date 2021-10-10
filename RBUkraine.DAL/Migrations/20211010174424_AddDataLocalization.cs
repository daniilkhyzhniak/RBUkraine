using Microsoft.EntityFrameworkCore.Migrations;

namespace RBUkraine.DAL.Migrations
{
    public partial class AddDataLocalization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "CharityEvents",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateTable(
                name: "AnimalTranslates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimalId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Language = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalTranslates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnimalTranslates_Animals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "Id", "Description", "IsDeleted", "LatinName", "Name", "Population" },
                values: new object[] { 1000, "Природоохранный статус вида: Пропавший в природе.", false, "Bison bonasus", "Зубр", 200 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$D/xQsLyq4CAMaTvl7EjGsO9IPBvXaSOIZaEJI9wcsDhI5AsLAl8Nu");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$cuZi1N5QNCSScfNPrjGqG.r2qoh1nmnWKt7mJ0zr0UJ8SRcAIUKWK");

            migrationBuilder.InsertData(
                table: "AnimalTranslates",
                columns: new[] { "Id", "AnimalId", "Description", "IsDeleted", "Language", "Name" },
                values: new object[] { 1, 1000, "Conservation status of the species: Extinct in nature.", false, 1, "Bison" });

            migrationBuilder.CreateIndex(
                name: "IX_AnimalTranslates_AnimalId",
                table: "AnimalTranslates",
                column: "AnimalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimalTranslates");

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 1000);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "CharityEvents",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");

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
    }
}
