using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MySchool.Migrations
{
    /// <inheritdoc />
    public partial class asdfgg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "asdf",
                column: "Password",
                value: "$2a$10$irAmmeKvGq9aVHAo9z.0heceZk0NS46HBkEy.OwYjYYZVxQZzzL/6");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "asdf",
                column: "Password",
                value: "$2a$10$ydpSlONFKjoeRs4SCmdNuu/jCbV/VN2vKZ/9vMtFyyMxvvQAzLELu");
        }
    }
}
