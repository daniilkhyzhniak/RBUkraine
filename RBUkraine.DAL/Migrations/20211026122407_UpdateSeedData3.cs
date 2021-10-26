using Microsoft.EntityFrameworkCore.Migrations;

namespace RBUkraine.DAL.Migrations
{
    public partial class UpdateSeedData3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CharitableOrganizations",
                keyColumn: "Id",
                keyValue: 123,
                column: "Description",
                value: "Маленькі серця");

            migrationBuilder.UpdateData(
                table: "CharitableOrganizations",
                keyColumn: "Id",
                keyValue: 2344,
                column: "Description",
                value: "БФ “Захист тварин Червоної книги України");

            migrationBuilder.UpdateData(
                table: "CharitableOrganizations",
                keyColumn: "Id",
                keyValue: 12312,
                column: "Description",
                value: "Happy Paw");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CharitableOrganizations",
                keyColumn: "Id",
                keyValue: 123,
                column: "Description",
                value: "кекнгшйфцукенгшпроіжуешкойьсу7шркедшфутцкфьгфпуцкешгдшугнкрешмт7дірнпшгрукегфужкщесгфу89кенгф8ьу0гке8ф09укгне98");

            migrationBuilder.UpdateData(
                table: "CharitableOrganizations",
                keyColumn: "Id",
                keyValue: 2344,
                column: "Description",
                value: "кекнгшйфцукенгшпроіжуешкойьсу7шркедшфутцкфьгфпуцкешгдшугнкрешмт7дірнпшгрукегфужкщесгфу89кенгф8ьу0гке8ф09укгне98");

            migrationBuilder.UpdateData(
                table: "CharitableOrganizations",
                keyColumn: "Id",
                keyValue: 12312,
                column: "Description",
                value: "кекнгшйфцукенгшпроіжуешкойьсу7шркедшфутцкфьгфпуцкешгдшугнкрешмт7дірнпшгрукегфужкщесгфу89кенгф8ьу0гке8ф09укгне98");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$Y0lc6fvOx8skR3.6Uh0WWOG8d0X.RwgvyoUArl1BSL2a8e9CQG6mC");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$3k0yR7v/96qdGRcPvEpLtevTuoyiuJlHFiL4DyOEXOClHogLIXE3W");
        }
    }
}
