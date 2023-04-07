using Microsoft.EntityFrameworkCore.Migrations;

namespace uniserProject.Migrations
{
    public partial class newtablemarka : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MarkaId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Marka",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marka", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_MarkaId",
                table: "Products",
                column: "MarkaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Marka_MarkaId",
                table: "Products",
                column: "MarkaId",
                principalTable: "Marka",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Marka_MarkaId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Marka");

            migrationBuilder.DropIndex(
                name: "IX_Products_MarkaId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MarkaId",
                table: "Products");
        }
    }
}
