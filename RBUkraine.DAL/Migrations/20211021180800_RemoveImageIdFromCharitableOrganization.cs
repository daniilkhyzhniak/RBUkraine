using Microsoft.EntityFrameworkCore.Migrations;

namespace RBUkraine.DAL.Migrations
{
    public partial class RemoveImageIdFromCharitableOrganization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharitableOrganizations_CharitableOrganizationImages_ImageId1",
                table: "CharitableOrganizations");

            migrationBuilder.DropIndex(
                name: "IX_CharitableOrganizations_ImageId1",
                table: "CharitableOrganizations");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "CharitableOrganizations");

            migrationBuilder.DropColumn(
                name: "ImageId1",
                table: "CharitableOrganizations");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$aqSd7ep/WXDfLdBbhgEG6eDmDSKfVvBv/8RMespM7KXhbKJm8dOwm");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$TbVPzczAwq/sUuKWz7lOo.BJTZ38Drw5lAfAURqfDsDtpxYoVxlWe");

            migrationBuilder.CreateIndex(
                name: "IX_CharitableOrganizationImages_CharitableOrganizationId",
                table: "CharitableOrganizationImages",
                column: "CharitableOrganizationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CharitableOrganizationImages_CharitableOrganizations_CharitableOrganizationId",
                table: "CharitableOrganizationImages",
                column: "CharitableOrganizationId",
                principalTable: "CharitableOrganizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharitableOrganizationImages_CharitableOrganizations_CharitableOrganizationId",
                table: "CharitableOrganizationImages");

            migrationBuilder.DropIndex(
                name: "IX_CharitableOrganizationImages_CharitableOrganizationId",
                table: "CharitableOrganizationImages");

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
        }
    }
}
