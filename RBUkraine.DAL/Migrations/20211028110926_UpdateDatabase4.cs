using Microsoft.EntityFrameworkCore.Migrations;

namespace RBUkraine.DAL.Migrations
{
    public partial class UpdateDatabase4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharityEventPurchase_CharityEvents_CharityEventId",
                table: "CharityEventPurchase");

            migrationBuilder.DropForeignKey(
                name: "FK_CharityEventPurchase_Users_UserId",
                table: "CharityEventPurchase");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsTranslate_News_NewsId",
                table: "NewsTranslate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewsTranslate",
                table: "NewsTranslate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CharityEventPurchase",
                table: "CharityEventPurchase");

            migrationBuilder.RenameTable(
                name: "NewsTranslate",
                newName: "NewsTranslates");

            migrationBuilder.RenameTable(
                name: "CharityEventPurchase",
                newName: "CharityEventPurchases");

            migrationBuilder.RenameIndex(
                name: "IX_NewsTranslate_NewsId",
                table: "NewsTranslates",
                newName: "IX_NewsTranslates_NewsId");

            migrationBuilder.RenameIndex(
                name: "IX_CharityEventPurchase_UserId",
                table: "CharityEventPurchases",
                newName: "IX_CharityEventPurchases_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CharityEventPurchase_CharityEventId",
                table: "CharityEventPurchases",
                newName: "IX_CharityEventPurchases_CharityEventId");

            migrationBuilder.AlterColumn<int>(
                name: "CharitableOrganizationId",
                table: "News",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AnimalId",
                table: "News",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewsTranslates",
                table: "NewsTranslates",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CharityEventPurchases",
                table: "CharityEventPurchases",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$uZSXVvzvRGMFfOcon3PTeeaM1k0JH8IPzD9oOkDI7NJWMLZGgmHVm");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$pAuAszX1WJOyPUc5Bn7cy.VQBsZhPAVtejpqXW3c/AqjsWPgepQcK");

            migrationBuilder.AddForeignKey(
                name: "FK_CharityEventPurchases_CharityEvents_CharityEventId",
                table: "CharityEventPurchases",
                column: "CharityEventId",
                principalTable: "CharityEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CharityEventPurchases_Users_UserId",
                table: "CharityEventPurchases",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsTranslates_News_NewsId",
                table: "NewsTranslates",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharityEventPurchases_CharityEvents_CharityEventId",
                table: "CharityEventPurchases");

            migrationBuilder.DropForeignKey(
                name: "FK_CharityEventPurchases_Users_UserId",
                table: "CharityEventPurchases");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsTranslates_News_NewsId",
                table: "NewsTranslates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewsTranslates",
                table: "NewsTranslates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CharityEventPurchases",
                table: "CharityEventPurchases");

            migrationBuilder.RenameTable(
                name: "NewsTranslates",
                newName: "NewsTranslate");

            migrationBuilder.RenameTable(
                name: "CharityEventPurchases",
                newName: "CharityEventPurchase");

            migrationBuilder.RenameIndex(
                name: "IX_NewsTranslates_NewsId",
                table: "NewsTranslate",
                newName: "IX_NewsTranslate_NewsId");

            migrationBuilder.RenameIndex(
                name: "IX_CharityEventPurchases_UserId",
                table: "CharityEventPurchase",
                newName: "IX_CharityEventPurchase_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CharityEventPurchases_CharityEventId",
                table: "CharityEventPurchase",
                newName: "IX_CharityEventPurchase_CharityEventId");

            migrationBuilder.AlterColumn<int>(
                name: "CharitableOrganizationId",
                table: "News",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AnimalId",
                table: "News",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewsTranslate",
                table: "NewsTranslate",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CharityEventPurchase",
                table: "CharityEventPurchase",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_CharityEventPurchase_CharityEvents_CharityEventId",
                table: "CharityEventPurchase",
                column: "CharityEventId",
                principalTable: "CharityEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CharityEventPurchase_Users_UserId",
                table: "CharityEventPurchase",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsTranslate_News_NewsId",
                table: "NewsTranslate",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
