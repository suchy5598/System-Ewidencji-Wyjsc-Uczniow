using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt.Migrations
{
    /// <inheritdoc />
    public partial class AddUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Exits",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$T8IVDzTvW.Lj7azUQeCxne/ywLCRuuIo3hzdQf1jBv33YgR.xewem");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$FDfAyiPXMfeI3yWItnchbO0VuEMMUwhDgZdjZx26qi5NOiY6tm0eG");

            migrationBuilder.CreateIndex(
                name: "IX_Exits_UserId",
                table: "Exits",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exits_Users_UserId",
                table: "Exits",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exits_Users_UserId",
                table: "Exits");

            migrationBuilder.DropIndex(
                name: "IX_Exits_UserId",
                table: "Exits");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Exits");

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
        }
    }
}
