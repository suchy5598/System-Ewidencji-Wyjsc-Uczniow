using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt.Migrations
{
    /// <inheritdoc />
    public partial class AddClasses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SchoolClassId",
                table: "Students",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Students_SchoolClassId",
                table: "Students",
                column: "SchoolClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Classes_SchoolClassId",
                table: "Students",
                column: "SchoolClassId",
                principalTable: "Classes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Classes_SchoolClassId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Students_SchoolClassId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "SchoolClassId",
                table: "Students");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$4jgNe7lnVIeE.9GmtcB3Wu7jyUPS1GSG1ndicVc/JFagYd39EuglO");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$KVMkFfYBfnlQGyHSvyXjrOdUqc0TjrE4vMaeXX247U/iPP/YP1nmC");
        }
    }
}
