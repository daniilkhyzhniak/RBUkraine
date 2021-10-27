using Microsoft.EntityFrameworkCore.Migrations;

namespace RBUkraine.DAL.Migrations
{
    public partial class updateDb3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CharityEventPurchase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CharityEventId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    AnimalId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharityEventPurchase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharityEventPurchase_Animals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CharityEventPurchase_CharityEvents_CharityEventId",
                        column: x => x.CharityEventId,
                        principalTable: "CharityEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CharityEventPurchase_Users_UserId",
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
                value: "$2a$11$tHZ.zwTG0hrEKOrfopENq.zLy1XBwTwmGU8PJ7a.WIPglGYV03kCK");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$3opHitnb9HIN2Ik7spm7y.tE3FjAPrCu48aRb8YBzlfdzbK1MOSAi");

            migrationBuilder.CreateIndex(
                name: "IX_CharityEventPurchase_AnimalId",
                table: "CharityEventPurchase",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_CharityEventPurchase_CharityEventId",
                table: "CharityEventPurchase",
                column: "CharityEventId");

            migrationBuilder.CreateIndex(
                name: "IX_CharityEventPurchase_UserId",
                table: "CharityEventPurchase",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharityEventPurchase");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$ordgxBqNOCCK6P76K.Xd8.oCruv10y6N/dR3ebczFzg8sOaWkAln.");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$d9KZvSHFHg/pt9X6Jt.VluOmo63MuJtPGMm0ysb4HDebbYQfCHNJS");
        }
    }
}
