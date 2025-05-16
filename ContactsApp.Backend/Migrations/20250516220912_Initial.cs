using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ContactsApp.Backend.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactSubCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactSubCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    SubCategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_ContactCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ContactCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contacts_ContactSubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "ContactSubCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "ContactCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Business" },
                    { 2, "Personal" },
                    { 3, "Other" }
                });

            migrationBuilder.InsertData(
                table: "ContactSubCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "CEO" },
                    { 2, "Manager" },
                    { 3, "Coworker" },
                    { 4, "Client" }
                });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "CategoryId", "DateOfBirth", "Email", "FirstName", "LastName", "PhoneNumber", "SubCategoryId" },
                values: new object[,]
                {
                    { 1, 1, new DateOnly(1992, 5, 14), "anna.kowalska92@gmail.com", "Anna", "Kowalska", "512345678", 1 },
                    { 2, 1, new DateOnly(1987, 11, 3), "michal.nowak87@wp.pl", "Michał", "Nowak", "601234567", 2 },
                    { 3, 1, new DateOnly(1990, 2, 27), "k.wisniewska@onet.pl", "Katarzyna", "Wiśniewska", "789456123", 3 },
                    { 4, 1, new DateOnly(1989, 7, 19), "piotr.zielinski@gmail.com", "Piotr", "Zieliński", "693321789", 3 },
                    { 5, 1, new DateOnly(1991, 3, 9), "agnieszka.dabrowska@interia.pl", "Agnieszka", "Dąbrowska", "530987654", 4 },
                    { 6, 1, new DateOnly(1990, 10, 21), "tomwojcik90@o2.pl", "Tomasz", "Wójcik", "665443221", 4 },
                    { 7, 2, new DateOnly(1993, 8, 5), "magda.kaminska@onet.eu", "Magdalena", "Kamińska", "507111333", null },
                    { 8, 2, new DateOnly(1985, 12, 30), "krz.lewandowski@wp.pl", "Krzysztof", "Lewandowski", "602556778", null },
                    { 9, 2, new DateOnly(1989, 4, 16), "paulina.jankowska89@gmail.com", "Paulina", "Jankowska", "723998445", null },
                    { 10, 2, new DateOnly(1994, 6, 11), "jakub.mazur@interia.eu", "Jakub", "Mazur", "511223344", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_CategoryId",
                table: "Contacts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_Email",
                table: "Contacts",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_SubCategoryId",
                table: "Contacts",
                column: "SubCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ContactCategories");

            migrationBuilder.DropTable(
                name: "ContactSubCategories");
        }
    }
}
