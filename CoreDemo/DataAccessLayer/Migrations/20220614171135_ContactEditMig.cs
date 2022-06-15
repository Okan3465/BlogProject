using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class ContactEditMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContactUSerName",
                table: "Contacts",
                newName: "ContactUserName");

            migrationBuilder.RenameColumn(
                name: "ContactName",
                table: "Contacts",
                newName: "ContactMail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContactUserName",
                table: "Contacts",
                newName: "ContactUSerName");

            migrationBuilder.RenameColumn(
                name: "ContactMail",
                table: "Contacts",
                newName: "ContactName");
        }
    }
}
