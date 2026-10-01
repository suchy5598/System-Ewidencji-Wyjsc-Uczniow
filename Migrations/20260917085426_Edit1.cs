using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt.Migrations
{
    /// <inheritdoc />
    public partial class Edit1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Classes_SchoolClassId",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "SchoolClassId",
                table: "Students",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$5u96lhjzhth8t9ZdaLPuJOvzMAU8Yd./sNF4cCsiwR4WTdr8kAqQK");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$F/UDuSC1c5UiRHpzrr3dqOPvPG8jkywdl9HTH9jqRGDW5FOOOtCvW");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Classes_SchoolClassId",
                table: "Students",
                column: "SchoolClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Classes_SchoolClassId",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "SchoolClassId",
                table: "Students",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$Hxhet5Oe5vPQ3sJT11I/VO2EHvl1CRluFxKVGQV09GpJ/ns1IuIei");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$9HtsjF/ePKPvNL2Zky0JbeV15eE7iLXRw48xV6mPVyZh/i2NtlqBC");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Classes_SchoolClassId",
                table: "Students",
                column: "SchoolClassId",
                principalTable: "Classes",
                principalColumn: "Id");
        }
    }
}
