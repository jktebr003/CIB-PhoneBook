using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
  public partial class ChangesMigration : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.InsertData(
        table: "PhoneBooks",
        columns: new[] { "Id", "Name" },
        values: new object[] { 1, "Yellow Pages" });

      migrationBuilder.InsertData(
        table: "Entries",
        columns: new[] { "Id", "Name", "PhoneNumber", "PhoneBookId" },
        values: new object[] { 1, "iOCO Coastal", "0214253400", 1 });

      migrationBuilder.InsertData(
        table: "Entries",
        columns: new[] { "Id", "Name", "PhoneNumber", "PhoneBookId" },
        values: new object[] { 2, "Cellucity", "0214254511", 1 });

    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {

    }
  }
}
