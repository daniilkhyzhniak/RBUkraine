using Microsoft.EntityFrameworkCore.Migrations;

namespace RBUkraine.DAL.Migrations
{
    public partial class UpdateCharitableOrganizationAndAnimalRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CharitableOrganizationId",
                table: "Animals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Species",
                table: "Animals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "CharitableOrganizations",
                columns: new[] { "Id", "Description", "Email", "IsDeleted", "Name", "PhoneNumber" },
                values: new object[] { 1, "Happy Paw", "happypaw@email.com", false, "Happy Paw", "12345" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$U33m8WVMmd68TJ4neIfziOSJMHnF8U1lvQF/a99AW6.jQScQbA1py");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$MRhltmuO/rreCSmwGdGH5OQAEWBU4DVo085f3ZJ0aQt4rLfQ5Ae5q");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 1000,
                columns: new[] { "CharitableOrganizationId", "Species" },
                values: new object[] { 1, "Зубр" });

            migrationBuilder.CreateIndex(
                name: "IX_Animals_CharitableOrganizationId",
                table: "Animals",
                column: "CharitableOrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_CharitableOrganizations_CharitableOrganizationId",
                table: "Animals",
                column: "CharitableOrganizationId",
                principalTable: "CharitableOrganizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_CharitableOrganizations_CharitableOrganizationId",
                table: "Animals");

            migrationBuilder.DropIndex(
                name: "IX_Animals_CharitableOrganizationId",
                table: "Animals");

            migrationBuilder.DeleteData(
                table: "CharitableOrganizations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "CharitableOrganizationId",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "Species",
                table: "Animals");

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
        }
    }
}
