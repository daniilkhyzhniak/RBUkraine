using Microsoft.EntityFrameworkCore.Migrations;

namespace RBUkraine.DAL.Migrations
{
    public partial class UpdateDonationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "СharitableСontributions");

            migrationBuilder.CreateTable(
                name: "Donations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimalId = table.Column<int>(type: "int", nullable: true),
                    CharitableOrganizationId = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Donations_Animals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Donations_CharitableOrganizations_CharitableOrganizationId",
                        column: x => x.CharitableOrganizationId,
                        principalTable: "CharitableOrganizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Donations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "IX_Donations_AnimalId",
                table: "Donations",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_Donations_CharitableOrganizationId",
                table: "Donations",
                column: "CharitableOrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Donations_UserId",
                table: "Donations",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Donations");

            migrationBuilder.CreateTable(
                name: "СharitableСontributions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    AnimalId = table.Column<int>(type: "int", nullable: true),
                    CharitableOrganizationId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_СharitableСontributions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_СharitableСontributions_Animals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_СharitableСontributions_CharitableOrganizations_CharitableOrganizationId",
                        column: x => x.CharitableOrganizationId,
                        principalTable: "CharitableOrganizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_СharitableСontributions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$tTmmnD21dMI1pNPEJt2eTuZ0i37tpD23RTz.YtA0pdmk2UzCfnMOG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$zVoHvh.3P3k/3plEzHcYc.4GGXaI/Z.DdRi.7inhE8CFsOxOBf3q6");

            migrationBuilder.CreateIndex(
                name: "IX_СharitableСontributions_AnimalId",
                table: "СharitableСontributions",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_СharitableСontributions_CharitableOrganizationId",
                table: "СharitableСontributions",
                column: "CharitableOrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_СharitableСontributions_UserId",
                table: "СharitableСontributions",
                column: "UserId");
        }
    }
}
