using Microsoft.EntityFrameworkCore.Migrations;

namespace MilliganNathaniel413Bookstore.Migrations
{
    public partial class addnumpages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumPages",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumPages",
                table: "Books");
        }
    }
}
