using Microsoft.EntityFrameworkCore.Migrations;

namespace RBUkraine.DAL.Migrations
{
    public partial class updateDb4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharityEventPurchase_Animals_AnimalId",
                table: "CharityEventPurchase");

            migrationBuilder.DropIndex(
                name: "IX_CharityEventPurchase_AnimalId",
                table: "CharityEventPurchase");

            migrationBuilder.DropColumn(
                name: "AnimalId",
                table: "CharityEventPurchase");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$afWqS9Of.WBeDZHceKROzuaEJWVhSuqf245F82MH1FxWFvoq3ssiC");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$kCgz/pFE095aO1P7ohyD5uwa4AAjUcYlGEfbRis/8/3DDQWR6M./S");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnimalId",
                table: "CharityEventPurchase",
                type: "int",
                nullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_CharityEventPurchase_Animals_AnimalId",
                table: "CharityEventPurchase",
                column: "AnimalId",
                principalTable: "Animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
