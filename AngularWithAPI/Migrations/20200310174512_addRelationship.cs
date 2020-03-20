using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AngularWithAPI.Migrations
{
    public partial class addRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Persones");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Persones",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Company = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PersoneId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_Persones_PersoneId",
                        column: x => x.PersoneId,
                        principalTable: "Persones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_PersoneId",
                table: "Contacts",
                column: "PersoneId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Persones");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Persones",
                nullable: false,
                defaultValue: 0);
        }
    }
}
