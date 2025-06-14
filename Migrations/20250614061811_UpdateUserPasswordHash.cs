using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthDemo.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserPasswordHash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "testuser",
                column: "Password",
                value: "jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "testuser",
                column: "Password",
                value: "123456");
        }
    }
}
