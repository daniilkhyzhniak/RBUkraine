using Microsoft.EntityFrameworkCore.Migrations;

namespace RBUkraine.DAL.Migrations
{
    public partial class AddDataLocalizationToCharityTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CharitableOrganizationTranslate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CharitableOrganizationId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Language = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharitableOrganizationTranslate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharitableOrganizationTranslate_CharitableOrganizations_CharitableOrganizationId",
                        column: x => x.CharitableOrganizationId,
                        principalTable: "CharitableOrganizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharityEventTranslate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Organizer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CharityEventId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Language = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharityEventTranslate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharityEventTranslate_CharityEvents_CharityEventId",
                        column: x => x.CharityEventId,
                        principalTable: "CharityEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$F6Rt5KlWi3W1Z63mXnF8xer5GjeEcDpf0pIyxMcRlbQ5l/fDsukxu");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$/HZ3v9.r5yPBKuXYJuVv7.yDGrhwXQLEHERkAQP14qjjmGjshtjxy");

            migrationBuilder.CreateIndex(
                name: "IX_CharitableOrganizationTranslate_CharitableOrganizationId",
                table: "CharitableOrganizationTranslate",
                column: "CharitableOrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_CharityEventTranslate_CharityEventId",
                table: "CharityEventTranslate",
                column: "CharityEventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharitableOrganizationTranslate");

            migrationBuilder.DropTable(
                name: "CharityEventTranslate");

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
        }
    }
}
