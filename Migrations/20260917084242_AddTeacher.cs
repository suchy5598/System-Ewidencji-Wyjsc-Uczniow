using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt.Migrations
{
    /// <inheritdoc />
    public partial class AddTeacher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$4jgNe7lnVIeE.9GmtcB3Wu7jyUPS1GSG1ndicVc/JFagYd39EuglO");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Login", "PasswordHash", "UserType" },
                values: new object[] { 2, "teacher", "$2a$11$KVMkFfYBfnlQGyHSvyXjrOdUqc0TjrE4vMaeXX247U/iPP/YP1nmC", 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$8sNddIVOiFOUjv2vtAZMYOImzQI91XmnvTPw9yfKFmJvGmcwkYxo.");
        }
    }
}
