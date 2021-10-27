using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RBUkraine.DAL.Migrations
{
    public partial class updateDbSprint2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimalImages_Animals_AnimalId",
                table: "AnimalImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Animals_CharitableOrganizations_CharitableOrganizationId",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimalTranslates_Animals_AnimalId",
                table: "AnimalTranslates");

            migrationBuilder.DropForeignKey(
                name: "FK_CharitableOrganizationImages_CharitableOrganizations_CharitableOrganizationId",
                table: "CharitableOrganizationImages");

            migrationBuilder.DropForeignKey(
                name: "FK_CharitableOrganizationTranslates_CharitableOrganizations_CharitableOrganizationId",
                table: "CharitableOrganizationTranslates");

            migrationBuilder.DropForeignKey(
                name: "FK_CharityEventTranslates_CharityEvents_CharityEventId",
                table: "CharityEventTranslates");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles");

            migrationBuilder.AddColumn<string>(
                name: "Nickname",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublishDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CharitableOrganizationId = table.Column<int>(type: "int", nullable: false),
                    AnimalId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                    table.ForeignKey(
                        name: "FK_News_Animals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_News_CharitableOrganizations_CharitableOrganizationId",
                        column: x => x.CharitableOrganizationId,
                        principalTable: "CharitableOrganizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NewsTranslate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewsId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Language = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsTranslate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewsTranslate_News_NewsId",
                        column: x => x.NewsId,
                        principalTable: "News",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_News_AnimalId",
                table: "News",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_News_CharitableOrganizationId",
                table: "News",
                column: "CharitableOrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsTranslate_NewsId",
                table: "NewsTranslate",
                column: "NewsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalImages_Animals_AnimalId",
                table: "AnimalImages",
                column: "AnimalId",
                principalTable: "Animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_CharitableOrganizations_CharitableOrganizationId",
                table: "Animals",
                column: "CharitableOrganizationId",
                principalTable: "CharitableOrganizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalTranslates_Animals_AnimalId",
                table: "AnimalTranslates",
                column: "AnimalId",
                principalTable: "Animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CharitableOrganizationImages_CharitableOrganizations_CharitableOrganizationId",
                table: "CharitableOrganizationImages",
                column: "CharitableOrganizationId",
                principalTable: "CharitableOrganizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CharitableOrganizationTranslates_CharitableOrganizations_CharitableOrganizationId",
                table: "CharitableOrganizationTranslates",
                column: "CharitableOrganizationId",
                principalTable: "CharitableOrganizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CharityEventTranslates_CharityEvents_CharityEventId",
                table: "CharityEventTranslates",
                column: "CharityEventId",
                principalTable: "CharityEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimalImages_Animals_AnimalId",
                table: "AnimalImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Animals_CharitableOrganizations_CharitableOrganizationId",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimalTranslates_Animals_AnimalId",
                table: "AnimalTranslates");

            migrationBuilder.DropForeignKey(
                name: "FK_CharitableOrganizationImages_CharitableOrganizations_CharitableOrganizationId",
                table: "CharitableOrganizationImages");

            migrationBuilder.DropForeignKey(
                name: "FK_CharitableOrganizationTranslates_CharitableOrganizations_CharitableOrganizationId",
                table: "CharitableOrganizationTranslates");

            migrationBuilder.DropForeignKey(
                name: "FK_CharityEventTranslates_CharityEvents_CharityEventId",
                table: "CharityEventTranslates");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles");

            migrationBuilder.DropTable(
                name: "NewsTranslate");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropColumn(
                name: "Nickname",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$4w2/LTYxtfAqIBZEIpBSLe79W0mWGrmedioipWCHbJ48mGytlpj9i");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$TNUWO/KSfd3uDkp0prYrqeuskfuafQlkSJDlr2thgOEZ/mAHsEpFm");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalImages_Animals_AnimalId",
                table: "AnimalImages",
                column: "AnimalId",
                principalTable: "Animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_CharitableOrganizations_CharitableOrganizationId",
                table: "Animals",
                column: "CharitableOrganizationId",
                principalTable: "CharitableOrganizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalTranslates_Animals_AnimalId",
                table: "AnimalTranslates",
                column: "AnimalId",
                principalTable: "Animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CharitableOrganizationImages_CharitableOrganizations_CharitableOrganizationId",
                table: "CharitableOrganizationImages",
                column: "CharitableOrganizationId",
                principalTable: "CharitableOrganizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CharitableOrganizationTranslates_CharitableOrganizations_CharitableOrganizationId",
                table: "CharitableOrganizationTranslates",
                column: "CharitableOrganizationId",
                principalTable: "CharitableOrganizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CharityEventTranslates_CharityEvents_CharityEventId",
                table: "CharityEventTranslates",
                column: "CharityEventId",
                principalTable: "CharityEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
