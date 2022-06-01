using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokeStack.Migrations
{
    public partial class m1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PokeModel",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pokeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    hp = table.Column<int>(type: "int", nullable: false),
                    atk = table.Column<int>(type: "int", nullable: false),
                    def = table.Column<int>(type: "int", nullable: false),
                    spA = table.Column<int>(type: "int", nullable: false),
                    spD = table.Column<int>(type: "int", nullable: false),
                    spe = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokeModel", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PokeModel");
        }
    }
}
