using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactsApp.Backend.Migrations
{
    /// <inheritdoc />
    public partial class PhoneNumberFixedLength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Contacts",
                type: "nchar(9)",
                fixedLength: true,
                maxLength: 9,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(9)",
                oldMaxLength: 9);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Contacts",
                type: "nvarchar(9)",
                maxLength: 9,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(9)",
                oldFixedLength: true,
                oldMaxLength: 9);
        }
    }
}
