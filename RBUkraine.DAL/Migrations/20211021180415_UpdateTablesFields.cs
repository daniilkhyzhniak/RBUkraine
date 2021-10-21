using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RBUkraine.DAL.Migrations
{
    public partial class UpdateTablesFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharitableOrganizationTranslate_CharitableOrganizations_CharitableOrganizationId",
                table: "CharitableOrganizationTranslate");

            migrationBuilder.DropForeignKey(
                name: "FK_CharityEventTranslate_CharityEvents_CharityEventId",
                table: "CharityEventTranslate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CharityEventTranslate",
                table: "CharityEventTranslate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CharitableOrganizationTranslate",
                table: "CharitableOrganizationTranslate");

            migrationBuilder.RenameTable(
                name: "CharityEventTranslate",
                newName: "CharityEventTranslates");

            migrationBuilder.RenameTable(
                name: "CharitableOrganizationTranslate",
                newName: "CharitableOrganizationTranslates");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "AnimalTranslates",
                newName: "Species");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "AnimalTranslates",
                newName: "Phylum");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Animals",
                newName: "Phylum");

            migrationBuilder.RenameColumn(
                name: "LatinName",
                table: "Animals",
                newName: "Order");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Animals",
                newName: "LatinSpecies");

            migrationBuilder.RenameIndex(
                name: "IX_CharityEventTranslate_CharityEventId",
                table: "CharityEventTranslates",
                newName: "IX_CharityEventTranslates_CharityEventId");

            migrationBuilder.RenameIndex(
                name: "IX_CharitableOrganizationTranslate_CharitableOrganizationId",
                table: "CharitableOrganizationTranslates",
                newName: "IX_CharitableOrganizationTranslates_CharitableOrganizationId");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "FoundationDate",
                table: "CharitableOrganizations",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "Founders",
                table: "CharitableOrganizations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "CharitableOrganizations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ImageId1",
                table: "CharitableOrganizations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Stockholders",
                table: "CharitableOrganizations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Class",
                table: "AnimalTranslates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConservationStatus",
                table: "AnimalTranslates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Family",
                table: "AnimalTranslates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Genus",
                table: "AnimalTranslates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Kingdom",
                table: "AnimalTranslates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Order",
                table: "AnimalTranslates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Class",
                table: "Animals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConservationStatus",
                table: "Animals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Family",
                table: "Animals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Genus",
                table: "Animals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Kingdom",
                table: "Animals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "CharitableOrganizationTranslates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Founders",
                table: "CharitableOrganizationTranslates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Stockholders",
                table: "CharitableOrganizationTranslates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CharityEventTranslates",
                table: "CharityEventTranslates",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CharitableOrganizationTranslates",
                table: "CharitableOrganizationTranslates",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CharitableOrganizationImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CharitableOrganizationId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharitableOrganizationImages", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AnimalTranslates",
                keyColumn: "Id",
                keyValue: 1,
                column: "Phylum",
                value: null);

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 1000,
                columns: new[] { "LatinSpecies", "Order", "Phylum" },
                values: new object[] { "Bison bonasus", null, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$nt0UAdshCx7lbhekd03HNujf9foHctmFJxt8ElGdgncgApmYUJbum");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$9vrY0zNDjvT7bSUq.b7iz..7BhS/ekdSkTlsm8wZgDn1cBMCq.7Wy");

            migrationBuilder.CreateIndex(
                name: "IX_CharitableOrganizations_ImageId1",
                table: "CharitableOrganizations",
                column: "ImageId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CharitableOrganizations_CharitableOrganizationImages_ImageId1",
                table: "CharitableOrganizations",
                column: "ImageId1",
                principalTable: "CharitableOrganizationImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharitableOrganizations_CharitableOrganizationImages_ImageId1",
                table: "CharitableOrganizations");

            migrationBuilder.DropForeignKey(
                name: "FK_CharitableOrganizationTranslates_CharitableOrganizations_CharitableOrganizationId",
                table: "CharitableOrganizationTranslates");

            migrationBuilder.DropForeignKey(
                name: "FK_CharityEventTranslates_CharityEvents_CharityEventId",
                table: "CharityEventTranslates");

            migrationBuilder.DropTable(
                name: "CharitableOrganizationImages");

            migrationBuilder.DropIndex(
                name: "IX_CharitableOrganizations_ImageId1",
                table: "CharitableOrganizations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CharityEventTranslates",
                table: "CharityEventTranslates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CharitableOrganizationTranslates",
                table: "CharitableOrganizationTranslates");

            migrationBuilder.DropColumn(
                name: "FoundationDate",
                table: "CharitableOrganizations");

            migrationBuilder.DropColumn(
                name: "Founders",
                table: "CharitableOrganizations");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "CharitableOrganizations");

            migrationBuilder.DropColumn(
                name: "ImageId1",
                table: "CharitableOrganizations");

            migrationBuilder.DropColumn(
                name: "Stockholders",
                table: "CharitableOrganizations");

            migrationBuilder.DropColumn(
                name: "Class",
                table: "AnimalTranslates");

            migrationBuilder.DropColumn(
                name: "ConservationStatus",
                table: "AnimalTranslates");

            migrationBuilder.DropColumn(
                name: "Family",
                table: "AnimalTranslates");

            migrationBuilder.DropColumn(
                name: "Genus",
                table: "AnimalTranslates");

            migrationBuilder.DropColumn(
                name: "Kingdom",
                table: "AnimalTranslates");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "AnimalTranslates");

            migrationBuilder.DropColumn(
                name: "Class",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "ConservationStatus",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "Family",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "Genus",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "Kingdom",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "CharitableOrganizationTranslates");

            migrationBuilder.DropColumn(
                name: "Founders",
                table: "CharitableOrganizationTranslates");

            migrationBuilder.DropColumn(
                name: "Stockholders",
                table: "CharitableOrganizationTranslates");

            migrationBuilder.RenameTable(
                name: "CharityEventTranslates",
                newName: "CharityEventTranslate");

            migrationBuilder.RenameTable(
                name: "CharitableOrganizationTranslates",
                newName: "CharitableOrganizationTranslate");

            migrationBuilder.RenameColumn(
                name: "Species",
                table: "AnimalTranslates",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Phylum",
                table: "AnimalTranslates",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Phylum",
                table: "Animals",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Order",
                table: "Animals",
                newName: "LatinName");

            migrationBuilder.RenameColumn(
                name: "LatinSpecies",
                table: "Animals",
                newName: "Description");

            migrationBuilder.RenameIndex(
                name: "IX_CharityEventTranslates_CharityEventId",
                table: "CharityEventTranslate",
                newName: "IX_CharityEventTranslate_CharityEventId");

            migrationBuilder.RenameIndex(
                name: "IX_CharitableOrganizationTranslates_CharitableOrganizationId",
                table: "CharitableOrganizationTranslate",
                newName: "IX_CharitableOrganizationTranslate_CharitableOrganizationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CharityEventTranslate",
                table: "CharityEventTranslate",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CharitableOrganizationTranslate",
                table: "CharitableOrganizationTranslate",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AnimalTranslates",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Conservation status of the species: Extinct in nature.");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 1000,
                columns: new[] { "Description", "LatinName", "Name" },
                values: new object[] { "Природоохранный статус вида: Пропавший в природе.", "Bison bonasus", "Зубр" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$dHpTEHFjJy/iwCfLfCSeb.K9ZmAJpMtEeJXID6I9shjfkChIpkU8u");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$S.hcCAjuYhprpArYy8iPLOrO5RRhgnm7jRYYCkVIjsAYOHcMhCqt2");

            migrationBuilder.AddForeignKey(
                name: "FK_CharitableOrganizationTranslate_CharitableOrganizations_CharitableOrganizationId",
                table: "CharitableOrganizationTranslate",
                column: "CharitableOrganizationId",
                principalTable: "CharitableOrganizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CharityEventTranslate_CharityEvents_CharityEventId",
                table: "CharityEventTranslate",
                column: "CharityEventId",
                principalTable: "CharityEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
