using Microsoft.EntityFrameworkCore.Migrations;

namespace RBUkraine.DAL.Migrations
{
    public partial class AddAnimalImagesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnimalDetailsAnimalId",
                table: "Animals",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AnimalImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnimalId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnimalImages_Animals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$viPjhbP5SlO/InmtJlWxcO.51uU/MYUe8BTg9ov8/Ol4v911mznB2");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$TjDdkK9fX4i4Yi0tXCLQX..YcDZ3.GX29c4OuZ9rHnETn0dakuBe2");

            migrationBuilder.CreateIndex(
                name: "IX_Animals_AnimalDetailsAnimalId",
                table: "Animals",
                column: "AnimalDetailsAnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalImages_AnimalId",
                table: "AnimalImages",
                column: "AnimalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_AnimalDetails_AnimalDetailsAnimalId",
                table: "Animals",
                column: "AnimalDetailsAnimalId",
                principalTable: "AnimalDetails",
                principalColumn: "AnimalId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_AnimalDetails_AnimalDetailsAnimalId",
                table: "Animals");

            migrationBuilder.DropTable(
                name: "AnimalImages");

            migrationBuilder.DropIndex(
                name: "IX_Animals_AnimalDetailsAnimalId",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "AnimalDetailsAnimalId",
                table: "Animals");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$uemooDCKVF2WOsc3R5U5VOK.q3dystoYVDHbVB2Joy0jDcH4c28Ey");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$DCp3pXzoURevRfOpK/93SOytesMpDUyloA3u4Rjsdq3x88GyllGKS");
        }
    }
}
