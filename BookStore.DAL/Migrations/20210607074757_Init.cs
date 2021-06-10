using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.DAL.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthPlace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ZipCode = table.Column<int>(type: "int", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PublishedYear = table.Column<int>(type: "int", nullable: true),
                    Subjects = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.UniqueConstraint("AK_Books_ISBN", x => x.ISBN);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Borrows",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    BorrowerId = table.Column<int>(type: "int", nullable: false),
                    BorrowedBookId = table.Column<int>(type: "int", nullable: false),
                    DeadLine = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Borrows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Borrows_Books_BorrowedBookId",
                        column: x => x.BorrowedBookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Borrows_Members_BorrowerId",
                        column: x => x.BorrowerId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "BirthDate", "BirthPlace", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(1821, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Moszkva", "Fjodor Mihajlovics Dosztojevszkij" },
                    { 2, new DateTime(1903, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Motihari", "George Orwell" },
                    { 3, new DateTime(1797, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sommers Town", "Mary Shelley" }
                });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "BirthDate", "Name", "PhoneNumber", "ZipCode" },
                values: new object[,]
                {
                    { 1, new DateTime(1983, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Teszt Elek", null, 1011 },
                    { 2, null, "Bádog Béla", "36203459876", null }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "ISBN", "PublishedYear", "Subjects", "Title" },
                values: new object[,]
                {
                    { 1, 1, "ISBN 963-07-0048-4", 1866, "", "Bűn és bűnhődés" },
                    { 2, 1, "ISBN 963 07 1282 2", 1880, "", "A Karamazov testvérek" },
                    { 3, 1, "ISBN 9630720876", 1869, "", "A félkegyelmű" },
                    { 4, 2, "ISBN 963-07-4815-0", 1949, "", "1984" },
                    { 5, 2, "ISBN 9630749467", 1945, "", "Állatfarm" },
                    { 6, 2, "ISBN 9630773201", 1939, "", "Légszomj" },
                    { 7, 3, "ISBN 9780008325923", 1818, "", "Frankenstein" },
                    { 8, 3, " ISBN 9789633440414", 1859, "", "Mathilda" }
                });

            migrationBuilder.InsertData(
                table: "Borrows",
                columns: new[] { "Id", "BorrowedBookId", "BorrowerId", "DeadLine", "ReturnDate" },
                values: new object[,]
                {
                    { 1, 1, 1, new DateTime(2021, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 2, 2, 1, new DateTime(2021, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 3, 3, 2, new DateTime(2021, 5, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 5, 2, new DateTime(2021, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 4, 29, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 7, 1, new DateTime(2021, 5, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Borrows_BorrowedBookId",
                table: "Borrows",
                column: "BorrowedBookId");

            migrationBuilder.CreateIndex(
                name: "IX_Borrows_BorrowerId",
                table: "Borrows",
                column: "BorrowerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Borrows");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
